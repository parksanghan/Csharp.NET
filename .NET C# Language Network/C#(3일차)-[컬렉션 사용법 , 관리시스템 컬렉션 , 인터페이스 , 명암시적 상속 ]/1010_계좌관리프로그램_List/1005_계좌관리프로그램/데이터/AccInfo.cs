//AccInfo.cs
using System;

namespace _1005_계좌관리프로그램
{
    internal class AccInfo
    {
        #region 맴버필드 & 프로퍼티 
        public int Id       { get; private set; }
        public int Input    { get; private set; }
        public int Output   { get; private set; }
        public int Balance  { get; private set; }
        #endregion

        #region 생성자 & 소멸자
        public AccInfo(int _id, int _input, int _output, int _balance)
        {
            Id          = _id;
            Input       = _input;
            Output      = _output;
            Balance     = _balance;
        }
        ~AccInfo()
        {

        }
        #endregion

        #region 메서드
        public void Print()
        {
            Console.Write(Id + "\t\t");
            Console.Write(Input + "\t\t");
            Console.Write(Output + "\t\t");
            Console.WriteLine(Balance);
        }
        #endregion
    }
}
