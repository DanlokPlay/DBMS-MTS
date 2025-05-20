using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MTS
{
    public partial class MTS : Form
    {
        // Строка подключения с аутентификацией Windows
        private string connectionString = @"Server=localhost;Database=MTS;Trusted_Connection=True;";

        public MTS()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Привязка обработчика к кнопке
            buttonRefresh.Click += ButtonRefresh_Click;
            buttonAddClient.Click += buttonAddClient_Click;
            buttonCallDetails.Click += buttonCallDetails_Click;

        }

        private void MTS_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadAllData()
        {
            LoadData("SELECT * FROM Tariff", dataGridViewTariff);
            LoadData("SELECT * FROM Client", dataGridViewClient);
            LoadData("SELECT * FROM Calls", dataGridViewCalls);
            LoadData("SELECT * FROM Call_Detail_Request", dataGridViewCallDetailRequest);
        }

        private void LoadData(string query, DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            using (AddClientForm addClientForm = new AddClientForm())
            {
                if (addClientForm.ShowDialog() == DialogResult.OK)
                {
                    // После добавления клиента обновляем данные на главной форме
                    LoadData("SELECT * FROM Client", dataGridViewClient);
                }
            }
        }

        private void buttonCallDetails_Click(object sender, EventArgs e)
        {
            using (CallDetailsForm form = new CallDetailsForm())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();

                // Обновляем таблицу после возможного добавления запроса
                LoadData("SELECT * FROM Call_Detail_Request", dataGridViewCallDetailRequest);
            }
        }
    }
}
