using LibCoordinate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPEObjectLib.Capture
{
    public partial class Leg
    {
        public Leg()
        {
            SetEvent(); //[20191204] 김시준 - 이벤트 참조 수정
        }

        private string m_ParentKey = "";
        public string ParentKey
        {
            get { return m_ParentKey; }
            set { m_ParentKey = value; }
        }

        private int m_LegID = -1;
        public int LegID
        {
            get { return m_LegID; }
            set
            {
                m_LegID = value;
                SetLegName();
            }
        }

        private string m_LegKey = "";
        public string LegKey
        {
            get { return m_LegKey; }
            set { m_LegKey = value; }
        }

        public CCoordinate<double> GetCoordinate(int index)
        {
            return m_Leg.GetPosition()[index];
        }
        
    }
}
