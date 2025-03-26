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
using _1114_Chating_client.ServiceReference1;

namespace _1114_Chating_client
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

        #region 인터페이스 메서드
        public void Receive(string name, string message)
        {
            string msg = string.Format("[Join] {0}, {1}", name, message);
            listbox.Items.Add(msg);
        }

        public void UserEnter(string name)
        {
            string msg = string.Format("[Join] {0}", name);
            listbox.Items.Add(msg);
        }

        public void UserLeave(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Join
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = name1.Text;
            client.Join(name);
        }

        //Leave
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Leave(name1.Text);
        }

        //Send
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string msg = send1.Text;
            client.Say(name1.Text, msg);
        }
    }
}
