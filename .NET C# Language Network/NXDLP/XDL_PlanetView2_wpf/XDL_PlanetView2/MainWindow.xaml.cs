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
using Microsoft.Win32;

using Pixoneer.NXDL;
using Pixoneer.NXDL.NGR;
using Pixoneer.NXDL.NXPlanet;
using Pixoneer.NXDL.NCC;

namespace XDL_PlanetView2
{
    public partial class MainWindow : Window
    {
        bool userMeasure = false;
        int countPos = 0;
        XGeoPoint posAngle0, posAngle1, posAngle2;

        public MainWindow()
        {
            InitializeComponent();
            nxPlanetView2D.BackColor = System.Drawing.Color.Black;
            nxPlanetView3D.BackColor = System.Drawing.Color.Black;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            // 카메라 위치 설정
            nxPlanetView2D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));
            nxPlanetView3D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0), XAngle.FromDegree(-90.0), XAngle.FromDegree(0.0));

            nxPlanetView3D.ToolboxDistUnit = NXPlanetView.eToolboxDistUnit.Mile; // 거리 측정 단위
            nxPlanetView3D.ToolboxAreaUnit = NXPlanetView.eToolboxAreaUnit.SquareKiloMeter; // 면적 측정 단위

            nxPlanetView2D.RefreshScreen();
            nxPlanetView3D.RefreshScreen();

            userMeasure = false;
            countPos = 0;
        }

        private void Measure2DLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점을 이용한 거리 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure2DPolyLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 다중 점을 이용한 거리 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;
        }

        private void Measure2DAreaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 다중 점을 이용한 면적 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;
        }

        private void Measure2DAngleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점으로 이뤄진 벡터와 진북방향과의 각도 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure2DAngle3PtMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 세 점으로 이루어진 각도 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }

        private void Measure2DCircleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점의 거리를 반지름으로 하는 원형 측정
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;
        }

        private void Measure3DLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점을 이용한 거리 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure3DPolyLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 다중 점을 이용한 거리 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;
        }

        private void Measure3DAreaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 다중 점을 이용한 면적 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;
        }

        private void Measure3DAngleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점으로 이뤄진 벡터와 진북방향과의 각도 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure3DAngle3PtMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 세 점으로 이루어진 각도 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }

        private void Measure3DCircleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 두 점의 거리를 반지름으로 하는 원형 측정
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;
        }

        private void UserDefinedMeasureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            userMeasure = !userMeasure;
            countPos = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Xfn.Close();
        }

        private bool nxPlanetLayer2D_OnRender(object sender, NXPlanetDrawArgs e)
        {
            if (!userMeasure) return false;
            if (countPos > 1)
            {
                e.Graphics.glDisable(XGraphics.GL_DEPTH_TEST);
                e.Graphics.glEnable(XGraphics.GL_BLEND);
                e.Graphics.glBlendFunc(XGraphics.GL_SRC_ALPHA, XGraphics.GL_ONE_MINUS_SRC_ALPHA);

                e.Graphics.glPushMatrix();

                e.Graphics.glColor3f(1.0f, 0.0f, 0.0f);
                e.Graphics.glLineWidth(3);
                e.Graphics.glBegin(XGraphics.GL_LINE_STRIP);

                XVertex3d posWorld = nxPlanetView2D.GeographicToWorld(posAngle0);
                e.Graphics.glVertex3d(posWorld - e.WOS);
                posWorld = nxPlanetView2D.GeographicToWorld(posAngle1);
                e.Graphics.glVertex3d(posWorld - e.WOS);

                if (countPos >= 3)
                {
                    posWorld = nxPlanetView2D.GeographicToWorld(posAngle2);
                    e.Graphics.glVertex3d(posWorld - e.WOS);
                }

                e.Graphics.glEnd();
                e.Graphics.glColor3f(1.0f, 1.0f, 1.0f);
                e.Graphics.glPopMatrix();
                e.Graphics.glEnable(XGraphics.GL_DEPTH_TEST);
            }
            return default(bool);
        }

        private bool nxPlanetLayer2D_OnWndProc(object sender, NXPlanetDrawArgs e, ref System.Windows.Forms.Message m)
        {
            if (m.Msg == XWndMsg.XWM_LBUTTONDOWN)
            {
                if (userMeasure)
                {
                    double x = XWndMsg.GetLowValue(m.LParam);
                    double y = XWndMsg.GetHighValue(m.LParam);

                    XGeoPoint gpPos = nxPlanetView2D.ScreenToGeographic(x, y);
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

                        double distance = 0.0;
                        distance += Xcc.CalcGeodeticDistance(XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd), XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd));
                        distance += Xcc.CalcGeodeticDistance(XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd), XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                        double angle = 0.0;
                        angle = Xcc.CalcGeodeticAngle(XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd), XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd), XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                        System.Diagnostics.Debug.WriteLine("Distance: " + distance.ToString());
                        System.Diagnostics.Debug.WriteLine("Angle: " + angle.ToString());
                    }
                    countPos++;
                    nxPlanetView2D.RefreshScreen();
                }
            }
            return default(bool);
        }
    }
}
