using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary> Добавляет псевдо-placeholder к TextBox-ам и ComboBox-ам. </summary>
    internal static class Placeholder
    {
        private static readonly Color HintColor = SystemColors.GrayText;
        private static readonly Color TextColor = SystemColors.WindowText;

        /* ------------ TextBox / MaskedTextBox ------------------------------ */

   

        /// <summary>Возвращает текст <b>без учёта подсказки</b>.</summary>
        public static string GetUserText(Control c)
        {
            // 1. Для TextBox / MaskedTextBox
            if (c is TextBoxBase tb)
                return tb.ForeColor == HintColor ? "" : tb.Text.Trim();

            // 2. Для ComboBox (DropDown)
            if (c is ComboBox cb)
                return cb.ForeColor == HintColor ? "" : cb.Text.Trim();

            return "";
        }

        public static void Attach(TextBoxBase tb, string hint)
        {
            void Show(object sender, EventArgs e)
            {
                if (tb.TextLength == 0)
                {
                    tb.Text = hint;
                    tb.ForeColor = HintColor;
                }
            }

            void Hide(object sender, EventArgs e)
            {
                if (tb.ForeColor == HintColor)
                {
                    tb.Clear();
                    tb.ForeColor = TextColor;
                }
            }

            // первичная инициализация
            Show(null, EventArgs.Empty);

            tb.GotFocus += Hide;
            tb.LostFocus += Show;
            tb.TextChanged += (s, e) =>
            {
                if (!tb.Focused && tb.TextLength == 0) Show(null, EventArgs.Empty);
            };
        }

        /* ------------ ComboBox (стиль DropDown!) --------------------------- */
        public static void Attach(ComboBox cb, string hint)
        {
            if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
                throw new InvalidOperationException("Для DropDownList placeholder невозможен.");

            void Show(object sender, EventArgs e)
            {
                if (cb.Text.Length == 0)
                {
                    cb.Text = hint;
                    cb.ForeColor = HintColor;
                }
            }

            void Hide(object sender, EventArgs e)
            {
                if (cb.ForeColor == HintColor)
                {
                    cb.Text = "";
                    cb.ForeColor = TextColor;
                }
            }

            Show(null, EventArgs.Empty);

            cb.GotFocus += Hide;
            cb.LostFocus += Show;
            cb.TextChanged += (s, e) =>
            {
                if (!cb.Focused && cb.Text.Length == 0) Show(null, EventArgs.Empty);
            };
        }
    }
}
