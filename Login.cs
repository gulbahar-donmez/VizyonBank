using Guna.UI2.WinForms;
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
using static System.Net.Mime.MediaTypeNames;

namespace VizyonBank
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       

        private void loginBtn_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-LC42T0O;Database=bankaDB;User Id=sa;Password=12345678;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string tcNO = tcNotxt.Text; // Kullanıcının girdiği TC numarası
                string sifre = sifreTxt.Text; // Kullanıcının girdiği şifre

                // Müşteriler tablosunda arama yap
                string sql = "SELECT * FROM Musteriler WHERE tcNO = @tcNO AND Sifre = @sifre";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tcNO", tcNO);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ad = reader["Ad"].ToString(); // Kullanıcının adını al
                            string soyad = reader["Soyad"].ToString(); // Kullanıcının adını al
                            string musteriId = reader["musteriId"].ToString(); // Kullanıcının id al

                            // MessageBox.Show("Merhaba, " + ad + " " + soyad);

                            Kartim f2 = new Kartim(musteriId);

                            f2.Show();
                            this.Hide();


                            connection.Close();
                            return;
                        }
                    }
                }

                // Personeller tablosunda arama yap
                sql = "SELECT * FROM Personeller WHERE tcNO = @tcNO AND Sifre = @sifre";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tcNO", tcNO);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // personelse yönlendireceğin sayfa 
                            MessageBox.Show("Merhaba, değerli personelimiz!");

                            // string personelId = reader["personelId"].ToString(); // Kullanıcının id al


                            Musteri f3 = new Musteri();
                            f3.Show();
                            this.Hide();



                            connection.Close();
                            return;
                        }
                    }
                }

                // Eğer bu noktaya gelindiyse, kullanıcı adı veya şifre hatalıdır
                MessageBox.Show("TC numarası veya şifre hatalı. Lütfen tekrar deneyin.");

            }
        }

        
        // şifreyi göstersin
        private void sifreGosterSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (sifreGosterSwitch.Checked)
            {
                // Şifreyi göster
                sifreTxt.UseSystemPasswordChar = false;
            }
            else
            {
                // Şifreyi gizle
                sifreTxt.UseSystemPasswordChar = true;
            }
        }

        private void tcNotxt_TextChanged(object sender, EventArgs e)
        {
            tcNotxt.MaxLength = 11;
            if (tcNotxt.Text.Length > 11)
                MessageBox.Show("tc no 11 haneden fazla olamaz");
            tcNotxt.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
        }
    }
}     
