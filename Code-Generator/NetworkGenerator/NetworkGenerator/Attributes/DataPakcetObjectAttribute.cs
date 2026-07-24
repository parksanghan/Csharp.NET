using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkGenerator.Attributes
{
    /// <summary>
    /// DataObjectAttribute 를 주입받은 IDataObject는 리플렉션 단계에서 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class DataPakcetObjectAttribute : Attribute
    { 
        public EMessageID MessageID { get; set; }

        public DataPakcetObjectAttribute(EMessageID messageId) { 
            MessageID = messageId;  
        }
    }
}
