using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _1120_testservice
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class testService1 : Itest
    {
        db db1 = new db();
        private ItestCallback callback = null;

        public testService1()
        {
            db1.Connect();
        }
        ~testService1()
        {
            db1.Close();
        }
        public void Student_Insert(Student student)
        {
            bool b = db1.Insert(student.Name, student.Phone, student.Gender);
            callback.Insert(b);
        }

        public void Student_Delete(string Name, string Phone)
        {
            bool b = db1.Delete(Name, Phone);
            callback.Delete(b);
        }

        public void Student_List()
        {
            callback = OperationContext.Current.GetCallbackChannel<ItestCallback>();
            List<Student> students = db1.List();
            callback.List(students);
        }

        public void Student_Select(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
    