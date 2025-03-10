using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PathGradient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

private void Form1_Paint(object sender, PaintEventArgs e)
{
    /*
    Point[] pts = { new Point(100, 0), new Point(0, 100), new Point(200, 100) };
    PathGradientBrush B = new PathGradientBrush(pts, WrapMode.Tile);
    e.Graphics.FillRectangle(B, ClientRectangle);
    //*/

    //*
    Point[] pts = { new Point(100, 0), new Point(0, 100), new Point(200, 100) };
    PathGradientBrush B = new PathGradientBrush(pts, WrapMode.Tile);
    B.CenterColor = Color.Yellow;
    B.SurroundColors = new Color[] { Color.Red, Color.Green, Color.Blue };
    B.CenterPoint = new Point(120, 80);
    e.Graphics.FillRectangle(B, ClientRectangle);
    //*/
}
    }
}