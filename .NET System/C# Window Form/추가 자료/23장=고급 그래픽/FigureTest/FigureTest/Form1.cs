using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FigureTest
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

            Path.AddLine(10, 10, 50, 50);
            Path.AddLine(100, 50, 140, 10);
            Path.CloseFigure();
            Path.StartFigure();
            Path.AddLine(140, 80, 100, 120);
            Path.AddLine(100, 150, 140, 190);
            Path.CloseFigure();

            e.Graphics.DrawPath(Pens.Black, Path);
        }
    }
}