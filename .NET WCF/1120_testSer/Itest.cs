using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _1120_testSer
{
    //(클라->서버)
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ItestCallback))]
    public interface Itest
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void test_Insert(Student student);
        //[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        //void test_Select(Student student);
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void test_List();
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void test_Delete(string name);
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void test_Insert2();
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void test_Update(string name, string phone, string gender);

    }

    //(서버->클라)
    public interface ItestCallback
    {
        [OperationContract(IsOneWay = true)]
        void Insert2(Student student);
        [OperationContract(IsOneWay = true)]
        void Insert(bool b);
        //[OperationContract(IsOneWay = true)]
        //void Select(bool b);
        [OperationContract(IsOneWay = true)]
        void Delete(bool b);
        [OperationContract(IsOneWay = true)]
        void List(List<Student> students);
        [OperationContract(IsOneWay = true)]
        void Update(bool b);
    }
}
