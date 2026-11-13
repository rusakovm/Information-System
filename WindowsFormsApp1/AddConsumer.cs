using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddConsumer : Form
    {
        private readonly ConsumerService _consumerService;
        private readonly RegionService _regionService;

        private const string DaDataToken =
            "775e48ce2dbaa1625ab553269afd50168d9f7834";   // вынесите в конфиг при желании

        public AddConsumer()
        {
            InitializeComponent();

            _consumerService = new ConsumerService(AppConnection.ConnectionString);
            _regionService = new RegionService(AppConnection.ConnectionString);


            Placeholder.Attach(txtFullName, "ФИО");
            Placeholder.Attach(txtContactPerson, "Адрес");
            Placeholder.Attach(txtPhone, "Номер телефона");
            Placeholder.Attach(txtEmail, "Email");

            // ComboBox'ы
            Placeholder.Attach(cmbRegion, "Выберите регион");
            Placeholder.Attach(cmbRegion, "Выберите город");


            // комбобокс сам обрабатывает текст
            cmbAddress.TextChanged += cmbAddress_TextChanged;
        }



        // ───────────────────────────────────────────────────────────────────
        // 1. Загрузка регионов
        // ───────────────────────────────────────────────────────────────────
        private void AddConsumer_Load(object sender, EventArgs e)
        {
            DataTable regions = _regionService.GetRegions();
            cmbRegion.DataSource = regions;
            cmbRegion.DisplayMember = "name";
            cmbRegion.ValueMember = "id";
            cmbRegion.SelectedIndex = -1;
        }

        // ───────────────────────────────────────────────────────────────────
        // 2. Нажата кнопка «Добавить потребителя»
        // ───────────────────────────────────────────────────────────────────
        private void btnAddConsumer_Click_1(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            var ask = MessageBox.Show("Вы уверены, что хотите добавить нового потребителя?",
                                      "Подтверждение",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

            if (ask != DialogResult.Yes) return;

            bool added = _consumerService.AddConsumer(
                txtFullName.Text.Trim(),
                txtContactPerson.Text.Trim(),
                txtPhone.Text.Trim(),
                txtEmail.Text.Trim(),
                cmbAddress.Text.Trim(),
                (int)cmbRegion.SelectedValue);

            if (added)
            {
                MessageBox.Show("Потребитель успешно добавлен.");
                this.DialogResult = DialogResult.OK;   // сообщаем вызывающей форме
                this.Close();
            }
        }

        // ───────────────────────────────────────────────────────────────────
        // 3. Подсказка адреса DaData (по изменению текста)
        // ───────────────────────────────────────────────────────────────────
        private async void cmbAddress_TextChanged(object sender, EventArgs e)
        {
            string query = cmbAddress.Text.Trim();
            if (query.Length < 3) return;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Token " + DaDataToken);

                string body = $"{{\"query\":\"{query}\",\"count\":5}}";
                using (var content = new StringContent(body, Encoding.UTF8, "application/json"))
                {
                    var resp = await client.PostAsync(
                                    "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address",
                                    content);
                    var json = await resp.Content.ReadAsStringAsync();
                    var arr = JObject.Parse(json)["suggestions"];

                    cmbAddress.Items.Clear();
                    foreach (var s in arr)
                        cmbAddress.Items.Add(s["value"].ToString());

                    cmbAddress.DroppedDown = true;
                    cmbAddress.SelectionStart = cmbAddress.Text.Length;
                }
            }
        }


        // ───────────────────────────────────────────────────────────────────
        // 4. Проверка ввода
        // ───────────────────────────────────────────────────────────────────
        private bool ValidateFields()
        {
            /* ----------- Название ----------- */
            if (Placeholder.GetUserText(txtFullName).Length == 0)
            {
                MessageBox.Show("Введите название потребителя.");
                txtFullName.Focus();
                return false;
            }

            /* ----------- Регион (проверяем SelectedIndex) ----------- */
            if (cmbRegion.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите регион.");
                cmbRegion.Focus();
                return false;
            }

            /* ----------- Телефон / e-mail при необходимости ----------- */
            string phone = Placeholder.GetUserText(txtPhone);
            if (phone.Length > 0 && !Regex.IsMatch(phone, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Телефон должен быть в формате +7XXXXXXXXXX.");
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    // ───────────────────────────────────────────────────────────────────
    //  Сервисы
    // ───────────────────────────────────────────────────────────────────
    public class ConsumerService
    {
        private readonly string _cs;
        public ConsumerService(string cs) => _cs = cs;

        public bool AddConsumer(string fullName, string contactPerson, string phone,
                         string email, string address, int regionId)
        {
            const string sql = @"INSERT INTO consumers
        (full_name, contact_person, phone, email, address, region_id)
        VALUES (@n,@cp,@ph,@em,@ad,@rid)";

            using (var cn = new SqlConnection(_cs))
            {
                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@n", fullName);
                    cmd.Parameters.AddWithValue("@cp", (object)contactPerson ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ph", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@em", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ad", (object)address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rid", regionId);

                    try { cn.Open(); cmd.ExecuteNonQuery(); return true; }
                    catch (SqlException ex)
                    { MessageBox.Show("Ошибка SQL: " + ex.Message); return false; }
                }
            }
        }


    }
        public class RegionService
        {
            private readonly string _cs;
            public RegionService(string cs) => _cs = cs;

            public DataTable GetRegions()
            {
                DataTable table = new DataTable();

                using (var cn = new SqlConnection(_cs))
                {
                    using (var da = new SqlDataAdapter(
                               "SELECT id, name FROM regions ORDER BY name", cn))
                    {
                        da.Fill(table);
                    }
                }
                return table;
            }

        }
}