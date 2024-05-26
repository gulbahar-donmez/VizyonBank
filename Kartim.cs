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
    public partial class Kartim : Form
    {
        
        private string musteriId;
        public Kartim(string musteriId)
        {
            InitializeComponent();

            this.musteriId = musteriId;

           // MessageBox.Show(musteriId);

            string connectionString = @"Server=DESKTOP-LC42T0O;Database=bankaDB;User Id=sa;Password=12345678;";

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

                sql = "SELECT dbo.HesapTurleri.HesapTuruAdi, dbo.Hesap.Bakiye, dbo.Musteriler.IBAN FROM dbo.Hesap INNER JOIN dbo.HesapTurleri ON dbo.Hesap.HesapTuruID = dbo.HesapTurleri.HesapTuruID INNER JOIN dbo.Musteriler ON dbo.Hesap.MusteriID = dbo.Musteriler.MusteriID WHERE Musteriler.MusteriID = @musteriId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@musteriId", musteriId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hesapTurAdi = reader["HesapTuruAdi"].ToString();
                            string bakiye = reader["Bakiye"].ToString();
                            string iban = reader["IBAN"].ToString();

                            hesapTurTxt.Text = hesapTurAdi;
                            bakiyeTxt.Text = bakiye + " " + "₺";
                            ibanTxt.Text = iban;

                        }
                    }
                }

                sql = "SELECT dbo.HesapHareketleri.IslemTuru, dbo.HesapHareketleri.Tutar, dbo.HesapHareketleri.Tarih FROM dbo.Hesap INNER JOIN dbo.HesapHareketleri ON dbo.Hesap.HesapID = dbo.HesapHareketleri.HesapID where Hesap.MusteriID = @musteriId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@musteriId", musteriId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            hesapHareketleriDG.DataSource = dt;
                        }
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

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void isimtxt_Click(object sender, EventArgs e)
        {

        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            Doviz f2 = new Doviz(musteriId);
            f2.Show();
            this.Hide();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Login f2 = new Login();
            f2.Show();
            this.Hide();
        }
    }
}
