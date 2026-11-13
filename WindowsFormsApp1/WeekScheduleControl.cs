using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class WeekScheduleControl : UserControl
    {

        private void WeekScheduleControl_Load(object sender, EventArgs e)
        {
            // можно оставить пустым или вызвать начальную загрузку
            // if (EmployeeId != 0) LoadWeek(EmployeeId, WeekStart);
        }

        public WeekScheduleControl()
        {
            InitializeComponent();


            this.AutoScroll = true;          // вмещаемся внутри маленькой формы
            this.AutoScrollMinSize = new Size(TimeW + 7 * DayW, 48 * CellH);
            DoubleBuffered = true;
            ResizeRedraw = true;


            DoubleBuffered = true;
            ResizeRedraw = true;
        }
        private ScheduleItem _selected;
        private readonly Pen _selPen = new Pen(Color.Black, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

        public IReadOnlyList<ScheduleItem> Items => _items;
        public int EmployeeId { get; set; }
        public DateTime WeekStart { get; private set; }   // всегда понедельник

        private readonly List<ScheduleItem> _items = new List<ScheduleItem>();


        protected override void OnPaint(PaintEventArgs e)
        {
            {
                var g = e.Graphics;
                g.Clear(Color.White);

                // учитываем автопрокрутку
                var ofs = this.AutoScrollPosition;          // Point (x,y) отрицательный

                // вертикальные линии
                for (int d = 0; d <= 7; d++)
                    g.DrawLine(Pens.Gray,
                               ofs.X + TimeW + d * DayW, ofs.Y,
                               ofs.X + TimeW + d * DayW, ofs.Y + 48 * CellH);

                // горизонтальные линии + подписи времени
                for (int h = 0; h <= 48; h++)
                {
                    int y = ofs.Y + h * CellH;
                    g.DrawLine(Pens.LightGray, ofs.X + TimeW, y, ofs.X + TimeW + 7 * DayW, y);

                    // каждые 60 мин печатаем метку
                    if (h % 2 == 0 && h < 48)
                    {
                        string label = TimeSpan.FromMinutes(h * 30).ToString(@"hh\:mm");
                        g.DrawString(label, Font, Brushes.DimGray, ofs.X + 2, y + 2);
                    }
                }


                // заголовки дней
                var names = new[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
                for (int d = 0; d < 7; d++)
                    g.DrawString(names[d], Font, Brushes.Black,
                                 ofs.X + TimeW + d * DayW + 4, ofs.Y + 2);

                // существующие блоки
                foreach (var it in _items) DrawItem(g, it, ofs);

                // прямоугольник-скетч
                if (_dragging)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.SkyBlue)),
                                    RectWithOffset(_currentRect, ofs));
            }
        }
        private Rectangle RectWithOffset(Rectangle r, Point ofs)
        {
            return new Rectangle(r.X + ofs.X, r.Y + ofs.Y, r.Width, r.Height);
        }
        private void DrawItem(Graphics g, ScheduleItem it, Point ofs)
        {
            int dayIdx = (int)(it.Date - WeekStart).TotalDays;
            int y1 = (int)(it.From.TotalMinutes / 30) * CellH;
            int y2 = (int)(it.To.TotalMinutes / 30) * CellH;

            var rect = new Rectangle(ofs.X + TimeW + dayIdx * DayW + 1,
                                     ofs.Y + y1 + 1,
                                     DayW - 2,
                                     y2 - y1 - 2);

            using (var b = new SolidBrush(it.Color))
                g.FillRectangle(b, rect);

            g.DrawRectangle(Pens.Black, rect);
            g.DrawString(it.Title, Font, Brushes.White, rect,
                new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
        }
        private int GridX(int rawX) => rawX - TimeW;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // координата без прокрутки
            Point p = WithoutScroll(e.Location);

            // пробуем найти блок под курсором
            _selected = HitTest(p);

            if (_selected != null)              // выбрали существующий — просто подсветим
            {
                Invalidate();
                return;
            }

            // иначе начинаем рисовать новый
            _dragging = true;
            _dragStart = p;
        }
        private ScheduleItem HitTest(Point p)
        {
            foreach (var it in _items)
            {
                int dayIdx = (int)(it.Date - WeekStart).TotalDays;
                int y1 = (int)(it.From.TotalMinutes / 30) * CellH;
                int y2 = (int)(it.To.TotalMinutes / 30) * CellH;

                var rect = new Rectangle(TimeW + dayIdx * DayW, y1, DayW, y2 - y1);
                if (rect.Contains(p)) return it;
            }
            return null;
        }



        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point cur = WithoutScroll(e.Location);

            int xGrid = GridX(_dragStart.X);
            int xGrid2 = GridX(cur.X);
            if (xGrid < 0) xGrid = 0;
            if (xGrid2 < 0) xGrid2 = 0;

            int x = xGrid / DayW * DayW;             // начало колонки дня
            int y1 = _dragStart.Y / CellH * CellH;
            int y2 = cur.Y / CellH * CellH;
            if (y2 < y1) { int t = y1; y1 = y2; y2 = t; }

            _currentRect = new Rectangle(
                TimeW + x + 1,                       // ← добавляем TimeW назад
                y1 + 1,
                DayW - 2,
                y2 - y1 - 2);

            Invalidate();
        }
        private Point WithoutScroll(Point p)
        {
            return new Point(p.X - AutoScrollPosition.X,
                             p.Y - AutoScrollPosition.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_dragging) return;
            _dragging = false;

            if (_currentRect.Height <= 0) { Invalidate(); return; }

            using (var dlg = new TitleColorDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) { Invalidate(); return; }

                // координаты _currentRect уже «чистые», потому что мы их
                // вычисляли из точек без прокрутки, поэтому дальше ничего
                // вычитать не нужно.
                int dayIdx = _currentRect.X / DayW;
                DateTime date = WeekStart.AddDays(dayIdx);

                var item = new ScheduleItem
                {
                    Id = 0,
                    Date = date,
                    From = TimeSpan.FromMinutes(_currentRect.Y / CellH * 30),
                    To = TimeSpan.FromMinutes(_currentRect.Bottom / CellH * 30),
                    Title = dlg.TitleText,
                    Color = dlg.SelectedColor
                };

                _items.Add(item);
                ChangeHappened?.Invoke();
            }

            Invalidate();
        }


        private void DrawItem(Graphics g, ScheduleItem it)
        {
            int dayIdx = (int)(it.Date - WeekStart).TotalDays;
            int y1 = (int)(it.From.TotalMinutes / 30) * CellH;
            int y2 = (int)(it.To.TotalMinutes / 30) * CellH;
            var rect = new Rectangle(dayIdx * DayW + 1, y1 + 1, DayW - 2, y2 - y1 - 2);

            using (var b = new SolidBrush(it.Color))
                g.FillRectangle(b, rect);
            

            g.DrawRectangle(Pens.Black, rect);
            g.DrawString(it.Title, Font, Brushes.White, rect, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
            if (it == _selected)
                g.DrawRectangle(_selPen, rect);
        }
        public ScheduleItem SelectedItem => _selected;

        public void RemoveSelected()
        {
            if (_selected == null) return;
            _items.Remove(_selected);
            _selected = null;
            ChangeHappened?.Invoke();   // чтобы форма могла активировать «Применить»
            Invalidate();
        }


        public void LoadWeek(int employeeId, DateTime monday)
        {
            EmployeeId = employeeId;
            WeekStart = monday.Date;        // всегда понедельник
            _items.Clear();

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(
                @"SELECT id, work_date, time_from, time_to, title, color_argb
                  FROM   employee_schedule
                  WHERE  employee_id = @e
                    AND  work_date  BETWEEN @d AND DATEADD(day,6,@d)", cn))
            {
                cmd.Parameters.AddWithValue("@e", employeeId);
                cmd.Parameters.AddWithValue("@d", WeekStart);
                cn.Open();

                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        _items.Add(new ScheduleItem
                        {
                            Id = r.GetInt32(0),
                            Date = r.GetDateTime(1),
                            From = (TimeSpan)r["time_from"],
                            To = (TimeSpan)r["time_to"],
                            Title = r.GetString(4),
                            Color = Color.FromArgb(r.GetInt32(5))
                        });
                    }
            }

            Invalidate();    // попросить перерисовать контрол
        }


        private const int CellH = 20;      // 30-минутка  → 20 px (48 × 20 ≈ 960)
        private const int DayW = 140;     // ширина одного дня
        private const int TimeW = 50;

        private bool _dragging;
        private Point _dragStart;
        private Rectangle _currentRect;
        private readonly Brush _border = new SolidBrush(Color.LightGray);

        public event Action ChangeHappened;
    }
}
