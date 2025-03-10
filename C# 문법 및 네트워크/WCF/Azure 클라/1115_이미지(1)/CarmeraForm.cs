using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace _1115_이미지_1_
{
    public partial class CarmeraForm : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        Control con = Control.singleton;

        public CarmeraForm()
        {
            InitializeComponent();
        }

        #region 비디오 표시 이벤트
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // 비디오 프레임을 PictureBox에 표시
            pictureBox1.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
        }
        #endregion

        #region 저장 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    // Capture the image
                    Bitmap capturedImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.DrawToBitmap(capturedImage, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                    string filename = @"C:\Picture\" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    capturedImage.Save(filename, ImageFormat.Png);
                    MessageBox.Show("이미지가 저장되었습니다.");

                    Action updateText = () => { con.fm.textBox1.Text = filename; };
                    con.fm.textBox1.Invoke(updateText);

                    Action updatePic = () => { con.fm.pictureBox1.ImageLocation = filename; };
                    con.fm.pictureBox1.Invoke(updatePic);
                }
                else
                {
                    MessageBox.Show("캡처할 이미지가 없습니다.");
                }

            }
            catch (Exception ex) {MessageBox.Show(ex.Message); }    
        }
        #endregion

        #region Load, Closing
        private void CarmeraForm_Load(object sender, EventArgs e)
        {
            // 사용 가능한 비디오 디바이스 목록 가져오기
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                // 첫 번째 비디오 디바이스 선택
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;

                // 비디오 소스 시작
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("사용 가능한 비디오 디바이스를 찾을 수 없습니다.");
            }
        }

        private void CarmeraForm_FormClosing(object sender, FormClosingEventArgs e)
        {  // 폼이 닫힐 때 비디오 소스 정리
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }
        #endregion
    }
}
