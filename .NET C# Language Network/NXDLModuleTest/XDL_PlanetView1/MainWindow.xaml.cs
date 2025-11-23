using Microsoft.Win32;
using Pixoneer.NXDL;
using Pixoneer.NXDL.NCC;
using Pixoneer.NXDL.NGR;
using Pixoneer.NXDL.NSCENE;
using Pixoneer.NXDL.NXPlanet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 
using WPF.Lib;

namespace XDL_PlanetView1
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
        private bool userMeasuring = false;
        NXPlanetLayerSceneDisplay nxPlanetLayerDisplay;
        NXPlanetLayerSceneEditor nxPlanetLayerEditor;  
        NXPlanetLayerVectorEditor nxPlanetVectorEditor;
        int countPos = 0;
        XGeoPoint posAngle0, posAngle1, posAngle2;
        public MainWindow()
        {
            InitializeComponent();
            DeleteEvents();
            InitEvents();   
            nxPlanetView1.BackColor = System.Drawing.Color.Black;
            countPos = 0;
            userMeasuring = false;
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

            nxPlanetLayerDisplay= new NXPlanetLayerSceneDisplay();
            nxPlanetLayerEditor = new  NXPlanetLayerSceneEditor();
            nxPlanetVectorEditor = new NXPlanetLayerVectorEditor();
              
             

            nxPlanetLayerDisplay.AttachTo(nxPlanetView1);
            nxPlanetLayerEditor.AttachTo(nxPlanetView1);
            nxPlanetLayer1.OnWndProc += new NXPlanetLayerWndProcEvent(this.nxPlanetLayer1_OnWndProc);
            nxPlanetLayerEditor.OnObjectCreated += new NXSCEditEvent(NxPlanetLayerEditor_OnObjectCreated);

           
            nxPlanetView1.RefreshScreen();
            nxPlanetView1.RefreshScreen();
        }


        // 오브젝트 생성 시 트리거 되는 이벤트 
        private bool NxPlanetLayerEditor_OnObjectCreated(XscObj Obj) // 모든 객체는 
        {
            if (Obj == null) return false;
          
            HandleXscObject(Obj);
            nxPlanetView1.RefreshScreen();
            return default(bool);


        }
        private void HandleXscObject(XscObj obj )
        {
            switch (obj)
            {
                case XscPoint p:
                    CreateXscObject(p);
                    break;

                case XscPolyLine l:
                    CreateXscObject(l);
                    break;

                case XscPolygon g:
                    CreateXscObject(g);
                    break;

                case XscCircle c:
                    CreateXscObject(c);
                    break;

                case XscSymbol s:
                    CreateXscObject(s);
                    break;
            }
        }
        private void CreateXscObject(XscPoint obj)
        {

            obj.LineColor = System.Drawing.Color.Yellow;
            // Point의 크기
            obj.LineWidth = 10;
            // PointType으로는 Cross, Dot, Triangle, Rect, X가 있다.
            obj.PointType = XscPoint.ePointType.Rect;
            obj.Name = "Point #" + obj.ObjID.ToString();
        }
        private void CreateXscObject(XscPolyLine obj)
        {

            obj.LineColor = System.Drawing.Color.Yellow;
            obj.LinePattern = XscLinePattern.eLinePatternType.DashDot;
            obj.LineWidth = 3;
            obj.Name = "Polyline #" + obj.ObjID.ToString();
        }
        private void CreateXscObject(XscPolygon obj)
        {

            obj.BorderColor = System.Drawing.Color.Blue;
            obj.BorderSize = 3;
            obj.LinePattern = XscLinePattern.eLinePatternType.DashDotDot;
            obj.FillColor = System.Drawing.Color.Cyan;
            // FillPattern이 Solid가 아닌 경우에는 배경색이 투명하게 처리된다.
            obj.FillPattern = XscFillPattern.eFillPatternType.Cross;
            obj.Name = "Polygon #" + obj.ObjID.ToString();
        }
        private void CreateXscObject(XscCircle obj)
        {

            obj.LineColor = System.Drawing.Color.Coral;
            obj.LineWidth = 2;
            obj.FillColor = System.Drawing.Color.GreenYellow;
            obj.FillPattern = XscFillPattern.eFillPatternType.Vertical;
            obj.Name = "Circle #" + obj.ObjID.ToString();
        }
        private void CreateXscObject(XscSymbol obj)
        {

            // DefaultSymbolName으로 사용할 수 있는 것은 Apartment, Image, Model, Public, Stadium, TextChart가 있다.
            // 이는 XDL 엔진 설치 폴더 중 Resource\Scenes\Icons에 저장되어 있다.
            // 사용자 정의의 심볼을 로딩하고자 한다면, DefaultSymbolName 대신 UserSymbolPath 속성으로 경로를 설정한다.
            obj.UserSymbolPath = "\"C:\\Pixoneer\\XDL3.0\\Resource\\Scenes\\Icons\\Stadium.png\"";
            obj.UpdateSymbol();
            obj.Name = "Symbol #" + obj.ObjID.ToString();

        }
        public void InitEvents()    
        {
           
        }

        public void DeleteEvents()
        {
            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Camera 위치 설정
            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            nxPlanetView1.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));

            // 컨트롤의 초기 설정
            checkBoxInverseMouseButton.IsChecked = nxPlanetView1.InverseMouseButton; // 마우스 버튼으로 회전 
            checkBoxInverseMouseWheel.IsChecked = nxPlanetView1.InverseMouseWheel; //휠 반전
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
            // 로드 시엔 2D
            nxPlanetView1.ToolboxDistUnit = NXPlanetView.eToolboxDistUnit.Mile;
            nxPlanetLayerEditor.AttachTo(nxPlanetView1);
            nxPlanetLayerDisplay.AttachTo(nxPlanetView1);
            // 화면 갱신 요청
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxInverseMouseButton_Checked(object sender, RoutedEventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면 이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환하려면 NXPlanetView의 InverseMouseButton을 true로 설정하면 된다.
            nxPlanetView1.InverseMouseButton = true;
        }
        
        private void ApplyCurrentModeDistUnit()
        {
            if (nxPlanetView1.EarthMode == NXPlanetView.eEarthMode.Planet2D) nxPlanetView1.ToolboxDistUnit = NXPlanetView.eToolboxDistUnit.Mile ;
            if (nxPlanetView1.EarthMode == NXPlanetView.eEarthMode.Planet3D) nxPlanetView1.ToolboxAreaUnit = NXPlanetView.eToolboxAreaUnit.SquareYard;

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
            if (m.Msg == Pixoneer.NXDL.XWndMsg.XWM_LBUTTONDOWN && userMeasuring)
            {
                double click_x = Pixoneer.NXDL.XWndMsg.GetLowValue(m.LParam);
                double click_y = Pixoneer.NXDL.XWndMsg.GetHighValue(m.LParam);
                XGeoPoint gpPos = nxPlanetView1.ScreenToGeographic(click_x, click_y);

                if (countPos == 0)
                {
                    posAngle0 = gpPos;
                }
                else if (countPos == 1)
                {
                    posAngle1 = gpPos;
                }
                else if (countPos == 2)
                {
                    posAngle2 = gpPos;

                    // (선택) 여기서 거리/각도 계산
                    double distance = 0.0;
                    distance += Xcc.CalcGeodeticDistance(
                        XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd),
                        XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd));

                    distance += Xcc.CalcGeodeticDistance(
                        XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd),
                        XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                    double angle = Xcc.CalcGeodeticAngle(
                        XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd),
                        XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd),
                        XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                    System.Diagnostics.Debug.WriteLine("Distance: " + distance.ToString());
                    System.Diagnostics.Debug.WriteLine("Angle: " + angle.ToString());
                }

                countPos++;
                nxPlanetView1.RefreshScreen();   // 점 찍을 때마다 다시 그리기 → Draw3PointsLine에서 선 나옴
            
        }
            return default(bool);
        }
        private void DrawCurrentPosPoints(object sender, NXPlanetDrawArgs  e)
        {
            XVertex3d pos = new XVertex3d();
            XGeoPoint gpoint = nxPlanetView1.ScreenToGeographic(scrPos.x, scrPos.y);
            pos.x =  scrPos.x;  
            pos.y = scrPos.y;
            pos.z = 0.0;
            String str = gpoint.lond.ToString() + ", " + gpoint.latd.ToString();
            textPrinter.Print(str, pos, Pixoneer.NXDL.NGR.eTextAlign.Align_Center, System.Drawing.Color.White, true, System.Drawing.Color.DarkBlue);
            if (!compassTexture.SendTextureToDevice()) return;
            NXCameraState state = nxPlanetView1.GetCameraState();

            int nXSize = compassTexture.Width;
            int nYSize = compassTexture.Height;

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
        }
        private void Draw3PointsLine(object sender ,NXPlanetDrawArgs e)
        {
            if (!userMeasuring) return;
            if (countPos > 1)
            {
                e.Graphics.glDisable(XGraphics.GL_DEPTH_TEST);
                e.Graphics.glEnable(XGraphics.GL_BLEND);
                e.Graphics.glBlendFunc(XGraphics.GL_SRC_ALPHA, XGraphics.GL_ONE_MINUS_SRC_ALPHA);

                e.Graphics.glPushMatrix();

                e.Graphics.glColor3f(1.0f, 0.0f, 0.0f);
                e.Graphics.glLineWidth(3);
                e.Graphics.glBegin(XGraphics.GL_LINE_STRIP);

                XVertex3d posWorld = nxPlanetView1.GeographicToWorld(posAngle0);
                e.Graphics.glVertex3d(posWorld - e.WOS);
                posWorld = nxPlanetView1.GeographicToWorld(posAngle1);
                e.Graphics.glVertex3d(posWorld - e.WOS);

                if (countPos >= 3)
                {
                    posWorld = nxPlanetView1.GeographicToWorld(posAngle2);
                    e.Graphics.glVertex3d(posWorld - e.WOS);
                }

                e.Graphics.glEnd();
                e.Graphics.glColor3f(1.0f, 1.0f, 1.0f);
                e.Graphics.glPopMatrix();
                e.Graphics.glEnable(XGraphics.GL_DEPTH_TEST);
            }
        }
        private bool nxPlanetLayer1_OnOrthoRender(object sender, NXPlanetDrawArgs e)
        {

           
            if (nxPlanetView1 == null) return false; // 컨트롤 초기화 시
            if (userMeasuring)
            {
                Draw3PointsLine(sender, e);
            }
            else if (!userMeasuring)
            {
                // 사용자 긋기 상태가 아닌경우 CurrentPos 호출 
                DrawCurrentPosPoints(sender, e);
            }
                //XVertex3d posWorld = new XVertex3d();
                // 화면 좌표를 위경도 좌표로 변환
                //XGeoPoint gpPoint = nxPlanetView1.ScreenToGeographic(scrPos.x, scrPos.y);
                //posWorld.x = scrPos.x;
                //posWorld.y = scrPos.y;
                //posWorld.z = 0.0;

                //String str = gpPoint.lond.ToString() + ", " + gpPoint.latd.ToString();

                //// 화면에 텍스트 좌표를 도시
                //bool result = textPrinter.Print(str, posWorld, Pixoneer.NXDL.NGR.eTextAlign.Align_Center, System.Drawing.Color.White, true, System.Drawing.Color.DarkBlue);

                //// 나침반 도시
                //if (!compassTexture.SendTextureToDevice()) return false;

                //NXCameraState state = nxPlanetView1.GetCameraState();

                //int nXSize = compassTexture.Width;
                //int nYSize = compassTexture.Height;

                //e.Graphics.glEnable(XGraphics.GL_BLEND);

                //e.Graphics.glBindTexture(XGraphics.GL_TEXTURE_2D, (uint)compassTexture.GLTextureID);
                //e.Graphics.glColor3f(1.0f, 1.0f, 1.0f);

                //e.Graphics.glPushMatrix();
                //e.Graphics.glTranslated(100.0, 100.0);
                //e.Graphics.glRotated(-state.azimuth.deg, 0.0, 0.0, 1.0);

                //e.Graphics.glBegin(XGraphics.GL_QUADS);
                //e.Graphics.glTexCoord2f(0, 1); e.Graphics.glVertex3d(-nXSize / 2, -nYSize / 2, 0);
                //e.Graphics.glTexCoord2f(0, 0); e.Graphics.glVertex3d(-nXSize / 2,  nYSize / 2, 0);

                //e.Graphics.glTexCoord2f(1, 0); e.Graphics.glVertex3d(nXSize / 2, nYSize / 2, 0);
                //e.Graphics.glTexCoord2f(1, 1); e.Graphics.glVertex3d(nXSize / 2, -nYSize / 2, 0);
                //e.Graphics.glEnd();

                //e.Graphics.glPopMatrix();

                //e.Graphics.glDisable(XGraphics.GL_BLEND);
                //e.Graphics.glEnable(XGraphics.GL_DEPTH_TEST);

                return default(bool);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Xfn.Close();
        
        }
        



        private void comboBoxMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (nxPlanetView1 == null) return;
            if (comboBoxMode.SelectedIndex == 0) nxPlanetView1.EarthMode = NXPlanetView.eEarthMode.Planet2D;
            if (comboBoxMode.SelectedIndex == 1) nxPlanetView1.EarthMode = NXPlanetView.eEarthMode.Planet3D;
        }

        private void view2D_Selected(object sender, RoutedEventArgs e)
        {
            nxPlanetView1.EarthMode = NXPlanetView.eEarthMode.Planet2D;
            ApplyCurrentModeDistUnit();
        }

        private void view3D_Selected(object sender, RoutedEventArgs e)
        {
            nxPlanetView1.EarthMode = NXPlanetView.eEarthMode.Planet3D;
            ApplyCurrentModeDistUnit();
        }
        private bool Check2DMode()
        {
            bool b= nxPlanetView1.EarthMode == NXPlanetView.eEarthMode.Planet2D;
            if (b) return true;
            else
            {
                System.Windows.Forms.MessageBox.Show("invalid Mode");
                return false;
            }
        }
        private bool Check3DMode()
        {
            bool b = nxPlanetView1.EarthMode == NXPlanetView.eEarthMode.Planet3D;
            if (b) return true;
            else
            {
                System.Windows.Forms.MessageBox.Show("invalid Mode");
                return false;
            }
        }

        private void Measure2DLineToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure2DPolyLineToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;
        }

        private void Measure2DAreaToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;
        }

        private void Measure2DAngleToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure2DAngle3PtToolStripMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }
       

        private void Measure2DCircleToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check2DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;

        }

        private void Measure3DLineToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure3DPolyLineToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;

        }

        private void Measure3DAreaToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;

        }

        private void Measure3DAngleToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure3DAngle3PtToolStripMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }

        private bool nxPlanetLayer1_OnRender(object sender, NXPlanetDrawArgs e)
        {
            if (userMeasuring)
            {
                Draw3PointsLine(sender, e);
            }

            return default(bool);


        }

        private void saveMenu_Click(object sender, RoutedEventArgs e)
        {
            XScene sc_root= nxPlanetLayerEditor.GetScene();
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();   
            saveFileDialog.Filter = "XDL Scene file(*.sml)|*.sml;||"; // 포멧필터 
            if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK  ) return;

            if(XScene.SaveScene(sc_root,saveFileDialog.FileName,sc_root.SR))System.Windows.Forms.MessageBox.Show("Save Success");
            else System.Windows.Forms.MessageBox.Show("Save Fail");

        }

        private void openMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "XDL Scene file(*.sml)|*.sml;||";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // NXPlanetLayerSceneDisplay의 Open 함수를 이용하여 scene 객체 로딩
            if (nxPlanetLayerDisplay.Open(ofd.FileName))
            {
                XScene scene = nxPlanetLayerDisplay.GetScene();
                // Scene 객체 도시 방법을 추가되는 순서대로 도시하도록 설정한다.
                // 기본값은 XScene.eDisplayOrder.OrderNone이다.
                scene.DisplayOrder = XScene.eDisplayOrder.OrderByAddSequence;

                System.Windows.Forms.MessageBox.Show("PlanetView 3D의 scene 객체를 갱신하겠습니다");
                nxPlanetView1.RefreshScreen();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("scene 파일을 로딩할 수 없습니다.");
            }
        }

        private void createPointMenu_Click(object sender, RoutedEventArgs e)
        {
            nxPlanetLayerEditor.CreateNewOBJ("XscPoint");// 내부에서 이벤트 트리거함
        }

        private void createPolyLineMenu_Click(object sender, RoutedEventArgs e)
        {
            nxPlanetLayerEditor.CreateNewOBJ("XscPolyLine");// 내부에서 이벤트 트리거함
           
        }

        private void createPolygonMenu_Click(object sender, RoutedEventArgs e)
        {
            nxPlanetLayerEditor.CreateNewOBJ("XscPolygon");// 내부에서 이벤트 트리거함

        }

        private void createCircleMenu_Click(object sender, RoutedEventArgs e)
        {
            nxPlanetLayerEditor.CreateNewOBJ("XscCircle");// 내부에서 이벤트 트리거함
        }

        private void createSymbolMenu_Click(object sender, RoutedEventArgs e)
        {
            nxPlanetLayerEditor.CreateNewOBJ("XscSymbol");// 내부에서 이벤트 트리거함
        }

   

        private void Measure3DCircleToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Check3DMode()) return;
            nxPlanetView1.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;
        }

        private void measure_Dist(object sender, RoutedEventArgs e)
        {
            userMeasuring = !userMeasuring;
            countPos = 0;
        }
    }
}
