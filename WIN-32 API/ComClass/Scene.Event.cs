using CBaseControl;
using CEnum;
using System;
using System.Collections.Generic;
using System.Event;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUtil;
using SensorScheduler;
using DataFormatLibrary;
using System.Collections;
using MElementLib;

namespace MPEObjectLib.Capture
{
    public partial class Scene
    {
        /**
         * @fn  public void SetEvent()
         *
         * @brief   이벤트 설정
         *
         * @author  Soletop
         * @date    2019-12-04
         */
        public void SetEvent()
        {
            //[20191204] 김시준 - 이벤트 참조 수정
            DeleteEvent();
            InitEvent();
        }

        public void InitEvent()
        {
            // 20181029 조승현 - [자체개선] 자동배치 개념을 수집체크 개념으로 이양
            //Evt_OB.Instance("BatchCheck").Event += BatchCheck;  // Scene 체크 이벤트
            Evt_OB.Instance("SceneCheck").Event += SceneCheck;  // Scene 체크 이벤트
            Evt_SOI.Instance("SetScene").Event += SetScene;  // Scene 설정 이벤트
            Evt_O.Instance("RemoveScene").Event += RemoveScene;
            Evt_S.Instance("RemoveMdfKey").Event += RemoveMdfKey;
            

            Evt_O.Instance("DeleteTargetScene").Event += DeleteTargetScene;

            ObjectEventDistributor.SceneCommandControlEvent += CommandControlStaticPropertyChanged;
        }

        private void CommandControlStaticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ChangedUnionAltitude"))
            {
                if (this.TargetCoordinate != null)
                {
                    this.TargetCoordinate.Altitude = this.TargetCoordinate.Altitude.ConvertSystemAltitude(CommandControl.UnionAltitudeOld);
                }
            }
            else if (e.PropertyName.Equals("ChangedUnionSpeed"))
            {
            }
            else if (e.PropertyName.Equals("ChangedUnionDistance"))
            {
            }
            else if (e.PropertyName.Equals("ChangedUnionWeight"))
            {
            }
        }

        public void DeleteEvent()
        {
            //Evt_OB.Instance("BatchCheck").Event -= BatchCheck;  // Scene 체크 이벤트
            Evt_OB.Instance("SceneCheck").Event -= SceneCheck;  // Scene 체크 이벤트
            Evt_SOI.Instance("SetScene").Event -= SetScene;  // Scene 설정 이벤트
            Evt_O.Instance("RemoveScene").Event -= RemoveScene;
            Evt_S.Instance("RemoveMdfKey").Event -= RemoveMdfKey;

            Evt_O.Instance("DeleteTargetScene").Event -= DeleteTargetScene;
            ObjectEventDistributor.SceneCommandControlEvent -= CommandControlStaticPropertyChanged;
        }

        private void DeleteTargetScene(object obj)
        {
            string BENum = obj.GetType().GetProperty("BENum").GetValue(obj).ToString();
            string Suffix = obj.GetType().GetProperty("StrSuffix").GetValue(obj).ToString();
            string parentKey = obj.GetType().GetProperty("ParentKey").GetValue(obj).ToString();

            var gmdfKey = EvtR_S_S.Instance("GetGMDF").Invoke(parentKey);
            var sceneGmdfKey = EvtR_S_S.Instance("GetGMDF").Invoke(this.MissionKey);

            if (this.BENum.Equals(BENum) && this.m_Suffix.Equals(Suffix) && gmdfKey == sceneGmdfKey)
                Evt_O.Instance("RemoveScene").Invoke(this);
        }

        // 20181029 조승현 - [자체개선] 자동배치 개념을 수집체크 개념으로 이양
        //private void BatchCheck(object obj, bool value)
        //{
        //    if (obj == this)
        //        this.AutoBatch = value;   // Scene 체크
        //}

        private void SceneCheck(object obj, bool value)
        {
            if (obj == this)
                this.IsCheck = value;   // Scene 체크
        }

        //private void SceneCollectionIDSet(object obj, object targetlist)
        //{
        //    if (obj != null && targetlist != null)
        //    {
        //        foreach (Scene sc in targetlist as IEnumerable)
        //        {
        //            if (this.BENum.Equals(sc.BENum) && this.CollectingID.Equals(sc.CollectingID) && !this.isOrigin)
        //            {
        //                List<S_LEG_REGION> abc = obj as List<S_LEG_REGION>;

        //                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(delegate
        //                {
        //                    foreach (S_LEG_REGION a in abc)
        //                    {
        //                        if(!this.SceneCollection.LegID.Contains(a.leg_number))
        //                            this.AddLegID(a.leg_number);
        //                    }
        //                }));
        //            }
        //        }
        //    }
        //}
    }
}
