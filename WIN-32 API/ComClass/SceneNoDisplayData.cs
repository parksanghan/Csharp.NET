using LibCoordinate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CEnum;
using SensorScheduler;

namespace MPEObjectLib.Capture
{
    [Serializable]
    public partial class SceneNoDisplayData
    {
        public CCoordinateList<double> m_Visible = new CCoordinateList<double>();  // 가시 영역 라인 생성을 위한 좌표
        public List<double> m_Visibility = new List<double>(); // 가시율
        public double m_BeginPer = -1; // 시작 퍼센트
        public double m_EndPer = -1;   // 종료 퍼센트
        // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
        public double m_BeginLegPer = -1;   // LEG의 시작 퍼센트
        public double m_EndLegPer = -1;     // LEG의 종료 퍼센트
        // 20170110 조승현 - [ 결함#1794 ] SAR GMTI 집중모드 정보추가
        public double m_StartCenterSlantRange = -1;

        public double m_CenterSlantRange = -1;
        public double m_NearSlantRange = -1;
        public double m_FarSlantRange = -1;
        public double m_StartAngle = -1;
        public double m_EndAngle = -1;
        public double m_Pitch = -1;
        // 20161206 조승현 - [ 결함#1630 ] 촬영계획 세부정보 수정 ( heading 추가 )
        public double m_Heading = -1;
        public double m_Roll = -1;
        // 20161013 조승현 - [ 자체개선 ] 촬영계획 상세보기 개선
        public double m_SwathMinNo = -1;    // swath 시작번호
        public double m_SwathMaxNo = -1;    // swath 끝 번호
        public double m_ScanTime = -1;          // 운용시간
        // 20180919 조승현 - [GCS-TB-MPE-370] 촬영계획 창에서 촬영시작시간으로 Sorting위해 추가
        public double m_BeginTime = -1;         // 촬영 시작시간
        public double m_Sal = -1;               // SAL
        public double m_HalfSal = -1;           // HalfSal

        // 20170821 조승현 GMTI 집중 전방/측방 선택을 위한 카메라모드 설정
        public E_CAMERA_MODE m_camera = E_CAMERA_MODE.e_cameramode_sar_gmti_sss_fls;

        public CCoordinateList<double> m_StartCoordinate = new CCoordinateList<double>();  // 촬영시작 좌표
        public CCoordinateList<double> m_EndCoordinate = new CCoordinateList<double>();  // 촬영 종료 좌표
        public CCoordinateList<double> m_FootPrintCoordinate = new CCoordinateList<double>();  // 풋프린트 다각형 좌표
        public CCoordinateList<double> m_CenterLineCoordinate = new CCoordinateList<double>();  // 센터라인 좌표

        // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
        public CCoordinateList<double> m_LeftLineCoordinate = new CCoordinateList<double>(); // 시작라인 좌표
        public CCoordinateList<double> m_RightLineCoordinate = new CCoordinateList<double>(); // 종료라인 좌표

        //20151118 Add by Jun
        public CCoordinateList<double> m_RealStartCoordinate = new CCoordinateList<double>();  // 준비시간 제외된 촬영시작 좌표
        public CCoordinateList<double> m_RealEndCoordinate = new CCoordinateList<double>();  // 준비시간 제외된 촬영 종료 좌표
        
        public CCoordinateList<double> GetStartEndCoordinate()
        {
            CCoordinateList<double> result = new CCoordinateList<double>();
            result.Add(m_StartCoordinate[0]);
            result.Add(m_EndCoordinate[0]);
            return result;
        }

        public CCoordinateList<double> GetRealStartEndCoordinate()
        {
            CCoordinateList<double> result = new CCoordinateList<double>();
            result.Add(m_RealStartCoordinate[0]);
            result.Add(m_RealEndCoordinate[0]);
            return result;
        }

        // 20151118 Edit by Jun
        public SceneNoDisplayData(
            CCoordinateList<double> Visible, 
            List<double> Visibility, 
            double BeginPer, 
            double EndPer,
            // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
            double BeginLegPer,
            double EndLegPer,
            // 20170110 조승현 - [ 결함#1794 ] SAR GMTI 집중모드 정보추가
            double StartCenterSlantRange,
            double CenterSlantRange,
            double NearSlantRange,
            double FarSlantRange,
            double StartAngle,
            double EndAngle,
            double Pitch,
            // 20161206 조승현 - [ 결함#1630 ] 촬영계획 세부정보 수정 ( heading 추가 )
            double Heading,
            double Roll,
            // 20161013 조승현 - [ 자체개선 ] 촬영계획 상세보기 개선
            double SwathMinNo,
            double SwathMaxNo,
            double ScanTime,
            // 20180919 조승현 - [GCS-TB-MPE-370] 촬영계획 창에서 촬영시작시간으로 Sorting위해 추가
            double BeginTime,
            double Sal,
            double HalfSal,
            E_CAMERA_MODE camera,
            CCoordinateList<double> FootPrint, 
            CCoordinateList<double> Start, 
            CCoordinateList<double> End, 
            CCoordinateList<double> RealStart, 
            CCoordinateList<double> RealEnd,
            CCoordinateList<double> CenterLine,
            CCoordinateList<double> LeftLine,
            CCoordinateList<double> RightLine)
        {
            m_Visible = Visible;
            m_Visibility = Visibility;
            m_BeginPer = BeginPer;
            m_EndPer = EndPer;
            // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
            m_BeginLegPer = BeginLegPer;
            m_EndLegPer = EndLegPer;
            m_StartCoordinate = Start;
            m_EndCoordinate = End;
            // 20170110 조승현 - [ 결함#1794 ] SAR GMTI 집중모드 정보추가
            m_StartCenterSlantRange = StartCenterSlantRange;
            m_CenterSlantRange = CenterSlantRange;
            m_NearSlantRange = NearSlantRange;
            m_FarSlantRange = FarSlantRange;
            m_StartAngle = StartAngle;
            m_EndAngle = EndAngle;
            m_Pitch = Pitch;
            // 20161206 조승현 - [ 결함#1630 ] 촬영계획 세부정보 수정 ( heading 추가 )
            m_Heading = Heading;
            m_Roll = Roll;
            // 20161013 조승현 - [ 자체개선 ] 촬영계획 상세보기 개선
            m_SwathMinNo = SwathMinNo;
            m_SwathMaxNo = SwathMaxNo;
            m_ScanTime = ScanTime;
            // 20180919 조승현 - [GCS-TB-MPE-370] 촬영계획 창에서 촬영시작시간으로 Sorting위해 추가
            m_BeginTime = BeginTime;
            m_Sal = Sal;
            m_HalfSal = HalfSal;

            m_FootPrintCoordinate = FootPrint;
            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
            m_CenterLineCoordinate = CenterLine;
            m_LeftLineCoordinate = LeftLine;
            m_RightLineCoordinate = RightLine;

            m_RealStartCoordinate = RealStart;
            m_RealEndCoordinate = RealEnd;

            // 20170821 조승현 GMTI 집중 전방/측방 선택을 위한 카메라모드 설정
            m_camera = camera;
        }
    }
}
