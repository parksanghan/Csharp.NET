using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XDL_PlanetView4
{
    public partial class AddPlaneForm : Form
    {
        public string pathModel;    // 모델 경로
        public string pathData;     // 시뮬레이션 데이터 경고
        public string nameModel;    // 모델 이름
        public int ID;              // 모델 식별자

        // AddPlaneForm 생성자
        public AddPlaneForm()
        {
            InitializeComponent();
        }

        // 3DS 모델 파일 경로를 설정하는 buttonLoadModel 버튼 이벤트 함수
        private void buttonLoadModel_Click(object sender, EventArgs e)
        {
            // 열기 대화 상자 생성
            OpenFileDialog dlg = new OpenFileDialog();
            // 3DS 모델 파일을 선택할 수 있도록 열기 대화상자 필터 설정
            dlg.Filter = "Model File(*.3ds)|*.3ds;||";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // 모델 경로 설정
            pathModel = dlg.FileName;
            textBoxPathModel.Text = pathModel;
        }

        // 시뮬레이션 데이터 경로를 설정하는 buttonLoadData 버튼 이벤트 함수
        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            // 열기 대화 상자 생성
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Data File(*.dat)|*.dat;||";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // 시뮬레이션 경로 설정
            pathData = dlg.FileName;
            textBoxPathData.Text = pathData;
        }

        // OK 버튼 이벤트 함수
        private void buttonOK_Click(object sender, EventArgs e)
        {
            ID = int.Parse(textBoxID.Text);
            nameModel = textBoxName.Text;
        }
    }
}
