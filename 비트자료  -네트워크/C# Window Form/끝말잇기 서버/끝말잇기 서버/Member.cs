using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 끝말잇기_서버
{
    internal class Member
    {
        public string Id { get;private set; }
        public string Pasword { get; private set; }

        public int AccountMoney { get; private set; }


        public Member(string id, string pasword)
        {
            Id = id;
            Pasword = pasword;
            AccountMoney = 5000;
        }

        public bool InputMoney(int money)
        {
            try
            {
                if (money > 0)
                {
                    AccountMoney += money;
                    return true;
                }
                else return false;
                
            }
            catch(Exception ex) {throw new Exception(ex.Message);    }
        }
        public bool OutputMoney(int money )
            
        {
            try
            {
                if (money >= 0 && AccountMoney >= money)
                {
                    AccountMoney -= money;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
