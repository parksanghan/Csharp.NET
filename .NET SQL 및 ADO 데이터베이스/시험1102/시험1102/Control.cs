using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 시험1102
{
    internal class Control
    {
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
           
            if (db.Connect()==true)
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
        public bool insertdata(Exam data)
        {
            return db.insertqury(data);
        }

        public bool deletedata(int id)
        {
            return db.deletedata(id);
        }
        public List<Exam> selectall()
        {
            return db.selectall();
        }


        public Exam selectitem(int id)
        {
            return db.selectitem(id);
        }
        public bool updatedata(int id , string ph)
        {
            return db.updateitem(id, ph);
        }
    }

}
