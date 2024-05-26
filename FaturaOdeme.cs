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

namespace VizyonBank
{
    public partial class FaturaOdeme : Form
    {
        private string musteriId;
        //sql bağlantısı
        private string connectionString = @"Server=DESKTOP-LC42T0O;Database=bankaDB;User Id=sa;Password=12345678;";
        public FaturaOdeme(string musteriId)
        {
            InitializeComponent();
            this.musteriId = musteriId;

            miktarGetir();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Musteriler WHERE musteriId = @musteriId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@musteriId", musteriId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ad = reader["Ad"].ToString();
                            string soyad = reader["Soyad"].ToString();

                            isimtxt.Text = "Hoş Geldiniz, " + ad + " " + soyad;
                        }
                    }
                }

                faturaGetir();


                connection.Close();
            }

        }

        private void miktarGetir()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT dbo.Hesap.Bakiye FROM dbo.Hesap WHERE Hesap.MusteriID = @musteriId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@musteriId", musteriId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string bakiye = reader["Bakiye"].ToString();
                            bakiyeTxt.Text = bakiye + " " + "₺";
                        }
                    }
                }
                connection.Close();

            }

        }

        private void faturaGetir()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                BindingSource bindingSource = new BindingSource();

                // dg tablosuna faturaları getirme
                string sql = "sp_faturaGetir";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@musteriId", musteriId));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        bindingSource.DataSource = dt;
                        faturalarDG.DataSource = bindingSource;
                    }
                }


                connection.Close();

            }

        }

        private void Home_Button_Click(object sender, EventArgs e)
        {
            Kartim f2 = new Kartim(musteriId);

            f2.Show();
            this.Hide();

        }

        private void Orders_Button_Click(object sender, EventArgs e)
        {
            ParaIslemleri f2 = new ParaIslemleri(musteriId);

            f2.Show();
            this.Hide();

        }

        private void Customers_Button_Click(object sender, EventArgs e)
        {
            FaturaOdeme f2 = new FaturaOdeme(musteriId);

            f2.Show();
            this.Hide();

        }

        private void Help_Button_Click(object sender, EventArgs e)
        {
            Iletisim f2 = new Iletisim(musteriId);
            f2.Show();
            this.Hide();



        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();



                string miktar = faturaNoTxt.Text; // Kullanıcının girdiği miktar

                //para çekme işlemini yapma
                string sql = "exec sp_faturaOde " + musteriId + " , " + miktar;

                SqlCommand command = new SqlCommand(sql, connection);


                if (!string.IsNullOrEmpty(miktar))
                {
                    int kontrol = command.ExecuteNonQuery();
                    connection.Close();

                    if (kontrol > 0)
                    {
                        MessageBox.Show("Fatura Ödeme işlemi başarılı.");
                        faturaNoTxt.Clear();
                        miktarGetir();
                        faturaGetir();
                    }
                    else
                    {
                        // Eğer bu noktaya gelindiyse, işlem başarısız olmuştur
                        MessageBox.Show("Fatura Ödeme işlemi başarısız. Lütfen tekrar deneyin.");
                    }
                }
                else
                {

                    MessageBox.Show("Fatura Numarası boş olamaz.");
                }
            }
        }

        private void faturalarDG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Fatura numarasını al (Örneğin, fatura numarası ilk sütunda bulunuyor)
                string faturaNo = faturalarDG.Rows[e.RowIndex].Cells[1].Value.ToString();

                // Fatura numarasını TextBox'a yaz
                faturaNoTxt.Text = faturaNo;
            }
        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            Doviz f2 = new Doviz(musteriId);

            f2.Show();
            this.Hide();
        }

        private void faturalarDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Login f2 = new Login();
            f2.Show();
            this.Hide();
        }
    }
}
