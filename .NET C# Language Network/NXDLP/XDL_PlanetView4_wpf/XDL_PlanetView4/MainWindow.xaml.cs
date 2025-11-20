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

using System.Threading;             // Thread 기능

using Pixoneer.NXDL;             // 기본 함수 관련 기능
using Pixoneer.NXDL.NXPlanet;    // 구 기반 도시 관련 기능
using Pixoneer.NXDL.NNCW;        // NCW 환경 관련 기능
using Pixoneer.NXDL.NEQUIP;      // NCW 환경에서 시뮬레이션하는 모델 관련 기능

using Microsoft.Win32;


namespace XDL_PlanetView4
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private XncwTheater xncwTheater;       // ncw 환경을 위한 레이어
        private XncwObserver xncwObserver2D;   // 2D Planet에서 시뮬레이션 모델 주시 기능을 담당할 레이어
        private XncwObserver xncwObserver3D;   // 3D Planet에서 시뮬레이션 모델 주시 기능을 담당할 레이어

        // NCW 모델을 관리하기 위한 List 객체
        private List<MyPlane> modelList = new List<MyPlane>();


        // 화면 갱신을 위한 Thread 
        private Thread threadRefresh = null;
        private bool runThread = false;

        private NXPlanetLayer nxPlanetLayer2D;

        public MainWindow()
        {
            InitializeComponent();

            // PlanetView 2D/3D의 배경색 설정
            nxPlanetView2D.BackColor = System.Drawing.Color.Black;
            nxPlanetView3D.BackColor = System.Drawing.Color.Black;

            // NCW를 위한 theater 객체, observer 객체 생성
            xncwTheater = new XncwTheater();
            xncwObserver2D = new XncwObserver();
            xncwObserver3D = new XncwObserver();

            // 윈도우 이벤트를 받기 위한 NXPlanetLayer 변수 및 이벤트 함수 추가
            nxPlanetLayer2D = new NXPlanetLayer();
            nxPlanetLayer2D.OnWndProc += new NXPlanetLayerWndProcEvent(nxPlanetLayer2D_OnWndProc);

            // xncwTheater의 OnPicked 이벤트 함수 추가
            xncwTheater.OnPicked += new XncwTheaterPickEvent(xncwTheater_OnPicked);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nxPlanetView2D.AddRenderLayer(ref nxPlanetLayer2D);

            // Theater 객체를 Planet 2D/3D 뷰에 추가
            xncwTheater.AttachTo(nxPlanetView2D);
            xncwTheater.AttachTo(nxPlanetView3D);

            // Theater의 모든 객체를 PlanetView 3D에서는 항상 주시할 수 있도록 설정
            xncwTheater.ShowAllObjects(nxPlanetView3D, true);

            // 각 PlanetView에 관찰을 위한 Observer 객체를 각각 추가한다.
            xncwObserver2D.AttachTo(nxPlanetView2D);
            xncwObserver3D.AttachTo(nxPlanetView3D);

            XGeoPoint gpEye = XGeoPoint.FromDegree(127.4, 38.0, 1500000);
            // Planet2D 모드의 camera 위치 설정
            nxPlanetView2D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0));
            // Planet3D 모드의 camera 위치 설정
            nxPlanetView3D.SetCameraPosition(gpEye, XAngle.FromDegree(0.0), XAngle.FromDegree(-90.0), XAngle.FromDegree(0.0));
        }

        bool xncwTheater_OnPicked(object sender, NXPlanetDrawArgs e, long ID)
        {
            // ID에 대응되는 장비를 xncwTheater에서 가져온다.
            XEquipObj obj = xncwTheater.GetEquipment(ID);
            // ID에 대응되는 객체가 없는 경우 null 객체를 반환한다.
            if (obj == null) return false;

            MyPlane modelPlane = (MyPlane)obj;

            //MyPlane 객체의 속성으로 PlaneProperty 값을 설정한다.
            Action action = () =>
            {
                PlaneProperty property = new PlaneProperty();

                property.modelID = (int)ID;
                property.modelName = modelPlane.modelName;
                property.modelScalable = modelPlane.Scalable;
                property.modelScaleX = modelPlane.Scale.x;
                property.modelScaleY = modelPlane.Scale.y;
                property.modelScaleZ = modelPlane.Scale.z;
                property.modelShowBoundingBox = modelPlane.modelShowBoundingBox;
                property.cameraMode = modelPlane.cameraMode;

                Nullable<bool> result = property.ShowDialog();
                if (result != true)
                {
                    return;// false;
            }

                // PlaneProperty xaml을 이용해 설정한 값을 MyPlane에 설정한다.
                modelPlane.Scalable = property.modelScalable;
                modelPlane.Scale = new XVertex3d(property.modelScaleX, property.modelScaleY, property.modelScaleZ);

                modelPlane.ShowBoundingBox(property.modelShowBoundingBox);
                modelPlane.cameraMode = property.cameraMode;

                // CameraMode를 Unusable로 설정한 경우,
                // 특정 객체를 주시하는 것이 아니라 모든 객체를 확인할 수 있도록 xncwObserver2D, xncwObserver3D의 SurveyNone() 함수로 설정
                if (modelPlane.cameraMode == XncwObserver.eViewMode.Unusable)
                {
                    xncwObserver2D.SurveyNone();
                    xncwObserver3D.SurveyNone();
                }
                else
                {
                // xncwObserver2D에서 선택한 객체와 camera mode로 대상 객체를 주시하도록 설정
                xncwObserver2D.SurveyTargetObj(obj, modelPlane.cameraMode);
                // 객체 주시를 위한 거리를 설정
                xncwObserver2D.SetDistance(4000);
                // xncwObserver3D에서 선택한 객체와 camera mode로 대상 객체를 주시하도록 설정
                xncwObserver3D.SurveyTargetObj(obj, modelPlane.cameraMode);
                    xncwObserver3D.SetDistance(4000);
                }

                // xncwTheater의 가시 영역을 갱신한다.
                xncwTheater.UpdateVisibleArea();

                // nxPlanetView2D와 nxPlanetView3D에 화면 갱신을 요청한다.
                nxPlanetView2D.RefreshScreen();
                nxPlanetView3D.RefreshScreen();
            };
            Dispatcher.BeginInvoke(action);
            return default(bool);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Xfn.Close();
        }

        // 화면 갱신을 요청하는 thread를 위한 함수
        public void RefreshScreenThread()
        {
            // thread 실행 여부 설정
            runThread = true;
            while (runThread)
            {
                // xnwTheater의 가시영역 갱신
                xncwTheater.UpdateVisibleArea();

                // nxPlanetView2D와 nxPlanetView3D에 화면 갱신 요청
                nxPlanetView2D.RefreshScreen();
                nxPlanetView3D.RefreshScreen();

                // 100 mili second 동안 threading을 쉰다.
                Thread.Sleep(100);
            }
        }

        private void addModelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // AddPlane 생성하여 MyPlane을 위한 속성 설정
            AddPlane addForm = new AddPlane();

            Nullable<bool> result = addForm.ShowDialog();
            if (result != true) return;

            MyPlane newPlane = new MyPlane();
            // MyPlane 객체에 매개변수를 이용하여 초기화
            if (!newPlane.Initialize(addForm.pathModel, addForm.pathData, addForm.nameModel, addForm.ID))
            {
                MessageBox.Show("Model을 로딩할 수 없습니다.");
                return;
            }

            // MyPlane 객체의 크기조절 여부를 조절한다.
            newPlane.Scalable = false;

            XEquipObj newObj = newPlane;
            // xncwTheater에 새로운 모델을 추가한다.
            if (xncwTheater.AddEquipment(newPlane.modelID, ref newObj))
            {
                // 모델 리스트에 새로운 모델 추가
                modelList.Add(newPlane);

                // 새로운 모델의 시뮬레이션 시작
                newPlane.Start();
                // xncwTheater의 가시영역 갱신
                xncwTheater.UpdateVisibleArea();

                // nxPlanetView2D와 nxPlanetView3D의 도시 갱신 요청
                nxPlanetView2D.RefreshScreen();
                nxPlanetView3D.RefreshScreen();

                if (threadRefresh == null)
                {
                    // 화면 갱신을 위한 thread 생성
                    threadRefresh = new Thread(RefreshScreenThread);
                    threadRefresh.Start();
                }
            }
            else
                // xncwTheater에 모델 추가를 하지 못하면 MyPlane을 해제한다.
                newPlane.Uninitialize();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 화면 갱신을 위한 thread를 해제한다.
            if (threadRefresh != null)
            {
                runThread = false;
                if (threadRefresh.IsAlive)
                    threadRefresh.Join();
                threadRefresh = null;
            }

            // MyPlane 객체를 해제한다.
            foreach (MyPlane obj in modelList)
            {
                obj.Uninitialize();
            }
            modelList.Clear();
        }

       // nxPlanetLayer2D 레이어의 OnWndProc 이벤트 함수
        private bool nxPlanetLayer2D_OnWndProc(object sender, NXPlanetDrawArgs e, ref System.Windows.Forms.Message m)
        {
            // 마우스 왼쪽 버튼을 더블 클릭
            if (m.Msg == Pixoneer.NXDL.XWndMsg.XWM_LBUTTONDBLCLK)
            {
                // 화면 좌표
                XVertex2d scrPos = new XVertex2d();
                scrPos.x = Pixoneer.NXDL.XWndMsg.GetLowValue(m.LParam);
                scrPos.y = Pixoneer.NXDL.XWndMsg.GetHighValue(m.LParam);

                // xncwTheater에 화면 좌표에 대해 nxPlanetView2D에서 hit-test를 수행한다.
                // Pick 함수가 성공적으로 실행되면 xncwTheater의 OnPicked가 호출된다.
                xncwTheater.Pick(nxPlanetView2D.GetHandle(), scrPos);
            }
            return default(bool);
        }
    }
}
