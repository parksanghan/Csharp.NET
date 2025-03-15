using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MatrixMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Matrix M = new Matrix();

            // 평행 이동
            //M.Translate(50, 50);

            // 확대
            //M.Scale(2, 2);

            // 회전
            //M.Rotate(-20);

            // 기울이기
            //M.Shear(1, 0);

            // 수평 뒤집기
            //M.Scale(-1, 1);
            //M.Translate(400, 0, MatrixOrder.Append);

            // 수직 뒤집기
            //M.Scale(1, -1);
            //M.Translate(0, 200, MatrixOrder.Append);

            // 회전 중심점 지정하기
            //M.RotateAt(-20, new PointF(170, 60));

            //e.Graphics.Transform = M;
            Matrix M2 = e.Graphics.Transform;
                M2.Rotate(-45);
                e.Graphics.Transform = M2;
            e.Graphics.DrawEllipse(new Pen(Color.Red, 3), 40, 85, 170, 60);
            e.Graphics.DrawString("행렬 변환", new Font("궁서", 25), 
                Brushes.Blue, 50, 100);
        }
    }
}