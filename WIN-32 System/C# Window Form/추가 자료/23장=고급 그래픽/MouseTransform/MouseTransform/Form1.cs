using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseTransform
{
    public partial class Form1 : Form
    {
        private Bitmap Baby;
        private float cx = 200, cy = 200;
        private float zoom = 1;
        private float angle = 0;

        public Form1()
        {
            Baby = Properties.Resources.baby;
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(cx, cy);
            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.RotateTransform(angle);

            Font F = new Font("궁서", 15);
            e.Graphics.DrawString("왼쪽 클릭 : 중앙점 이동", Font, Brushes.Black, -100, -160);
            e.Graphics.DrawString("휠 : 확대/축소", F, Brushes.Red, -100, -140);
            e.Graphics.DrawString("위, 아래 키 : 회전", Font, Brushes.Green, -100, -120);
            e.Graphics.DrawString("스페이스 바 : 리셋", Font, Brushes.Blue, -100, -100);
            e.Graphics.DrawRectangle(new Pen(Color.Red, 3), -120, -180, 200, 100);
            e.Graphics.DrawImage(Baby, -140, -70);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cx = e.X;
                cy = e.Y;
                Invalidate();
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom += 0.1f;
            }
            else
            {
                if (zoom > 0.1f) zoom -= 0.1f;
            }
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                cx = 200;
                cy = 200;
                zoom = 1;
                angle = 0;
                Invalidate();
            }
            if (e.KeyCode == Keys.Up)
            {
                angle -= 5;
                Invalidate();
            }
            if (e.KeyCode == Keys.Down)
            {
                angle += 5;
                Invalidate();
            }
        }
    }
}