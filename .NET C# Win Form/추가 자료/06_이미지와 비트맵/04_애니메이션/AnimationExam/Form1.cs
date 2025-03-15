using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnimationExam
{
    public partial class Form1 : Form
    {
        private Bitmap[] bmp = new Bitmap[12];
        private int index;
        private bool m_Auto;
        private Timer time;

        public Form1()
        {
            InitializeComponent();
            this.index = 0;
            this.m_Auto = true;
            this.time = new Timer();
            this.time.Interval = 50;  // 0.5 초에 한번씩 호출
            this.time.Tick += new EventHandler(time_Tick);

        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            // 파일을 메모리에 읽어 들임
            for (int i = 0; i < 12; i++)
            {
                string path = String.Format("runner {0,2:d2}.bmp", i + 1);
                bmp[i] = new Bitmap(path);
            }
            // 이미지가 출력될 panel-Image
            this.panel_Image.Width = bmp[0].Width;
            // 개체의 폭과 높이를 설정
            this.panel_Image.Height = bmp[0].Height;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 프로그램이 종료될 때 Bitmap 이미지를 Dispose
            for (int i = 0; i < 12; i++)
            {
                bmp[i].Dispose();
            }
        }   
        // 애니메이션 실행 부분 
        private void btn_Animation_Click(object sender, EventArgs e)
        {
            if (this.index < 12)
            {
                this.btn_Animation.Text = "애니메이션-" + (this.index + 1);
                Graphics g = this.panel_Image.CreateGraphics();
                g.DrawImage(bmp[index], 0, 0);
                g.Dispose();
                this.index++;
            }
            else
            {
                // 인덱스 번호가 12가 되면 runner01.bmp이미지를 화면에 출력
                this.index = 0;
            }
        }

        // JPG생성
        private void btn_MakeJPG_Click(object sender, EventArgs e)
        {
            // 이미지 파일 저장은 예외가 발생될 수 있으므로 예외처리 
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    // runner01.jpg ~ runner02.jpg이름을 갖도록 설정 
                    string path = String.Format("{0}\\runner {1,2:d2}.jpg", Application.StartupPath, i + 1);
                    bmp[i].Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_AutoRepeat_Click(object sender, EventArgs e)
        {
            if (this.m_Auto)
            {
                // 자동 방법이 눌려지면 0.5초 간격으로 time_tick 메서드 호출 
                this.time.Enabled = true;
                this.m_Auto = false;
                this.btn_AutoRepeat.Text = "반복실행중...";
            }
            else
            {
                this.time.Enabled = false;
                this.m_Auto = false;
                this.btn_AutoRepeat.Text = "자동반복";
            }
        }

        // 0.05초마다 pannel_Image에 runner01 ~ runner12.bmp이미지를 그림
        private void time_Tick(object obj, EventArgs ea)
        {
            Graphics g = this.panel_Image.CreateGraphics();

            if (this.index < 12)
            {
                g.DrawImage(bmp[this.index], 0, 0);
                this.index++;
            }
            else
            {
                g.DrawImage(bmp[0], 0, 0);
                this.index = 0;
            }
            g.Dispose();
        }

    }
}