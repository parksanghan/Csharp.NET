using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ImageViewer
{
    public partial class Form1 : Form
    {
        private Bitmap m_OriginalBmp;   // 원본 이미지
        private bool m_breadBmp;      // 이미지 읽기 확인 
        private Bitmap m_SmallBmp;      // 오른쪽에 출력될 이미지  
        private Rectangle m_region;     // 마우스가 가리키는 영역
        private bool m_bMouseDown;    // 마우스 버튼이 눌렸는지 유무
        private int m_Ratio;         // 이미지 확대/축소 비율
	
        public Form1()
        {
            InitializeComponent();

            // 초기화 
            this.m_SmallBmp = new Bitmap(this.m_SmallPanel.Width, this.m_SmallPanel.Height);
            this.m_Ratio = 0;
        }

        // 파일 열기 메뉴 선택
        private void menuItem_FileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = "C:\\";
            fd.Filter = "Image Files | *.JPG;*.GIF;*.PNG;*.TIF;*.BMP;*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.m_OriginalBmp = new Bitmap(fd.FileName);
                this.m_breadBmp = true;
                m_region = new Rectangle(this.m_OriginalPanel.Width / 2 - 100, this.m_OriginalPanel.Height / 2 - 100, 100, 100);
                this.m_OriginalPanel.Invalidate();
            }
        }

        // 종료 메뉴 선택
        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            m_SmallBmp.Dispose();
            m_OriginalBmp.Dispose();
            this.Close();	
        }
        
        // 왼쪽 Panel 영역의 Paint 이벤트(마우스 드레그 영역에 노란색 점선 그리기)
        private void m_OriginalPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.m_breadBmp)
            {
                Graphics g = e.Graphics;
                g.FillRectangle(Brushes.White, this.m_OriginalPanel.ClientRectangle);
                Rectangle rect = new Rectangle(0, 0, this.m_OriginalBmp.Width, this.m_OriginalBmp.Height);
                g.DrawImage(this.m_OriginalBmp, rect);

                // 노란색 점선 그리기 
                Pen pen = new Pen(Brushes.Gold, 3);
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, m_region);
                pen.Dispose();

            }
        }

        // 왼쪽 Panel에서 MouseDown이벤트 처리 
        private void m_OriginalPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_bMouseDown = true;
        }

        // 왼쪽 Panel에서 MouseDown이벤트 처리 
        private void m_OriginalPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.m_bMouseDown = false;
        }

        // 왼쪽 Panel에서 MouseMove이벤트 처리 
        private void m_OriginalPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_bMouseDown)
            {
                m_region.X = e.X;
                m_region.Y = e.Y;

                this.m_SmallBmp.Dispose();

                // 오른쪽 패널의 새로운 비트맵 생성 및 그리기 
                this.m_SmallBmp = new Bitmap(this.m_SmallPanel.Width, this.m_SmallPanel.Height);
                Graphics gi = Graphics.FromImage(this.m_SmallBmp);
                gi.DrawImage(this.m_OriginalBmp, this.m_SmallPanel.ClientRectangle, this.m_region, GraphicsUnit.Pixel);
                gi.Dispose();

                // 왼쪽 , 오른쪽 패널 갱신
                this.m_OriginalPanel.Invalidate();
                this.m_SmallPanel.Invalidate();
            }
        }
     
        // 오른쪽 패널의 Paint이벤트 처리 
        private void m_SmallPanel_Paint(object sender, PaintEventArgs e)
        {
            // 읽어 들인 이미지 확인
            if (this.m_breadBmp)
            {
                Graphics g = e.Graphics;

                // 패널을 힌색으로 칠함
                g.FillRectangle(Brushes.White, this.m_SmallPanel.ClientRectangle);
                // 왼쪽 패널에서 설정한 이미지 출력 
                g.DrawImage(this.m_SmallBmp, 0, 0);
            }

        }


        // 오른쪽 패널에 출력할 이미지 크기 조정
        private void ResizeSmallImage()
        {
            // 이미지 배율 
            float ratio = (float)(1.0 + this.m_Ratio * .25);
            int w = (int)(this.m_SmallPanel.Width * ratio);
            int h = (int)(this.m_SmallPanel.Height * ratio);

            Rectangle rect = new Rectangle(0, 0, w, h);

            Bitmap tempBmp = new Bitmap(w, h);

            Graphics gi = Graphics.FromImage(tempBmp);
            gi.DrawImage(this.m_SmallBmp, rect);

            this.m_SmallBmp.Dispose();
            this.m_SmallBmp = new Bitmap(w, h);
            this.m_SmallBmp = tempBmp.Clone(rect, tempBmp.PixelFormat);

            gi.Dispose();
            tempBmp.Dispose();

            this.m_SmallPanel.Invalidate();
        }     

        // + 버튼 클릭 이벤트 
        private void btn_Plus_Click(object sender, EventArgs e)
        {
            if (this.m_Ratio < 3)
            {
                this.m_Ratio++;
                this.ResizeSmallImage();
            }
        }

        // - 버튼 클릭 이벤트 
        private void btn_Minus_Click(object sender, EventArgs e)
        {
            if (this.m_Ratio > -3)
            {
                this.m_Ratio--;
                this.ResizeSmallImage();
            }	
        }

       

    }
}