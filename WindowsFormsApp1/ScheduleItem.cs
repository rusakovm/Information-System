using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>Один блок расписания (например «Рабочее время»).</summary>
    public class ScheduleItem
    {
        public int Id { get; set; }            // 0 → ещё не в БД
        public DateTime Date { get; set; }            // День
        public TimeSpan From { get; set; }            // Начало
        public TimeSpan To { get; set; }            // Конец
        public string Title { get; set; }            // подпись
        public Color Color { get; set; }            // цвет прямоугольника
    }
}
