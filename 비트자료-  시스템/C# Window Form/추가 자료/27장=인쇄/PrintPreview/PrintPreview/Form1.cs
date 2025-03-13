using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace PrintPreview
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
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.DocumentName = "테스트 문서";
                printDocument1.Print();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 여백 경계선 출력
            Pen P = new Pen(Color.Blue, 5);
            P.Alignment = PenAlignment.Inset;
            e.Graphics.DrawRectangle(P, e.MarginBounds);

            // 문자열 및 사각형 출력
            Font F = new Font("바탕", 12);
            e.Graphics.DrawString("바탕 12pt 문자열", F, Brushes.Black,
                e.MarginBounds.Left + 100, e.MarginBounds.Top + 100);
            e.Graphics.DrawRectangle(new Pen(Color.Black, 2),
                e.MarginBounds.Left + 90, e.MarginBounds.Top + 90, 200, 50);
        }
    }
}