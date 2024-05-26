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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace VizyonBank
{
    public partial class Musteri : Form
    {


        private int musteriId;
        bool isDataCleared = false;
        public Musteri()
        {
            InitializeComponent();
            musteriGetir();
            InitializeComboBox();


        }
        SqlConnection connection = new SqlConnection(@"Server=DESKTOP-LC42T0O;Database=bankaDB;User Id=sa;Password=12345678;");

        private void musteriGetir()
        {

            SqlCommand command = new SqlCommand("select * from Musteriler", connection);
            DataTable dt = new DataTable();
            connection.Open();

            SqlDataReader sdr = command.ExecuteReader();
            dt.Load(sdr);
            connection.Close();

            musteriDG.DataSource = dt;
        }       

        private void ResetFormControls()
        {
            musteriId = 0;
            adiTxt.Clear();
            soyadiTxt.Clear();
            mailTxt.Clear();
            telTxt.Clear();
            ibanTxt.Clear();
            adresTxt.Clear();
            tcTxt.Clear();
            

            adiTxt.Focus();

        }



        private void musteriSil()
        {
            if (musteriId > 0)
            {
                SqlCommand cmd = new SqlCommand("delete FROM Musteriler WHERE MusteriID=@ID", connection);

                connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.musteriId);

                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Silme İşlemini Onaylıyor Musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Müşteri Silme İşlemi Başarı İle Gerçekleştirildi", "SİLİNDİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    musteriGetir();
                    ResetFormControls();

                }

                if (secim == DialogResult.No)
                {
                    MessageBox.Show("işleminiz iptal edildi", "iptal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }




            }
        }

        private bool IsValid()
        {
            if (adiTxt.Text == string.Empty)
            {
                MessageBox.Show("Lütfen Müşterinin Adını Giriniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        private void InitializeComboBox()
        {
            // Erkek ve kadın seçeneklerini ComboBox'a ekle
            cinsiyetComboBox.Items.Add("Erkek");
            cinsiyetComboBox.Items.Add("Kadın");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (isDataCleared)
            {
                MessageBox.Show("Veri temizlendi. Lütfen önce yeni veri girin.");
                return;
            }


            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("insert into Musteriler VALUES (@Ad , @Soyad ,@Email ,@Telefon ,@Adres ,@tcNO,@Sifre,@IBAN ,@Cinsiyet)", connection);
                
                connection.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Ad", adiTxt.Text);
                cmd.Parameters.AddWithValue("@Soyad", soyadiTxt.Text);
                cmd.Parameters.AddWithValue("@Email", mailTxt.Text);
                cmd.Parameters.AddWithValue("@Telefon", telTxt.Text);
                cmd.Parameters.AddWithValue("@Adres", adresTxt.Text);
                cmd.Parameters.AddWithValue("@tcNO", tcTxt.Text);
                cmd.Parameters.AddWithValue("@Sifre", sifreTxt.Text);
                cmd.Parameters.AddWithValue("@IBAN", ibanTxt.Text);

                // Kullanıcının seçtiği değeri al
                string selectedGender = cinsiyetComboBox.SelectedItem.ToString();
                cmd.Parameters.AddWithValue("@Cinsiyet", selectedGender);

                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Ekleme İşlemini Onaylıyor Musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Müşteri Ekleme İşlemi Başarı İle Gerçekleştirildi", "SİLİNDİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    musteriGetir();
                    ResetFormControls();



                }

                if (secim == DialogResult.No)
                {
                    MessageBox.Show("işleminiz iptal edildi", "iptal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }

        }



        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTEmizle_Click(object sender, EventArgs e)
        {
            ResetFormControls();
            isDataCleared = true;
        }

        private void musteriDG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            musteriId = Convert.ToInt32(musteriDG.SelectedRows[0].Cells[0].Value);
            adiTxt.Text = musteriDG.SelectedRows[0].Cells[1].Value.ToString();
            soyadiTxt.Text = musteriDG.SelectedRows[0].Cells[2].Value.ToString();
            mailTxt.Text = musteriDG.SelectedRows[0].Cells[3].Value.ToString();
            telTxt.Text = musteriDG.SelectedRows[0].Cells[4].Value.ToString();
            adresTxt.Text = musteriDG.SelectedRows[0].Cells[5].Value.ToString();
            tcTxt.Text = musteriDG.SelectedRows[0].Cells[6].Value.ToString();
            sifreTxt.Text = musteriDG.SelectedRows[0].Cells[7].Value.ToString();
            ibanTxt.Text = musteriDG.SelectedRows[0].Cells[8].Value.ToString();
            cinsiyetComboBox.Text = musteriDG.SelectedRows[0].Cells[9].Value.ToString();


        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (isDataCleared)
            {
                MessageBox.Show("Veri temizlendi. Lütfen önce yeni veri girin.");
                return;
            }

            if (musteriId > 0)
            {
                SqlCommand cmd = new SqlCommand("update Musteriler set Ad=@Ad ,Soyad= @Soyad ,Email=@Email ,Telefon=@Telefon ,Adres=@Adres ,tcNO=@tcNO,Sifre=@Sifre,IBAN=@IBAN ,Cinsiyet=@Cinsiyet WHERE MusteriID=@ID", connection);

                connection.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Ad", adiTxt.Text);
                cmd.Parameters.AddWithValue("@Soyad", soyadiTxt.Text);
                cmd.Parameters.AddWithValue("@Email", mailTxt.Text);
                cmd.Parameters.AddWithValue("@Telefon", telTxt.Text);
                cmd.Parameters.AddWithValue("@Adres", adresTxt.Text);
                cmd.Parameters.AddWithValue("@tcNO", tcTxt.Text);
                cmd.Parameters.AddWithValue("@Sifre", sifreTxt.Text);
                cmd.Parameters.AddWithValue("@IBAN", ibanTxt.Text);

                // Kullanıcının seçtiği değeri al
                string selectedGender = cinsiyetComboBox.SelectedItem.ToString();
                cmd.Parameters.AddWithValue("@Cinsiyet", selectedGender);

                cmd.Parameters.AddWithValue("@ID", this.musteriId);

                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Düzenleme İşlemini Onaylıyor Musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Müşteri Düzenleme İşlemi Başarı İle Gerçekleştirildi", "Düzenlendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    musteriGetir();
                    ResetFormControls();

                }

                if (secim == DialogResult.No)
                {
                    MessageBox.Show("işleminiz iptal edildi", "iptal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            else
            {
                MessageBox.Show("Lütfen Düzenlemek İçin Bir Müşteri Seçiniz!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            musteriSil();
        }

        private void adiTxt_TextChanged(object sender, EventArgs e)
        {
            adiTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsLetter(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != ' ')
                    ev.Handled = true;
            };

        }

        private void soyadiTxt_TextChanged(object sender, EventArgs e)
        {
            soyadiTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsLetter(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != ' ')
                    ev.Handled = true;
            };
        }

        private void adresTxt_TextChanged(object sender, EventArgs e)
        {
            adresTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsLetter(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != ' ')
                    ev.Handled = true;
            };
        }

        private void telTxt_TextChanged(object sender, EventArgs e)
        {
            telTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
        }

        private void tcTxt_TextChanged(object sender, EventArgs e)
        {
            tcTxt.MaxLength = 11;
            if (tcTxt.Text.Length > 11)
                MessageBox.Show("tc no 11 haneden fazla olamaz");
            tcTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                    ev.Handled = true;
            };
        }

        

        private void ibanTxt_TextChanged(object sender, EventArgs e)
        {
            ibanTxt.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != ' ')
                    ev.Handled = true;
            };
        }

       
        private void sifreTxt_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
