using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SineCosineExam
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;

        public Form1()
        {
            DrawSineCosine();
            this.Text = "삼각함수 그리기";				
     		this.Size = new Size(400, 300);
        }

        private void DrawSineCosine()
        {
            int x, y;

            this.bitmap = new Bitmap(400, 300);
     
     		for(x = 0; x < 400; x++)
     		{   // X 축 그리기
     	 	    this.bitmap.SetPixel(x, 100, Color.Black);
     		}
     
     		for(x = 0; x <= 360; x++)
     		{
     		    y = (int)(Math.Sin(x*Math.PI/180)*100);
     		    if(y < 0)
     			y = 100 + Math.Abs(y);
     		    else
     			y = 100 - y;
     
     		    this.bitmap.SetPixel(x, y, Color.Blue);
     		}	
     
     		for(x = 0; x <= 360; x++)
     		{
     		    y = (int)(Math.Cos(x*Math.PI/180)*100);
     		    if(y < 0)
     			y = 100 + Math.Abs(y);
     		    else
     			y = 100 - y;
     
     		    this.bitmap.SetPixel(x, y, Color.Red);
     		}	
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
          	Graphics g = pea.Graphics;
     		g.DrawImage(this.bitmap, 0, 0, this.bitmap.Width, this.bitmap.Height);

        }
    }
}