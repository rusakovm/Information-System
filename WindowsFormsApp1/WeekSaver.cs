using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class WeekSaver
    {
        public static void Save(WeekScheduleControl sch)
        {
            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    // 1. чистим неделю
                    var del = new SqlCommand(
                        @"DELETE FROM employee_schedule
                      WHERE employee_id=@e AND work_date BETWEEN @d AND DATEADD(day,6,@d)", cn, tx);
                    del.Parameters.AddWithValue("@e", sch.EmployeeId);
                    del.Parameters.AddWithValue("@d", sch.WeekStart);
                    del.ExecuteNonQuery();

                    // 2. вставляем текущие блоки
                    var ins = new SqlCommand(
                        @"INSERT INTO employee_schedule
                      (employee_id, work_date, time_from, time_to, title, color_argb)
                      VALUES (@e,@dt,@f,@t,@ttl,@c)", cn, tx);
                    ins.Parameters.Add("@e", SqlDbType.Int).Value = sch.EmployeeId;
                    ins.Parameters.Add("@dt", SqlDbType.Date);
                    ins.Parameters.Add("@f", SqlDbType.Time);
                    ins.Parameters.Add("@t", SqlDbType.Time);
                    ins.Parameters.Add("@ttl", SqlDbType.NVarChar, 100);
                    ins.Parameters.Add("@c", SqlDbType.Int);

                    foreach (var it in sch.Items)          // добавьте public IReadOnlyList<ScheduleItem> Items => _items;
                    {
                        ins.Parameters["@dt"].Value = it.Date;
                        ins.Parameters["@f"].Value = it.From;
                        ins.Parameters["@t"].Value = it.To;
                        ins.Parameters["@ttl"].Value = it.Title;
                        ins.Parameters["@c"].Value = it.Color.ToArgb();
                        ins.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }
    }
}
