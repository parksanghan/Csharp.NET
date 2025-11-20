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

namespace XDL_PlanetView1
{
    public partial class Form1 : Form
    {
        private XVertex2d scrPos = new XVertex2d();
        private XTextPrinter textPrinter = new XTextPrinter();
        private XTexture compassTexture = new XTexture();

        public Form1()
        {
            InitializeComponent();
            nxPlanetView1.BackColor = Color.Black;

            Font coordFont = new Font("Gulim", 12, FontStyle.Regular | FontStyle.Bold);
            if (!textPrinter.Initialize(coordFont))
            {
                System.Diagnostics.Debug.WriteLine("Fail to initialize text printer for coordinate display!");
            }

            if (!compassTexture.Load("c:\\Pixoneer\\XDL3.0\\Resource\\compass.png"))
            {
                System.Diagnostics.Debug.WriteLine("Fail to load compass texture!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Camera 위치 설정
            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            nxPlanetView1.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));

            // 컨트롤의 초기 설정
            checkBoxInverseMouseButton.Checked = nxPlanetView1.InverseMouseButton;
            checkBoxInverseMouseWheel.Checked = nxPlanetView1.InverseMouseWheel;
            checkBoxRotatable.Checked = nxPlanetView1.Rotatable;

            checkBoxShowPBP.Checked = nxPlanetView1.ShowPBP;
            checkBoxShowStar.Checked = true;
            checkBoxShowStatusInfo.Checked = nxPlanetView1.ShowStatusInfo;

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

        private void checkBoxInverseMouseButton_CheckedChanged(object sender, EventArgs e)
        {
            // PlanetView는 기본적으로 마우스 왼쪽 버튼은 화면이동을, 오른쪽 버튼은 화면회전 기능을 담당한다.
            // 이 기능을 전환하려면 NXPlanetView의 InserverMouseButton을 true로 설정하면 된다.
            nxPlanetView1.InverseMouseButton = checkBoxInverseMouseButton.Checked;
        }

        private void checkBoxInverseMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            // PlanetView는 기본적으로 마우스 휠을 당기면 화면확대가, 밀면 화면축소가 된다.
            // 이을 전환하려면 NXPlanetView의 InverseMouseWheel을 true로 설정하면 된다.
            nxPlanetView1.InverseMouseWheel = checkBoxInverseMouseWheel.Checked;
        }

        private void checkBoxRotatable_CheckedChanged(object sender, EventArgs e)
        {
            // NXPlanetView의 Rotatble 속성을 설정하면 화면회전 여부를 설정할 수 있다.
            nxPlanetView1.Rotatable = checkBoxRotatable.Checked;
        }

        private void comboBoxGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gridIndex = comboBoxGrid.SelectedIndex;
            if (gridIndex == 0) // none
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridNone;
            else if (gridIndex == 1)    // Degrees
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridDegrees;
            else if (gridIndex == 2)    // GARS
                nxPlanetView1.GridType = NXPlanetView.eGridType.GridGARS;
        }

        private void buttonScaleApply_Click(object sender, EventArgs e)
        {
            double mapAltitude = 1500000.0;
            int scaleIndex = comboBoxScale.SelectedIndex;
            if (scaleIndex == 0)   // 1000000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_1000000);
            else if (scaleIndex == 1)   // 500000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_500000);
            else if (scaleIndex == 2)   // 100000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_100000);
            else if (scaleIndex == 3)   // 50000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_50000);
            else if (scaleIndex == 4)   // 10000
                mapAltitude = nxPlanetView1.GetMapAltitude(NXPlanetView.eMapScale.Scale_10000);


            // 현재의 camera 정보 가져오기
            NXCameraState state = nxPlanetView1.GetCameraState();

            XGeoPoint eyePos = new XGeoPoint();
            eyePos.lond = state.lonEye.deg;
            eyePos.latd = state.latEye.deg;
            eyePos.hgt = mapAltitude;

            // 위치를 유지한 상태로 카메라의 높이만 수정하여 설정
            nxPlanetView1.SetCameraPosition(eyePos, XAngle.FromDegree(0.0));
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowPBP_CheckedChanged(object sender, EventArgs e)
        {
            // 지형 지도 위에 텍스트형태의 지명을 중첩하여 도시할 수 있는데, 이에 대한 여부를 설정한다.
            nxPlanetView1.ShowPBP = checkBoxShowPBP.Checked;
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowStar_CheckedChanged(object sender, EventArgs e)
        {
            // 화면 축소를 해서 지도 영역 밖으로 벗어나는 경우, 배경 별을 추가도시 여부를 설정한다.
            nxPlanetView1.ShowStar = checkBoxShowStar.Checked;
            nxPlanetView1.RefreshScreen();
        }

        private void checkBoxShowStatusInfo_CheckedChanged(object sender, EventArgs e)
        {
            // XDL 엔진의 도시 상태 정보를 화면에 도시 여부를 설정한다.
            nxPlanetView1.ShowStatusInfo = checkBoxShowStatusInfo.Checked;
            nxPlanetView1.RefreshScreen();
        }

        private bool nxPlanetLayer1_OnWndProc(object sender, NXPlanetDrawArgs e, ref Message m)
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
            bool result = textPrinter.Print(str, posWorld, Pixoneer.NXDL.NGR.eTextAlign.Align_Center, Color.White, true, Color.DarkBlue);

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
 
    }
}
