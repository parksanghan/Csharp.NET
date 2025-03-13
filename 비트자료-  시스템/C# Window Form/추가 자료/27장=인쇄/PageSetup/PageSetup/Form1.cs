using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PageSetup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = new PageSettings();
            pageSetupDialog1.PrinterSettings = new PrinterSettings();
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                PageSettings PS = pageSetupDialog1.PageSettings;
                listBox1.Items.Clear();
                listBox1.Items.Add("가로 방향 인쇄 : " + PS.Landscape);
                listBox1.Items.Add("용지 종류 : " + PS.PaperSize.Kind);
                listBox1.Items.Add("급지 장치 : " + PS.PaperSource.Kind);
                listBox1.Items.Add("여백 : " + PS.Margins);
                listBox1.Items.Add("용지 크기 : " + PS.Bounds);
                listBox1.Items.Add("컬러 인쇄 : " + PS.Color);
            }
        }
    }
}