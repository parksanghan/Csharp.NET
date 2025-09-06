using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;

namespace PathIsVisible
{
    public partial class Form1 : Form
    {
        private GraphicsPath Path;

        public Form1()
        {
            InitializeComponent();
            Path = new GraphicsPath();
            StringFormat sm = new StringFormat();
            Path.AddString("한", new FontFamily("궁서"), 0, 200, new Point(10, 10), sm);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(new Pen(Color.Blue, 5), Path);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Path.IsVisible(e.Location) == true)
                {
                    SystemSounds.Beep.Play();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (Path.IsOutlineVisible(e.Location, new Pen(Color.Black,5)) == true)
                {
                    SystemSounds.Asterisk.Play();
                }
            }
        }
    }
}