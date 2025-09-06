using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PrintOption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                PrinterSettings PS = printDialog1.PrinterSettings;
                listBox1.Items.Clear();
                listBox1.Items.Add("인쇄할 프린터 : " + PS.PrinterName);
                listBox1.Items.Add("인쇄 매수 : " + PS.Copies);
                listBox1.Items.Add("한부씩 인쇄 : " + PS.Collate);
                switch (PS.PrintRange)
                {
                    case PrintRange.AllPages:
                        listBox1.Items.Add("문서 전체 인쇄");
                        break;
                    case PrintRange.CurrentPage:
                        listBox1.Items.Add("현재 페이지 인쇄");
                        break;
                    case PrintRange.Selection:
                        listBox1.Items.Add("선택 영역 인쇄");
                        break;
                    case PrintRange.SomePages:
                        listBox1.Items.Add(PS.FromPage + " ~ " + PS.ToPage + "까지 인쇄");
                        break;
                }
                if (PS.PrintToFile)
                {
                    PS.PrintFileName = @"C:\Spool.prn";
                    listBox1.Items.Add(PS.PrintFileName + " 파일에 인쇄");
                }
            }

        }
    }
}