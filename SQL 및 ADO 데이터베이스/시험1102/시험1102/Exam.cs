using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 시험1102
{
    internal class Exam
    {
        internal int ID { get; private set; }
        internal string NAME { get; private set; }
        internal string PHONE { get; private set; }
        internal string Gender { get; private set; }


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
