using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace 서버
{
    [ServiceContract]
    internal interface IDatabase
    {
        [OperationContract]
        bool Connect();
        [OperationContract]
        bool Close();
        [OperationContract]
        bool insertqury(int id, string name, string ph, string gender);
        [OperationContract]
        bool deletedata(int id);
        [OperationContract]
        string selectall();
        [OperationContract]
        string selectitem(int id);
        [OperationContract]
        bool updateitem(int id, string ph);
    }
}
