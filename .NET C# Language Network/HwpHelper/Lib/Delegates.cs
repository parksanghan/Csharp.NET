using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Lib
{

     
    //Return
    public delegate R RetrrnDelegate<R>();
    public delegate R RetrrnDelegate<R, T1>(T1 t1);
    public delegate R RetrrnDelegate<R,T1,T2>(T1 t1, T2 t2);
 
    //Void
    public delegate void VoidDelegate(); 

    // 리턴이 있는타입으로 새로운 쓰레드에서동작하지않음 리턴이 있다는건 결국동기니간 
    public class CReturnDelegate<R>
    {
        /// <summary>
        /// 이벤트를 추가합니다. += / -=
        /// </summary>
        public RetrrnDelegate<R> Event;
        /// <summary>
        /// 등록된 이벤트를 실행합니다.
        /// </summary>
        /// <returns></returns>
        public R Invoke()
        {
            if (Event != null)
            {
                try
                {
                    return Event();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("!!!!!!!!!!!!!!" + ex.ToString());
                }
            }

            return default(R);
        }

    }
    public class CReturnDelegate<R, T1>
    {
        /// <summary>
        /// 이벤트를 추가합니다. += / -=
        /// </summary>
        public RetrrnDelegate<R, T1> Event;

        /// <summary>
        /// 등록된 이벤트를 실행합니다.
        /// </summary>
        public R Invoke(T1 t1)
        {
            if (Event != null)
            {
                try
                {
                    return Event(t1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("!!!!!!!!!!!!!!" + ex.ToString());
                }
            }

            return default(R);
        }
    }
    public class CReturnDelegate<R, T1,T2>
    {
        /// <summary>
        /// 이벤트를 추가합니다. += / -=
        /// </summary>
        public RetrrnDelegate<R, T1,T2> Event;

        /// <summary>
        /// 등록된 이벤트를 실행합니다.
        /// </summary>
        public R Invoke(T1 t1,T2 t2)
        {
            if (Event != null)
            {
                try
                {
                    return Event(t1,t2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("!!!!!!!!!!!!!!" + ex.ToString());
                }
            }

            return default(R);
        }
    }

    public class CVoidDelegate
    {
        public VoidDelegate Event;//  아무인자도 리턴도 없는 타입
        public void Invoke(bool threading = false)
        {
            if (Event == null) return;
            if (threading)
            {
                new Thread((ThreadStart)delegate
                {
                    Event();
                }).Start();
                return;
            }
            try
            {
                Event();
            }
            catch (Exception ex) { Debug.WriteLine(""+ex); }
        }
    }
}
