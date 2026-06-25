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
    public partial class Leg : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                m_Leg.Dispose();

                m_EOPolygonLeft.Dispose();
                m_EOPolygonRight.Dispose();

                m_IRPolygonLeft.Dispose();
                m_IRPolygonRight.Dispose();

                // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
                // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
#if BSix
                //m_SARPolygonForward.Dispose();
                m_SARPolygonComplex.Dispose();
                m_SARPolygonLeft.Dispose();
                m_SARPolygonRight.Dispose();
#else
                m_SARPolygonLeft.Dispose();
                m_SARPolygonRight.Dispose();
#endif
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private MPolygon m_EOPolygonLeft = new MPolygon();
        private MPolygon m_EOPolygonRight = new MPolygon();

        private MPolygon m_IRPolygonLeft = new MPolygon();
        private MPolygon m_IRPolygonRight = new MPolygon();



        // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
        // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
#if BSix
        //private MPolygon m_SARPolygonForward = new MPolygon();
        private MPolygon m_SARPolygonComplex = new MPolygon();

        // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
        private MPolygon m_SARPolygonLeft = new MPolygon();
        private MPolygon m_SARPolygonRight = new MPolygon();
#else
        private MPolygon m_SARPolygonLeft = new MPolygon();
        private MPolygon m_SARPolygonRight = new MPolygon();
#endif

        private MLine m_Leg = new MLine();

        public object GetMLeg()
        {
            return m_Leg;
        }

        public void SetNoLegName()
        {
            this.m_Leg.SetText("");
        }

        // 20170607 김형철 데드코드 삭제
        public void SetLegName()
        {
            //this.m_Leg.SetText(this.LegID.ToString());
            this.m_Leg.SetText(string.Format("{0} LEG", this.LegID));
        }

        public void VisibleLeg(bool value)
        {
            this.m_Leg.SetVisible(value);
        }

        public void VisibleEO(bool value)
        {
            this.m_EOPolygonLeft.SetVisible(value);
            this.m_EOPolygonRight.SetVisible(value);
        }

        public void VisibleIR(bool value)
        {
            this.m_IRPolygonLeft.SetVisible(value);
            this.m_IRPolygonRight.SetVisible(value);
        }

        public void VisibleSAR(bool value)
        {
            // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
            // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
            // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
            this.m_SARPolygonLeft.SetVisible(value);
            this.m_SARPolygonRight.SetVisible(value);
        }

        // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
#if BSix
        public void VisibleGMTI(bool value)
        {
            this.m_SARPolygonComplex.SetVisible(value);
        }
#endif

        // [GCS-TB-MPE-535][촬영가능영역 투명도 조정]
        public void SetLegElement(string LegKey)
        {
            if (!this.LegKey.Equals(LegKey)) return;

            byte opacity = EvtR_Y_Y.Instance("GetEnvironmentValue").Invoke("CaptureEnableOpacity");
            if (m_Leg.IsCreate)  // FootPrint 설정
            {
                m_Leg.IsCreate = false;  // 생성 상태로 변경
                m_Leg.ObjectType = ObjType.Leg;
                //m_Leg.ObjectMissionType = MISSIONTYPE.MDF;
                m_Leg.SetColor(100, 219, 218, 85);
                m_Leg.SetLinePattern(LinePatternType.Solid);
                m_Leg.SetLineWidth(6);
                m_Leg.SetDisplayOnly(ParentKey);
                
                Evt_SO.Instance("AddObject").Invoke(m_Leg.Key, m_Leg);
                Evt_O.Instance("CreateObjectComplate").Invoke(m_Leg);
            }

            if (m_EOPolygonLeft.IsCreate)   // StartEndLine 생성
            {
                m_EOPolygonLeft.IsCreate = false;
                m_EOPolygonLeft.ObjectType = ObjType.Polygon;
                //m_EOPolygonLeft.ObjectMissionType = MISSIONTYPE.MDF;
                m_EOPolygonLeft.SetColor(opacity, 255, 0, 255);
                m_EOPolygonLeft.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_EOPolygonLeft.Key, m_EOPolygonLeft);
            }

            if (m_EOPolygonRight.IsCreate)   // StartEndLine 생성
            {
                m_EOPolygonRight.IsCreate = false;
                m_EOPolygonRight.ObjectType = ObjType.Polygon;
                //m_EOPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
                m_EOPolygonRight.SetColor(opacity, 255, 0, 255);
                m_EOPolygonRight.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_EOPolygonRight.Key, m_EOPolygonRight);
            }

            if (m_IRPolygonLeft.IsCreate)   // StartEndLine 생성
            {
                m_IRPolygonLeft.IsCreate = false;
                m_IRPolygonLeft.ObjectType = ObjType.Polygon;
                //m_IRPolygonLeft.ObjectMissionType = MISSIONTYPE.MDF;
                m_IRPolygonLeft.SetColor(opacity, 255, 255, 0);
                m_IRPolygonLeft.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_IRPolygonLeft.Key, m_IRPolygonLeft);
            }

            if (m_IRPolygonRight.IsCreate)   // StartEndLine 생성
            {
                m_IRPolygonRight.IsCreate = false;
                m_IRPolygonRight.ObjectType = ObjType.Polygon;
                //m_IRPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
                m_IRPolygonRight.SetColor(opacity, 255, 255, 0);
                m_IRPolygonRight.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_IRPolygonRight.Key, m_IRPolygonRight);
            }

            // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
            // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
