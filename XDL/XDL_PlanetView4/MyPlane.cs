using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;        // FileStream 관련 기능
using System.Threading; // Threading 관련 기능

using Pixoneer.NXDL;                // 기본 함수 관련 기능
using Pixoneer.NXDL.NNCW;           // NCW 환경 관련 기능
using Pixoneer.NXDL.NEQUIP;         // NCW 환경에서 시뮬레이션하는 모델 관련 기능

namespace XDL_PlanetView4
{
    class MyPlane : XAircraft
    {
        public string  modelPathData;  // 시뮬레이션 데이터 파일 경로
        public string modelPathModel; // 모델 파일 경로
        public string modelName;      // 모델 이름
        public int modelID;             // 모델 구별자

        // 시뮬레이션 주시(관측) 모드
        public XncwObserver.eViewMode cameraMode;
        // 모델의 경계 영역 도시 여부
        public bool modelShowBoundingBox;

        private FileStream modelFileData;   // 시뮬레이션 데이터 파일
        private Thread modelThread;         // 시뮬레이션을 위한 쓰레드
        private bool runThread;             // 시뮬레이션 쓰레드 여부 설정

        // 클래스 생성자
        public MyPlane()
        {
            modelPathData = "";
            modelPathModel = "";
            modelName = "MyPlane";
            modelID = 0;

            // Camera 관측 모드
            // Unuable인 경우 특정 모델을 주시하지 않을 때 사용
            cameraMode = XncwObserver.eViewMode.Unusable;
            modelShowBoundingBox = false;
            modelFileData = null;
            modelThread = null;
            runThread = false;
        }

        // 모델 경로, 시뮬레이션 데이터 경로, 이름 및 ID를 입력으로 받아 초기화하는 함수
        public bool Initialize(string pathModel, string pathData, string name, int nID)
        {
            // 파일 경로로부터 모델 데이터 로딩
            if (!LoadModel(pathModel)) return false;

            // 멤버 변수 설정
            modelName = name;
            modelID = nID;
            modelPathData = pathData;

            // 시뮬레이션 데이터 로딩
            modelFileData = new FileStream(modelPathData, FileMode.Open, FileAccess.Read);
            return true;
        }

        // 시뮬레이션 기능 및 데이터를 해제하는 함수
        public void Uninitialize()
        {
            // 쓰레드가 있는 경우 이를 해제
            if (modelThread != null)
            {
                runThread = false;
                if (modelThread.IsAlive)
                    modelThread.Join();
                modelThread = null;
            }

            // 시뮬레이션 데이터가 있는 경우 이를 해제
            if (modelFileData != null) modelFileData.Close();
        }

        // 시뮬레이션 함수
        public void SimulationThread()
        {
            // 시뮬레이션 데이터가 없는 경우 진행하지 않는다.
            if (modelFileData == null) return;

            // 시뮬레이션 실행 여부를 실행하는 것으로 설정
            runThread = true;
            // 시뮬레이션 단위 데이터 크기 설정
            int frameDataSize = 14/* 시뮬레이션 단위를 이루는 항목 개수 */ * 4 /*float 데이터 크기 */;
            // 전체 파일 크기를 시뮬레이션 단위 데이터 크기로 나누어 전체 시뮬레이션 단위 개수 계산
            long dataSize = modelFileData.Length / frameDataSize;
            // 시뮬레이션 단위를 읽기 위한 buffer 생성
            byte[] buff = new byte[frameDataSize];

            while (runThread)
            {
                int idx = 0, count = 0;
                // 시뮬레이션 데이터 파일의 처음으로 이동
                modelFileData.Seek(0, SeekOrigin.Begin);

                do
                {
                    if (!runThread) break;

                    // 시뮬레이션 데이터 파일에서 시뮬레이션 단위 데이터 읽기
                    count = modelFileData.Read(buff, 0, frameDataSize);

                    // 시뮬레이션 단위 데이터에서 속도 가져오기
                    double speed = BitConverter.ToSingle(buff, 0);
                    // 시뮬레이션 단위 데이터에서 위치의 위도 가져오기
                    double lat = BitConverter.ToSingle(buff, 4);
                    // 시뮬레이션 단위 데이터에서 위치의 경도 가져오기
                    double lon = BitConverter.ToSingle(buff, 8);
                    // 시뮬레이션 단위 데이터에서 위치의 높이 가져오기
                    double alt = BitConverter.ToSingle(buff, 12);

                    // 시뮬레이션 단위 데이터에서 자세의 yaw 가져오기
                    double yaw = BitConverter.ToSingle(buff, 16);
                    // 시뮬레이션 단위 데이터에서 자세의 roll 가져오기
                    double roll = BitConverter.ToSingle(buff, 20);
                    // 시뮬레이션 단위 데이터에서 자세의 pitch 가져오기
                    double pitch = BitConverter.ToSingle(buff, 24);

                    XGeoPoint geoPos = new XGeoPoint();
                    geoPos.lond = lon;
                    geoPos.latd = lat;
                    geoPos.hgt = alt;

                    // 모델의 위치 설정
                    SetPosition(geoPos);
                    // 모델의 자세 설정
                    SetYawPitchRoll(XAngle.FromDegree(yaw), XAngle.FromDegree(pitch), XAngle.FromDegree(roll));
                    // 100 milli second 동안 시뮬레이션을 쉰다.
                    Thread.Sleep(100);

                    idx++;
                }
                // Thread 종료 조건을 검사함
                while ((count == frameDataSize) && (idx < dataSize));
                Thread.Sleep(100);
            }

        }

        // 시뮬레이션을 시작하는 함수
        public void Start()
        {
            // 이미 thread가 실행된 상태이면 리턴
            if (modelThread != null) return;

            // SimulationThread 함수를 이용한 thread 생성
            modelThread = new Thread(SimulationThread);
            // Thread 시작
            modelThread.Start();
        }

    }
}
