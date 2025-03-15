using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace 서버
{
    [ServiceContract]
    internal interface IControlService
    {
        [OperationContract]
        bool Load();
        [OperationContract]
        bool FormClosed();
        [OperationContract]
        bool insecrtdata(int data, string name, string ph, string ger);

        [OperationContract]
        bool deletedata(int id);

        [OperationContract]
        string selectall();
        [OperationContract]
        string selectitem(int id);
        [OperationContract]
        bool updatedata(int id, string ph);
    }
}
