//WBArray.cs
using System;

namespace _1006_상속기반관리프로그램
{
    internal class WBArray
    {
		public int Max	{ get; private set; }   //최대 저장공간 개수
		public int Size { get; private set; }   //저장 개수, 저장 위치

		object[] arr;                           //저장소

        #region 생성자 
        public WBArray()
        {
            Max = 10;
            Size = 0;
            arr = new object[Max];  
        }

		public WBArray(int _max)
        {
            Max = _max;
            Size = 0;
            arr = new object[Max];
        }
        #endregion

        #region 배열에 저장된 특정 인덱스의 값 반환(메서드, 인덱서)
        public object getData(int idx)
        {
            if (idx >= 0 && idx < Size)  //유효한 인덱스
                return arr[idx];
            else
                throw new Exception("인덱스 범위를 벗어남"); //return null;
        }
        public object this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < Size)  //유효한 인덱스
                    return arr[idx];
                else
                    throw new Exception("인덱스 범위를 벗어남"); //return null;
            }
        }






        #endregion

        #region 메서드
        public void PushBack(object value)
        {
            if (Max <= Size)   
                throw new Exception("Overflow");

            arr[Size] = value;
            Size++;
        }

		public void Erase(int idx)
        {
            if (idx < 0 && idx >= Size)  //유효한 인덱스인가?
                throw new Exception("잘못된 인덱스");

            for (int i = idx; i < Size - 1; i++)  //이동!
            {
                    arr[i] = arr[i + 1];
            }
            Size--;
        }
        #endregion 
    }
}
