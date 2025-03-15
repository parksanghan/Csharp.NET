using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MultiPage
{
    public partial class Form1 : Form
    {
        Font PF;
        int Start, End, Now;
        const int LPP = 20;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.DocumentName = "테스트 문서";
                switch (printDialog1.PrinterSettings.PrintRange)
                {
                    case PrintRange.AllPages:
                        Start = 0; End = 4;
                        break;
                    case PrintRange.CurrentPage:
                        Start = 2; End = 2;
                        break;
                    case PrintRange.Selection:
                        Start = 3; End = 3;
                        break;
                    case PrintRange.SomePages:
                        Start = printDialog1.PrinterSettings.FromPage;
                        End = printDialog1.PrinterSettings.ToPage;
                        break;
                }
                printDocument1.Print();
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            PF = new Font("바탕", 14);
            Now = Start;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string Text;
            for (int line = 0; line < LPP; line++)
            {
                Text = string.Format("{0}번째 줄입니다.", Now * LPP + line + 1);
                e.Graphics.DrawString(Text, PF, Brushes.Black,
                    e.MarginBounds.Left + 10, e.MarginBounds.Top + line * 40);
            }

            Now++;
            if (Now <= End)
            {
                e.HasMorePages = true;
            }
        }

        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            PF.Dispose();
        }

    }
}