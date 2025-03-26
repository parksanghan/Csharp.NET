using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp9
{
    internal class Movie
    {
        public string title{ get; set; }
        public DateTime release { get; set; }
        public int timemin { get; set; }

        public string descrition { get; set; }

        public string imagepath { get; set; }

        public string linker { get; set; }
        

        public Movie(string title, DateTime appear , int timeminoute , string dsec , string Imageload , string linker)
        {
            this.title = title;
            this.release = appear;
                
            this.timemin = timeminoute;
            this.descrition = dsec;
                this.imagepath = Imageload;
            this.linker = linker;

        }
        public override string ToString()
        {
            return title +"\t"+ release +"\t"+ timemin + "\t"+ descrition
                + "\t"+ imagepath+ "\t"+ linker;
        }


    }
}
