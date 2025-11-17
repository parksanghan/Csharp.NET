using PLIIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLIIB.EventLib
{
    /// <summary>
    /// 이벤트 딕셔너리 클래스 리턴 타입 Void 매개변수 T1,T2
    ///
    /// </summary>
    public class EVT
    {
        private Dictionary<string, ClsReturnEventDelegate<int, int, int>> _events
        = new Dictionary<string, ClsReturnEventDelegate<int, int, int>>();

        private ClsReturnEventDelegate<int, int, int> GetOrCreate(string key)
        {
            if (!_events.TryGetValue(key, out var evt))
            {
                evt = new ClsReturnEventDelegate<int, int, int>();
                _events[key] = evt;
            }
            return evt;
        }

        public static ClsReturnEventDelegate<int, int, int> Instance(string key)
        {
            return ClsSingletn<EVT>.Instance.GetOrCreate(key);
        }
    }
}
