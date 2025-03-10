using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
인덱서 사용 예제
 */ 
namespace _1005
{
    internal class _02_MyDictionary
    {
        //[0] key0=value0
        //[1] key1=value1
        //[2] key2=value2
        private string[] storage = new string[3];
        
        public _02_MyDictionary()
        {
            for (int i = 0; i < storage.Length; i++)
            {
                storage[i] = string.Format("key{0}=value{0}", i, i);
            }
        }

        #region 인덱서
        public string this[int index] //정수를 인자로 받는 인덱서
        {
            get
            {
                if (AvailIndex(index)) //유효한 인덱스일 때
                {
                    return GetValue(storage[index]); //storage[index] 요소의 값을 반환
                }
                return string.Empty;
            }
        }
        
        public string this[string key] //문자열을 인자로 받는 인덱서
        {
            get
            {
                //key : "key1"
                //element : "key1=value1"
                string element = FindKey(key); //키에 해당하는 요소 문자열을 찾는다.
                
                //return : value1
                return GetValue(element); //요소 문자열에서 값을 추출하여 반환한다.
            }
        }
        #endregion

        #region  메서드
        private bool AvailIndex(int index)
        {
            return (index >= 0) && (index < storage.Length);
        }
        
        private string FindKey(string key)
        {
            //key = "key1"
            //return : "key1=value1"
            foreach (string s in storage) //보관된 각 요소에 대해 반복 수행
            {
                //s : key0=value0 --> key1=value1 --> key2=value2
                if (s.IndexOf(key) == 0) 
                {
                    return s; 
                }
            }
            return string.Empty;
        }

        
        private string GetValue(string s)
        {
            //s : key2=value2
            //index : 4
            //return : value2
            int index = s.IndexOf('='); //'='문자가 시작되는 위치를 찾는다.
            return s.Substring(index + 1); //index+1 뒤에 있는 부분 문자열을 반환한다.
        }

        public int Size //요소를 보관하는 storage의 크기를 반환
        {
            get { return storage.Length; }
        }
        #endregion 
    }
}
