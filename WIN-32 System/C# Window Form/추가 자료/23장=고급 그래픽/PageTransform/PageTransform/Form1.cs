using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PageTransform
{
    public partial class Form1 : Form
    {
        private float scale = 1.0f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Point;
            e.Graphics.PageScale = scale;

            e.Graphics.DrawRectangle(new Pen(Color.Blue, 3), 50, 90, 150, 50);
            e.Graphics.DrawString("페이지 변환", new Font("궁서", 25 * scale), 
                Brushes.Black, 50, 100);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                scale += 0.1f;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (scale > 0.1f) scale -= 0.1f;
            }
            Invalidate();
        }
    }
}