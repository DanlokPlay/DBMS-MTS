using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace MTS
{
    public partial class CallDetailsForm : Form
    {
        private string connectionString = @"Server=localhost;Database=MTS;Trusted_Connection=True;";

        public CallDetailsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Настройка маски
            maskedTextBoxPhone.Mask = "8 (000) 000-00-00";
            maskedTextBoxPhone.Text = "8 ";
            maskedTextBoxPhone.TextChanged += MaskedTextBoxPhone_TextChanged;

            listBoxSuggestions.Visible = false;
            listBoxSuggestions.Click += ListBoxSuggestions_Click;

            buttonSubmitRequest.Click += buttonSubmitRequest_Click;
        }

        private void MaskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            string rawInput = new string(maskedTextBoxPhone.Text.Where(char.IsDigit).ToArray());

            if (rawInput.Length < 1)
            {
                listBoxSuggestions.Visible = false;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT phone_number FROM Client WHERE phone_number LIKE @pattern";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pattern", rawInput + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            listBoxSuggestions.Items.Clear();
                            while (reader.Read())
                            {
                                listBoxSuggestions.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                listBoxSuggestions.Visible = listBoxSuggestions.Items.Count > 0;
            }
            catch
            {
                listBoxSuggestions.Visible = false;
            }
        }

        private void ListBoxSuggestions_Click(object sender, EventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                string selectedPhone = listBoxSuggestions.SelectedItem.ToString();
                maskedTextBoxPhone.Text = FormatPhone(selectedPhone);
                listBoxSuggestions.Visible = false;
            }
        }

        private string FormatPhone(string phone)
        {
            if (phone.Length != 11) return phone;

            return $"8 ({phone.Substring(1, 3)}) {phone.Substring(4, 3)}-{phone.Substring(7, 2)}-{phone.Substring(9, 2)}";
        }

        private void buttonSubmitRequest_Click(object sender, EventArgs e)
        {
            string phoneRaw = maskedTextBoxPhone.Text;
            string phone = "8" + new string(phoneRaw.Where(char.IsDigit).Skip(1).ToArray());

            if (phone.Length != 11 || !phone.StartsWith("8"))
            {
                MessageBox.Show("Введите корректный номер телефона в формате 8 (XXX) XXX-XX-XX");
                return;
            }

            DateTime startDate = dateTimePickerStart.Value.Date;
            DateTime endDate = dateTimePickerEnd.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("Конечная дата не может быть раньше начальной");
                return;
            }

            int? clientId = GetClientIdByPhone(phone);
            if (clientId == null)
            {
                MessageBox.Show("Клиент с таким номером не найден");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Вставка запроса на детализацию
                    string insertQuery = @"INSERT INTO Call_Detail_Request (client_id, start_date, end_date)
                                   VALUES (@client_id, @start_date, @end_date)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@client_id", clientId.Value);
                        insertCmd.Parameters.AddWithValue("@start_date", startDate);
                        insertCmd.Parameters.AddWithValue("@end_date", endDate);
                        insertCmd.ExecuteNonQuery();
                    }

                    // Получение статистики по звонкам клиента за указанный период
                    string statsQuery = @"
                SELECT 
                    COUNT(*) AS CallCount, 
                    ISNULL(SUM(call_cost), 0) AS TotalCost
                FROM Calls
                WHERE client_id = @client_id 
                  AND call_time >= @start_date 
                  AND call_time <= @end_date";

                    using (SqlCommand statsCmd = new SqlCommand(statsQuery, conn))
                    {
                        statsCmd.Parameters.AddWithValue("@client_id", clientId.Value);
                        statsCmd.Parameters.AddWithValue("@start_date", startDate);
                        statsCmd.Parameters.AddWithValue("@end_date", endDate);

                        using (SqlDataReader reader = statsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int callCount = reader.GetInt32(0);
                                decimal totalCost = reader.GetDecimal(1);

                                MessageBox.Show(
                                    $"Запрос на детализацию успешно добавлен.\n\n" +
                                    $"Период: {startDate:dd.MM.yyyy} – {endDate:dd.MM.yyyy}\n" +
                                    $"Количество звонков: {callCount}\n" +
                                    $"Общая стоимость звонков: {totalCost:C}",
                                    "Успешно",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении запроса: " + ex.Message);
            }
        }

        private int? GetClientIdByPhone(string phone)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT client_id FROM Client WHERE phone_number = @phone";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : (int?)null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
