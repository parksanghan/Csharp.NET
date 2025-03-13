using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_WindowForm1
{
    public partial class _03_Graphics : Form
    {
        public _03_Graphics()
        {
            InitializeComponent();            
        }

        //1. Paint이벤트 핸들러
        private void _03_Graphics_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //100*100
            g.DrawRectangle(new Pen(Color.Black), 210, 10, 100, 100);
            g.FillRectangle(new SolidBrush(Color.Aqua), 210+1, 10+1, 99, 99);
        }

        //2. override OnPaint 
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //100*100
            g.DrawRectangle(new Pen(Color.Black), 310, 10, 100, 100);
            g.FillRectangle(new SolidBrush(Color.Aqua), 310 + 1, 10 + 1, 99, 99);
        }

        //3. Graphics 객체 생성(부모윈도우) --> 소멸처리 반드시 필요
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
           
            g.DrawRectangle(new Pen(Color.Black), 310, 10, 100, 100);
            g.FillRectangle(new SolidBrush(Color.Red), 310 + 1, 10 + 1, 99, 99);

            g.Dispose();
        }

        //4. Graphics 객체 생성(자식윈도우) --> 소멸처리 반드시 필요
        private void button2_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                Rectangle rect = pictureBox1.ClientRectangle;
                g.DrawRectangle(new Pen(Color.Black), rect);
                g.FillRectangle(new SolidBrush(Color.Red), rect);
            } //g.Dispose() 자동호출!
        }

        //5. 사용자 그리기
        private void button3_Click(object sender, EventArgs e)
        {
            //리스트박스의 속성창
            listBox1.Items.Add("빨강");
            listBox1.Items.Add("녹색");
            listBox1.Items.Add("파랑");

            //리스트박스의 속성창
            listBox1.DrawItem += new DrawItemEventHandler(GDIExam_DrawItem);
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        // ListBox가 다시 그려질 때마다 호출됨(데이터 갯수만큼 호출됨)
        private void GDIExam_DrawItem(object sender, DrawItemEventArgs e)
        {         
            Brush brush = Brushes.Black;
            switch (e.Index)
            {
                case 0:   brush = Brushes.Black;     break;                    
                case 1:   brush = Brushes.Yellow;    break;
                case 2:   brush = Brushes.Red;       break;
                case 3:   brush = Brushes.Green;     break;
                case 4:   brush = Brushes.Blue;      break;
            }

            Graphics g = e.Graphics;
            g.DrawString(listBox1.Items[e.Index].ToString(),
                          e.Font, brush, e.Bounds, StringFormat.GenericDefault);

            Console.WriteLine("{0} : DrawItem 이벤트 실행", e.ToString());
        }

        //6. 이미지 출력(원본출력)
        private void button4_Click(object sender, EventArgs e)
        {
            Image imageFile = Image.FromFile("ocean.jpg");
            
            Graphics grfx = this.CreateGraphics();
            grfx.DrawImage(imageFile, 800, 0);
            grfx.Dispose();


            //Graphics grfx = Graphics.FromImage(imageFile);
            //Font font = new Font("돋음", 20);
            //Brush brush = Brushes.Pink;

            //grfx.DrawString("이미지에 글자쓰기", font, brush, 10, 10);
            //grfx.Dispose();

            //imageFile.Save("ocean.gif", System.Drawing.Imaging.ImageFormat.Gif);

            //this.image = Image.FromFile("ocean.gif");
            //this.Invalidate(this.ClientRectangle);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Image imageFile = Image.FromFile("ocean.jpg");

            //이미지에 글자 출력
            using (Graphics g = Graphics.FromImage(imageFile))
            {
                g.DrawString("이미지에 글자쓰기",
                    new Font("돋음", 20), Brushes.Pink, 10, 10);
            }

            //새로운 파일로 저장(확장자 자유롭게)
            imageFile.Save("ocean.gif", System.Drawing.Imaging.ImageFormat.Gif);


            //출력
            Image imageFile1 = Image.FromFile("ocean.gif");

            Graphics grfx = this.CreateGraphics();
            grfx.DrawImage(imageFile1, 800, 300);
            grfx.Dispose();
        }
    }
}
