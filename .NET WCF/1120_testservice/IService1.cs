using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _1120_testservice
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ItestCallback))]

    public interface Itest
    { 

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Student_Delete(string Name, string Phone);

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Student_List();

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Student_Insert(Student student);

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Student_Select(Student student);

    }

    public interface ItestCallback
    {
        [OperationContract(IsOneWay = true)]
        void Insert(bool b);
        [OperationContract(IsOneWay = true)]
        void List(List<Student> students);
        [OperationContract(IsOneWay = true)]
        void Delete(bool b);
        [OperationContract(IsOneWay = true)]
        void Select(Student student);
    }

    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Gender { get; set; }

        public override string ToString()
        {
            return "[이름]" + Name + "\t" + "[번호]" + Phone + "\t" + "[성별]" + Gender + "\t";

        }
    }
}
