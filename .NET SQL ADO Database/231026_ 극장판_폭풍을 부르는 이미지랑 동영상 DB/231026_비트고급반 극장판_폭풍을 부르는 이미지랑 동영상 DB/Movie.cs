using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB
{
    internal class Movie
    {
        public string name;     //제목
        public string genre;   //장르
        public int runtime;     //러닝타임

        public DateTime releaseTime;    //개봉일

        public string description;   //국가
        public string poster;    //포스터 경로
        public string videoLink;    //영상 링크 

        public Movie(string name, string genre, int runtime, DateTime releaseTime, string description, string poster, string videoLink)
        {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.releaseTime = releaseTime;
            this.description = description;
            this.poster = poster;
            this.videoLink = videoLink;
        }
        
        public override string ToString() 
        {
            return name + "\t개요 : " + genre+ "("+ runtime + "분)";
        }
    }
}
