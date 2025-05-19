using Example.Window1;
using System.Windows;
using System.Windows.Controls;

namespace _1107_WPF
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        Person per = null;
        Person per1 = null;
        public Window2()
        {
            InitializeComponent();
            per = (Person)FindResource("person");
            per1 = (Person)FindResource("person2"); 
            Validation.AddErrorHandler(age, age_ValidationError);
            Validation.AddErrorHandler(name, name_ValidationError);
        }
        void age_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control control = sender as Control;
            control.ToolTip = (string)e.Error.ErrorContent;
        }
        void name_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control control = sender as Control;
            control.ToolTip = (string)e.Error.ErrorContent;
        }


        private void eraseButton_Click_1(object sender, RoutedEventArgs e)
        {
            per1.Name = "";
            per1.Phone = "";
            per1.Age = null;
            per1.Male = null;

            per.Name = "";
            per.Phone = "";
            per.Age = null;
            per.Male = null;
        }

       
    }
}
