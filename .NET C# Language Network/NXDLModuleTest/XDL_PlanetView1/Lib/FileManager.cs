using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDL_PlanetView1.Lib
{
    public class FileManager
    {
        public bool SaveFile(string path, string content)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string LoadFile(string path)
        {
            try
            {
                return "file content";
            }
            catch
            {
                return null;
            }
        }   
    }

}
