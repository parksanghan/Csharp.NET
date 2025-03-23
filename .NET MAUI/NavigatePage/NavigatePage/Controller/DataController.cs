using NavigatePage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigatePage.Controller
{
    internal class DataController
    {
        public DataController(MyData data)
        {
            Data = data;
        }

        public MyData Data { get; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
