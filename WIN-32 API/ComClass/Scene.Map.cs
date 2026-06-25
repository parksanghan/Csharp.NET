using CElementStatic;
using CEnum;
using LibCoordinate;
using MPEObjectLib.Line;
using MPEObjectLib.Polygon;
using System;
using System.Collections.Generic;
using System.Event;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPEObjectLib.Capture
{
    using CBaseControl;
    using CElementInterface;

    public partial class Scene : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                m_SceneFootPrint.Dispose();
                m_SceneStartEndLine.Dispose();
                // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
                m_SceneCenterLine.Dispose();
                m_SceneLeftCenterLine.Dispose();
                m_SceneRightCenterLine.Dispose();

                m_SceneRealStartEndLine.Dispose();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private MPolygon m_SceneFootPrint = new MPolygon(); // The scene foot print
        private MLine m_SceneStartEndLine = new MLine();	// The scene start end line
        private MLine m_SceneCenterLine = new MLine();  // The scene center line

        // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
        private MLine m_SceneLeftCenterLine = new MLine();  // The scene center line
        private MLine m_SceneRightCenterLine = new MLine();  // The scene center line
                                                      
        private MLine m_SceneRealStartEndLine = new MLine();  // by Jun

        public MPolygon GetFootprint()
        {
            return m_SceneFootPrint;
        }

        // 20220516 김형철 - 트리 촬영계획 관련 원복한 코드
        //public bool GetVisibleObj()
        //{
        //    return m_SceneFootPrint.GetVisible();
        //}

        public string GetFootPrintKey()
        {
            return m_SceneFootPrint.Key;
        }

        public CCoordinateList<double> GetFootPrintCoordinate()
        {
            return m_SceneFootPrint.GetPosition();
        }

        public CCoordinateList<double> GetOriginFootprintCoordinate()
        {
            var originScene = EvtR_O_SI.Instance("GetCapturePlanEOIRSceneOrigin").Invoke(MissionKey, CollectingID) as Scene
                  ?? EvtR_O_SI.Instance("GetCapturePlanSARSceneOrigin").Invoke(MissionKey, CollectingID) as Scene;

            return originScene != null ? originScene.GetFootPrintCoordinate() : null;
        }

        public CCoordinateList<double> GetStartEndCoordinate()
        {
            return m_SceneStartEndLine.GetPosition();
        }

        // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
        public CCoordinateList<double> GetCenterLineCoordinate()
        {
            return m_SceneCenterLine.GetPosition();
        }
        public CCoordinateList<double> GetLeftCenterLineCoordinate()
        {
            return m_SceneLeftCenterLine.GetPosition();
        }
        public CCoordinateList<double> GetRightCenterLineCoordinate()
        {
            return m_SceneRightCenterLine.GetPosition();
        }

        public CCoordinateList<double> GetRealStartEndCoordinate()
        {
            return m_SceneRealStartEndLine.GetPosition();
        }

        public void SetVisible(bool value)
        {
            if (m_SceneFootPrint != null) m_SceneFootPrint.SetVisible(value);
            // 20190729 조승현 - [자체개선] 지도 도시지연 현상 개선
            if (m_SceneStartEndLine != null)
            {
                if( CommandControl.SystemType == SYSTEMTYPE.PPC)
                    m_SceneStartEndLine.SetVisible(false);
                else
                    m_SceneStartEndLine.SetVisible(value);
            }
            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
            if (m_SceneCenterLine != null) m_SceneCenterLine.SetVisible(value);
            if (m_SceneLeftCenterLine != null) m_SceneLeftCenterLine.SetVisible(value);
            if (m_SceneRightCenterLine != null) m_SceneRightCenterLine.SetVisible(value);
            if (m_SceneRealStartEndLine != null) m_SceneRealStartEndLine.SetVisible(value);
            
            Evt.Instance("MapRefresh").Invoke();
        }

        public void SetScene(string TargetKey, object obj, int CollectionID)
        {
            //if (this.LegID == -1) return; // 데이터가 없는 경우 판단 필요

            SENSORTYPE sensortype = (SENSORTYPE)obj;    // SensorType 비교를 위해 전환
            if (!(this.BENum + this.m_Suffix).Equals(TargetKey) || this.m_SensorType != sensortype || this.CollectingID != CollectionID) return; // 해당 Scene이 아닐경우 retrun

            // 원본 데이터면 return;
            if (this.IsOrigin) return;
            SetMapObject();
        }

        public void SetFootPrintIsCreate(bool value)
        {
            m_SceneFootPrint.IsCreate = value;
        }

        public void SetFootPrintCount(int value)
        {
            m_SceneFootPrint.Count = value;
        }

        public void SetFootPrintClear()
        {
            m_SceneFootPrint.SetClearPosition();
            m_SceneFootPrint.Count = 0;
        }

        public void SetCoordinates(Scene sc)
        {
            SetFootPrintClear();
            m_SceneFootPrint.SetPosition(sc.GetFootPrintCoordinate());
            m_SceneStartEndLine.SetPosition(sc.GetStartEndCoordinate());
            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
            m_SceneCenterLine.SetPosition(sc.GetCenterLineCoordinate());
            m_SceneLeftCenterLine.SetPosition(sc.GetLeftCenterLineCoordinate());
            m_SceneRightCenterLine.SetPosition(sc.GetRightCenterLineCoordinate());

            m_SceneRealStartEndLine.SetPosition(sc.GetRealStartEndCoordinate());
        }

        public void SetFootprintCoordinate(CCoordinateList<double> coordiList)
        {
            SetFootPrintClear();
            m_SceneFootPrint.SetPosition(coordiList);
        }

        public bool m_MapObjectSetStatus = true;

        public void CreateMapObject()
        {
            m_SceneFootPrint.IsCreate = true;
            m_SceneFootPrint.CreateMapObject();
            m_SceneFootPrint.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneFootPrint.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneFootPrint.SetText("");

            m_SceneStartEndLine.IsCreate = true;
            m_SceneStartEndLine.CreateMapObject();
            m_SceneStartEndLine.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneStartEndLine.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneStartEndLine.SetText("");
            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.

            m_SceneCenterLine.IsCreate = true;
            m_SceneCenterLine.CreateMapObject();
            m_SceneCenterLine.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneCenterLine.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneCenterLine.SetText("");

            m_SceneLeftCenterLine.IsCreate = true;
            m_SceneLeftCenterLine.CreateMapObject();
            m_SceneLeftCenterLine.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneLeftCenterLine.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneLeftCenterLine.SetText("");

            m_SceneRightCenterLine.IsCreate = true;
            m_SceneRightCenterLine.CreateMapObject();
            m_SceneRightCenterLine.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneRightCenterLine.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneRightCenterLine.SetText("");

            m_SceneRealStartEndLine.IsCreate = true;
            m_SceneRealStartEndLine.CreateMapObject();
            m_SceneRealStartEndLine.Key = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneRealStartEndLine.GroupKey = ElementStatic.MPEKey + Guid.NewGuid().ToString().Replace("-", "");
            m_SceneRealStartEndLine.SetText("");
        }

        public void SetMapObject()
        {
            if (m_SceneFootPrint.IsCreate)  // FootPrint 설정
            {
                m_MapObjectSetStatus = false;
                m_SceneFootPrint.IsCreate = false;  // 생성 상태로 변경
                m_SceneFootPrint.ObjectType = ObjType.Scene;
                //m_SceneFootPrint.ObjectMissionType = MISSIONTYPE.MDF;

                // 촬영계획 footprint 시, 센서별로 도시 색상 구분
                if (this.m_SensorType != SENSORTYPE.EOIR &&
                    this.m_SensorType != SENSORTYPE.EO &&
                    this.m_SensorType != SENSORTYPE.IR)
                    m_SceneFootPrint.SetColor(100, 255, 100, 105);
                else
                    m_SceneFootPrint.SetColor(100, 100, 255, 105);

                m_SceneFootPrint.SetDisplayOnly(m_ParentKey);

                Evt_O.Instance("AddScene").Invoke(this);
                Evt_SO.Instance("AddObject").Invoke(m_SceneFootPrint.Key, m_SceneFootPrint);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneFootPrint.Key);
                Evt_O.Instance("CreateObjectComplate").Invoke(m_SceneFootPrint);
            }

            if (m_SceneStartEndLine.IsCreate)   // StartEndLine 생성
            {
                m_SceneStartEndLine.IsCreate = false;
                m_SceneStartEndLine.ObjectType = ObjType.Line;
                //m_SceneStartEndLine.ObjectMissionType = MISSIONTYPE.MDF;
                m_SceneStartEndLine.SetLineWidth(5);
                m_SceneStartEndLine.SetColor(255, 255, 0, 255);
                m_SceneStartEndLine.SetDisplayOnly(m_ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SceneStartEndLine.Key, m_SceneStartEndLine);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneStartEndLine.Key);
            }

            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
            if (m_SceneCenterLine.IsCreate) // CenterLine 생성
            {
                m_SceneCenterLine.IsCreate = false;
                m_SceneCenterLine.ObjectType = ObjType.Line;
                //m_SceneCenterLine.ObjectMissionType = MISSIONTYPE.MDF;
                m_SceneCenterLine.SetLineWidth(2);
                m_SceneCenterLine.SetColor(255, 0, 255, 255);
                m_SceneCenterLine.SetDisplayOnly(m_ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SceneCenterLine.Key, m_SceneCenterLine);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneCenterLine.Key);
            }
            if (m_SceneLeftCenterLine.IsCreate) // LeftLine 생성
            {
                m_SceneLeftCenterLine.IsCreate = false;
                m_SceneLeftCenterLine.ObjectType = ObjType.Line;
                //m_SceneLeftCenterLine.ObjectMissionType = MISSIONTYPE.MDF;
                m_SceneLeftCenterLine.SetLineWidth(1);
                m_SceneLeftCenterLine.SetColor(255, 0, 255, 255);
                m_SceneLeftCenterLine.SetDisplayOnly(m_ParentKey);
                m_SceneLeftCenterLine.SetLinePattern(LinePatternType.Dash);

                Evt_SO.Instance("AddObject").Invoke(m_SceneLeftCenterLine.Key, m_SceneLeftCenterLine);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneLeftCenterLine.Key);
            }
            if (m_SceneRightCenterLine.IsCreate) // RightLine 생성
            {
                m_SceneRightCenterLine.IsCreate = false;
                m_SceneRightCenterLine.ObjectType = ObjType.Line;
                //m_SceneRightCenterLine.ObjectMissionType = MISSIONTYPE.MDF;
                m_SceneRightCenterLine.SetLineWidth(1);
                m_SceneRightCenterLine.SetColor(255, 0, 255, 255);
                m_SceneRightCenterLine.SetDisplayOnly(m_ParentKey);
                m_SceneRightCenterLine.SetLinePattern(LinePatternType.Dash);

                Evt_SO.Instance("AddObject").Invoke(m_SceneRightCenterLine.Key, m_SceneRightCenterLine);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneRightCenterLine.Key);
            }

            // 20151118 Add by Jun
            if (m_SceneRealStartEndLine.IsCreate)   // StartEndLine 생성
            {
                m_SceneRealStartEndLine.IsCreate = false;
                m_SceneRealStartEndLine.ObjectType = ObjType.Line;
                //m_SceneRealStartEndLine.ObjectMissionType = MISSIONTYPE.MDF;
                m_SceneRealStartEndLine.SetLineWidth(50);
                m_SceneRealStartEndLine.SetColor(200, 41, 142, 255);
                m_SceneRealStartEndLine.SetLinePattern(LinePatternType.Solid);
                m_SceneRealStartEndLine.SetDisplayOnly(m_ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SceneRealStartEndLine.Key, m_SceneRealStartEndLine);
                Evt_SS.Instance("AddSceneKey").Invoke(m_ParentKey, m_SceneRealStartEndLine.Key);
            }
        }

        public void SetFootprintColor()
        {
            if (this.ShootingState == SceneShootingState.Fail || this.ShootingState == SceneShootingState.Complete)
            {
                m_SceneFootPrint.SetColor(100, 169, 169, 169);
                m_SceneStartEndLine.SetColor(255, 169, 169, 169);
                m_SceneCenterLine.SetColor(255, 169, 169, 169);
                m_SceneLeftCenterLine.SetColor(255, 169, 169, 169);
                m_SceneRightCenterLine.SetColor(255, 169, 169, 169);
                m_SceneRealStartEndLine.SetColor(200, 169, 169, 169);
            }
            else if (this.ShootingState == SceneShootingState.Holding)
            {
                m_SceneFootPrint.SetColor(100, 255, 255, 0);
                m_SceneStartEndLine.SetColor(255, 255, 255, 0);
                m_SceneCenterLine.SetColor(255, 255, 255, 0);
                m_SceneLeftCenterLine.SetColor(255, 255, 255, 0);
                m_SceneRightCenterLine.SetColor(255, 255, 255, 0);
                m_SceneRealStartEndLine.SetColor(200, 255, 255, 0);
            }
            else
            {
                // 촬영계획 footprint 시, 센서별로 도시 색상 구분
                if (this.m_SensorType != SENSORTYPE.EOIR &&
                    this.m_SensorType != SENSORTYPE.EO &&
                    this.m_SensorType != SENSORTYPE.IR)
                    m_SceneFootPrint.SetColor(100, 255, 100, 105);
                else
                    m_SceneFootPrint.SetColor(100, 100, 255, 105);

                m_SceneStartEndLine.SetColor(255, 255, 0, 255);
                m_SceneCenterLine.SetColor(255, 0, 255, 255);
                m_SceneLeftCenterLine.SetColor(255, 0, 255, 255);
                m_SceneRightCenterLine.SetColor(255, 0, 255, 255);
                m_SceneRealStartEndLine.SetColor(200, 41, 142, 255);
            }
        }

        public void SetMapOriginObject()
        {
            if (m_SceneFootPrint.IsCreate)
            {
                m_SceneFootPrint.IsCreate = false;
                m_SceneFootPrint.SetText(string.Format("{0}_원본", this.UiName));
                m_SceneFootPrint.SetVisible(true);

                // 촬영계획 footprint 시, 센서별로 도시 색상 구분
                if (this.m_SensorType != SENSORTYPE.EOIR && this.m_SensorType != SENSORTYPE.EO && this.m_SensorType != SENSORTYPE.IR) m_SceneFootPrint.SetColor(100, 255, 100, 105);
                else m_SceneFootPrint.SetColor(100, 100, 255, 105);

                m_SceneFootPrint.SetDisplay(m_ParentKey);
                m_SceneFootPrint.ModifyObjecet(m_SceneFootPrint.GetPosition());

                Evt_SO.Instance("AddObject").Invoke(m_SceneFootPrint.Key, m_SceneFootPrint);
                //Evt_O.Instance("CreateObjectComplate").Invoke(m_SceneFootPrint);
            }
            else
            {
                m_SceneFootPrint.SetVisible(true);
            }
            
            Evt.Instance("MapRefresh").Invoke();
        }

        private void SetFootPrintCoordinate()
        {
            if (LegIndex == -1)
            {
                //CCoordinateList<double> list = new CCoordinateList<double>();
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                //m_SceneFootPrint.ModifyObjecet(list);
                m_SceneFootPrint.SetVisible(false);
                return;
            }

            m_SceneFootPrint.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].m_FootPrintCoordinate);

            // 20210829 조승현 - [GCS-TB-MPE-75] 바람 적용중일땐 기존 도시/미도시 상태 유지.
            if (!EvtR_B.Instance("GetWindApplyStatus").Invoke())
            {
                m_SceneFootPrint.SetVisible(true);
            }
        }

        private void SetStartEndLineCoordinate()
        {
            if (LegIndex == -1)
            {
                WeakReference list = new WeakReference(new CCoordinateList<double>());
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                //CCoordinateList<double> list = new CCoordinateList<double>();
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                m_SceneStartEndLine.ModifyObjecet(list.Target as CCoordinateList<double>);
                return;
            }
            m_SceneStartEndLine.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].GetStartEndCoordinate());
        }
        // 20151118 Add by Jun
        private void SetRealStartEndLineCoordinate()
        {
            if (LegIndex == -1)
            {
                WeakReference list = new WeakReference(new CCoordinateList<double>());
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                //CCoordinateList<double> list = new CCoordinateList<double>();
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                m_SceneRealStartEndLine.ModifyObjecet(list.Target as CCoordinateList<double>);
                return;
            }
            m_SceneRealStartEndLine.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].GetRealStartEndCoordinate());
        }

        private void SetCenterLineCoordinate( bool bFlag )
        {
            if (LegIndex == -1)
            {
                WeakReference list = new WeakReference(new CCoordinateList<double>());
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                (list.Target as CCoordinateList<double>).Add(new WeakReference(new CCoordinate<double>()).Target as CCoordinate<double>);
                //CCoordinateList<double> list = new CCoordinateList<double>();
                //list.Add(new CCoordinate<double>());
                //list.Add(new CCoordinate<double>());
                // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
                if (bFlag)
                {
                    m_SceneLeftCenterLine.ModifyObjecet(list.Target as CCoordinateList<double>);
                    m_SceneRightCenterLine.ModifyObjecet(list.Target as CCoordinateList<double>);
                }
                else
                {
                    m_SceneCenterLine.ModifyObjecet(list.Target as CCoordinateList<double>);
                }
                return;
            }
            // SHJO_20160621 : 촬영계획 반경 수정
            if (bFlag)
            {
                m_SceneLeftCenterLine.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].m_LeftLineCoordinate);
                m_SceneRightCenterLine.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].m_RightLineCoordinate);
            }
            else
            {
                m_SceneCenterLine.ModifyObjecet(SceneCollection.m_SceneNoDisplayData[0].m_CenterLineCoordinate);
            }
        }

        public void RemoveScene(object obj)
        {
            if (obj != this) return;
            this.m_SceneFootPrint.DeleteObject();
            this.m_SceneStartEndLine.DeleteObject();
            // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
            this.m_SceneCenterLine.DeleteObject();
            this.m_SceneLeftCenterLine.DeleteObject();
            this.m_SceneRightCenterLine.DeleteObject();

            this.m_SceneRealStartEndLine.DeleteObject();
            this.DeleteEvent();
            this.Dispose(true);
        }

        public void RemoveMdfKey(string mdfKey)
        {
            if (this.m_ParentKey == mdfKey && this.IsOrigin)
            {
                this.m_SceneFootPrint.DeleteObject();
                this.m_SceneStartEndLine.DeleteObject();
                // SHJO_20160621 : 광역은 센터라인을 시작라인, 종료라인으로 구분.
                this.m_SceneCenterLine.DeleteObject();
                this.m_SceneLeftCenterLine.DeleteObject();
                this.m_SceneRightCenterLine.DeleteObject();

                this.m_SceneRealStartEndLine.DeleteObject();
                this.DeleteEvent();
                this.Dispose(true);
            }
        }
    }
}
