using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Example.Window1;
/*
    바인딩 처리
    1) 초기화 시점 : 데이터 > 컨트롤 동기화

    2) 컨트롤 정보 수정 > 데이터 변경[자동화]
        -WPF에서 제공

    3)데이터를 변경 > 컨트롤 변경?
        -C#에서 제공
        > 데이터가 변경된 사실을 WPF의 바인딩 시스템에 알려줘야함

 */

namespace _1107_WPF
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        private Person per = new Person() { Name = "홍길동", Phone = "010-1111-1234", Age = 20 };


        public Window1()
        {
            InitializeComponent();

            panel.DataContext = per;        //데이터 바인딩 시작!
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            per.Name = "";
            per.Phone = "";
            per.Age = null;
        }
    }
}
