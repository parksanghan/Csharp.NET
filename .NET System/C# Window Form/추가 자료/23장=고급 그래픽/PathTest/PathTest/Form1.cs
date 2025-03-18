using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PathTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath Path1 = new GraphicsPath();
            GraphicsPath Path2 = new GraphicsPath();
            Pen P = new Pen(Color.Black, 5);
            P.DashStyle = DashStyle.DashDotDot;

            // 직접 그리기 
            e.Graphics.DrawLine(P, 10, 10, 70, 100);
            e.Graphics.DrawLine(P, 70, 100, 130, 10);

            // 패스로 외곽선 그리기
            Path1.AddLine(10, 110, 70, 200);
            Path1.AddLine(70, 200, 130, 110);
            e.Graphics.DrawPath(P, Path1);

            // 패스 채우기
            Path2.AddLine(140, 110, 200, 200);
            Path2.AddLine(200, 200, 260, 110);
            e.Graphics.FillPath(Brushes.Blue, Path2);
        }
    }
}