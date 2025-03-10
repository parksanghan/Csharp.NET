using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using WpfApp1.ServiceReference1;
using WpfApp1.ServiceReference2;

using static WpfApp1.MainWindow;
using System.Windows.Forms;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback, IFileCallback
    {
        private PictureClient pic = new PictureClient();
        private System.Drawing.Image picImage = null;
        private IChat client = null;
        private IFile fileclient = null;
        
        public MainWindow()
        {
            InitializeComponent();
            InstanceContext site = new InstanceContext(this);
            client = new ChatClient(site);
            fileclient = new FileClient(site);

        }
        public void ServiceLoad()
        {
            this.Title = "파일서비스 연결성공";
        }

        public void ServiceClose()
        {
            System.Windows.MessageBox.Show("파일서비스 종료");
        }
        public void GetPictureACK(byte[] strFileName)
        {
            picImage = System.Drawing.Image.FromStream
               (new MemoryStream(strFileName));
            using (MemoryStream stream = new MemoryStream(strFileName))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                imagebox.Source = image; // 여기에 이미지를 표시할 WPF 컨트롤을 설정하세요.
            }

        }

        public void GetPictureListACK(string[] msg)
        {
            listbox2.Items.Clear();
            foreach (string ss in msg) {
                listbox2.Items.Add(ss);
            }
        }
        public void UploadPictureACK()
        {
            this.Title = "업로드 성공";
        }
        #region 콜백 메서드 
        public void Receive(string name, string message)
        {
            string msg = string.Format("[{0}] {1}", name, message);
            listbox1.Items.Add(msg);
        }



        public void UserEnter(string name)
        {
            string msgtemp = string.Format("{0}님이 로그인하셨습니다.", name);
            listbox1.Items.Add(msgtemp);
        }

        public void UserLeave(string name)
        {
            string msgtemp = string.Format("{0}님이 로그아웃하셨습니다.", name);
            listbox1.Items.Add(msgtemp);
        }
        #endregion

        private void joinbtn_Click(object sender, RoutedEventArgs e) // join btn
        {
            client.Join(textbox1.Text);


        }

        private void leavebtn_Click(object sender, RoutedEventArgs e) // leavebtn
        {
            client.Leave(textbox1.Text);
           
        }

        private void sendbtn_Click(object sender, RoutedEventArgs e) // send
        {
            client.Say(textbox1.Text, messagetext.Text);
        }

        private void 그림들가져오기_Click(object sender, RoutedEventArgs e)
        {
            fileclient.GetPictureList();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // 업로드
        {
            Stream readStream;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "그림파일 (*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)|*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)";
            dlg.RestoreDirectory = true;    // 현재 디렉토리를 저장해놓는다.
            if (dlg.ShowDialog() == true)
            {
                if ((readStream = dlg.OpenFile()) != null)
                {
                    byte[] bytePic;
                    BinaryReader picReader = new BinaryReader(readStream);
                    bytePic = picReader.ReadBytes(Convert.ToInt32(readStream.Length));
                    FileInfo fi = new FileInfo(dlg.FileName);
                    fileclient.UploadPicture(fi.Name, bytePic);
                }
                readStream.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // 그림하나 가져와로드 
        {
            fileclient.GetPicture(listbox2.SelectedItem.ToString());
        }

 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fileclient.FileServiceLoad();
            fileclient.GetPictureList();
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            fileclient.FileServiceClose();
        }
    }
}
