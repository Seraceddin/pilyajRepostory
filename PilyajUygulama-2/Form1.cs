using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilyajUygulama_2
{
    public partial class Form1 : Form
    {
        float xxmin,yymin,xxmax,yymax;
        string xmin,ymin,xmax,ymax;
        string tahtax, tahtay, klm_reng;
        float oranx, orany;

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Enabled = true;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = listBox1.SelectedItem.ToString();
        }

        string ittem;
        OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            CheckFileExists = false,
            CheckPathExists = false,
            Multiselect = false,
            Title = "Yeni İş Aç",
            DefaultExt = "dxf",
            Filter = "dxf files (*.dxf)|*.dxf",
        };
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            string dosya = textBox1.Text;
            string oku;
            StreamReader str = new StreamReader(dosya);
            oku = str.ReadLine();
            while(oku!=null)
            {
                listBox2.Items.Add(oku);
                oku = str.ReadLine();
            }
            str.Close();
            for (int say=0; say<listBox2.Items.Count; say++)
            {
                listBox2.SetSelected(say, true); //if kontrollerinde item bilgisini yakalamak için gerekli
                if (listBox2.SelectedItem.ToString() == "HEADER")
                {
                    listBox2.SetSelected(say + 14, true);
                    tahtax = listBox2.SelectedItem.ToString();
                    listBox2.SetSelected(say + 16, true);
                    tahtay = listBox2.SelectedItem.ToString();
                    label2.Text = tahtax + "X" + tahtay; // burada headerden tahta boyutları alınır

                    oranx = float.Parse(tahtax, CultureInfo.InvariantCulture.NumberFormat) / 785;
                    orany = float.Parse(tahtay, CultureInfo.InvariantCulture.NumberFormat) / 585;
                    label1.Text = oranx.ToString() + "-" + orany.ToString();
                    //oranlar hesaplanır
                }

                if (listBox2.SelectedItem.ToString() == "LINE") // item "LINE" ise yapılacaklar
                {
                    ittem = listBox2.SelectedItem.ToString();

                    listBox2.SetSelected(say + 6, true);                                    
                    klm_reng = listBox2.SelectedItem.ToString();

                    listBox2.SetSelected(say + 8, true);                                    //
                    xmin = listBox2.SelectedItem.ToString();                                // minumum x koordinat bilgisi
                    xxmin =float.Parse(xmin, CultureInfo.InvariantCulture.NumberFormat);    //
                    listBox2.SetSelected(say + 10, true);                                   // 
                    ymin = listBox2.SelectedItem.ToString();                                // minumum y koordinat bilgisi
                    yymin = float.Parse(ymin, CultureInfo.InvariantCulture.NumberFormat);   //
                    listBox2.SetSelected(say + 12, true);                                   //
                    xmax = listBox2.SelectedItem.ToString();                                // maximum x koordinat bilgisi
                    xxmax = float.Parse(xmax, CultureInfo.InvariantCulture.NumberFormat);   //
                    listBox2.SetSelected(say + 14, true);                                   //
                    ymax = listBox2.SelectedItem.ToString();                                // maximum y koordinat bilgisi
                    yymax = float.Parse(ymax, CultureInfo.InvariantCulture.NumberFormat);   //
                    czz();  // "LINE" bilgilerine göre çiz
                    if(float.Parse(klm_reng)==2)
                    {
                        listBox1.Items.Add(say + " " + ittem + " " + xxmin + " " + yymin + " " + xxmax + " " + yymax);
                    }
                }
                
            }
            textBox2.Text = listBox2.Items.Count.ToString(); // Bize dxf de kaç satır olduğu bilgisini verir.
            
        }
        void czz()
        {
            Pen klm;
            Graphics gp = panel1.CreateGraphics();
            if(float.Parse(klm_reng) == 1)
            {
                 klm = new Pen(Color.White, 1);
            }
            else if (float.Parse(klm_reng) == 2)
            {
                 klm = new Pen(Color.Green, 1);
            }
            else
            {
                klm = new Pen(Color.Red, 1);
            }
            gp.DrawLine(klm, 5 + xxmin, 5 + yymin, 5 + xxmax, 5 + yymax);
            //gp.DrawLine(klm, 5+(xxmin/oranx), 5+(yymin/orany), 5+(xxmax/oranx), 5+(yymax/orany)); //Burada verilen 5 grafiğin kenardan uzaklığı olarak verilmiştir.
            // oranx ve oran y cizimin ölçülerine göre panele orantılı yerleştirilmesi için kullanılmıştır.
        }
    } 
}