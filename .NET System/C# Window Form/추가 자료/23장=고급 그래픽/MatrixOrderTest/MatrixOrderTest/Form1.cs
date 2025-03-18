using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MatrixOrderTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /* 확대 후 이동
            e.Graphics.TranslateTransform(100, 100);
            e.Graphics.ScaleTransform(2, 2);
            //*/

            //* 이동 후 확대
            e.Graphics.ScaleTransform(2, 2);
            e.Graphics.TranslateTransform(100, 100);
            //*/

            /* 확대 후 이동
            e.Graphics.ScaleTransform(2, 2);
            e.Graphics.TranslateTransform(100, 100, MatrixOrder.Append);
            //*/

            /* 이동 후 확대
            e.Graphics.ScaleTransform(2, 2);
            e.Graphics.TranslateTransform(100, 100,MatrixOrder.Prepend);
            //*/

            e.Graphics.DrawEllipse(Pens.Black, 0, 0, 100, 50);

        }
    }
}