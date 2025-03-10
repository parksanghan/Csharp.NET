using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 서버
{
    internal class ControlService:IControlService
    {
        public SqlConnection scon = null;
        public const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";
        public SqlConnection SCon { get { return scon; } }
        public SqlDataReader reader = null;
       
        //public Form1 form = null;
        Database db = Database.Singleton;
        #region 싱글톤
        static public ControlService Singleton { get; private set; }
        static ControlService()
        {
            Singleton = new ControlService();
        }
        private ControlService()
        {
            
        }
        #endregion
        public bool Load()
        {

            if (db.Connect() == true)
                return true;
            else
                return false;
        }
        public bool FormClosed()
        {
            if (db.Close())
                return true;
            else
                return false;
        }
        public bool insecrtdata(int data, string name, string ph, string ger)
        {
            return db.insertqury(data, name, ph, ger);
        }

        public bool deletedata(int id)
        {
            return db.deletedata(id);
        }
        public string selectall()
        {
            return db.selectall();
        }


        public string selectitem(int id)
        {
            return db.selectitem(id);
        }
        public bool updatedata(int id, string ph)
        {
            return db.updateitem(id, ph);
        }
    }
}
