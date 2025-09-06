using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MatrixTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 평행 이동
            //e.Graphics.Transform = new Matrix(1, 0, 0, 1, 50, 50);

            // 확대
            //e.Graphics.Transform = new Matrix(2, 0, 0, 2, 0, 0);

            // 회전
            //double rad = -20 * Math.PI / 180;
            //e.Graphics.Transform = new Matrix((float)Math.Cos(rad), (float)Math.Sin(rad), 
            //    -(float)Math.Sin(rad), (float)Math.Cos(rad), 0, 0);

            // 기울이기
            //e.Graphics.Transform = new Matrix(1, 0, 1, 1, 0, 0);

            // 수평 뒤집기
            //e.Graphics.Transform = new Matrix(-1, 0, 0, 1, 400, 0);

            // 수직 뒤집기
            //e.Graphics.Transform = new Matrix(1, 0, 0, -1, 0, 200);

            e.Graphics.DrawEllipse(new Pen(Color.Red, 3), 40, 85, 170, 60);
            e.Graphics.DrawString("행렬 변환", new Font("궁서", 25), 
                Brushes.Blue, 50, 100);
        }
    }
}