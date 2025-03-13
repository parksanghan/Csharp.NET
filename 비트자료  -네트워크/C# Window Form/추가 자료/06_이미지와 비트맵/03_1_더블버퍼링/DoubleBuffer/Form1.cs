using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleBuffer
{
    public partial class Form1 : Form
    {
        private Bitmap m_bmpBackground;

        public Form1()
        {
            DrawInitBitmap();
            InitializeComponent();
            this.Size = new Size(500, 400);
        }

        private void DrawInitBitmap()
        {
            Bitmap house = new Bitmap("house.bmp");
            Bitmap car = new Bitmap("car.jpg");
            Bitmap man = new Bitmap("man.gif");
            Bitmap table = new Bitmap("table.bmp");

            // 실제 출력할 이미지 
            m_bmpBackground = new Bitmap(500, 400);

            Font font = new Font("궁서체", 15);

            // 메모리 내부에서 이미지에 그리는 작업을 수행
            Graphics g = Graphics.FromImage(m_bmpBackground);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 500, 400));
                        
            g.DrawImage(house, new Rectangle(0,0,500,300));     // 궁전
            g.FillEllipse(Brushes.Red, new Rectangle(330, 40, 30, 30));
            g.DrawEllipse(Pens.Blue, new Rectangle(320, 30, 50, 50));
            g.DrawImage(car, 50, 310);    // 자동차 추가
            g.DrawImage(man, 30, 310);    // 사람 추가
            g.DrawImage(table, 150, 300); // 식탁 추가
            g.DrawString("더블 버퍼링 예제", font, Brushes.Black, 300, 320);

            g.Dispose();     // Bitmap 파일로 저장
            font.Dispose();
            car.Dispose();
            man.Dispose();
            table.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(this.m_bmpBackground, 0, 0);
        }
    }
}