#if BSix
            //if (m_SARPolygonForward.IsCreate) // CenterLine 생성
            //{
            //    m_SARPolygonForward.IsCreate = false;
            //    m_SARPolygonForward.ObjectType = ObjType.Polygon;
            //    //m_SARPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
            //    m_SARPolygonForward.SetColor(opacity, 0, 255, 255);
            //    m_SARPolygonForward.SetDisplayOnly(ParentKey);

            //    Evt_SO.Instance("AddObject").Invoke(m_SARPolygonForward.Key, m_SARPolygonForward);
            //}
            // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
            if (m_SARPolygonLeft.IsCreate) // CenterLine 생성
            {
                m_SARPolygonLeft.IsCreate = false;
                m_SARPolygonLeft.ObjectType = ObjType.Polygon;
                //m_SARPolygonLeft.ObjectMissionType = MISSIONTYPE.MDF;
                m_SARPolygonLeft.SetColor(opacity, 0, 255, 255);
                m_SARPolygonLeft.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SARPolygonLeft.Key, m_SARPolygonLeft);
            }

            if (m_SARPolygonRight.IsCreate) // CenterLine 생성
            {
                m_SARPolygonRight.IsCreate = false;
                m_SARPolygonRight.ObjectType = ObjType.Polygon;
                //m_SARPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
                m_SARPolygonRight.SetColor(opacity, 0, 255, 255);
                m_SARPolygonRight.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SARPolygonRight.Key, m_SARPolygonRight);
            }
            if (m_SARPolygonComplex.IsCreate) // CenterLine 생성
            {
                m_SARPolygonComplex.IsCreate = false;
                m_SARPolygonComplex.ObjectType = ObjType.Polygon;
                //m_SARPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
                m_SARPolygonComplex.SetColor(opacity, 0, 255, 255);
                m_SARPolygonComplex.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SARPolygonComplex.Key, m_SARPolygonComplex);
            }
#else
            if (m_SARPolygonLeft.IsCreate) // CenterLine 생성
            {
                m_SARPolygonLeft.IsCreate = false;
                m_SARPolygonLeft.ObjectType = ObjType.Polygon;
                //m_SARPolygonLeft.ObjectMissionType = MISSIONTYPE.MDF;
                m_SARPolygonLeft.SetColor(opacity, 0, 255, 255);
                m_SARPolygonLeft.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SARPolygonLeft.Key, m_SARPolygonLeft);
            }

            if (m_SARPolygonRight.IsCreate) // CenterLine 생성
            {
                m_SARPolygonRight.IsCreate = false;
                m_SARPolygonRight.ObjectType = ObjType.Polygon;
                //m_SARPolygonRight.ObjectMissionType = MISSIONTYPE.MDF;
                m_SARPolygonRight.SetColor(opacity, 0, 255, 255);
                m_SARPolygonRight.SetDisplayOnly(ParentKey);

                Evt_SO.Instance("AddObject").Invoke(m_SARPolygonRight.Key, m_SARPolygonRight);
            }
#endif
        }
    }
}
