using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLIIB.Common
{
    public class ClsSingletn<T> where T : new()  // Generic Singleton Class
    {
        public static readonly Lazy<T> instance = new Lazy<T>(() => new T());
        // 접근 불가 전역 인스턴스
        public static T Instance => instance.Value;
        // private 생성자: 외부에서 인스턴스화 불가
        protected ClsSingletn() { } 
    }
}
