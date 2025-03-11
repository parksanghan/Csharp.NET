using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1031_조별실습
{
    internal class Data
    {
        #region 프로퍼티
        internal string Title { get; private set; }
        internal string Link {  get; private set; }
        internal string Image { get; private set; }
        internal string Author { get; private set; }
        internal int Discount { get; private set; }
        internal string Publisher { get; private set; }
        internal string Pubdate {  get; private set; }
        internal long Isbn {  get; private set; }
        internal string Description { get; private set; }
        #endregion

        #region 생성자
        public Data(string title, string link, string image, string author, int discount, string publisher, string pubdate, long isbn, string description)
        {
            Title = title;
            Link = link;
            Image = image;
            Author = author;
            Discount = discount;
            Publisher = publisher;
            Pubdate = pubdate;
            Isbn = isbn;
            Description = description;
        }
        #endregion

        #region 메서드
        private static string ConvertString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf('<');
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.IndexOf('>');
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }
        #endregion
    }
}
