using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Transform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 수평 이동
            //e.Graphics.TranslateTransform(40, 50);

            // 확대
            //e.Graphics.ScaleTransform(2, 2);

            // 회전
            //e.Graphics.RotateTransform(-15f);

			// 90도 회전
			//e.Graphics.TranslateTransform(40, 200);
			//e.Graphics.RotateTransform(-90f);
			
			e.Graphics.DrawEllipse(Pens.Black, 30, 30, 100, 50);
            e.Graphics.DrawString("타원을 하나 그렸다.", Font, Brushes.Blue, 40, 95);
        }
    }
}