using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageCreateAndConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 비트맵 이미지 생성
            Bitmap bmp1 = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            Bitmap bmp2 = new Bitmap(200, 200, PixelFormat.Format32bppArgb);
            
            Graphics g1 = Graphics.FromImage(bmp1);
            Graphics g2 = Graphics.FromImage(bmp2);
            
            // 비트맵에 그리기 
            g1.FillRectangle(Brushes.Red, new Rectangle(0, 0, 100, 100));
            
            g2.DrawString("32비트 알파,RGB", this.Font, Brushes.Black, 10, 10);
            g2.DrawEllipse(new Pen(Color.FromArgb(255, 255, 0, 0), 40), new Rectangle(50, 50, 50, 50));
            g2.DrawEllipse(new Pen(Color.FromArgb(150, 0, 255, 0), 40), new Rectangle(100, 50, 50, 50));
            g2.DrawEllipse(new Pen(Color.FromArgb(50, 0, 0, 255), 40), new Rectangle(75, 75, 50, 50));

            g1.Dispose();
            g2.Dispose();

            // 폼 생성 배경 이미지로 출력
            Form form1 = new Form();
            form1.Size = new Size(100, 100);
            form1.BackgroundImage = bmp1;
            form1.Show();

            Form form2 = new Form();
            form2.Size = new Size(200, 200);
            form2.BackgroundImage = bmp2;
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 
            Bitmap bmp1 = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            Bitmap bmp2 = new Bitmap(100, 100, PixelFormat.Format16bppGrayScale);

            //Format32bppArgb : 알파8비트, 적8비트, 녹8비트 청8비트
            Bitmap bmp3 = new Bitmap(100, 100, PixelFormat.Format32bppArgb);

            bool b_alpha_1 = ((bmp1.PixelFormat & PixelFormat.Alpha) != 0);
            bool b_alpha_2 = ((bmp2.PixelFormat & PixelFormat.Alpha) != 0);
            bool b_alpha_3 = ((bmp3.PixelFormat & PixelFormat.Alpha) != 0);

            string str1 = String.Format("bmp1 이미지 Alpha ={0}", (b_alpha_1)?"포함":"포함안됨");
            string str2 = String.Format("bmp2 이미지 Alpha ={0}", (b_alpha_2)?"포함":"포함안됨");
            string str3 = String.Format("bmp3 이미지 Alpha ={0}", (b_alpha_3)?"포함":"포함안됨");

            MessageBox.Show(str1+"\n"+str2+"\n"+str3, "Alpha 성분 유무");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // 이미지 변환
                Image img = Image.FromFile("event.gif");    // gif 형식 리딩
                img.Save("event_bmp.bmp", ImageFormat.Bmp); // bmp 형식 변환
                img.Save("event_jpg.jpg", ImageFormat.Jpeg);// jpeg형식 변환
                img.Save("event_png.png", ImageFormat.Png); // png 형식 변환
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "에러발생");
            }
        }
    }

}