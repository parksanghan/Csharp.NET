using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _1120_testSer
{
    internal class Testserivce : Itest
    {
        WBdb db = new WBdb();
        private ItestCallback callback = null;

        public Testserivce()
        {
            db.Connect();
        }
        ~Testserivce() 
        {
            db.Close();
        }

        public void test_Delete(string name)
        {
            bool b = db.Delete(name);
            callback.Delete(b);
        }

        public void test_Insert(Student student)
        {
            callback = OperationContext.Current.GetCallbackChannel<ItestCallback>();
            bool b = db.Insert(student.Name, student.Phone, student.Gender);
            callback.Insert(b);
        }

        public void test_Insert2()
        {
            Student student = new Student();
            callback.Insert2(student);
        }

        public void test_List()
        {
            callback = OperationContext.Current.GetCallbackChannel<ItestCallback>();
            List<Student> students = db.List();
            callback.List(students);
        }
        public void test_Update(string name,string phone,string gender)
        {
            bool b = db.Update(name,phone,gender);
            callback = OperationContext.Current.GetCallbackChannel<ItestCallback>();
            callback.Update(b);
        }
    }
}
