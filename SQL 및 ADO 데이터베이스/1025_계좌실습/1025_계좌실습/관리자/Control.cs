using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1025_계좌실습
{
    internal class Control
    {
        private WbDB db = null;

        #region 싱글톤
        public static Control Instance = null;
        static Control()
        {
            Instance = new Control();
        }

        private Control()
        {
        }
        #endregion

        #region 시작 & 종료
        public bool Load()
        {
            db = new WbDB();
            return db.Connect();
            
        }

        public bool FormClosed()
        {
            return  db.Close();
        }
        #endregion

        #region 기능
        public bool Account_Insert(int accid, string name, int balance)
        {
            return db.Account_Insert(accid, name, balance);
        }

        public bool Account_Delete(int accid)
        {
            return db.Account_Delete(accid);
        }
        
        public List<Account> Account_SelectAll()
        {
            List<string> accounts = db.Account_SelectAll();

            List<Account> accs = new List<Account>();
            foreach(string account in accounts)
            {
                string[] sp = account.Split('#');
                accs.Add( new Account(int.Parse(sp[0]), sp[1], int.Parse(sp[2])));
            }
            return accs; 
        }

        public Account Accont_Select(int accid)
        {
            string account_string = db.Account_Select(accid);
            if (account_string == null)
                return null;

            string[] sp = account_string.Split('#');
            Account account = new Account(int.Parse(sp[0]), sp[1], int.Parse(sp[2]));
            return account;
        }
        #endregion
    }
}

