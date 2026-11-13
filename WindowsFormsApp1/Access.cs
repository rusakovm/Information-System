using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;  // ← здесь объявлен класс Image
using System;

namespace WindowsFormsApp1
{
    public static class Access
    {
        public static string Login { get; private set; }
        public static string FullName { get; private set; }   // ← добавили
        public static string PositionName { get; private set; }   // ← добавили

        public static byte[] PhotoBytes { get; private set; }
        public static Image PhotoImage
            => PhotoBytes == null ? null : Image.FromStream(new MemoryStream(PhotoBytes));

        public static HashSet<string> Rights { get; } = new HashSet<string>();

        public static event Action InfoChanged;   // ①
        public static event Action RightsChanged;

        public static void Reload()
        {
            if (string.IsNullOrEmpty(Login)) return;
            Init(Login);          // перечитываем FullName / Rights из БД
            RightsChanged?.Invoke();
        }

        public static void Init(string login)
        {
            Login = login;
            FullName = null;         // очистим на всякий случай
            PositionName = null;
            PhotoBytes = null;
            Rights.Clear();

            /* Вытаскиваем сразу: ФИО, должность и права */
            const string sql = @"
SELECT e.last_name + ' ' + e.first_name          AS full_name,
       pos.name                                   AS position_name,
       e.photo, 
       pp.permission_code
FROM   user_auth ua
JOIN   employees e            ON e.id  = ua.id
JOIN   positions pos          ON pos.id = e.position_id
LEFT   JOIN position_permissions pp
           ON pp.position_id = pos.id
WHERE  ua.username = @u";

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@u", login);
                cn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        /* 0 и 1 читаем один раз */
                        if (FullName == null)
                        {
                            FullName = r.GetString(0);
                            PositionName = r.GetString(1);
                        }

                        /* 2: фото ─ сохраняем только при первом ненулевом значении  */
                        if (PhotoBytes == null && !r.IsDBNull(2))
                            PhotoBytes = (byte[])r.GetValue(2);

                        /* 3: permission_code (может быть NULL) */
                        if (!r.IsDBNull(3))
                            Rights.Add(r.GetString(3));
                    }
                }
            }
            InfoChanged?.Invoke();
        }

        public static bool Has(string code) => Rights.Contains(code);
    }
}
