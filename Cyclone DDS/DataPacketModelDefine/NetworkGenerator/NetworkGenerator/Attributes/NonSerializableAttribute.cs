using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkGenerator.Attributes
{
    public class NonSerializableAttribute : Attribute
    {
        public string Reason {  get; set; }

        public NonSerializableAttribute() {
            Reason = String.Empty;
        }
        public NonSerializableAttribute(string reason)
        {
            /// Reason에 reason을 할당한다.
            Reason = reason;

        }
    }
}
