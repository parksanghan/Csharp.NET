using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pixoneer.NXDL;
using Pixoneer.NXDL.NGR;
using Pixoneer.NXDL.NXPlanet;
using Pixoneer.NXDL.NCC;

namespace XDL_PlanetView2
{
    public partial class Form1 : Form
    {
        bool userMeasure = false;
        int countPos = 0;
        XGeoPoint posAngle0, posAngle1, posAngle2;    // index 0 : start, index 1 : center, index 2 : end
        public Form1()
        {
            InitializeComponent();
            nxPlanetView2D.BackColor = Color.Black;
            nxPlanetView3D.BackColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            // Planet2D 모드의 camera 위치 설정
            nxPlanetView2D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));
            // Planet3D 모드의 camera 위치 설정
            nxPlanetView3D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0), XAngle.FromDegree(-90.0), XAngle.FromDegree(0.0));

            // PlanetView의 측정 단위 설정
            // 거리 측정 단위
            nxPlanetView3D.ToolboxDistUnit = NXPlanetView.eToolboxDistUnit.Mile;
            // 면적 측정 단위
            nxPlanetView3D.ToolboxAreaUnit = NXPlanetView.eToolboxAreaUnit.SquareKiloMeter;
         
            nxPlanetView2D.RefreshScreen();
            nxPlanetView3D.RefreshScreen();

            // 사용자 정의 측정을 위한 변수 초기화
            userMeasure = false;
            countPos = 0;
        }

        private void Measure2DLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure2DPolyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;
        }

        private void Measure2DAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;
        }

        private void Measure2DAngleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure2DAngle3PtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }

        private void Measure2DCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView2D.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;
        }

        private void Measure3DLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.DistanceMeasurer;
        }

        private void Measure3DPolyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.PathMeasurer;
        }

        private void Measure3DAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AreaMeasurer;
        }

        private void Measure3DAngleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer;
        }

        private void Measure3DAngle3PtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.AngleMeasurer2;
        }

        private void Measure3DCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nxPlanetView3D.ToolboxMode = NXPlanetView.eToolboxMode.CircleMeasurer;
        }

        private void UserDefinedMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userMeasure = !userMeasure;
            countPos = 0;
        }

        private bool nxPlanetLayer2D_OnWndProc(object sender, NXPlanetDrawArgs e, ref Message m)
        {
            if (m.Msg == Pixoneer.NXDL.XWndMsg.XWM_LBUTTONDOWN)
            {
                if (userMeasure)
                {
                    double x = Pixoneer.NXDL.XWndMsg.GetLowValue(m.LParam);
                    double y = Pixoneer.NXDL.XWndMsg.GetHighValue(m.LParam);

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

                        // 두 선분의 길이와 3 점으로 이루어지는 각도 계산
                        double distance = 0.0;
                        distance += Xcc.CalcGeodeticDistance(XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd),
                            XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd));
                        distance += Xcc.CalcGeodeticDistance(XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd),
                            XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                        double angle = 0.0;
                        angle = Xcc.CalcGeodeticAngle(XAngle.FromDegree(posAngle1.lond), XAngle.FromDegree(posAngle1.latd),
                            XAngle.FromDegree(posAngle0.lond), XAngle.FromDegree(posAngle0.latd),
                            XAngle.FromDegree(posAngle2.lond), XAngle.FromDegree(posAngle2.latd));

                        System.Diagnostics.Debug.WriteLine("Distance : " + distance.ToString());
                        System.Diagnostics.Debug.WriteLine("Angle    : " + angle.ToString());
                    }

                    countPos++;

                    nxPlanetView2D.RefreshScreen();
                }
            }
            return default(bool);
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
    }
}
