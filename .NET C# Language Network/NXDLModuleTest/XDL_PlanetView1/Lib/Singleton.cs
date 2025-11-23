using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Lib
{
    /// <summary>
    /// 키를 기준으로 Value 싱글톤 인스턴스를 생성합니다.
    /// Ex) Singleton<String,String>
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Val"></typeparam>
    public class Singleton<Key, Val> where Val : new()
    {
      
        private static Dictionary<Key, Val> _instance = new Dictionary<Key, Val>();
   
      
        protected Singleton() { }
        /// <summary>
        /// 키 Value 형태로 싱글톤 Instance에 저장합니다.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Val Instance(Key k)
        {
            lock (_instance)
            {
                CreateInstance(k);
                return _instance[k];
            }
        }

        /// <summary>
        /// 내부호출 : 키를 기준으로 인스턴스 생성합니다. 
        /// </summary>
        /// <param name="k"></param>
        private static void CreateInstance(Key k)
        {
            if (!_instance.ContainsKey(k))
            {
                _instance.Add(k, new Val());
            }
        }
        /// <summary>
        /// 모든 키 Value를 삭제합니다.
        /// </summary>
        public static void Clear()
        {
            lock (_instance)
            {
                _instance.Clear();
            }
        }
        /// <summary>
        ///  해당 키를 삭제합니다.
        /// </summary>
        /// <param name="k"></param>
        public static void Remove(Key k)
        {
            lock (_instance)
            {
                _instance.Remove(k);
            }
        }
        /// <summary>
        /// 
        ///등록된 모든 키를 가져옵니다.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static List<Key> GetKeys(Key k)
        {
            return _instance.Keys.ToList();
        }
        
    }
}
