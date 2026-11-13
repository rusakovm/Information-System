using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using ZXing;
using ZXing.Common;
using DR = System.Drawing;          // DR.Rectangle
using IT = iTextSharp.text;

namespace WindowsFormsApp1
{
    // DTO для одной строки квитанции по счётчику
    public class MeterReceiptRow
    {
        public string MeterSerial { get; set; }
        public decimal StartValue { get; set; }
        public decimal EndValue { get; set; }
        public decimal Volume => EndValue - StartValue;
        public decimal Tariff { get; set; }
        public decimal Amount => Volume * Tariff;
    }

    // DTO для всей квитанции
    public class ReceiptData
    {
        public string FullName { get; }
        public string Address { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
        public List<MeterReceiptRow> Rows { get; }

        public ReceiptData(string fullName, string address,
                           DateTime fromDate, DateTime toDate,
                           List<MeterReceiptRow> rows)
        {
            FullName = fullName;
            Address = address;
            FromDate = fromDate;
            ToDate = toDate;
            Rows = rows;
        }
    }

    public partial class viewReceiptControl : UserControl
    {
        private ReceiptData _receiptInfo;

        public viewReceiptControl()
        {
            InitializeComponent();
            LoadConsumers();
        }

        private void viewReceiptControl_Load(object sender, EventArgs e)
        {
            // уже сделано в конструкторе
        }

