using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 시험1102
{
    public class Exam
    {
        public int ID { get; private set; }
        public string NAME { get; private set; }
        public string PHONE { get; private set; }
        public string Gender { get; private set; }


        public Exam(int iD, string nAME, string pHONE, string gender)
        {
            ID = iD;
            NAME = nAME;
            PHONE = pHONE;
            Gender = gender;
        }
        public override string ToString()
        {
            return ID +","+ NAME +","+ PHONE +","+ Gender;
        }
    }
}
