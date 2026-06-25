using System.Windows.Media;
using CBaseControl;
using CCoordinateModel;
using CEnum;
using CoordinateLib;
using CUtil;
using LibCoordinate;
using MElementLib.Dot;
using MPEObjectLib.Dot;
using MPEObjectLib.Line;
using MPEObjectLib.Polygon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Event;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SensorScheduler;
using System.Threading;

namespace MPEObjectLib.Capture
{
    // 20170914 Add by Jun
    [Serializable]
    public class ErrorMessage
    {
        public int LegId { get; set; }
        public string Message { get; set; }

        public ErrorMessage(int legId, string msg)
        {
            LegId = legId;
            Message = msg;
        }
    }

    [Serializable]
    public partial class Scene : INotifyPropertyChanged
    {
        public Scene()
        {
            SetEvent(); //[20191204] 김시준 - 이벤트 참조 수정

            // [190321][김형철][GCS-TB-MPE-490] LEG분석 후 수집 별 LEG목록 도시 오류
            //Evt_O.Instance("Resetglobal_leg").Event += Clearglobal_leg;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;   // Event queue for all listeners interested in PropertyChanged events.
        protected void NotifyPropertyChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public List<ErrorMessage> ListErrorMessage = new List<ErrorMessage>();

        private string m_ParentKey = "";	// The parent key
        public string m_TargetKey = "";    // TargetKey BeNum + Suffix
        public int m_SceneStatus = 0; // 도시 여부 2 : 도시

        // 20180510 조승현 - [GCS-TB-MPE-319] 촬영계획작성 종류부호 도시 오류
        private string m_suffix = "";
        public string m_Suffix
        {
            get { return m_suffix; }
            set { m_suffix = value; }
        }
        //public string m_Suffix = "";	// The suffix

        public string MissionKey
        {
            get { return m_ParentKey; }
            set { m_ParentKey = value; }
        }

        // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
        [NonSerialized]
        private SolidColorBrush backgroundColor = Brushes.White;
        public SolidColorBrush BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                NotifyPropertyChange("BackgroundColor");
            }
        }

        // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
        private bool isEnable = true;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                NotifyPropertyChange("IsEnable");
            }
        }

        // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
        private SceneEditType editType = SceneEditType.Editable;
        public SceneEditType EditType
        {
            get { return editType; }
            set
            {
                editType = value;
                SetLegion();
                if (editType == SceneEditType.NotEditable)
                {
                    Evt_IIIII.Instance("SetLegColor").Invoke(GetLegID(), 100, 255, 0, 0);
                }
                else
                {
                    Evt_IIIII.Instance("SetLegColor").Invoke(GetLegID(), 100, 219, 218, 85);
                }
            }
        }

        //public CaptureAnalisysType AnalisysType
        //{
        //    get
        //    {
        //        if (!HasCollection && ListErrorMessage.Count > 0)
        //        {
        //            // Collection 미존재 및 에러리스트가 존재하면 분석 실패
        //            return CaptureAnalisysType.Fail;
        //        }
        //        else
        //        {
        //            return CaptureAnalisysType.None;
        //        }
        //    }
        //}

        // 20170216 분석결과없음 추가
        public bool HasCollection
        {
            get { return SceneCollection.GetCount() > 0; }
        }

        // 20161021 [김형철][자체개선] 촬영계획 미배치 추가
        public bool IsAllocated
        {
            get
            {
                return HasCollection && (LegIndex != -1);
            }
        }

        // 20160829 [김형철][자체개선] 실시간 촬영계획 보류처리 관련
        // 촬영계획 UI 업데이트
        public void SetLegion()
        {
            // 20170216 분석결과없음 추가
            if (!HasCollection)
            {
                IsEnable = true;
                m_IsCheck = true;
                // 20181029 조승현 - [자체개선] 자동배치 개념을 수집체크 개념으로 이양
                //m_AutoBatch = true;

                // 20181105 조승현 - [자체개선] 분석전, 분석실패 개념 삭제
                //if (AnalisysType == CaptureAnalisysType.None)
                //{
                //    BackgroundColor = Brushes.BurlyWood;
                //}
                //else
                //{
                //    BackgroundColor = Brushes.Orange;
                //}
                BackgroundColor = Brushes.LightGreen;
            }
            // 20161021 [김형철][자체개선] 촬영계획 미배치 추가
            else if (!IsAllocated)
            {
                IsEnable = true;
                m_IsCheck = true;
                // 20181029 조승현 - [자체개선] 자동배치 개념을 수집체크 개념으로 이양
                //m_AutoBatch = true;
                BackgroundColor = Brushes.LightGreen;
            }
            else
            {
                if (shootingState == SceneShootingState.Holding)
                {
                    IsEnable = false;
                    m_IsCheck = false;
                    //m_AutoBatch = false;
                    BackgroundColor = Brushes.Yellow;
                }
                else if (shootingState == SceneShootingState.Complete || shootingState == SceneShootingState.Fail)
                {
                    IsEnable = false;
                    m_IsCheck = false;
                    //m_AutoBatch = false;
                    BackgroundColor = Brushes.DarkGray;
                }
                else
                {
                    if (editType == SceneEditType.NotEditable)
                    {
                        IsEnable = false;
                        m_IsCheck = false;
                        //m_AutoBatch = false;
                        BackgroundColor = Brushes.Red;
                    }
                    else
                    {
                        IsEnable = true;
                        // 20171205 조승현 - [GCS-TB-MPE-146] 배치된 촬영계획 체크박스 해제
                        m_IsCheck = false;
                        BackgroundColor = Brushes.White;
                    }
                }
            }
            
            NotifyPropertyChange("IsCheck");
            Evt.Instance("UpdateAllocatedCount").Invoke();
        }

        // 20160829 [김형철][자체개선] 실시간 촬영계획 보류처리 관련
        // 촬영계획 촬영상태
        private SceneShootingState shootingState = SceneShootingState.None;
        public SceneShootingState ShootingState
        {
            get { return shootingState; }
            set
            {
                shootingState = value;
                SetLegion();
                SetFootprintColor();
            }
        }

        // Sensor Type
        private SENSORTYPE m_sensorType = SENSORTYPE.None;
        public SENSORTYPE m_SensorType
        {
            get
            {
                return m_sensorType;
            }
            set
            {
                m_sensorType = value;
                this.SetFootprintName();
                this.IsWide = this.CheckWide(this.SensorModeEOIR, this.SensorModeSAR);
            }
        }


        private SARSENSORMODE m_sarsensorMode = SARSENSORMODE.None;
        public SARSENSORMODE m_SarSensorMode
        {
            get
            {
                return m_sarsensorMode;
            }
            set
            {
                m_sarsensorMode = value;
                //this.SetFootprintName();
                //this.IsWide = this.CheckWide(this.SensorModeEOIR, this.SensorModeSAR);
            }
        }

        private SceneCollectionProperty m_SceneCollection = new SceneCollectionProperty();
        public SceneCollectionProperty SceneCollection
        {
            get { return m_SceneCollection; }
            set { m_SceneCollection = value; }
        }

        // 20160909 조승현 - [ 자체개선 317 ] 5. Leg ID 순서정렬
        private int m_SelectedLegID = -1;
        public int SelectedLegID
        {
            get { return GetLegID(); }
            set { m_SelectedLegID = value; }
        }


        // 20170103 조승현 - [ 결함 #1788 ] 촬영구간 겹침 시 경고 도시 ( 실시간 )
        private bool CheckingOverlapScene(int LegNo, Scene sceneCollection, double scenePercent, double sceneEndPercent)
        {
            var sceneList = EvtR_O_SI.Instance("GetScene").Invoke(sceneCollection.MissionKey, LegNo) as List<Scene>;

            List<Scene> compareList = new List<Scene>();

            if (sceneCollection.m_SensorType == SENSORTYPE.EO || sceneCollection.m_SensorType == SENSORTYPE.EOIR
                || sceneCollection.m_SensorType == SENSORTYPE.IR)
            {
                compareList = sceneList.Where(item => item.m_SensorType != SENSORTYPE.SAR &&
                    item.m_SensorType != SENSORTYPE.GMTI).ToList();
            }
            else
            {
                compareList = sceneList.Where(item => item.m_SensorType != SENSORTYPE.EO &&
                    item.m_SensorType != SENSORTYPE.EOIR && item.m_SensorType != SENSORTYPE.IR).ToList();
            }

            // 20170119 조승현 - [ 결함#1838 ] 미할당에서 leg 선택 시 겹침 check
            if (scenePercent != -1)
            {
                foreach (Scene scene in compareList)
                {
                    if (scene.CollectingID == sceneCollection.CollectingID)
                        continue;

                    //for (double i = scenePercent; i < sceneEndPercent; i++)
                    for (double i = scenePercent; i < sceneEndPercent; i += 0.01)
                    {
                        // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
                        // 시작구간 포함??
                        if ((scene.GetBeginLegPer() <= i) && (scene.GetEndLegPer() >= i))
                        {
                            return false;
                        }
                        //// 종료구간 포함??
                        //if ((scene.GetBeginLegPer() <= sceneEndPercent) && (scene.GetEndLegPer() >= sceneEndPercent))
                        //{
                        //    return false;
                        //} 
                    }
                }
            }
            return true;
        }

        // 20170821 조승현 - GMTI 광역 촬영최소각도 체크
        private bool CheckingMinCaptureAngle(double startangle, double endangle)
        {
            bool bResult = true;

            if (startangle != -1)
            {
                if (Math.Abs(endangle - startangle) < 4.8)
                {
                    bResult = false;
                }
            }

            return bResult;
        }

        private int nOriginLegIndex = -1;
        private int nOriginSelectedID = -1;
        // 20170810 조승현 이전 촬영횟수 저장.
        private int nOriginCaptureNum = -1;

        private int m_CalculateLegIndex = -1;
        public int CalculateLegIndex
        {
            get { return m_CalculateLegIndex; }
            set
            {
                var newLegIndex = GetLegIndex(value);
                if (CommandControl.SystemType == SYSTEMTYPE.PpcEdit)
                {
                    var cWp = EvtR_O.Instance("GetCurrentWayPoint").Invoke() as DotWayPoint;
                    if (cWp != null && cWp.ObjectType == ObjType.FlightWayPoint)
                    {
                        if (EvtR_B_SI.Instance("GetIsContainFromNotEditableLegList").Invoke(cWp.Key, GetLegID(newLegIndex)))
                        {
                            var msgInfo = new List<object> { MessageType.Alert, "알림" };
                            // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                            //Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, MessageResultType.None, "수정불가 구간의 수집을 선택할수 없습니다.", null);
                            Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, MessageResultType.None, "수정불가 LEG의 수집은 선택할 수 없습니다.", null);
                            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => NotifyPropertyChange("LegIndex")));
                            return;
                        }
                    }
                }
                nOriginCaptureNum = CaptureNumber;
                nOriginSelectedID = this.SelectedLegID;
                if (m_LegIndex != newLegIndex)
                {
                    if (newLegIndex == -1 && nOriginSelectedID > 0)
                    {
                        Evt_SI.Instance("RemoveSceneFromCollectionId").Invoke(MissionKey, m_CollectingID);
                    }

                    nOriginLegIndex = m_LegIndex;
                    m_LegIndex = newLegIndex;


                    if (this.SelectedLegID == value)
                    {
                        bool bResult = EvtR_B_IBO.Instance("SendSelectedLegID").Invoke(this.SelectedLegID, EvtR_B_S.Instance("GetEnableWindCorrection").Invoke(MissionKey), this);

                        if (!bResult)
                        {
                            var msgInfo = new List<object> { MessageType.Alert, "경고" };
                            // 20220629 조광현 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                            //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.None, "해당 LEG에선 유효한 Scene이 아닙니다.", null);
                            Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.None, "해당 LEG에 배치가 불가능합니다.", null);
                            OverrideLegNotAsing();
                            return;
                        }

                        // 20170105 조승현 - [ 자체개선 ] LegIndex 변경된 이후 촬영구간 겹침 판단. ( LEG_ID 선택 and 광역 생성 시 )
                        if ((nOriginLegIndex + 1) != value) // LegIndex가 CalculateLegIndex보다 1이 작음. ( legindex는 인덱스고 calculateLegindex는 선택된 아이템이기때문 )
                        {

                            var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();

                            if (!isLoading)
                            {
                                // 20170810 조승현
                                if (this.m_SarSensorMode == SARSENSORMODE.SPOTGMTI)
                                {
                                    if (m_LegIndex != -1)
                                    {
                                        CaptureNumber = nOriginCaptureNum;
                                    }
                                }
                            }

                            if (!CheckingOverlapScene(this.SelectedLegID, this, this.GetBeginLegPer(), this.GetEndLegPer()))
                            {
                                // 20180503 조승현 - [GCS-TB-MPE-320] 촬영 자동 배치 후 이상현상 
                                var isAutoAsignOrder = EvtR_B.Instance("AutoAsignOrderStatus").Invoke();

                                // 자동배치 중일때 겹침발생하면 기존 겹침발생 시 취소하는 로직 적용.
                                if (isAutoAsignOrder)
                                {
                                    OverrideLegNotAsing();
                                    return;
                                }

                                //var reorderingstatus = EvtR_B.Instance("ReorderingStatus").Invoke();
                                //if (reorderingstatus)
                                //{
                                //    OverrideLegNotAsing();
                                //    return;
                                //}

                                // 20180820 조승현 - [자체개선] 임무항로점 삭제 시 촬영계획 재정렬 하면서 촬영계획 겹침 발생문제
                                // Reordering을 먼저 true로 해주고, Reordering중일때는 겹침발생 return하도록 조치.
                                //if (EvtR_B.Instance("GetIsReordering").Invoke())
                                //{
                                //    OverrideLegNotAsing();
                                //    return;
                                //}

                                // 협역일때..
                                // 20170214 조승현 - [ 결함#1929 ] B6 EO/IR 촬영계획 겹침 수정 필요
#if !BSix
                                if ((this.m_SensorModeEOIR == EOIRSENSORMODE.Narrow) || (this.m_SensorModeSAR == SARSENSORMODE.HR) || (this.m_SensorModeSAR == SARSENSORMODE.HR_SLC) ||
                                    (this.m_SensorModeSAR == SARSENSORMODE.SPOTGMTI))
#else
                        if ((this.m_SensorModeEOIR == EOIRSENSORMODE.Wide) || (this.m_SensorModeSAR == SARSENSORMODE.HR) || (this.m_SensorModeSAR == SARSENSORMODE.HR_SLC) || (this.m_SensorModeSAR == SARSENSORMODE.SPOTGMTI))
#endif
                                {
                                    // 20180914 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                    if (EvtR_B.Instance("MessageAddExistCheck").Invoke())
                                    {
                                        switch (EvtR_S.Instance("GetMessageAddPopupKey").Invoke())
                                        {
                                            case "SetCaptureSliderWindow":
                                                Evt_OS.Instance("SetCaptureSliderWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonWindow":
                                                Evt_OS.Instance("SetCapturePolygonWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonSliderWindow":
                                                Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Invoke(null, "No");
                                                break;
                                        }

                                        // 20181018 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                        Evt.Instance("RemoveMessageAdd").Invoke();

                                    }

                                    var msgInfo = new List<object> {MessageType.Alert, "경고"};

                                    Evt_OS.Instance("SetCaptureSliderWindowResult").Event += SetCaptureSliderWindowResultCallback;
                                    //Evt_I.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                                    // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle, "촬영구간이 겹쳐서 미세조정이 필요합니다. \n미세조정 화면으로 전환하시겠습니까?", null);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle, "다른 수집과 촬영구간 겹침이 발생하여 수동조정이 필요합니다. \n수동조정 하시겠습니까?", null);
                                    return;
                                }
                                // 광역일때..
#if !BSix
                                else if ((this.m_SensorModeEOIR == EOIRSENSORMODE.Wide) || (this.m_SensorModeSAR == SARSENSORMODE.ST) || (this.m_SensorModeSAR == SARSENSORMODE.WIDE) ||
                                         (this.m_SensorModeSAR == SARSENSORMODE.ST_SLC) || (this.m_SensorModeSAR == SARSENSORMODE.WIDE_SLC))
#else
                        else if ((this.m_SensorModeSAR == SARSENSORMODE.ST) || (this.m_SensorModeSAR == SARSENSORMODE.ST_SLC))
#endif
                                {
                                    // 20180914 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                    if (EvtR_B.Instance("MessageAddExistCheck").Invoke())
                                    {
                                        switch (EvtR_S.Instance("GetMessageAddPopupKey").Invoke())
                                        {
                                            case "SetCaptureSliderWindow":
                                                Evt_OS.Instance("SetCaptureSliderWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonWindow":
                                                Evt_OS.Instance("SetCapturePolygonWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonSliderWindow":
                                                Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Invoke(null, "No");
                                                break;
                                        }

                                        Evt.Instance("RemoveMessageAdd").Invoke();
                                    }

                                    var msgInfo = new List<object> {MessageType.Alert, "경고"};
                                    Evt_OS.Instance("SetCapturePolygonWindowResult").Event += SetCapturePolygonWindowResultCallback;
                                    //Evt_I.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                                    // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                    //    "촬영구간이 겹쳐서 Leg변경이나 광역 원본영역 변경이 필요합니다. \n광역 원본영역 변경 화면으로 전환하시겠습니까?", null);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                        "[광역 수집] 다른 수집과 촬영구간 겹침이 발생하여 LEG 변경 또는 광역 수집 원본영역 변경이 필요합니다. \n광역 원본영역을 변경하시겠습니까?", null);
                                    return;
                                }
#if BSix
                        else if(this.m_SensorModeEOIR == EOIRSENSORMODE.Narrow)
                        {
                            // 20180914 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                    if (EvtR_B.Instance("MessageAddExistCheck").Invoke())
                                    {
                                        switch (EvtR_S.Instance("GetMessageAddPopupKey").Invoke())
                                        {
                                            case "SetCaptureSliderWindow":
                                                Evt_OS.Instance("SetCaptureSliderWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonWindow":
                                                Evt_OS.Instance("SetCapturePolygonWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonSliderWindow":
                                                Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Invoke(null, "No");
                                                break;
                                        }

                                        // 20181018 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                        Evt.Instance("RemoveMessageAdd").Invoke();

                                    }

                                    var msgInfo = new List<object> {MessageType.Alert, "경고"};

                                    Evt_OS.Instance("SetCaptureSliderWindowResult").Event += SetCaptureSliderWindowResultCallback;
                                    //Evt_I.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                                    // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle, "촬영구간이 겹쳐서 미세조정이 필요합니다. \n미세조정 화면으로 전환하시겠습니까?", null);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SetCaptureSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle, "다른 수집과 촬영구간 겹침이 발생하여 수동조정이 필요합니다. \n수동조정 하시겠습니까?", null);
                                    return;
                        }
#endif
                                // GMTI 광역일때..
                                else if (this.m_SensorModeSAR == SARSENSORMODE.SCANGMTI)
                                {
                                    // 20180914 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                    if (EvtR_B.Instance("MessageAddExistCheck").Invoke())
                                    {
                                        switch (EvtR_S.Instance("GetMessageAddPopupKey").Invoke())
                                        {
                                            case "SetCaptureSliderWindow":
                                                Evt_OS.Instance("SetCaptureSliderWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonWindow":
                                                Evt_OS.Instance("SetCapturePolygonWindowResult").Invoke(null, "No");
                                                break;
                                            case "SetCapturePolygonSliderWindow":
                                                Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Invoke(null, "No");
                                                break;
                                        }

                                        Evt.Instance("RemoveMessageAdd").Invoke();
                                    }

                                    var msgInfo = new List<object> {MessageType.Alert, "경고"};
                                    Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Event += SetCapturePolygonSliderWindowResultCallback;
                                    //Evt_I.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                                    // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                    //    "촬영구간이 겹쳐서 Leg변경이나 광역 원본영역 변경 또는 미세조정이 필요합니다. \n수동설정 화면으로 전환하시겠습니까?", null);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                        "촬영구간 겹침이 발생하여 LEG 변경 또는 광역 원본영역 변경 또는 수동조정이 필요합니다. \n광역 수집 원본영역을 변경하시겠습니까?", null);
                                    return;
                                }
                            }

                            // 20170821 조승현 - GMTI 광역 촬영최소각도 체크
                            if (this.m_SensorModeSAR == SARSENSORMODE.SCANGMTI)
                            {
                                // 20180914 조승현 - [GCS-TB-MPE-191] 촬영계획 겹침 알림창 중복도시 시오류 보완
                                if (EvtR_B.Instance("MessageAddExistCheck").Invoke())
                                {
                                    switch (EvtR_S.Instance("GetMessageAddPopupKey").Invoke())
                                    {
                                        case "SetCaptureSliderWindow":
                                            Evt_OS.Instance("SetCaptureSliderWindowResult").Invoke(null, "No");
                                            break;
                                        case "SetCapturePolygonWindow":
                                            Evt_OS.Instance("SetCapturePolygonWindowResult").Invoke(null, "No");
                                            break;
                                        case "SetCapturePolygonSliderWindow":
                                            Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Invoke(null, "No");
                                            break;
                                    }

                                    Evt.Instance("RemoveMessageAdd").Invoke();
                                }

                                if (!CheckingMinCaptureAngle(this.GetStartAngle(), this.GetEndAngle()))
                                {
                                    var msgInfo = new List<object> {MessageType.Alert, "경고"};
                                    Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Event += SetCapturePolygonSliderWindowResultCallback;
                                    //Evt_I.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                                    // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                    //    "촬영구간이 겹쳐서 Leg변경이나 광역 원본영역 변경 또는 미세조정이 필요합니다. \n수동설정 화면으로 전환하시겠습니까?", null);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SetCapturePolygonSliderWindow", msgInfo, CEnum.MessageResultType.OkCancle,
                                        "촬영구간 겹침이 발생하여 LEG 변경 또는 광역 원본영역 변경 또는 수동조정이 필요합니다. \n광역 수집 원본영역을 변경하시겠습니까?", null);
                                    return;
                                }
                            }
                        }

                        // 20200709 조승현 - [자체개선] 수동조정 percentage 저장
                        this.InitialWindFlag = EvtR_B_S.Instance("GetEnableWindCorrection").Invoke(this.MissionKey);
                        this.InitialBeginPecent = this.GetBeginPer();

                        SetLegion();
                        SetNotifyDataScene();
                    }
                }
            }
        }


        private int m_LegIndex = -1;
        public int LegIndex
        {
            get { return m_LegIndex; }
            set
            {
                if (CommandControl.SystemType == SYSTEMTYPE.PpcEdit)
                {
                    var cWp = EvtR_O.Instance("GetCurrentWayPoint").Invoke() as DotWayPoint;
                    if (cWp != null && cWp.ObjectType == ObjType.FlightWayPoint)
                    {
                        if (EvtR_B_SI.Instance("GetIsContainFromNotEditableLegList").Invoke(cWp.Key, GetLegID(value)))
                        {
                            var msgInfo = new List<object> { MessageType.Alert, "알림" };
                            // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                            //Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, MessageResultType.None, "수정불가 구간의 수집을 선택할수 없습니다.", null);
                            Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, MessageResultType.None, "수정불가 LEG의 수집은 선택할 수 없습니다.", null);
                            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => NotifyPropertyChange("LegIndex")));
                            return;
                        }
                    }
                }
                nOriginCaptureNum = CaptureNumber;
                nOriginSelectedID = this.SelectedLegID;
                if (m_LegIndex != value)
                {
                    if (value == -1 && nOriginSelectedID > 0)
                    {
                        Evt_SI.Instance("RemoveSceneFromCollectionId").Invoke(MissionKey, m_CollectingID);
                        // 20200709 조승현 - [자체개선] 수동조정 percentage 저장
                        this.ManualPecent = 0.0;
                    }

                    nOriginLegIndex = m_LegIndex;
                    m_LegIndex = value;
                    SetLegion();
                }
                SetNotifyDataScene();
            }
        }

        private void SetNotifyDataScene()
        {
            if (this.SceneCollection.m_SceneNoDisplayData.Count > 0)
            {
                NotifyPropertyChange("LegIndex");
                NotifyPropertyChange("SelectedLegID");
                NotifyPropertyChange("StartCoordinateString");
                NotifyPropertyChange("EndCoordinateString");
                NotifyPropertyChange("Sight");
                NotifyPropertyChange("Spot");
                NotifyPropertyChange("ObserveWidth");
                NotifyPropertyChange("ObserveLength");
                SetFootPrintCoordinate();
                SetStartEndLineCoordinate();
                SetRealStartEndLineCoordinate();
            
                // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
                //SetCenterLineCoordinate();
    #if !BSix
                if ((this.SensorModeEOIR == EOIRSENSORMODE.Wide) || (this.SensorModeSAR == SARSENSORMODE.WIDE) || (this.SensorModeSAR == SARSENSORMODE.ST) || (this.SensorModeSAR == SARSENSORMODE.WIDE_SLC) || (this.SensorModeSAR == SARSENSORMODE.ST_SLC))
    #else
                    if (/*(this.SensorModeSAR == SARSENSORMODE.WIDE) || */(this.SensorModeSAR == SARSENSORMODE.ST) || (this.SensorModeSAR == SARSENSORMODE.ST_SLC))
    #endif
                {
                    SetCenterLineCoordinate(true);
                }
                else
                {
                    SetCenterLineCoordinate(false);
                }
            }

            Evt_OB.Instance("TargetImageChange").Invoke(EvtR_O_S.Instance("GetTargetElement").Invoke(this.BENum + this.m_Suffix), m_LegIndex != -1 ? true : false);
            /////wychoi/////
            Evt_OO.Instance("CreateSceneComplate").Invoke(m_SceneFootPrint, this, true);
            //Debug.WriteLine("촬영계획 SetNotifyDataScene 6 --------------------------- {0:yyyy-MM-dd HH:mm:ss.ff}", DateTime.Now);
            // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
            if (m_LegIndex == -1)
                EditType = SceneEditType.Editable;
            else
            {
                var cWp = EvtR_O.Instance("GetCurrentWayPoint").Invoke() as DotWayPoint;
                if (cWp != null && cWp.ObjectType == ObjType.FlightWayPoint)
                    Evt_SO.Instance("SetSceneEditable").Invoke(cWp.Key, this);
            }
        }

        // 20160109 조승현 - [ 자체개선 ] 촬영구간 겹칠 시 띄우는 미세조정 팝업에서 미세조정 후 취소 누를경우 처리
        private void ReturnOriginLeg(int nIndex, bool bFlag)
        {
            if (bFlag)
            {
                LegIndex = nIndex;
                //CalculateLegIndex = nIndex + 1;
                SetNotifyDataScene();
            }

            Evt_IB.Instance("ReturnOriginLeg").Event -= ReturnOriginLeg;
        }

        // 20180503 조승현 - [GCS-TB-MPE-320] 촬영 자동 배치 후 이상현상 
        private void OverrideLegNotAsing()
        {
            LegIndex = nOriginLegIndex;
            CaptureNumber = nOriginCaptureNum;
            //this.SelectedLegID = nOriginSelectedID;
            NotifyPropertyChange("LegIndex");
            NotifyPropertyChange("SelectedLegID");
            // 20170905 조승현 - 촬영계획 겹침 시 미세조정 취소하면 다시 미배치 색상도시.
            SetLegion();
            SetNotifyDataScene();
        }

        // 20170105 조승현 - [ 자체개선 ] LegIndex 변경된 이후 촬영구간 겹침 판단.
        private void SetCapturePolygonSliderWindowResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                // 변경할 LEG_ID 에 해당하는 광역 원본영역 변경을 한다.
                Evt_OBI.Instance("SetCapturePolygonWindow").Invoke(this, true, nOriginLegIndex);
                SetNotifyDataScene();

                Evt_IB.Instance("ReturnOriginLeg").Event -= ReturnOriginLeg;
                Evt_IB.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
            }
            else if (r.Equals("No"))
            {
                LegIndex = nOriginLegIndex;
                //m_LegIndex = -1;
                CaptureNumber = nOriginCaptureNum;
                //this.SelectedLegID = nOriginSelectedID;
                NotifyPropertyChange("LegIndex");
                NotifyPropertyChange("SelectedLegID");
                //CalculateLegIndex = nOriginLegIndex + 1;
                // 20170905 조승현 - 촬영계획 겹침 시 미세조정 취소하면 다시 미배치 색상도시.
                SetLegion();
                SetNotifyDataScene();
            }

            Evt_OS.Instance("SetCapturePolygonSliderWindowResult").Event -= SetCapturePolygonSliderWindowResultCallback;
        }

        // 20170105 조승현 - [ 자체개선 ] LegIndex 변경된 이후 촬영구간 겹침 판단.
        private void SetCapturePolygonWindowResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                // 변경할 LEG_ID 에 해당하는 광역 원본영역 변경을 한다.
                Evt_OBI.Instance("SetCapturePolygonWindow").Invoke(this, true, nOriginLegIndex);
                SetNotifyDataScene();

                Evt_IB.Instance("ReturnOriginLeg").Event -= ReturnOriginLeg;
                Evt_IB.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
            }
            else if (r.Equals("No"))
            {
                LegIndex = nOriginLegIndex;
                //m_LegIndex = -1;
                CaptureNumber = nOriginCaptureNum;
                //this.SelectedLegID = nOriginSelectedID;
                NotifyPropertyChange("LegIndex");
                NotifyPropertyChange("SelectedLegID");
                //CalculateLegIndex = nOriginLegIndex + 1;
                // 20170905 조승현 - 촬영계획 겹침 시 미세조정 취소하면 다시 미배치 색상도시.
                SetLegion();
                SetNotifyDataScene();
            }

            Evt_OS.Instance("SetCapturePolygonWindowResult").Event -= SetCapturePolygonWindowResultCallback;
        }

        // 20170105 조승현 - [ 자체개선 ] LegIndex 변경된 이후 촬영구간 겹침 판단.
        private void SetCaptureSliderWindowResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                // 변경할 LEG_ID 에 해당하는 Scene을 수동설정한다.
                Evt_SOBI.Instance("SetCaptureSliderWindow").Invoke(EvtR_S_O.Instance("SetTinyControlLegend").Invoke(this), this, true, nOriginLegIndex);

                SetNotifyDataScene();

                Evt_BB.Instance("SetTbOverlapState").Invoke(true, false);

                Evt_IB.Instance("ReturnOriginLeg").Event -= ReturnOriginLeg;
                Evt_IB.Instance("ReturnOriginLeg").Event += ReturnOriginLeg;
                //Evt_B.Instance("SetbdOverlapState").Invoke(false);
            }
            else if (r.Equals("No"))
            {
                LegIndex = nOriginLegIndex;
                //m_LegIndex = -1;
                CaptureNumber = nOriginCaptureNum;
                //this.SelectedLegID = nOriginSelectedID;
                NotifyPropertyChange("LegIndex");
                NotifyPropertyChange("SelectedLegID");
                //CalculateLegIndex = nOriginLegIndex + 1;
                // 20170905 조승현 - 촬영계획 겹침 시 미세조정 취소하면 다시 미배치 색상도시.
                SetLegion();
            }

            Evt_OS.Instance("SetCaptureSliderWindowResult").Event -= SetCaptureSliderWindowResultCallback;
        }

        // 20170216 조승현 - [ 자체개선 ] LegIndex 변경된 이후 촬영구간 겹쳐서 선택한 Leg에 배치할 수 없을때..
        private void SetNotPlaceResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                LegIndex = nOriginLegIndex;
                CaptureNumber = nOriginCaptureNum;
                //this.SelectedLegID = nOriginSelectedID;
                //CalculateLegIndex = nOriginLegIndex + 1;
                NotifyPropertyChange("LegIndex");
                NotifyPropertyChange("SelectedLegID");
            }

            Evt_OS.Instance("SetNotPlaceResult").Event -= SetNotPlaceResultCallback;
        }


        private bool isWide = false;
        public bool IsWide
        {
            get { return isWide; }
            set { isWide = value; NotifyPropertyChange("IsWide"); }
        }

        private bool CheckWide(EOIRSENSORMODE eoirMode, SARSENSORMODE sarMode)
        {
#if !BSix
            switch (eoirMode)
            {
                case EOIRSENSORMODE.Wide:
                    return true;
            }
#endif

#if !BSix
            switch (sarMode)
            {
                case SARSENSORMODE.ST:
                case SARSENSORMODE.ST_SLC:
                case SARSENSORMODE.WIDE:
                case SARSENSORMODE.WIDE_SLC:
                case SARSENSORMODE.SCANGMTI:
                    return true;
            }
#else
            switch (sarMode)
            {
                case SARSENSORMODE.ST:
                case SARSENSORMODE.ST_SLC:
                //case SARSENSORMODE.WIDE:
                case SARSENSORMODE.SCANGMTI:
                    return true;
            }
#endif
            return false;
        }

        private bool isOrigin = false;
        public bool IsOrigin
        {
            get { return isOrigin; }
            set { isOrigin = value; NotifyPropertyChange("IsOrigin"); }
        }

        private bool m_IsCheck = false; // 체크 여부
        public bool IsCheck
        {
            get { return m_IsCheck; }
            set
            {
                // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
                if (EditType == SceneEditType.NotEditable ||
                    shootingState == SceneShootingState.Holding ||
                    shootingState == SceneShootingState.Complete ||
                    shootingState == SceneShootingState.Fail)
                {
                    m_IsCheck = false;
                }
                else
                {
                    m_IsCheck = value;
                }

                NotifyPropertyChange("IsCheck");
            }
        }

        private bool m_IsPointCapture = false; // 초점보정촬영 여부
        public bool IsPointCapture
        {
            get { return m_IsPointCapture; }
            set { m_IsPointCapture = value; NotifyPropertyChange("IsPointCapture"); }
        }

        private string m_BENum = "";	// BeNumber
        public string BENum
        {
            get { return m_BENum; }
            set { m_BENum = value; }
        }

        private string m_Name = ""; // 표적명
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private CoordinateModel m_TargetCoordinate = null;
        public CoordinateModel TargetCoordinate
        {
            get { return m_TargetCoordinate; }
            set { m_TargetCoordinate = value; }
        }

        private int m_CollectingID = -1;  // 수집 아이디
        public int CollectingID
        {
            get { return m_CollectingID; }
            set
            {
                m_CollectingID = value;
                this.SetFootprintName();
                this.IsWide = this.CheckWide(this.SensorModeEOIR, this.SensorModeSAR);
            }
        }

        private int m_PIR = -1; // 우선순위
        public int PIR
        {
            get { return m_PIR; }
            set
            {
                if (value < -1)
                {
                    m_PIR = -1;
                    var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                    if (!isLoading)
                    {
                        var msgInfo = new List<object> { MessageType.Alert, "알림" };
                        Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, CEnum.MessageResultType.None, "우선순위 최소값은 -1 입니다.", null);
                    }
                }
                else
                {
                    m_PIR = value;
                }
            }
        }

        // 20181029 조승현 - [자체개선] 자동배치 개념을 수집체크 개념으로 이양
        //private bool m_AutoBatch = false;   // 자동 배치
        //public bool AutoBatch
        //{
        //    get { return m_AutoBatch; }
        //    set
        //    {
        //        // 20160823 [김형철][자체개선 133] 실시간 촬영계획 수정 기능 구현
        //        if (EditType == SceneEditType.Editable &&
        //            shootingState != SceneShootingState.Holding)
        //        {
        //            m_AutoBatch = value;
        //        }
        //        NotifyPropertyChange("AutoBatch");
        //    }
        //}

        // SHJO_20160621 : 수동설정 활성화/비활성화
        //        private bool m_EOIRTinyControlEnable = false;
        //        public bool EOIRTinyControlEnable
        //        {
        //            get 
        //            {
        //                // SHJO_20160621 : Muav에선 협역, B6에선 좌표지향일때 수동설정 버튼 활성화
        //#if !BSix
        //                if (m_SensorModeEOIR == EOIRSENSORMODE.Narrow)
        //#else
        //                if (m_SensorModeEOIR != EOIRSENSORMODE.Narrow)
        //#endif
        //                {
        //                    m_EOIRTinyControlEnable = true;
        //                }
        //                else
        //                {
        //                    m_EOIRTinyControlEnable = false;
        //                }
        //                return m_EOIRTinyControlEnable; 
        //            }
        //            set { m_EOIRTinyControlEnable = value; NotifyPropertyChange("EOIRTinyControlEnable"); }
        //        }

        //private bool m_SARTinyControlEnable = false;
        //public bool SARTinyControlEnable
        //{
        //    get
        //    {
        //        if ((m_SensorModeSAR == SARSENSORMODE.HR) || (m_SensorModeSAR == SARSENSORMODE.SPOTGMTI))
        //        {
        //            m_SARTinyControlEnable = true;
        //        }
        //        else
        //        {
        //            m_SARTinyControlEnable = false;
        //        }
        //        return m_SARTinyControlEnable;
        //    }
        //    set { m_SARTinyControlEnable = value; NotifyPropertyChange("SARTinyControlEnable"); }
        //}

        // 20151207 Add by Jun
        private int m_CaptureNumber = 8;  // 수집 횟수
        private int m_CaptureTime = 20; // 시간
        public int CaptureNumber
        {
            get
            {
                if ((m_SensorType == SENSORTYPE.EO) || (m_SensorType == SENSORTYPE.IR) || (m_SensorType == SENSORTYPE.EOIR))
                {
                    return m_CaptureTime;
                }
                else
                {
                    if (m_SarSensorMode == SARSENSORMODE.SPOTGMTI)
                    {
                        return m_CaptureNumber;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            set
            {
                // SHJO_20160620 : 촬영계획 수정( EO/IR탭에서는 시간값 적용 )
                if ((m_SensorType == SENSORTYPE.EO) || (m_SensorType == SENSORTYPE.IR) || (m_SensorType == SENSORTYPE.EOIR))
                {
                    // 20171128 조승현 - [GCS-TB-MPE-203] EO좌표지향 촬영 시간 입력 제한
                    if (m_CaptureTime != -1 && 1 <= value && value <= 2000)
                    {
                        var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                        if (!isLoading)
                        {
                            //20170921 Add by Jun
                            if (m_CaptureTime != value)
                            {
                                var msgInfo = new List<object> { MessageType.Alert, "경고" };
                                Evt_OS.Instance("CaptureTimeChangeMessageResult").Event -= CaptureTimeChangeMessageResultCallback;
                                Evt_OS.Instance("CaptureTimeChangeMessageResult").Event += CaptureTimeChangeMessageResultCallback;
                                // 20220809 최진경 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                //Evt_OOOOO.Instance("MessageAdd").Invoke("CaptureTimeChangeMessage", msgInfo, CEnum.MessageResultType.OkCancle, "시간 변경으로 촬영구간이 변경됩니다. \n수집을 초기화 하시겠습니까?", (object)value);
                                Evt_OOOOO.Instance("MessageAdd").Invoke("CaptureTimeChangeMessage", msgInfo, CEnum.MessageResultType.OkCancle, "EO/IR 수집 시간 변경으로 인해 촬영구간이 변경됩니다. \n수집을 초기화 하시겠습니까?", (object)value);
                                //CheckingAsync();

                                //if (bOkflag)
                                //    m_CaptureNumber = value;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            m_CaptureTime = value;
                        }
                    }
                    else
                    {
                        var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                        if (!isLoading)
                        {
                            var msgInfo = new List<object> { MessageType.Alert, "알림" };
                            Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, MessageResultType.None, "시간(초) 범위를 확인해주세요. [1~2000]", null);
                        }
                    }
                }
                else
                {
                    if (m_SarSensorMode == SARSENSORMODE.SPOTGMTI)
                    {
                        int nResult = 84;
                        if (m_LegIndex != -1)
                        {
                            nResult = EvtR_I_I.Instance("GetCaptureNumFromSwathNum").Invoke((int)this.SceneCollection.GetSwathMaxNo(m_LegIndex));
                        }
                        else
                        {
                            if ((value >= 8) && (value <= 84))
                            {
                                m_CaptureNumber = value;
                            }
                        }

                        if (value < 8)
                        {
                            m_CaptureNumber = 8;
                            var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                            if (!isLoading)
                            {
                                var msgInfo = new List<object> { MessageType.Alert, "알림" };
                                // 20220629 조광현 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                //Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, CEnum.MessageResultType.None, "GMTI 최소 수집 횟수는 8회 입니다.", null);
                                Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, CEnum.MessageResultType.None, "GMTI 재스캔 횟수 최소값은 8회 입니다.", null);
                                
                                // 20181205 조승현 - [GCS-TB-MPE-466] 배치된 GMTI집중 수집 스캔 회수 최대값 수정 시 오류 
                                this.LegIndex = -1;
                                SetLegion();
                            }
                        }
                        //else if (value > 84)
                        else if (value > nResult)
                        {
                            m_CaptureNumber = nResult;
                            //m_CaptureNumber = 84;
                            var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                            if (!isLoading)
                            {
                                var msgInfo = new List<object> { MessageType.Alert, "알림" };
                                // 20220629 조광현 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                //Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, CEnum.MessageResultType.None, "GMTI 최대 수집 횟수는 " + m_CaptureNumber + "회 입니다.", null);
                                Evt_OOOOO.Instance("MessageAdd").Invoke("", msgInfo, CEnum.MessageResultType.None, "GMTI 재스캔 횟수 최대값은 " + m_CaptureNumber + "회 입니다.", null);
                                
                                // 20181205 조승현 - [GCS-TB-MPE-466] 배치된 GMTI집중 수집 스캔 회수 최대값 수정 시 오류 
                                this.LegIndex = -1;
                                SetLegion();
                            }
                        }
                        else
                        {
                            var isLoading = EvtR_B.Instance("XmlMissionLoadingStatus").Invoke();
                            if (!isLoading)
                            {
                                //20170921 Add by Jun
                                if (m_CaptureNumber != value && m_LegIndex != -1)
                                {
                                    var msgInfo = new List<object> { MessageType.Alert, "경고" };
                                    Evt_OS.Instance("SceneDeleteMessageResult").Event -= SceneDeleteMessageResultCallback;
                                    Evt_OS.Instance("SceneDeleteMessageResult").Event += SceneDeleteMessageResultCallback;
                                    // 20220629 조광현 [GCS-TB-MPE-450] MPE 내 알림 문구, 각 팝업창 별 경고 알림 문구 검토
                                    //Evt_OOOOO.Instance("MessageAdd").Invoke("SceneDeleteMessage", msgInfo, CEnum.MessageResultType.OkCancle, "횟수 변경으로 촬영구간이 변경됩니다. \n수집을 초기화 하시겠습니까?", (object)value);
                                    Evt_OOOOO.Instance("MessageAdd").Invoke("SceneDeleteMessage", msgInfo, CEnum.MessageResultType.OkCancle, "GMTI 재스캔 횟수 변경으로 인해 촬영구간이 변경됩니다. \n수집을 초기화 하시겠습니까?", (object)value);
                                    //CheckingAsync();

                                    //if (bOkflag)
                                    //    m_CaptureNumber = value;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                m_CaptureNumber = value;
                            }
                        }
                    }
                    //else
                    //{
                    //    m_CaptureNumber = value;
                    //}
                }
                NotifyPropertyChange("CaptureNumber");  // 변경 후 알림.
            }
        }

        // 20171128 조승현 - [GCS-TB-MPE-203] EO좌표지향 촬영 시간 입력 제한
        private void CaptureTimeChangeMessageResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                this.LegIndex = -1;
                this.Clear();
                this.SetVisible(false);
                //bOkflag = true;
                SetLegion();
                m_CaptureTime = (int)o;
                NotifyPropertyChange("CaptureNumber");  // 변경 후 알림.
            }

            // 20180124 조승현 - [자체개선] 이벤트 삭제 오류
            Evt_OS.Instance("CaptureTimeChangeMessageResult").Event -= CaptureTimeChangeMessageResultCallback;
        }

        //bool bOkflag = false;

        private void SceneDeleteMessageResultCallback(object o, string r)
        {
            if (r.Equals("Ok"))
            {
                this.LegIndex = -1;
                this.Clear();
                this.SetVisible(false);
                //bOkflag = true;
                SetLegion();
                m_CaptureNumber = (int)o;
                NotifyPropertyChange("CaptureNumber");  // 변경 후 알림.
            }
            else
            {
                //bOkflag = false; 
            }
            Evt_OS.Instance("SceneDeleteMessageResult").Event -= SceneDeleteMessageResultCallback;
        }

        //private async void CheckingAsync()
        //{
        //    var task = Task.Run(() => CheckingSelect());
        //    await task;
        //}

        //private void CheckingSelect()
        //{
        //    while (true)
        //    {
        //        if (bOkflag)
        //            break;
        //    }
        //}

        //public int CaptureNumber
        //{
        //    get
        //    {
        //        if (LegIndex == -1 || SceneCollection.GetCount() < LegIndex) return -1;
        //        return SceneCollection.Get_SAR_SPOTGMTI_CONUT(LegIndex);
        //    }
        //}

        public float ObserveLength
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return -1;
                return SceneCollection.GetObserveLength(0);
            }
        }

        // 20180919 조승현 - [GCS-TB-MPE-370] 촬영계획 창에서 촬영시작시간으로 Sorting위해 추가
        public double CaptureBeginTime
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return -1;
                return SceneCollection.GetCaptureBeginTime(0);
            }
        }

        // 20200709 조승현 - [자체개선] 수동조정 percentage 저장
        private bool m_InitialWindFlag = false;
        public bool InitialWindFlag
        {
            get
            {
                return m_InitialWindFlag;
            }
            set
            {
                m_InitialWindFlag = value;
            }
        }

        private double m_InitialBeginPecent = 0.0;
        public double InitialBeginPecent
        {
            get
            {
                return m_InitialBeginPecent;
            }
            set
            {
                m_InitialBeginPecent = value;
            }
        }

        private double m_ManualPecent = 0.0;
        public double ManualPecent
        {
            get
            {
                return m_ManualPecent;
            }
            set
            {
                m_ManualPecent = value;
            }
        }

        private double m_NoWindPercent = 0.0;
        public double NoWindPecent
        {
            get
            {
                return m_NoWindPercent;
            }
            set
            {
                m_NoWindPercent = value;
            }
        }

        private double m_WindPercent = 0.0;
        public double WindPecent
        {
            get
            {
                return m_WindPercent;
            }
            set
            {
                m_WindPercent = value;
            }
        }


        public float ObserveWidth
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return -1;
                return SceneCollection.GetObserveWidth(0);
            }
        }

        public string StartCoordinateString
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return "";
                return SceneCollection.GetStartPositionString(0);
            }
        }

        public string EndCoordinateString
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return "";
                return SceneCollection.GetEndPositionString(0);
            }
        }

        public float Sight
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return -1;
                return SceneCollection.GetSight(0);
            }
        }

        public int Spot
        {
            get
            {
                if (LegIndex == -1 || SceneCollection.GetCount() < 1) return -1;
                return SceneCollection.GetSpot(0);
            }
        }


        private TARGETDIVISION m_TargetDivision = TARGETDIVISION.Emergency; // 표적 구분
        public TARGETDIVISION TargetDivision
        {
            get { return m_TargetDivision; }
            set { m_TargetDivision = value; }
        }

        private EOIRSENSORTYPE m_SensorTypeEOIR = EOIRSENSORTYPE.None;	// EOIR SensorType
        public EOIRSENSORTYPE SensorTypeEOIR
        {
            get { return m_SensorTypeEOIR; }
            set
            {
                m_SensorTypeEOIR = value;
                if (m_SensorTypeEOIR == EOIRSENSORTYPE.None)
                    SensorModeEOIR = EOIRSENSORMODE.None;
                NotifyPropertyChange("SensorTypeEOIR");
                Evt.Instance("UpdateEoIrDataGrid").Invoke();
            }
        }

        private EOIRSENSORMODE m_SensorModeEOIR = EOIRSENSORMODE.None;  // EOIR SensorMode
        public EOIRSENSORMODE SensorModeEOIR
        {
            get { return m_SensorModeEOIR; }
            set
            {
                m_SensorModeEOIR = value;
                if (value == EOIRSENSORMODE.Narrow) m_CaptureTime = -1;
                else
                {
                    // 20190910 조승현 - [GCS-TB-MPE-527] EO광역 시간 입력 시 촬영 미배치 전환. 차단 필요
#if !BSix
                    m_CaptureTime = -1;
#else
                    // 20200417 조승현 - [GCS-TB-MPE-646] 좌표지향 시간 20초 도시 오류
                    // 임무 로드 시 저장된 CaptureNumber값이 아닌 20으로 불러오는 문제 수정.
                    m_CaptureTime = CaptureNumber;
                    //m_CaptureTime = 20;
#endif
                }
                NotifyPropertyChange("SensorModeEOIR");
                this.SetFootprintName();
                this.IsWide = this.CheckWide(this.SensorModeEOIR, this.SensorModeSAR);
                Evt.Instance("UpdateEoIrDataGrid").Invoke();
            }
        }

        // 20180730 [김형철][GCS-TB-MPE-207] FOV None 옵션 제거
        private EOIRFOV m_Fov = EOIRFOV.NFOV;
        public EOIRFOV Fov
        {
            get { return m_Fov; }
            set
            {
                if (value == 0)
                {
                    m_Fov = EOIRFOV.NFOV;
                }
                else
                {
                    m_Fov = value;
                }

                NotifyPropertyChange("Fov");
            }
        }

        private SARSENSORTYPE m_SensorTypeSAR = SARSENSORTYPE.None;   // SAR SensorType
        public SARSENSORTYPE SensorTypeSAR
        {
            get { return m_SensorTypeSAR; }
            set
            {
                m_SensorTypeSAR = value;
                if (m_SensorTypeSAR == SARSENSORTYPE.None)
                    SensorModeSAR = SARSENSORMODE.None;
                NotifyPropertyChange("SensorTypeSAR");
            }
        }

        private SARSENSORMODE m_SensorModeSAR = SARSENSORMODE.None; // SAR SensorMode
        public SARSENSORMODE SensorModeSAR
        {
            get { return m_SensorModeSAR; }
            set
            {
                m_SensorModeSAR = value;
                NotifyPropertyChange("SensorModeSAR");
                this.SetFootprintName();
                this.IsWide = this.CheckWide(this.SensorModeEOIR, this.SensorModeSAR);
                // 20170202 조승현 - [ 자체개선 ] SAR 모드 변경 시 spotgmti일때 횟수 표시, 이외에는 N/A
                if (value == SARSENSORMODE.SPOTGMTI)
                {
                    if (m_CaptureNumber < 8)
                    {
                        m_CaptureNumber = 8;
                    }

                    if (m_CaptureNumber > 84)
                    {
                        m_CaptureNumber = 84;
                    }

                    m_sarsensorMode = value;
                    NotifyPropertyChange("m_SarSensorMode");
                    NotifyPropertyChange("CaptureNumber");
                }
                else
                {
                    m_CaptureNumber = 0;
                    m_sarsensorMode = value;
                    NotifyPropertyChange("m_SarSensorMode");
                    NotifyPropertyChange("CaptureNumber");
                }

            }
        }

        private int time = 0;
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChange("Time");
            }
        }

        public string UiName { get; set; }

        public void SetFootprintName()
        {
            if (this.m_SceneFootPrint.GetMapObject() == null) return;

            var colId = CollectingID.ToString();
            string mode;

            if (this.m_SensorType != SENSORTYPE.EOIR && this.m_SensorType != SENSORTYPE.EO && this.m_SensorType != SENSORTYPE.IR)
            {
                mode = EnumTypeConverter.GetEnumDescription(this.SensorModeSAR);
                //mode = this.SensorModeSAR.ToString();
            }
            else
            {
                mode = EnumTypeConverter.GetEnumDescription(this.SensorModeEOIR);
                //mode = this.SensorModeEOIR.ToString();
            }

            string name = string.Format("{0} ({1})", mode, colId);

            this.m_SceneFootPrint.SetText(name);
            //this.UiName =  name;
            // 20180122 조승현 - [GCS-TB-MPE-264] 촬영계획 트리 추가정보 도시
            string strmode = "";

            if (this.m_SensorType != SENSORTYPE.EOIR && this.m_SensorType != SENSORTYPE.EO && this.m_SensorType != SENSORTYPE.IR)
            {
                switch (this.SensorModeSAR)
                {
                    case SARSENSORMODE.HR:
                    case SARSENSORMODE.HR_SLC:
                        strmode = "고해상";
                        break;
                    case SARSENSORMODE.ST:
                    case SARSENSORMODE.ST_SLC:
                        strmode = "표준";
                        break;
#if !BSix
                    case SARSENSORMODE.WIDE:
                    case SARSENSORMODE.WIDE_SLC:
                        strmode = "광역";
                        break;
#endif
                    case SARSENSORMODE.SCANGMTI:
                        strmode = "GMTI광역";
                        break;
                    case SARSENSORMODE.SPOTGMTI:
                        strmode = "GMTI집중";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.SensorModeEOIR)
                {
                    case EOIRSENSORMODE.Narrow:
#if BSix
                        strmode = "지역";
#else
                        strmode = "협역";
#endif
                        break;
                    case EOIRSENSORMODE.Wide:
#if BSix
                        strmode = "좌표";
#else
                        strmode = "광역";
#endif
                        break;
                    default:
                        break;
                }
            }

            this.UiName = string.Format("{0}({1})-{2}", strmode, colId, this.m_Name);

            SetFootprintColor();
        }

        public void Clear()
        {
            this.ClearLegID();
            this.ClearSight();
            this.ClearSpot();
            this.ClearNodisplayData();
            this.ClearObserveLength();
            this.ClearObserveWidth();
            this.ClearContainTargetList();
            this.ClearPerperdicular();
            this.ListErrorMessage.Clear();
        }

        // 20160818 [김형철][자체개선] 임무항로점 추가/삭제/수정에 따른 촬영계획 수정
        public void RemoveData(int legIndex)
        {
            if (legIndex == -1) return;
            var selectedLegId = this.SelectedLegID;

            this.RemoveLegID(legIndex);
            this.RemoveSight(legIndex);
            this.RemoveSpot(legIndex);
            this.RemoveNoDisplayData(legIndex);
            this.RemoveObserveLength(legIndex);
            this.RemoveObserveWidth(legIndex);
            this.RemoveContainTargetList(legIndex);
            this.RemovePerperdicular(legIndex);

            this.LegIndex = this.GetLegIndex(selectedLegId); // 20180219 형철선임한테 문의..
        }

        public void ClearContainTargetList()
        {
            this.SceneCollection.ContainTargetList.Clear();
        }

        // 20181106
        public void Clearglobal_leg(object obj)
        {
            //            // 20181119 조승현 - [자체개선] this.SceneCollection.LegID2에 한번만 설정해주면 모든 Scene에서 다같이 사용하므로
            //            // 불필요하게 모든 Scene마다 다 돌필요 없음.
            //            if (this.SceneCollection.LegID2.Count == (obj as List<S_LEG_REGION>).Count) return;
            //
            //            this.SceneCollection.LegID2.Clear();
            //            foreach (S_LEG_REGION slr in obj as List<S_LEG_REGION>)
            //            {
            //                this.SceneCollection.LegID2.Add(slr.leg_number);
            //            }
        }

        public void ClearLegID()
        {
            this.SceneCollection.LegID.Clear();
            //this.SceneCollection.UILegID.Clear(); // 20160120 Add by Jun

            SetLegion();
        }

        public void ClearObserveLength()
        {
            this.SceneCollection.ObserveLength.Clear();
        }

        public void ClearObserveWidth()
        {
            this.SceneCollection.ObserveWidth.Clear();
        }

        public void ClearSight()
        {
            this.SceneCollection.Sight.Clear();
        }

        public void ClearSpot()
        {
            this.SceneCollection.Spot.Clear();
        }

        public void ClearPerperdicular()
        {
            this.SceneCollection.Perperdicular.Clear();
        }

        public void ClearNodisplayData()
        {
            this.SceneCollection.m_SceneNoDisplayData.Clear();
        }

        // 20190305 조승현 - 데드코드 처리.
        //public int GetLegCount()
        //{
        //    return this.SceneCollection.GetCount();
        //}

        public void AddLegID(int LegID)
        {
            this.SceneCollection.LegID.Add(LegID);
            // 20151217 Add by Jun 레그번호 0번 부터 시작하는 것을 도시용으로 +1 함.
            //this.SceneCollection.UILegID.Add(LegID + 1);
            SetLegion();
        }

        public void ChangeLegID(int oldLegId, int newLegId)
        {
            this.SceneCollection.LegID.Remove(oldLegId);
            this.SceneCollection.LegID.Add(newLegId);
            // 20181107 조승현
            //this.LegIndex = newLegId-1;
            //foreach (var item in this.SceneCollection.LegID)
            //{
            //    if (item == oldLegId)
            //    {
            //        item = newLegId;
            //    }
            //}
        }

        public void RemoveLegId(int removeLegId)
        {
            this.SceneCollection.LegID.Remove(removeLegId);
        }

        // 20170607 김형철 데드코드 삭제
        //public void InsertLegID(int index, int LegID)
        //{
        //    this.SceneCollection.LegID.Insert(index, LegID);
        //    SetLegion();
        //}

        public void AddContainTargetList(List<string> containTargetList)
        {
            this.SceneCollection.ContainTargetList.Add(containTargetList);
        }

        public void InsertContainTargetList(int index, List<string> containTargetList)
        {
            this.SceneCollection.ContainTargetList.Insert(index, containTargetList);
        }

        public void RemoveContainTargetList(int index)
        {
            if (this.SceneCollection.ContainTargetList.Count > index)
                this.SceneCollection.ContainTargetList.RemoveAt(index);
        }

        public void RemoveLegID(int index)
        {
            if (this.SceneCollection.LegID.Count > index)
            {
                this.SceneCollection.LegID.RemoveAt(index);
                SetLegion();
            }
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveLegID(int LegID)
        //{
        //    this.SceneCollection.LegID.Remove(LegID);
        //    SetLegion();
        //}

        public void AddObserveWidth(float ObserveWidth)
        {
            this.SceneCollection.ObserveWidth.Add(ObserveWidth);
        }

        public void InsertObserveWidth(int index, float ObserveWidth)
        {
            this.SceneCollection.ObserveWidth.Insert(index, ObserveWidth);
        }

        public void RemoveObserveWidth(int index)
        {
            if (this.SceneCollection.ObserveWidth.Count > index)
                this.SceneCollection.ObserveWidth.RemoveAt(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveObserveWidth(float ObserveWidth)
        //{
        //    this.SceneCollection.ObserveWidth.Remove(ObserveWidth);
        //}

        public void AddObserveLength(float ObserveLength)
        {
            this.SceneCollection.ObserveLength.Add(ObserveLength);
        }

        public void InsertObserveLength(int index, float ObserveLength)
        {
            this.SceneCollection.ObserveLength.Insert(index, ObserveLength);
        }

        public void RemoveObserveLength(int index)
        {
            if (this.SceneCollection.ObserveLength.Count > index)
                this.SceneCollection.ObserveLength.RemoveAt(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveObserveLength(float ObserveLength)
        //{
        //    this.SceneCollection.ObserveLength.Remove(ObserveLength);
        //}

        public void AddSight(float Sight)
        {
            this.SceneCollection.Sight.Add(Sight);
        }

        public void InsertSight(int index, float Sight)
        {
            this.SceneCollection.Sight.Insert(index, Sight);
        }

        public void RemoveSight(int index)
        {
            if (this.SceneCollection.Sight.Count > index)
                this.SceneCollection.Sight.RemoveAt(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveSight(float Sight)
        //{
        //    this.SceneCollection.Sight.Remove(Sight);
        //}

        public void AddSpot(int Spot)
        {
            this.SceneCollection.Spot.Add(Spot);
        }

        public void AddPerperdicular(bool perperdicular)
        {
            this.SceneCollection.Perperdicular.Add(perperdicular);
        }

        public void InsertSpot(int index, int Spot)
        {
            this.SceneCollection.Spot.Insert(index, Spot);
        }

        // 20170607 김형철 데드코드 삭제
        //public void InsertPerperdicular(int index, bool perperdicular)
        //{
        //    this.SceneCollection.Perperdicular.Insert(index, perperdicular);
        //}

        public void RemoveSpot(int index)
        {
            if (this.SceneCollection.Spot.Count > index)
                this.SceneCollection.Spot.RemoveAt(index);
        }

        public void RemovePerperdicular(int index)
        {
            if (this.SceneCollection.Perperdicular.Count > index)
                this.SceneCollection.Perperdicular.RemoveAt(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveSpot(int Spot)
        //{
        //    this.SceneCollection.Spot.Remove(Spot);
        //}

        public void AddNoDisplayData(SceneNoDisplayData data)
        {
            this.SceneCollection.m_SceneNoDisplayData.Add(data);
        }

        public void InsertNoDisplayData(int index, SceneNoDisplayData data)
        {
            this.SceneCollection.m_SceneNoDisplayData.Insert(index, data);
        }

        public void RemoveNoDisplayData(int index)
        {
            if (this.SceneCollection.m_SceneNoDisplayData.Count > index)
                this.SceneCollection.m_SceneNoDisplayData.RemoveAt(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public void DataRemoveNoDisplayData(SceneNoDisplayData data)
        //{
        //    this.SceneCollection.m_SceneNoDisplayData.Remove(data);
        //}

        //20151221 Add by Jun
        //public int GetUILegID()
        //{
        //    return SceneCollection.GetUILegID(this.LegIndex);
        //}

        public int GetLegID()
        {
            return SceneCollection.GetLegID(this.LegIndex);
        }

        // 20160818 [김형철][자체개선] 임무항로점 추가/삭제/수정에 따른 촬영계획 수정
        public int GetLegIndex(int legId)
        {
            int value = legId - 1;
            foreach (var leg in SceneCollectionProperty.LegID2)
            {
                if ((legId - 1).Equals(leg))
                {
                    value = leg;
                    break;
                }
            }
            return value;
            //return SceneCollectionProperty.LegID2.IndexOf(legId);
        }

        public int GetLegID(int index)
        {
            return SceneCollection.GetLegID(index);
        }

        // 20170607 김형철 데드코드 삭제
        //public int GetSpot()
        //{
        //    return SceneCollection.GetSpot(this.LegIndex);
        //}

        // 20170607 김형철 데드코드 삭제
        //public float GetSight()
        //{
        //    return SceneCollection.GetSight(this.LegIndex);
        //}

        // 20170607 김형철 데드코드 삭제
        //public SceneNoDisplayData GetNoDisplayData()
        //{
        //    return SceneCollection.GetNoDisplayData(this.LegIndex);
        //}

        // 20170607 김형철 데드코드 삭제
        //public CCoordinateList<double> GetVisible(int Index)
        //{
        //    return SceneCollection.GetVisible(Index);
        //}

        public CCoordinateList<double> GetVisible()
        {
            return SceneCollection.GetVisible(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public List<double> GetVisibility(int Index)
        //{
        //    return SceneCollection.GetVisibility(Index);
        //}

        public List<double> GetVisibility()
        {
            return SceneCollection.GetVisibility(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetBeginPer(int Index)
        //{
        //    return SceneCollection.GetBeginPer(Index);
        //}

        public double GetBeginPer()
        {
            return SceneCollection.GetBeginPer(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
        //public double GetBeginLegPer(int Index)
        //{
        //    return SceneCollection.GetBeginLegPer(Index);
        //}

        public double GetBeginLegPer()
        {
            return SceneCollection.GetBeginLegPer(this.LegIndex);
        }
        
        public double GetBeginTime()
        {
            return SceneCollection.GetBeginTime(this.LegIndex);
        }

        // 20170821 조승현 GMTI 집중 전방/측방 선택을 위한 카메라모드 설정
        public E_CAMERA_MODE GetCameraMode()
        {
            return SceneCollection.GetCameraMode(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetEndPer(int Index)
        //{
        //    return SceneCollection.GetEndPer(Index);
        //}

        public double GetEndPer()
        {
            return SceneCollection.GetEndPer(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        // 20170106 조승현 - [ 결함#1748 ] 미세조정 후 촬영구간 겹침 알림 결함
        //public double GetEndLegPer(int Index)
        //{
        //    return SceneCollection.GetEndLegPer(Index);
        //}

        public double GetEndLegPer()
        {
            return SceneCollection.GetEndLegPer(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetCenterSlantRange(int Index)
        //{
        //    return SceneCollection.GetCenterSlantRange(Index);
        //}

        public double GetCenterSlantRange()
        {
            return SceneCollection.GetCenterSlantRange(this.LegIndex);
        }

        // 20190514 조승현 - [자체개선] slantrange 도시 기준 변경.
        public double GetStartCenterSlantRange()
        {
            return SceneCollection.GetStartCenterSlantRange(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetNearSlantRange(int Index)
        //{
        //    return SceneCollection.GetNearSlantRange(Index);
        //}

        // 20170607 김형철 데드코드 삭제
        //public double GetNearSlantRange()
        //{
        //    return SceneCollection.GetNearSlantRange(this.LegIndex);
        //}

        // 20170607 김형철 데드코드 삭제
        //public double GetFarSlantRange(int Index)
        //{
        //    return SceneCollection.GetFarSlantRange(Index);
        //}

        // 20170607 김형철 데드코드 삭제
        //public double GetFarSlantRange()
        //{
        //    return SceneCollection.GetFarSlantRange(this.LegIndex);
        //}

        // 20170607 김형철 데드코드 삭제
        //public double GetStartAngle(int Index)
        //{
        //    return SceneCollection.GetStartAngle(Index);
        //}

        // 20170607 김형철 데드코드 삭제
        // 20170821 조승현 - GMTI 광역 촬영최소각도 체크
        public double GetStartAngle()
        {
            return SceneCollection.GetStartAngle(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetEndAngle(int Index)
        //{
        //    return SceneCollection.GetEndAngle(Index);
        //}

        // 20170607 김형철 데드코드 삭제
        // 20170821 조승현 - GMTI 광역 촬영최소각도 체크
        public double GetEndAngle()
        {
            return SceneCollection.GetEndAngle(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetPitch(int Index)
        //{
        //    return SceneCollection.GetPitch(Index);
        //}

        public double GetPitch()
        {
            return SceneCollection.GetPitch(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        // 20161207 조승현 - [ 결함#1630 ] 촬영계획 세부정보 및 미세정보 창 내용 수정 필요
        //public double GetHeading(int Index)
        //{
        //    return SceneCollection.GetHeading(Index);
        //}

        public double GetHeading()
        {
            return SceneCollection.GetHeading(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public double GetRoll(int Index)
        //{
        //    return SceneCollection.GetRoll(Index);
        //}

        public double GetRoll()
        {
            return SceneCollection.GetRoll(this.LegIndex);
        }

        public CCoordinateList<double> GetStartPosition()
        {
            return SceneCollection.GetStartPosition(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public CCoordinateList<double> GetStartPosition(int Index)
        //{
        //    return SceneCollection.GetStartPosition(Index);
        //}

        public CCoordinateList<double> GetEndPosition()
        {
            return SceneCollection.GetEndPosition(this.LegIndex);
        }

        // 20200217 조승현 - [GCS-TB-MPE-272] 촬영계획 결과보기 창 개선
        public CCoordinateList<double> GetRealStartPosition()
        {
            return SceneCollection.GetRealStartPosition(this.LegIndex);
        }


        public CCoordinateList<double> GetRealEndPosition()
        {
            return SceneCollection.GetRealEndPosition(this.LegIndex);
        }

        // 20170607 김형철 데드코드 삭제
        //public CCoordinateList<double> GetEndPosition(int Index)
        //{
        //    return SceneCollection.GetEndPosition(Index);
        //}

        public void CopyData(CTarget target)
        {
            this.BENum = target.BENum;  // 객체 정보 복사
            this.Name = target.Name;
            this.TargetDivision = target.TargetDivision;
            this.PIR = target.Scene_priority;
            this.m_Suffix = target.StrSuffix;
            // 20161222 조승현 - [ 결함#1727 ] 4.배치표적 목록에 할당되지 않은 수집목록 표시
            SetLegion();

            this.IsPointCapture = target is PointCapture;
        }

        // 20190305 조승현 - 데드코드 처리.
        //public virtual void GetShowProperty(Dictionary<string, string> ShowProperty)
        //{
        //    ShowProperty.Clear();

        //    ShowProperty.Add("표적 BE 번호", "BENum"); // Show Property 설정
        //    ShowProperty.Add("표적명", "Name");
        //    //ShowProperty.Add("Key", "Key");
        //    ShowProperty.Add("위도", "TargetCoordinate.MPELatitudeLL");
        //    ShowProperty.Add("경도", "TargetCoordinate.MPELongitudeLL");
        //    ShowProperty.Add("표적 고도", "TargetCoordinate.Altitude");
        //    ShowProperty.Add("우선 순위", "PIR");


        //    ShowProperty.Add("EO/IR 촬영 센서", "SensorTypeEOIR");
        //    ShowProperty.Add("EO/IR 촬영 모드", "SensorModeEOIR");
        //    ShowProperty.Add("SAR 촬영 센서", "SensorTypeSAR");
        //    ShowProperty.Add("SAR 촬영 모드", "SensorModeSAR");
        //    //ShowProperty.Add("부모키", "ParentKey");

        //    ShowProperty.Add("관측 길이", "ObserveLength");
        //    ShowProperty.Add("관측 폭", "ObserveWidth");

        //    ShowProperty.Add("LEG ID", "SceneCollection.LegID");

        //    ShowProperty.Add("수집 ID", "CollectingID");
        //    ShowProperty.Add("촬영 시작 위치", "StartCoordinateString");
        //    ShowProperty.Add("촬영 종료 위치", "EndCoordinateString");
        //    ShowProperty.Add("가시율", "Sight");
        //    ShowProperty.Add("Spot", "Spot");
        //}

        // 20170607 김형철 데드코드 삭제
        //public bool ConfirmMPolygon(object obj)
        //{
        //    if (obj is MPolygon)
        //        if (m_SceneFootPrint.ObjID == (obj as MPolygon).ObjID) return true;
        //    return false;
        //}


        public CCoordinateList<double> GetPosition()
        {

            CCoordinateList<double> positionList = new CCoordinateList<double>();   // 상중앙점 가져오기
            ///
            if (TargetCoordinate != null)
            {
                WeakReference position = new WeakReference(new CCoordinate<double>(
                    TargetCoordinate.MPELongitudeLL,
                    TargetCoordinate.MPELatitudeLL,
                    TargetCoordinate.Altitude.ConvertAltitude(UNIONALTITUDE.M)
                    ));
                //CCoordinate<double> position = new CCoordinate<double>(
                //    TargetCoordinate.MPELongitudeLL,
                //    TargetCoordinate.MPELatitudeLL,
                //    TargetCoordinate.Altitude.ConvertAltitude(UNIONALTITUDE.M));
                positionList.GetList().Add(position.Target as CCoordinate<double>);
            }

            return positionList;
        }


        public void SetPosition(CCoordinateList<double> coordinateList)
        {
            if (coordinateList != null && coordinateList.Count > 0)
            {
                if (TargetCoordinate == null)
                    TargetCoordinate = new CoordinateModel();
                TargetCoordinate.MPELongitudeLL = coordinateList.GetList()[0].X;  // 상중앙점 값 설정
                TargetCoordinate.MPELatitudeLL = coordinateList.GetList()[0].Y;
                TargetCoordinate.Altitude = coordinateList.GetList()[0].Z.ConvertSystemAltitude(UNIONALTITUDE.M);
                CoordinateSwitch.SetCoordinateModel(CoordinateType.LATLON, TargetCoordinate);
            }
        }

        public void Reset()
        {
            var myLegIdList = SceneCollection.LegID.ToList();
            foreach (int i in myLegIdList)
            {
                int idx = GetLegIndex(i);
                RemoveData(idx);
            }
            LegIndex = -1;
            SetVisible(false);
        }
    }
}
