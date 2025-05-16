using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _1107_IPC
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private PipeServer pipeServer = new PipeServer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pipeServer.Run(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            pipeServer.Stop();
        }

        public void LogPrint(string logmessage)
        {
            this.Dispatcher.Invoke(new Action(delegate () // 크로스 쓰레드 상황일때만 
            {
                
                txtBox.Text = logmessage;
                if (logmessage == "play")
                {
                    BtnSelectFile_Click(this, new RoutedEventArgs());
                }
                else if (logmessage == "pause")
                {
                    BtnPause_Click(this, new RoutedEventArgs());    
                }
                else if (logmessage == "stop")
                {
                    BtnStop_Click(this, new RoutedEventArgs());
                }
            }));
            //Dispatcher.Invoke(() =>
            //{
            //    txtBox.Text = logmessage;
            //    if (logmessage == "play")
            //    {
            //        BtnSelectFile_Click(this, new RoutedEventArgs());
            //    }
            //    else if (logmessage == "pause")
            //    {
            //        BtnPause_Click(this, new RoutedEventArgs());
            //    }
            //    else if (logmessage == "stop")
            //    {
            //        BtnStop_Click(this, new RoutedEventArgs());
            //    }
            //});
        
        }
        private void media_Opened(object sender, RoutedEventArgs e) // 동영상 열기
        {
            //슬라이더 값의 초기화
            playSlider.Minimum = 0; // 최소 재생 시간
            playSlider.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds; // 최대 재생 시간
        }

        private void media_Ended(object sender, RoutedEventArgs e) // 동영상 중지
        {
            media.Stop(); // 중지 
        }

        private void media_Failed(object sender, ExceptionRoutedEventArgs e) // 동영상 오류 
        {
            MessageBox.Show("재생 오류 : " + e.ErrorException.Message.ToString());
        }

        private void BtnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true) // 파일 지정 및 초기화
            {
                media.Source = new Uri(dlg.FileName);
                media.Volume = 0.5;
                media.SpeedRatio = 1;

                DispatcherTimer Dtimer = new DispatcherTimer() // 타이머
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                Dtimer.Tick += TimerTick; // 매 초 증감
                Dtimer.Start(); // 타이머 시작 

                media.Play(); // 동영상 재생 
            }
        }

        //미디어 파일 타임 핸들러

        void TimerTick(object sender, EventArgs e)
        {
            if (media.Source != null) // 파일 검증
            {
                if (media.NaturalDuration.HasTimeSpan) // 재생 시간 출력
                    lblPlayTime.Content = String.Format("{0} / {1}", media.Position.ToString(@"mm\:ss"), media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else // 파일 없음
                lblPlayTime.Content = "No File";


            playSlider.Value = media.Position.TotalSeconds; // 미디어 재생 시간 = 슬라이더 시간
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e) // 재생 
        {
            media.Play();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e) // 중지 
        {
            media.Stop();
        }
        private void BtnPause_Click(object sender, RoutedEventArgs e) // 정지
        {
            media.Pause();
        }
        private void PlaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void VolumeSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }
        private void VolumeSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            media.Volume = volumeSlider.Value; // 사용자 볼륨값
        }
        //플레이 슬라이더를 움직일 경우
        private void PlaySlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            media.Pause(); // 미디어 정지
        }
        //드래그 완료시
        private void PlaySlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            media.Position = TimeSpan.FromSeconds(playSlider.Value); //움직인 시간으로 값 지정
            media.Play(); // 미디어 재생
        }
    }
}
