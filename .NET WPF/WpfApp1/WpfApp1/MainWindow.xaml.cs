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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Func1();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FunC2();
        }
        private void Func1()
        {
           Window1 win = new Window1();
            if (win.ShowDialog()==true)
            {
                Func1();
            }
        }
        private void FunC2()
        {
            if(Dialog.ShowDialog(null,new Window1()))
            {
                FunC2();
            }
        }
    }
}
