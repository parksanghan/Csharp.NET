using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1031_조별실습
{
    internal class Database
    {
        private SqlConnection scon = null;
        private const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";

        #region 서버 연결, 해제
        public bool load()
        {
            try
            {
                scon = new SqlConnection();
                scon.ConnectionString = constr;
                scon.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool close()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
