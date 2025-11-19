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
using System.Drawing;
using Microsoft.Win32;

using Pixoneer.NXDL;
using Pixoneer.NXDL.NGR;
using Pixoneer.NXDL.NXPlanet;
using System.Windows.Forms;

namespace Window1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private XVertex2d scrPos = new XVertex2d();
        private XTextPrinter textPrinter = new XTextPrinter();
        private XTexture compassTexture = new XTexture();
        private XConfiguration config = new XConfiguration();
        public MainWindow()
        {
            InitializeComponent();
            nxPlanetView1.BackColor = System.Drawing.Color.Black;

            Font coordFont = new Font("Gulim", 12, System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Bold);
            if (!textPrinter.Initialize(coordFont))
            {
                System.Diagnostics.Debug.WriteLine("Fail to initialize text printer for coordinate display!");
            }

            if (!compassTexture.Load("C:\\Pixoneer\\XDL3.0\\Resource\\compass.png"))
            {
                System.Diagnostics.Debug.WriteLine("Fail to load compass texture!");
            }

            System.Diagnostics.Debug.WriteLine(config.BlueMarble);

            nxPlanetLayer1.OnWndProc += new NXPlanetLayerWndProcEvent(this.nxPlanetLayer1_OnWndProc);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Camera 위치 설정
            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            nxPlanetView1.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));

            // 컨트롤의 초기 설정
            checkBoxInverseMouseButton.IsChecked = nxPlanetView1.InverseMouseButton;
            checkBoxInverseMouseWheel.IsChecked = nxPlanetView1.InverseMouseWheel;
            checkBoxRotatable.IsChecked = nxPlanetView1.Rotatable;

            checkBoxShowPBP.IsChecked = nxPlanetView1.ShowPBP;
            checkBoxShowStar.IsChecked = true;
            checkBoxStatusInfo.IsChecked = nxPlanetView1.ShowStatusInfo;

            if (nxPlanetView1.GridType == NXPlanetView.eGridType.GridNone)
                comboBoxGrid.SelectedIndex = 0;
            else if (nxPlanetView1.GridType == NXPlanetView.eGridType.GridDegrees)
                comboBoxGrid.SelectedIndex = 1;
            else if (nxPlanetView1.GridType == NXPlanetView.eGridType.GridGARS)
                comboBoxGrid.SelectedIndex = 2;
            else
                comboBoxGrid.SelectedIndex = 0;

            // 화면 갱신 요청
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxInverseMouseButton_Checked(object sender, RoutedEventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면 이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환하려면 NXPlanetView의 InverseMouseButton을 true로 설정하면 된다.
            nxPlanetView1.InverseMouseButton = true;
        }

        private void checkBoxInverseMouseButton_UnChecked(object sender, RoutedEventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면 이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환 후 해제하려면 NXPlanetView의 InverseMouseButton을 false로 설정하면 된다.                     
            nxPlanetView1.InverseMouseButton = false;
        }

        private void checkBoxInverseMouseWheel_Checked(object sender, RoutedEventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면 이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환하려면 NXPlanetView의 InverseMouseButton을 true로 설정하면 된다.
            nxPlanetView1.InverseMouseWheel = true;
        }

        private void checkBoxInverseMouseWheel_Unchecked(object sender, RoutedEventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면 이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환 후 해제하려면 NXPlanetView의 InverseMouseButton을 false로 설정하면 된다.
            nxPlanetView1.InverseMouseWheel = false;
        }

        private void checkBoxRotatable_Checked(object sender, RoutedEventArgs e)
        {
            // NXPlanetView의 Rotatable 속성을 설정하려면 화면회전 여부를 설정할 수 있다.
            nxPlanetView1.Rotatable = true;
        }

        private void checkBoxRotatable_Unchecked(object sender, RoutedEventArgs e)
        {
            // NXPlanetView의 Rotatable 속성을 설정하려면 화면회전 여부를 설정할 수 있다.
            nxPlanetView1.Rotatable = false;
        }

        private void comboBoxGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxGrid.SelectedIndex == 0)  // None
            {
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridNone;
            }
            else if (comboBoxGrid.SelectedIndex == 1)  // Degree
            {
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridDegrees;
            }
            else if (comboBoxGrid.SelectedIndex == 2)  // GARS
            {
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridGARS;
            }
        }

        private void buttonScaleApply_Click(object sender, RoutedEventArgs e)
        {
            double mapAltitude = 1500000.0;
            int scaleIndex = comboBoxScale.SelectedIndex;
            if (scaleIndex == 0)        // 1000000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_1000000);
            else if (scaleIndex == 1)   // 500000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_500000);
            else if (scaleIndex == 2)   // 100000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_100000);
            else if (scaleIndex == 3)   // 50000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_50000);
            else if (scaleIndex == 4)   // 10000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_10000);

            // 현재의 Camera 정보 가져오기
            NXCameraState state = nxPlanetView1.GetCameraState();

            XGeoPoint eyePos = new XGeoPoint();
            eyePos.lond = state.lonEye.deg;
            eyePos.latd = state.latEye.deg;
            eyePos.hgt = mapAltitude;

            // 위치를 유지한 상태로 카메라의 높이만 수정하여 설정
            nxPlanetView1.SetCameraPosition(eyePos, XAngle.FromDegree(0.0));
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowPBP_Checked(object sender, RoutedEventArgs e)
        {
            // 지형 지도 위에 텍스트형태의 지명을 중첩하여 도시할 수 있는데, 이에 대한 여부를 설정한다.
            nxPlanetView1.ShowPBP = true;
            nxPlanetView1.RefreshScreen();
        }
        private void checkBoxShowPBP_Unchecked(object sender, RoutedEventArgs e)
        {
            // 지형 지도 위에 텍스트형태의 지명을 중첩하여 도시할 수 있는데, 이에 대한 여부를 설정한다.
            nxPlanetView1.ShowPBP = false;
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowStar_Checked(object sender, RoutedEventArgs e)
        {
            // 화면 축소를 해서 지도 영역 밖으로 벗어나는 경우, 배경 별을 추가도시 여부를 설정한다.
            nxPlanetView1.ShowStar = true;
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowStar_Unchecked(object sender, RoutedEventArgs e)
        {
            // 화면 축소를 해서 지도 영역 밖으로 벗어나는 경우, 배경 별을 추가도시 여부를 설정한다.
            nxPlanetView1.ShowStar = false;
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxStatusInfo_Checked(object sender, RoutedEventArgs e)
        {
            // XDL 엔진의 도시 상태 정보를 화면에 도시 여부를 설정한다.
            nxPlanetView1.ShowStatusInfo = true;
            nxPlanetView1.RefreshScreen();
        }
        private void checkBoxStatusInfo_Unchecked(object sender, RoutedEventArgs e)
        {
            // XDL 엔진의 도시 상태 정보를 화면에 도시 여부를 설정한다.
            nxPlanetView1.ShowStatusInfo = false;
            nxPlanetView1.RefreshScreen();
        }

        private bool nxPlanetLayer1_OnWndProc(object sender, NXPlanetDrawArgs e, ref System.Windows.Forms.Message m)
        {
            if (m.Msg == Pixoneer.NXDL.XWndMsg.XWM_MOUSEMOVE)
            {
                scrPos.x = Pixoneer.NXDL.XWndMsg.GetLowValue(m.LParam);
                scrPos.y = Pixoneer.NXDL.XWndMsg.GetHighValue(m.LParam);
                nxPlanetView1.RefreshScreen();
            }
            return default(bool);
        }

        private bool nxPlanetLayer1_OnOrthoRender(object sender, NXPlanetDrawArgs e)
        {
            if (nxPlanetView1 == null) return false;

            XVertex3d posWorld = new XVertex3d();
            // 화면 좌표를 위경도 좌표로 변환
            XGeoPoint gpPoint = nxPlanetView1.ScreenToGeographic(scrPos.x, scrPos.y);
            posWorld.x = scrPos.x;
            posWorld.y = scrPos.y;
            posWorld.z = 0.0;

            String str = gpPoint.lond.ToString() + ", " + gpPoint.latd.ToString();

            // 화면에 텍스트 좌표를 도시
            bool result = textPrinter.Print(str, posWorld, Pixoneer.NXDL.NGR.eTextAlign.Align_Center, System.Drawing.Color.White, true, System.Drawing.Color.DarkBlue);

            // 나침반 도시
            if (!compassTexture.SendTextureToDevice()) return false;

            NXCameraState state = nxPlanetView1.GetCameraState();

            int nXSize = compassTexture.Width;
            int nYSize = compassTexture.Height;

            e.Graphics.glDisable(XGraphics.GL_DEPTH_TEST);
            e.Graphics.glEnable(XGraphics.GL_BLEND);

            e.Graphics.glBindTexture(XGraphics.GL_TEXTURE_2D, (uint)compassTexture.GLTextureID);
            e.Graphics.glColor3f(1.0f, 1.0f, 1.0f);

            e.Graphics.glPushMatrix();
            e.Graphics.glTranslated(100.0, 100.0);
            e.Graphics.glRotated(-state.azimuth.deg, 0.0, 0.0, 1.0);

            e.Graphics.glBegin(XGraphics.GL_QUADS);
            e.Graphics.glTexCoord2f(0, 1); e.Graphics.glVertex3d(-nXSize / 2, -nYSize / 2, 0);
            e.Graphics.glTexCoord2f(0, 0); e.Graphics.glVertex3d(-nXSize / 2, nYSize / 2, 0);

            e.Graphics.glTexCoord2f(1, 0); e.Graphics.glVertex3d(nXSize / 2, nYSize / 2, 0);
            e.Graphics.glTexCoord2f(1, 1); e.Graphics.glVertex3d(nXSize / 2, -nYSize / 2, 0);
            e.Graphics.glEnd();

            e.Graphics.glPopMatrix();

            e.Graphics.glDisable(XGraphics.GL_BLEND);
            e.Graphics.glEnable(XGraphics.GL_DEPTH_TEST);

            return default(bool);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Xfn.Close();
        }
    }
}
