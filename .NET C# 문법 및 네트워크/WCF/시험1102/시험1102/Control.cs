using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 시험1102.ServiceReference1;
namespace 시험1102
{
    internal class Control
    {
        public SqlConnection scon = null;
        public const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";

        public SqlDataReader reader = null;
        DB db = null;
        public  Form1 form = null;
       
        #region 싱글톤
        static public Control Singleton { get; private set; }
        static Control()
        {
            Singleton = new Control();
        }
        private Control()
        {
            db = new DB();
        }
        #endregion
        public bool Load()
        {
           
            if (dbClient.Connect()==true)
                return true;
            else
                return false;
        }
        public bool FormClosed()
        {
            if (dbClient.Close())
                return true;
            else
                return false;
        }
        public bool insecrtdata(int data, string name , string ph, string ger)
        {
            return dbClient.insertqury(data,name, ph, ger);
        }

        public bool deletedata(int id)
        {
            return dbClient.deletedata(id);
        }
        public string selectall()
        {
            return dbClient.selectall();
        }


        public string selectitem(int id)
        {
            return dbClient.selectitem(id);
        }
        public bool updatedata(int id, string ph)
        {
            return  dbClient.updateitem(id, ph);
        }
    }

}
