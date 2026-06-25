using LibCoordinate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CEnum;
using SensorScheduler;

namespace MPEObjectLib.Capture
{

    [Serializable]
    public partial class SceneCollectionProperty
    {
        private static ObservableCollection<int> global_leg = new ObservableCollection<int>();
        public List<SceneNoDisplayData> m_SceneNoDisplayData = new List<SceneNoDisplayData>();
        
        private ObservableCollection<int> m_LegID = new ObservableCollection<int>();   // Identifier for the leg
        public ObservableCollection<int> LegID
        {
            get { return m_LegID; }
            set { m_LegID = value; }
        }

        // [190321][김형철][GCS-TB-MPE-490] LEG분석 후 수집 별 LEG목록 도시 오류
        public static ObservableCollection<int> LegID2
        {
            get { return global_leg; }
        }

        private ObservableCollection<List<string>> m_ContainTargetList = new ObservableCollection<List<string>>();
        public ObservableCollection<List<string>> ContainTargetList
        {
            get { return m_ContainTargetList; }
            set { m_ContainTargetList = value; }
        }

        // 20151217 Add by Jun UI 도시용 Leg 번호
        // 20160512 Edit by Jun Leg 번호 1부터 변경
        //private ObservableCollection<int> m_UILegID = new ObservableCollection<int>();   // Identifier for the leg
        //public ObservableCollection<int> UILegID
        //{
        //    get { return m_UILegID; }
        //    set { m_UILegID = value; }
        //}

        private ObservableCollection<float> m_Sight = new ObservableCollection<float>(); // The sight
        public ObservableCollection<float> Sight
        {
            get { return m_Sight; }
            set { m_Sight = value; }
        }

        private ObservableCollection<int> m_Spot = new ObservableCollection<int>();	// The spot
        public ObservableCollection<int> Spot
        {
            get { return m_Spot; }
            set { m_Spot = value; }
        }

        private ObservableCollection<bool> m_Perperdicular = new ObservableCollection<bool>();	// The Perperdicular
        public ObservableCollection<bool> Perperdicular
        {
            get { return m_Perperdicular; }
            set { m_Perperdicular = value; }
        }

        private ObservableCollection<float> m_ObserveLength = new ObservableCollection<float>(); // 관측 길이
        public ObservableCollection<float> ObserveLength
        {
            get { return m_ObserveLength; }
            set { m_ObserveLength = value; }
        }

        private ObservableCollection<float> m_ObserveWidth = new ObservableCollection<float>();  // 관측 폭
        public ObservableCollection<float> ObserveWidth
        {
            get { return m_ObserveWidth; }
            set { m_ObserveWidth = value; }
        }

        public int GetCount()
        {
            return m_LegID.Count;
        }


        // 20181105 조승현
        public int GetContainTargetListCount()
        {
            return m_ContainTargetList.Count;
        }

        // 20151221 Add by Jun
        //public int GetUILegID(int Index)
        //{
        //    if (this.UILegID.Count <= Index || Index == -1) return -1;
        //    return this.UILegID[Index];
        //}

        public int GetLegID(int Index)
        {
            if (LegID2.Count + 2 <= Index || Index == -1) return -1;
            // 20201202 조승현 - [자체개선] 재정렬 실패 시 트리/레그 리스트 동기화
            int value = Index + 1;
            foreach (var leg in LegID2)
            {
                if ((Index + 1).Equals(leg))
                {
                    value = leg;
                    break;
                }
            }
            return value;
            //return LegID2[Index];
        }

        public List<string> GetContainTargetList(int Index)
        {
            if (this.ContainTargetList.Count <= Index || Index == -1) return null;
            return this.ContainTargetList[Index];
        }

        public float GetObserveLength(int Index)
        {
            if (this.ObserveLength.Count <= Index || Index == -1) return -1;
            return this.ObserveLength[Index];
        }

        public float GetObserveWidth(int Index)
        {
            if (this.ObserveWidth.Count <= Index || Index == -1) return -1;
            return this.ObserveWidth[Index];
        }

        public float GetSight(int Index)
        {
            if (this.Sight.Count <= Index || Index == -1) return -1;
            return this.Sight[Index];
        }

        public int GetSpot(int Index)
        {
            if (this.Spot.Count <= Index || Index == -1) return -1;
            return this.Spot[Index];
        }

