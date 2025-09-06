using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PathTransform
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
            Path.AddString("한글", new FontFamily("궁서"), 0, 100, 
                new Point(10, 30), new StringFormat());

            /* 확장 후 외곽선 그리기
            Path.Widen(new Pen(Color.Black, 3));
            e.Graphics.DrawPath(Pens.Black, Path);
            //*/

            /* 확장 후 채우기
            Path.Widen(new Pen(Color.Black, 3));
            e.Graphics.FillPath(Brushes.Blue, Path);
            //*/

            /* 곡선 펴기
            Path.Flatten(new Matrix(), 12f);
            e.Graphics.DrawPath(Pens.Black, Path);
            //*/

            /* 회전
            Matrix M = new Matrix();
            M.Rotate(-10);
            Path.Transform(M);
            e.Graphics.FillPath(Brushes.Blue, Path);
            //*/

            //* 휘기
            RectangleF R = Path.GetBounds();
            PointF[] arPoint = new PointF[4];
            arPoint[0] = new PointF(R.Left, R.Top + 30);
            arPoint[1] = new PointF(R.Right, R.Top - 10);
            arPoint[2] = new PointF(R.Left + 10, R.Bottom - 10);
            arPoint[3] = new PointF(R.Right + 30, R.Bottom + 30);

            Path.Warp(arPoint, R);
            e.Graphics.FillPath(Brushes.Blue, Path);
            //*/
        }
    }
}