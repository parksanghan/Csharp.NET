//FaithAccount.cs
using System;

namespace _1006_상속기반관리프로그램
{
    internal class FaithAccount : Account
	{
        #region 생성자 
        public FaithAccount(int mem_num) : base(mem_num)
        {
        }
       
        public FaithAccount(int mem_num, int balance) : base(mem_num, balance)
        {
            base.InputMoney((int)(balance * 0.01));
        }
        #endregion 

        #region 메서드(입금)
        public override void InputMoney(int money)
        {
            try
            {
                base.InputMoney(money);
                base.InputMoney((int)(money * 0.01));
                Console.WriteLine("입금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