        // 20170607 김형철 데드코드 삭제
        //public bool GetPerperdicular(int Index)
        //{
        //    if (this.Perperdicular.Count <= Index || Index == -1) return false;
        //    return this.Perperdicular[Index];
        //}

        // 20170607 김형철 데드코드 삭제
        //public SceneNoDisplayData GetNoDisplayData(int Index)
        //{
        //    if (this.m_SceneNoDisplayData.Count < Index) return null;
        //    return this.m_SceneNoDisplayData[Index];
        //}

        // 20170607 김형철 데드코드 삭제
        //public CCoordinateList<double> GetStartEndCoordinate(int Index)
        //{
        //    return m_SceneNoDisplayData[Index].GetStartEndCoordinate();
        //}

        public CCoordinateList<double> GetVisible(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return null;
            return m_SceneNoDisplayData[0].m_Visible;
        }

        public List<double> GetVisibility(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return null;
            return m_SceneNoDisplayData[0].m_Visibility;
        }

        public double GetBeginPer(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_BeginPer;
        }

        // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
        public double GetBeginLegPer(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_BeginLegPer;
        }

        public double GetEndLegPer(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_EndLegPer;
        }

        public double GetEndPer(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_EndPer;
        }
        
        public double GetBeginTime(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_BeginTime;
        }

        public double GetCenterSlantRange(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_CenterSlantRange;
        }

        // 20170110 조승현 - [ 결함#1794 ] SAR GMTI 집중모드 정보추가
        public double GetStartCenterSlantRange(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_StartCenterSlantRange;
        }

        // 20161013 조승현 - [ 자체개선 ] 촬영계획 상세보기 개선
        public double GetSwathMinNo(int Index)  // swath 시작번호
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_SwathMinNo;
        }

        public double GetSwathMaxNo(int Index)  // swath 끝 번호
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_SwathMaxNo;
        }

        public double GetScanTime(int Index)    // 운용시간
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_ScanTime;
        }

        // 20180919 조승현 - [GCS-TB-MPE-370] 촬영계획 창에서 촬영시작시간으로 Sorting위해 추가
        public double GetCaptureBeginTime(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_BeginTime;
        }

        // 20170821 조승현 GMTI 집중 전방/측방 선택을 위한 카메라모드 설정
        public E_CAMERA_MODE GetCameraMode(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return E_CAMERA_MODE.e_cameramode_sar_gmti_sss_fls;
            return m_SceneNoDisplayData[0].m_camera;
        }

        public double GetSal(int Index) // SAL
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_Sal;
        }

        public double GetHalfSal(int Index) // HALFSAL
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_HalfSal;
        }

        public double GetNearSlantRange(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_NearSlantRange;
        }

        public double GetFarSlantRange(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_FarSlantRange;
        }

        public double GetStartAngle(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_StartAngle;
        }

        public double GetEndAngle(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_EndAngle;
        }

        public double GetPitch(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_Pitch;
        }

        // 20161206 조승현 - [ 결함#1630 ] 촬영계획 세부정보 수정 ( heading 추가 )
        public double GetHeading(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_Heading;
        }

        public double GetRoll(int Index)
        {
            if (this.LegID.Count < 1 || Index == -1) return -1;
            return m_SceneNoDisplayData[0].m_Roll;
        }

        public string GetStartPositionString(int Index)
        {
            return m_SceneNoDisplayData[0].m_StartCoordinate[0].Y.ToString() + ", " + m_SceneNoDisplayData[0].m_StartCoordinate[0].X.ToString();
        }

        public string GetEndPositionString(int Index)
        {
            return m_SceneNoDisplayData[0].m_EndCoordinate[0].Y.ToString() + ", " + m_SceneNoDisplayData[0].m_EndCoordinate[0].X.ToString();
        }

        public CCoordinateList<double> GetStartPosition(int Index)
        {
            return m_SceneNoDisplayData[0].m_StartCoordinate;
        }

        public CCoordinateList<double> GetEndPosition(int Index)
        {
            return m_SceneNoDisplayData[0].m_EndCoordinate;
        }

        // 20200217 조승현 - [GCS-TB-MPE-272] 촬영계획 결과보기 창 개선
        public CCoordinateList<double> GetRealStartPosition(int Index)
        {
            return m_SceneNoDisplayData[0].m_RealStartCoordinate;
        }


        public CCoordinateList<double> GetRealEndPosition(int Index)
        {
            return m_SceneNoDisplayData[0].m_RealEndCoordinate;
        }
    }
}
