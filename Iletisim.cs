using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VizyonBank
{
    public partial class Iletisim : Form
    {
        private string musteriId;
        public Iletisim(string musteriId)
        {
            InitializeComponent();
            this.musteriId = musteriId;
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

   

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            Doviz f2 = new Doviz(musteriId);

            f2.Show();
            this.Hide();

        }

        private void Help_Button_Click(object sender, EventArgs e)
        {
            Iletisim f2 = new Iletisim(musteriId);

            f2.Show();
            this.Hide();

        }

        private void Customers_Button_Click(object sender, EventArgs e)
        {
            FaturaOdeme f2 = new FaturaOdeme(musteriId);

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
