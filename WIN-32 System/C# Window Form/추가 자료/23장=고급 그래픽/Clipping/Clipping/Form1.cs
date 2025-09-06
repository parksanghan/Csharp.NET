using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Clipping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath Path = new GraphicsPath();

            Path.FillMode = FillMode.Winding;
            Path.AddEllipse(50, 10, 100, 80);
            Path.AddEllipse(20, 75, 160, 120);
            e.Graphics.FillPath(Brushes.White, Path);

            e.Graphics.SetClip(Path);

            for (int y = 0; y < Bottom; y += 20)
            {
                string str = "눈사람 모양의 클리핑 영역에 글자를 쓴 것입니다.";
                e.Graphics.DrawString(str, Font, Brushes.Blue, 0, y);
            }
        }
    }
}