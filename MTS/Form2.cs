using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace MTS
{
    public partial class AddClientForm : Form
    {
        private string connectionString = @"Server=localhost;Database=MTS;Trusted_Connection=True;";

        public AddClientForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            comboBoxClientType.SelectedIndexChanged += ComboBoxClientType_SelectedIndexChanged;
            Load += AddClientForm_Load;
            buttonSave.Click += ButtonSave_Click;

            // Настройка маски для maskedTextBoxPhone
            maskedTextBoxPhone.Mask = "8 (999) 000-00-00";
            maskedTextBoxPhone.SkipLiterals = true; // курсор пропускает скобки и пробелы
            maskedTextBoxPhone.Text = "8 "; // +7 по умолчанию
        }

        private void AddClientForm_Load(object sender, EventArgs e)
        {
            comboBoxClientType.Items.Clear();
            comboBoxClientType.Items.AddRange(new string[] { "Физическое", "Юридическое" });
            comboBoxClientType.SelectedIndex = 0;
        }

        private void ComboBoxClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = comboBoxClientType.SelectedItem.ToString();
            LoadTariffsForClientType(selectedType);
        }

        private void LoadTariffsForClientType(string clientType)
        {
            string tariffType = (clientType == "Юридическое") ? "Корпоративный" : "Некорпоративный";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT tariff_id, tariff_name FROM Tariff WHERE tariff_type = @tariffType";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tariffType", tariffType);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBoxTariff.Items.Clear();
                            while (reader.Read())
                            {
                                comboBoxTariff.Items.Add(new ComboboxItem()
                                {
                                    Text = reader["tariff_name"].ToString(),
                                    Value = reader["tariff_id"]
                                });
                            }
                        }
                    }
                }

                if (comboBoxTariff.Items.Count > 0)
                    comboBoxTariff.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки тарифов: " + ex.Message);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введите имя клиента");
                return;
            }

            if (comboBoxTariff.SelectedItem == null)
            {
                MessageBox.Show("Выберите тариф");
                return;
            }

            if (!decimal.TryParse(textBoxBalance.Text, out decimal balance))
            {
                MessageBox.Show("Введите корректный баланс");
                return;
            }

            string phoneRaw = maskedTextBoxPhone.Text;

            // Строгая проверка визуального формата номера
            if (!IsValidPhoneNumber(phoneRaw))
            {
                MessageBox.Show("Номер телефона должен быть в формате 8 (XXX) XXX-XX-XX");
                return;
            }

            // Преобразуем к 11-значному виду: 89XXXXXXXXX
            string phone = new string(phoneRaw.Where(char.IsDigit).ToArray());

            if (PhoneExists(phone))
            {
                MessageBox.Show("Клиент с таким номером телефона уже существует");
                return;
            }

            string name = textBoxName.Text.Trim();
            string clientType = comboBoxClientType.SelectedItem.ToString();
            int tariffId = (int)((ComboboxItem)comboBoxTariff.SelectedItem).Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string insertQuery = @"INSERT INTO Client (name, client_type, balance, phone_number, tariff_id) 
                                   VALUES (@name, @client_type, @balance, @phone_number, @tariff_id)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@client_type", clientType);
                        cmd.Parameters.AddWithValue("@balance", balance);
                        cmd.Parameters.AddWithValue("@phone_number", phone);
                        cmd.Parameters.AddWithValue("@tariff_id", tariffId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Клиент успешно добавлен");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении клиента: " + ex.Message);
            }
        }


        private bool IsValidPhoneNumber(string phone)
        {
            // Проверка на строгое соответствие шаблону 8 (XXX) XXX-XX-XX
            return System.Text.RegularExpressions.Regex.IsMatch(
                phone,
                @"^8 \(\d{3}\) \d{3}-\d{2}-\d{2}$"
            );
        }

        private bool PhoneExists(string phone)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Client WHERE phone_number = @phone";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                // В случае ошибки считаем, что номер существует,
                // чтобы избежать дублирования
                return true;
            }
        }
    }

    // Класс для удобного хранения пары текст/значение в ComboBox
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
