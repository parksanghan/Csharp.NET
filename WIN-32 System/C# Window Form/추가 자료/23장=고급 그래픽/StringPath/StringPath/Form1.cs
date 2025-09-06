using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StringPath
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
            Path.AddString("대한민국", new FontFamily("궁서"), 0, 60, 
                new Point(10, 10), new StringFormat());

            //* 외곽선 그리기
            e.Graphics.DrawPath(Pens.Black, Path);
            //*/

            /* 채우기
            e.Graphics.FillPath(Brushes.Blue, Path);
            //*/

            /* PathPoints로 순회하기
            PointF[] arP = Path.PathPoints;
            Text = Path.PointCount.ToString();
            for (int i = 1; i < arP.Length; i++)
            {
                if (Path.PathTypes[i] != (byte)PathPointType.Start)
                {
                    e.Graphics.DrawLine(Pens.Black, arP[i - 1], arP[i]);
                }
                System.Threading.Thread.Sleep(1);
            }
            //*/

            /* Start에 대한 타입 점검을 뺐을 때
            PointF[] arP = Path.PathPoints;
            for (int i = 1; i < arP.Length; i++)
            {
                e.Graphics.DrawLine(Pens.Black, arP[i - 1], arP[i]);
                System.Threading.Thread.Sleep(1);
            }
            //*/

            /* PathData로 순회하기
            PathData pd = Path.PathData;
            Text = pd.Points.Length.ToString();
            for (int i = 0 ; i <  pd.Points.Length; i++)
            {
                if (pd.Types[i] != (byte)PathPointType.Start)
                {
                    e.Graphics.DrawLine(Pens.Black, pd.Points[i-1], pd.Points[i]);
                }
                System.Threading.Thread.Sleep(1);
            }
            //*/
        }
    }
}