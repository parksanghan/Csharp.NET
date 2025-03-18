using System;
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
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback
    {
        private ChatClient client = null;

        public MainWindow()
        {
            InitializeComponent();

            InstanceContext site = new InstanceContext(this);
            client = new ChatClient(site);
        }

        public void Receive(string name, string message)
        {
            string msg = string.Format("[{0}] {1}", name, message);
            ListBox.Items.Add(msg);
        }

        public void UserEnter(string name)
        {
            string msgtemp = string.Format("{0}님이 로그인하셨습니다.", name);
            ListBox.Items.Add(msgtemp);
        }

        public void UserLeave(string name)
        {
            string msgtemp = string.Format("{0}님이 로그아웃하셨습니다.", name);
            ListBox.Items.Add(msgtemp);
        }

        #region 인터페이스
        #endregion


        //Join
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.Join(Name.Text);
        }

        //Leave
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Leave(Name.Text);
        }

        //Say
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            client.Say(Name.Text, Send.Text);
        }
    }
}
