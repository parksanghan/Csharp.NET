//control.cs
using System;
using System.Collections.Generic;


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
        
        public bool Account_Input(int accid, int money)
        {
            return db.Account_Input(accid, money);
        }

        public bool Account_Output(int accid, int money)
        {
            return db.Account_Output(accid, money);
        }

        public List<AccountIO> AccountIO_SelectAll(int accid)
        {
            List<string> accounts = db.AccountIO_SelectAll(accid);

            List<AccountIO> accs = new List<AccountIO>();
            foreach (string account in accounts)
            {
                string[] sp = account.Split('#');
                accs.Add(new AccountIO(
                    int.Parse(sp[1]), int.Parse(sp[2]), int.Parse(sp[3]),
                    int.Parse(sp[4]), DateTime.Parse(sp[5])));
            }
            return accs;
        }
        #endregion
    }
}

