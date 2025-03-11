//accountio.cs
using System;

namespace _1025_계좌실습
{
    internal class AccountIO
    {
        #region 맴버필드 & 프로퍼티 
        public int Id { get; private set; }
        public int Input { get; private set; }
        public int Output { get; private set; }
        public int Balance { get; private set; }

        public DateTime Date { get; private set; }
        #endregion

        #region 생성자
        public AccountIO(int _id, int _input, int _output, int _balance, DateTime date)
        {
            Id = _id;
            Input = _input;
            Output = _output;
            Balance = _balance;
            Date = date;
        }
        #endregion

        public override string ToString()
        {
            return Id + "\t" + Input + "\t" + Output + "\t" + Balance + "\t" + Date;
        }
    }
}
