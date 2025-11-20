using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pixoneer.NXDL;
using Pixoneer.NXDL.NSCENE;

namespace XDL_PlanetView3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            nxSceneLayerDisplay = new NXPlanetLayerSceneDisplay();
            nxSceneLayerEditor = new NXPlanetLayerSceneEditor();
            nxSceneLayerEditor.OnObjectCreated += new NXSCEditEvent(nxSceneLayerEditor_OnObjectCreated);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nxSceneLayerEditor.AttachTo(nxPlanetView2D);
            nxSceneLayerDisplay.AttachTo(nxPlanetView3D);

            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            // Planet2D 모드의 camera 위치 설정
            nxPlanetView2D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));
            // Planet3D 모드의 camera 위치 설정
            nxPlanetView3D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0), XAngle.FromDegree(-90.0), XAngle.FromDegree(0.0));

            nxPlanetView2D.Rotatable = false;
            nxPlanetView2D.ShowGrid = false;

            nxPlanetView2D.RefreshScreen();
            nxPlanetView3D.RefreshScreen();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Xfn.Close();
        }

        bool nxSceneLayerEditor_OnObjectCreated(XscObj pObj)
        {
            if (pObj == null) return false;

            // Scene 객체의 클래스 이름 가져오기
            string typeName = pObj.GetTypeName();
            if (typeName == "XscPoint")
            {
                XscPoint objPoint = (XscPoint)pObj;
                objPoint.LineColor = Color.Yellow;
                // Point의 크기
                objPoint.LineWidth = 10;
                // PointType으로는 Cross, Dot, Triangle, Rect, X가 있다.
                objPoint.PointType = XscPoint.ePointType.Rect;
                objPoint.Name = "Point #" + objPoint.ObjID.ToString();
            }
            else if (typeName == "XscPolyLine")
            {
                XscPolyLine objPolyLine = (XscPolyLine)pObj;
                objPolyLine.LineColor = Color.LightGreen;
                objPolyLine.LinePattern = XscLinePattern.eLinePatternType.DashDot;
                objPolyLine.LineWidth = 3;
                objPolyLine.Name = "Polyline #" + objPolyLine.ObjID.ToString();
            }
            else if (typeName == "XscPolygon")
            {
                XscPolygon objPolygon = (XscPolygon)pObj;
                objPolygon.BorderColor = Color.Blue;
                objPolygon.BorderSize = 3;
                objPolygon.LinePattern = XscLinePattern.eLinePatternType.DashDotDot;
                objPolygon.FillColor = Color.Cyan;
                // FillPattern이 Solid가 아닌 경우에는 배경색이 투명하게 처리된다.
                objPolygon.FillPattern = XscFillPattern.eFillPatternType.Cross;
                objPolygon.Name = "Polygon #" + objPolygon.ObjID.ToString();
            }
            else if (typeName == "XscCircle")
            {
                XscCircle objCircle = (XscCircle)pObj;
                objCircle.LineColor = Color.Coral;
                objCircle.LineWidth = 2;
                objCircle.FillColor = Color.GreenYellow;
                objCircle.FillPattern = XscFillPattern.eFillPatternType.Vertical;
                objCircle.Name = "Circle #" + objCircle.ObjID.ToString();
            }
            else if (typeName == "XscSymbol")
            {
                XscSymbol objSymbol = (XscSymbol)pObj;
                // DefaultSymbolName으로 사용할 수 있는 것은 Apartment, Image, Model, Public, Stadium, TextChart가 있다.
                // 이는 XDL 엔진 설치 폴더 중 Resource\Scenes\Icons에 저장되어 있다.
                // 사용자 정의의 심볼을 로딩하고자 한다면, DefaultSymbolName 대신 UserSymbolPath 속성으로 경로를 설정한다.
                objSymbol.DefaultSymbolName = "Apartment";
                objSymbol.UpdateSymbol();
                objSymbol.Name = "Symbol #" + objSymbol.ObjID.ToString();
            }

            nxPlanetView2D.RefreshScreen();
            return default(bool);
        }

        private void pointToolStripButton_Click(object sender, EventArgs e)
        {
            nxSceneLayerEditor.CreateNewOBJ("XscPoint");
        }

        private void polylineToolStripButton_Click(object sender, EventArgs e)
        {
            nxSceneLayerEditor.CreateNewOBJ("XscPolyLine");
        }

        private void polygonToolStripButton_Click(object sender, EventArgs e)
        {
            nxSceneLayerEditor.CreateNewOBJ("XscPolygon");
        }

        private void circleToolStripButton_Click(object sender, EventArgs e)
        {
            nxSceneLayerEditor.CreateNewOBJ("XscCircle");
        }

        private void symbolToolStripButton_Click(object sender, EventArgs e)
        {
            nxSceneLayerEditor.CreateNewOBJ("XscSymbol");
        }

        private void saveToFileToolStripButton_Click(object sender, EventArgs e)
        {
            // 생성한 scene 객체를 가지고 있는 최상위 XScene 객체를 얻어온다.
            XScene sceneRoot = nxSceneLayerEditor.GetScene();
            if (sceneRoot == null) return;

            // 저장을 위한 FileDialog 생성
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "XDL Scene file(*.sml)|*.sml;||";
            if (saveDlg.ShowDialog() != DialogResult.OK) return;

            // 파일경로에 XScene 객체의 데이터를 XML 형태로 저장한다.
            // 세 번째 매개변수인 XSpatialReference는 출력 좌표계 정보를 설정하는 것으로서,
            // 여기에서는 XScene 객체의 좌표계 정보로 하겠다.
            if (XScene.SaveScene(sceneRoot, saveDlg.FileName, sceneRoot.SR))
            {
                MessageBox.Show("PlanetView 2D의 scene 객체를 파일로 저장하였습니다.");
            }
            else
            {
                MessageBox.Show("PlanetView 2D의 scene 객체를 파일로 저장하지 못했습니다.");
            }
        }

        private void loadFileToolStripButton_Click(object sender, EventArgs e)
        {
            // 파일을 선택하기 위해서 FileDialog 생성
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "XDL Scene file(*.sml)|*.sml;||";
            if (openDlg.ShowDialog() != DialogResult.OK) return;

            // NXPlanetLayerSceneDisplay의 Open 함수를 이용하여 scene 객체 로딩
            if (nxSceneLayerDisplay.Open(openDlg.FileName))
            {
                XScene scene = nxSceneLayerDisplay.GetScene();
                // Scene 객체 도시 방법을 추가되는 순서대로 도시하도록 설정한다.
                // 기본값은 XScene.eDisplayOrder.OrderNone이다.
                scene.DisplayOrder = XScene.eDisplayOrder.OrderByAddSequence;

                MessageBox.Show("PlanetView 3D의 scene 객체를 갱신하겠습니다");
                nxPlanetView3D.RefreshScreen();
            }
            else
            {
                MessageBox.Show("scene 파일을 로딩할 수 없습니다.");
            }
        }

    }
}
