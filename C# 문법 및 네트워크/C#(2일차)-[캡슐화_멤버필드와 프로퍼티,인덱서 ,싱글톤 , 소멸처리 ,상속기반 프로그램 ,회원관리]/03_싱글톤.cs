using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Gof의 디자인패턴 중 싱글톤 패턴
- 단 한개의 객체만 생성

 */ 
namespace _1005
{
    internal class _03_Singleton
    {
        #region 싱글톤 패턴
        static _03_Singleton instance;
        public static _03_Singleton Instance
        {
            get { return instance; } 
        }

        static _03_Singleton()  //최초 한번만 호출
        {
            instance = new _03_Singleton();
        }

        private _03_Singleton() { }
        #endregion 
    }
}
