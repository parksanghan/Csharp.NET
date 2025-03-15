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

namespace client
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPCClient client = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
 ;        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (client.start() == true) this.Title = "연결중";
            else
            {
                this.Title = "Error";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            client.stop();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            client.senddata("select");

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            client.senddata("play");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            client.senddata("pause");

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            client.senddata("play");

        }
    }
}
