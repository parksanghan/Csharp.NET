using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _1120_testSer
{
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
