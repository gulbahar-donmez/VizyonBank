using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VizyonBank
{
    public partial class Doviz : Form
    {
        private string musteriId;
        public Doviz(string musteriId)
        {
            this.musteriId = musteriId;
            InitializeComponent();
            Doviz1 dalis = new Doviz1();
            Doviz1 dsatis = new Doviz1();

            Doviz1 ealis = new Doviz1();
            Doviz1 esatis = new Doviz1();

            Doviz1 Salis = new Doviz1();
            Doviz1 Ssatis = new Doviz1();

            Doviz1 Yalis = new Doviz1();
            Doviz1 Ysatis=new Doviz1();

            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDosya = new XmlDocument();
            xmlDosya.Load(bugun);

            dalis.alisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            dolarAlisTxt.Text = dalis.alisFiyati;

            dsatis.satisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            satisDolar.Text = dsatis.satisFiyati;



            ealis.alisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            euroAlis.Text = ealis.alisFiyati;

            esatis.satisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            satisEuroTxt.Text = esatis.satisFiyati;


            Salis.alisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteBuying").InnerXml;
            sterlinAlis.Text = Salis.alisFiyati;

            Ssatis.satisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling").InnerXml;
            sterlinSatis.Text = Ssatis.satisFiyati;


            Yalis.alisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='JPY']/BanknoteBuying").InnerXml;
            yenAlis.Text = Yalis.alisFiyati;

            Ysatis.satisFiyati = xmlDosya.SelectSingleNode("Tarih_Date/Currency[@Kod='JPY']/BanknoteSelling").InnerXml;
            yenSatis.Text = Ysatis.satisFiyati;



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

        private void Customers_Button_Click(object sender, EventArgs e)
        {
            FaturaOdeme f2 = new FaturaOdeme(musteriId);

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

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Login f2 = new Login();
            f2.Show();
            this.Hide();
        }
    }
}
