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
    public partial class ParaIslemleri : Form
    {
        private string musteriId;

        //sql bağlantısı
        private string connectionString = @"Server=DESKTOP-LC42T0O;Database=bankaDB;User Id=sa;Password=12345678;";
        public ParaIslemleri(string musteriId)
        {
            InitializeComponent();
            // Constructor alınan kısım
            this.musteriId = musteriId;
            //MessageBox.Show(musteriId);
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
                
                connection.Close();

            }


        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void paraCekBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string miktar = miktarTxt.Text; // Kullanıcının girdiği miktar

                //para çekme işlemini yapma
                string sql = "exec sp_paraCek " + musteriId + " , " + miktar;

                SqlCommand command = new SqlCommand(sql, connection);


                if (!string.IsNullOrEmpty(miktar))
                {
                    int kontrol = command.ExecuteNonQuery();
                    connection.Close();

                    if (kontrol > 0)
                    {
                        MessageBox.Show("Para çekme işlemi başarılı.");
                        miktarTxt.Clear();
                        miktarGetir();
                    }
                    else
                    {
                        // Eğer bu noktaya gelindiyse, işlem başarısız olmuştur
                        MessageBox.Show("Para çekme işlemi başarısız. Lütfen tekrar deneyin.");
                    }
                }
                else
                {

                    MessageBox.Show("Miktar boş olamaz.");
                }
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

        private void paraYatirBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string miktar = miktarTxt.Text; // Kullanıcının girdiği miktar

                //para çekme işlemini yapma
                string sql = "exec sp_paraYatir " + musteriId + " , " + miktar;

                SqlCommand command = new SqlCommand(sql, connection);



                if (!string.IsNullOrEmpty(miktar))
                {
                    int kontrol = command.ExecuteNonQuery();
                    connection.Close();

                    if (kontrol > 0)
                    {
                        MessageBox.Show("Para Yatırma işlemi başarılı.");
                        miktarTxt.Clear();
                        miktarGetir();
                    }
                    else
                    {
                        // Eğer bu noktaya gelindiyse, işlem başarısız olmuştur
                        MessageBox.Show("Para Yatırma işlemi başarısız. Lütfen tekrar deneyin.");
                    }
                }
                else
                {

                    MessageBox.Show("Miktar boş olamaz.");
                }
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

        private void miktarTxt_TextChanged(object sender, EventArgs e)
        {
            miktarTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            FaturaOdeme f2 = new FaturaOdeme(musteriId);

            f2.Show();
            this.Hide();
        }

        private void gunaPanel3_Paint(object sender, PaintEventArgs e)
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
