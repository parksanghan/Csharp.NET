//ContriAccount.cs
using System;


namespace _1006_상속기반관리프로그램
{
    internal class ContriAccount : Account
    {
        public int ContriBution { get; private set; }

        #region 생성자 
        public ContriAccount(int mem_num) : base(mem_num)
        {
            ContriBution = 0;
        }

        public ContriAccount(int mem_num, int balance) : base(mem_num, balance)
        {
            base.OutputMoney((int)(balance * 0.01));
            ContriBution = (int)(balance * 0.01);
        }
        #endregion 

        #region 메서드(입금, 출력)
        public override void InputMoney(int money)
        {
            try
            {
                base.InputMoney(money);
                base.OutputMoney((int)(money * 0.01));
                ContriBution = ContriBution + (int)(money * 0.01);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void Print()
        {
            base.Print();

            Console.Write(ContriBution + "원\t");
        }

        public override void Println()
        {
            base.Print();
            Console.WriteLine("[기 부 금] " + ContriBution + "원");
        }

        #endregion
    }
}