        private void LoadConsumers()
        {
            using (var conn = new SqlConnection(AppConnection.ConnectionString))
            using (var da = new SqlDataAdapter(
                "SELECT id, full_name FROM consumers ORDER BY full_name", conn))
            {
                var table = new DataTable();
                da.Fill(table);
                cmbConsumer.DataSource = table;
                cmbConsumer.DisplayMember = "full_name";
                cmbConsumer.ValueMember = "id";
                cmbConsumer.SelectedIndex = -1;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (cmbConsumer.SelectedValue == null)
                return;

            int consumerId = (int)cmbConsumer.SelectedValue;
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;

            // 1) Получаем общие данные: ФИО, адрес и тариф
            string fullName = "";
            string address = "";
            decimal tariff = 0m;
            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(@"
SELECT c.full_name, c.address, r.gas_tariff
  FROM consumers c
  JOIN regions   r ON r.id = c.region_id
 WHERE c.id = @cid", cn))
            {
                cmd.Parameters.AddWithValue("@cid", consumerId);
                cn.Open();
                using (var r = cmd.ExecuteReader())
                    if (r.Read())
                    {
                        fullName = r.GetString(0);
                        address = r.GetString(1);
                        tariff = r.GetDecimal(2);
                    }
            }

            // 2) Собираем по каждому счётчику его min/max показания
            var rows = new List<MeterReceiptRow>();
            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(@"
SELECT m.serial_number,
       MIN(mr.reading_value) AS StartValue,
       MAX(mr.reading_value) AS EndValue
  FROM meters m
  JOIN meter_readings mr ON mr.meter_id = m.id
 WHERE m.consumer_id = @cid
   AND mr.reading_date BETWEEN @from AND @to
 GROUP BY m.serial_number
HAVING COUNT(*) > 0", cn))
            {
                cmd.Parameters.AddWithValue("@cid", consumerId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                cn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        rows.Add(new MeterReceiptRow
                        {
                            MeterSerial = r.GetString(0),
                            StartValue = r.GetDecimal(1),
                            EndValue = r.GetDecimal(2),
                            Tariff = tariff
                        });
                    }
            }

            if (rows.Count == 0)
            {
                lblInfo.Text = "Нет показаний за выбранный период.";
                _receiptInfo = null;
                return;
            }

            // 3) Выводим в метку сводную информацию
            var sb = new StringBuilder();
            sb.AppendLine($"ФИО: {fullName}");
            sb.AppendLine($"Адрес: {address}");
            sb.AppendLine($"Период: {from:dd.MM.yyyy} – {to:dd.MM.yyyy}");
            sb.AppendLine();
            foreach (var r in rows)
            {
                sb.AppendLine(
                  $"Счётчик {r.MeterSerial}: {r.StartValue:F2} → {r.EndValue:F2}  |  " +
                  $"Объём {r.Volume:F2} м³  |  Итого {r.Amount:F2} ₽");
            }
            lblInfo.Text = sb.ToString();

            // 4) Сохраняем DTO для PDF
            _receiptInfo = new ReceiptData(fullName, address, from, to, rows);
        }

        public void GenerateFormattedReceiptPdf(string filePath, ReceiptData data)
        {
            // 1) Создаём документ
            Document doc = new Document(PageSize.A4, 36, 36, 36, 36);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            // 2) Шрифты с кириллицей
            var bf = BaseFont.CreateFont(
                              @"C:\Windows\Fonts\arial.ttf",
                              BaseFont.IDENTITY_H,
                              BaseFont.EMBEDDED);
            var fReg = new iTextSharp.text.Font(bf, 10);
            var fBold = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
            var fTitle = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);

            // 3) Заголовок и клиент
            doc.Add(new Paragraph("Квитанция на оплату газа", fTitle));
            doc.Add(new Paragraph($"ФИО: {data.FullName}", fReg));
            doc.Add(new Paragraph($"Адрес: {data.Address}", fReg));
            doc.Add(new Paragraph($"Период: {data.FromDate:dd.MM.yyyy} – {data.ToDate:dd.MM.yyyy}", fReg));
            doc.Add(new Paragraph("\n"));

            // 4) Таблица: счётчик | нач. | кон. | объём | тариф | сумма
            var table = new PdfPTable(6) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 2, 1, 1, 1, 1, 1 });

            void AddHeader(string text) =>
                table.AddCell(new PdfPCell(new Phrase(text, fBold))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

            AddHeader("Счётчик");
            AddHeader("Нач. показ.");
            AddHeader("Кон. показ.");
            AddHeader("Объём, м³");
            AddHeader("Тариф, ₽");
            AddHeader("Сумма, ₽");

            foreach (var row in data.Rows)
            {
                table.AddCell(new Phrase(row.MeterSerial, fReg));
                table.AddCell(new Phrase(row.StartValue.ToString("F2", CultureInfo.InvariantCulture), fReg));
                table.AddCell(new Phrase(row.EndValue.ToString("F2", CultureInfo.InvariantCulture), fReg));
                table.AddCell(new Phrase(row.Volume.ToString("F2", CultureInfo.InvariantCulture), fReg));
                table.AddCell(new Phrase(row.Tariff.ToString("F2", CultureInfo.InvariantCulture), fReg));
                table.AddCell(new Phrase(row.Amount.ToString("F2", CultureInfo.InvariantCulture), fReg));
            }

            // ИТОГО
            decimal grandTotal = data.Rows.Sum(r => r.Amount);
            table.AddCell(new PdfPCell(new Phrase("Итого", fBold))
            {
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });
            table.AddCell(new PdfPCell(new Phrase(grandTotal.ToString("F2"), fBold)));

            doc.Add(table);

            // 5) Дата печати
            doc.Add(new Paragraph($"\nДата печати: {DateTime.Now:dd.MM.yyyy}", fReg));

            // 6) QR-код (SBP)
            doc.Add(new Paragraph("\nДля оплаты отсканируйте QR-код:", fBold));
            string sbpQr = GenerateSberbankQrString(
                recipientName: "ПАО Газпром",
                account: "40817810563002020014",
                bik: "042809679",
                inn: "7707083893",
                kpp: "695202001",
                consumerId: cmbConsumer.SelectedValue?.ToString(),
                amount: grandTotal,
                purpose: "Оплата за газ по квитанции"
            );
            var qr = GenerateQrImage(sbpQr);
            qr.Alignment = Element.ALIGN_LEFT;
            doc.Add(qr);

            doc.Close();
        }

        private iTextSharp.text.Image GenerateQrImage(string qrText)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Height = 150, Width = 150, Margin = 1 }
            };

            var pixelData = writer.Write(qrText);
            using (var bmp = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bmpData = bmp.LockBits(
                    new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(
                        pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
                }
                finally { bmp.UnlockBits(bmpData); }

                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    var img = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    img.ScaleAbsolute(100, 100);
                    return img;
                }
            }
        }

        private string GenerateSberbankQrString(
            string recipientName, string account, string bik,
            string inn, string kpp, string consumerId,
            decimal amount, string purpose)
        {
            consumerId = string.IsNullOrEmpty(consumerId) ? "000000" : consumerId;
            string kopecks = ((int)(amount * 100)).ToString();
            return $"ST00012|Name={recipientName}|PersonalAcc={account}|BankName=Сбербанк|BIC={bik}|"
                 + $"PayeeINN={inn}|KPP={kpp}|PersAcc={consumerId}|Sum={kopecks}|Purpose={purpose}";
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            if (_receiptInfo == null)
            {
                MessageBox.Show("Сначала выполните расчёт.");
                return;
            }

            using (var dlg = new SaveFileDialog
            {
                Filter = "PDF файл|*.pdf",
                FileName = "Квитанция.pdf"
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GenerateFormattedReceiptPdf(dlg.FileName, _receiptInfo);
                    MessageBox.Show("PDF сохранён.");
                }
            }
        }
    }
}
