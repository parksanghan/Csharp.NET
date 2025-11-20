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
using System.Windows.Shapes;

namespace XDL_PlanetView4
{
    /// <summary>
    /// AddPlane.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddPlane : Window
    {
        public string pathModel;  // 모델 경로
        public string pathData;   // 시뮬레이션 데이터 경고
        public string nameModel;  // 모델 이름
        public int ID;            // 모델 식별자

        // AddPlane 생성자
        public AddPlane()
        {
            InitializeComponent();
        }

        // OK 버튼 이벤트함수
        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ID = int.Parse(textBoxID.Text);
            nameModel = textBoxName.Text;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

         // 3DS 모델 파일 경로를 설정하는 buttonLoadModel 버튼 이벤트 함수
        private void buttonLoadModel_Click(object sender, RoutedEventArgs e)
        {
            // 열기 대화 상자 생성
            OpenFileDialog dlg = new OpenFileDialog();
            // 3DS 모델 파일을 선택할 수 있도록 열기 대화상자 필터 설정
            dlg.Filter = "Model FIle(*.3ds)|*.3ds;||";
            Nullable<bool> result = dlg.ShowDialog();
            if (result != true) return;

            // 모델 경로 설정
            pathModel = dlg.FileName;
            textBoxPathModel.Text = pathModel;
        }

        // 시뮬레이션 데이터 경로를 설정하는 buttonLoadData 버튼 이벤트 함수
        private void buttonLoadData_Click(object sender, RoutedEventArgs e)
        {
            // 열기 대화 상자 생성
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Data File(*.dat)|*.dat;||";
            Nullable<bool> result = dlg.ShowDialog();
            if (result != true) return;

            // 시뮬레이션 경로 설정
            pathData = dlg.FileName;
            textBoxPathData.Text = pathData;
        }
    }
}
