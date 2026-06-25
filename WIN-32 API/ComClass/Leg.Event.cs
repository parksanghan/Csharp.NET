using System;
using System.Collections.Generic;
using System.Event;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPEObjectLib.Capture
{
    public partial class Leg
    {
        /**
         * @fn  public void SetEvent()
         *
         * @brief   이벤트 설정
         *
         * @author  Soletop
         * @date    2019-12-04
         */
        private void SetEvent()
        {
            //[20191204] 김시준 - 이벤트 참조 수정
            DeleteEvent();
            InitEvent();
        }

        private void InitEvent()
        {
            Evt_S.Instance("SetLegElement").Event += SetLegElement;
            Evt_Y7.Instance("SetLeg").Event += SetLeg;  // Scene 설정 이벤트
            Evt_Y3.Instance("SetLegIR").Event += SetLegIR;  // Scene 설정 이벤트
            Evt_O.Instance("RemoveLeg").Event += RemoveLeg;

            // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
#if BSix
            //Evt_Y8.Instance("SetLegB6").Event += SetLegB6;  // Scene 설정 이벤트
            Evt_Y8.Instance("SetLegB6").Event += SetLegB6;  // Scene 설정 이벤트
#endif

            Evt_IIIII.Instance("SetLegColor").Event += SetLegColor;
        }

        private void DeleteEvent()
        {
            Evt_S.Instance("SetLegElement").Event -= SetLegElement;
            Evt_Y7.Instance("SetLeg").Event -= SetLeg;  // Scene 설정 이벤트
            Evt_Y3.Instance("SetLegIR").Event -= SetLegIR;  // Scene 설정 이벤트
            Evt_O.Instance("RemoveLeg").Event -= RemoveLeg;

            // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
#if BSix
            //Evt_Y8.Instance("SetLegB6").Event -= SetLegB6;  // Scene 설정 이벤트
            Evt_Y8.Instance("SetLegB6").Event -= SetLegB6;  // Scene 설정 이벤트
#endif

            Evt_IIIII.Instance("SetLegColor").Event -= SetLegColor;
        }

        private void SetLeg(dynamic Key, dynamic LegID, dynamic obj, dynamic obj2, dynamic obj3, dynamic obj4, dynamic obj5)
        {
            if (!this.LegKey.Equals(Key)) return;

            this.LegID = LegID;
            this.ParentKey = ParentKey;
            this.m_Leg.ModifyObjecet(obj);
            this.m_EOPolygonLeft.ModifyObjecet(obj2);
            this.m_EOPolygonRight.ModifyObjecet(obj3);
#if !BSix
            this.m_SARPolygonLeft.ModifyObjecet(obj4);
            this.m_SARPolygonRight.ModifyObjecet(obj5);
#endif
        }

        // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
#if BSix
        // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
        //private void SetLegB6(dynamic Key, dynamic LegID, dynamic obj, dynamic obj2, dynamic obj3, dynamic obj4, dynamic obj5, dynamic obj6)
        //{
        //    if (!this.LegKey.Equals(Key)) return;

        //    this.LegID = LegID;
        //    this.ParentKey = ParentKey;
        //    this.m_Leg.ModifyObjecet(obj);
        //    this.m_EOPolygonLeft.ModifyObjecet(obj2);
        //    this.m_EOPolygonRight.ModifyObjecet(obj3);
        //    this.m_SARPolygonLeft.ModifyObjecet(obj4);
        //    this.m_SARPolygonRight.ModifyObjecet(obj5);
        //    this.m_SARPolygonForward.ModifyObjecet(obj6);
        //}
        private void SetLegB6(dynamic Key, dynamic LegID, dynamic obj, dynamic obj2, dynamic obj3, dynamic obj4, dynamic obj5, dynamic obj6)
        {
            if (!this.LegKey.Equals(Key)) return;

            this.LegID = LegID;
            this.ParentKey = ParentKey;
            this.m_Leg.ModifyObjecet(obj);
            this.m_EOPolygonLeft.ModifyObjecet(obj2);
            this.m_EOPolygonRight.ModifyObjecet(obj3);
            //this.m_SARPolygonForward.ModifyObjecet(obj4);
           // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
            this.m_SARPolygonLeft.ModifyObjecet(obj4);
            this.m_SARPolygonRight.ModifyObjecet(obj5);
            this.m_SARPolygonComplex.ModifyObjecet(obj6);
        }
#endif

        private void SetLegIR(dynamic Key, dynamic obj, dynamic obj2)
        {
            if (!this.LegKey.Equals(Key)) return;
            this.m_IRPolygonLeft.ModifyObjecet(obj);
            this.m_IRPolygonRight.ModifyObjecet(obj2);
        }

        private void RemoveLeg(object obj)
        {
            if (obj != this) return;
            this.m_Leg.DeleteObject();
            this.m_EOPolygonLeft.DeleteObject();
            this.m_EOPolygonRight.DeleteObject();
            this.m_IRPolygonLeft.DeleteObject();
            this.m_IRPolygonRight.DeleteObject();

            // 20180116 조승현 - [GCS-TB-MPE-142] SAR 촬영영역 도시 오류
            // 20181123 조승현 - [GCS-TB-MPE-265] SAR촬영영역 도시 개선
#if BSix
            //this.m_SARPolygonForward.DeleteObject();
            // 20191018 조승현 - [GCS-TB-MPE-565] 촬영가능영역 관련 수정
            this.m_SARPolygonLeft.DeleteObject();
            this.m_SARPolygonRight.DeleteObject();
            this.m_SARPolygonComplex.DeleteObject();
#else
            this.m_SARPolygonLeft.DeleteObject();
            this.m_SARPolygonRight.DeleteObject();
#endif
            this.DeleteEvent();
        }

        private void SetLegColor(int legId, int a, int r, int g, int b)
        {
            if (!this.LegID.Equals(legId)) return;
            this.m_Leg.SetColor(a, r, g, b);
        }
    }
}
