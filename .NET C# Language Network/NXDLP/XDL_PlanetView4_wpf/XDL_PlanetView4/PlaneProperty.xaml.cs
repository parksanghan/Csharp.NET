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

using Pixoneer.NXDL;
using Pixoneer.NXDL.NNCW;
using System.Windows.Threading;

namespace XDL_PlanetView4
{
    ///// <summary>
    /// PlaneProperty.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlaneProperty : Window
    {
        public string modelName;    // 모델 이름
        public int modelID;

        public double modelScaleX, modelScaleY, modelScaleZ;    // 모델 크기 조절 요소
        public bool modelScalable;                              // 모델 크기 조절 여부
        public XncwObserver.eViewMode cameraMode;               // 카메라 모드
        public bool modelShowBoundingBox;                       // 모델 경계 영역 도시 여부

        public PlaneProperty()
        {
            InitializeComponent();

            modelName = "";
            modelID = 0;
            modelScaleX = modelScaleY = modelScaleZ = 2.0;
            modelScalable = false;
            cameraMode = XncwObserver.eViewMode.Unusable;
            modelShowBoundingBox = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxID.Text = modelID.ToString();
            textBoxName.Text = modelName;

            textBoxScaleX.Text = modelScaleX.ToString();
            textBoxScaleY.Text = modelScaleY.ToString();
            textBoxScaleZ.Text = modelScaleZ.ToString();

            checkBoxScalable.IsChecked = modelScalable;
            checkBoxShowBoundingBox.IsChecked = modelShowBoundingBox;

            int idx = (int)cameraMode;

            // Camera mode가 유효한 값이 아닌 경우, Camera mode를 위한 combo box의 마지막 아이템인 Unusable로 설정
            if (idx < 0 || idx >= comboBoxCameraMode.Items.Count)
                idx = comboBoxCameraMode.Items.Count - 1;

            comboBoxCameraMode.SelectedIndex = idx;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            int idx = comboBoxCameraMode.SelectedIndex;

            // Camera mode가 유효하지 않는 경우에는 Inusable으로 설정
            if (idx > (int)XncwObserver.eViewMode.Far2Obj)
                idx = (int)XncwObserver.eViewMode.Unusable;

            cameraMode = (XncwObserver.eViewMode)idx;
            modelName = textBoxName.Text;

            // 모델 크기 조절. ModelScalable이 true인 경우 적용된다.
            modelScaleX = double.Parse(textBoxScaleX.Text);
            modelScaleY = double.Parse(textBoxScaleY.Text);
            modelScaleZ = double.Parse(textBoxScaleZ.Text);

            Nullable<bool> result = checkBoxScalable.IsChecked;
            if (result != true)
            {
                modelScalable = false;
            }
            else
            {
                modelScalable = true;
            }

            Nullable<bool> result1 = checkBoxShowBoundingBox.IsChecked;
            if (result1 != true)
            {
                modelShowBoundingBox = false;
            }
            else
            {
                modelShowBoundingBox = true;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
