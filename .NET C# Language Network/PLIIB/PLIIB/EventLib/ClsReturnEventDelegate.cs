using System.Diagnostics;

namespace PLIIB.EventLib
{

    /// <summary>
    /// 사용자 정보를 관리하는 클래스입니다.
    /// </summary>
    /// <remarks>
    /// 이 클래스는 사용자 등록, 수정, 삭제 등의 기능을 제공합니다.
    /// </remarks>
    ///  
    // void 타입
    public delegate void VoidEventDelegate<in T1>(T1 p1);
    public delegate void VoidEventDelegate<in T1,in T2>(T1 p1, T2 p2);
    public delegate void VoidEventDelegate<in T1, in T2,in T3>(T1 p1, T2 p2,T3 p3);
    public delegate void VoidEventDelegate<in T1, in T2,in T3,in T4>(T1 p1, T2 p2, T3 p3 , T4 p4);
    // 리턴 타입
    public delegate R ReturnEventDelegate<R, in T1>(T1 p1);
    public delegate R ReturnEventDelegate<R, in T1, in T2>(T1 p1, T2 p2);
    public delegate R ReturnEventDelegate<R, in T1, in T2, in T3>(T1 p1, T2 p2, T3 p3);
    public class ClsReturnEventDelegate<R,T1,T2>
    {
        public ReturnEventDelegate<R,T1,T2> Event;

        public R Invoke(T1 p1, T2 p2)
        {
            if (Event != null)
            {
                try
                {
                    
                    return Event(p1, p2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("!!!!!!!!!!!! " + ex);
                }
            }

            return default;
        }

    }
    public class ClsReturnEventDelegate<T1,T2>
    {
        public VoidEventDelegate<T1, T2> Event;
        public void Invoke(T1 p1,T2 p2)
        {
            if (Event != null)
            {
                try
                {

                    //return Event(p1,p2);
                    //Void 타입으므로 실행만
                    Event(p1, p2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("!!!!!!!!!!!! " + ex);
                }
            }

          
        }

    }
}
