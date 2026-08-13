using System;
using System.Collections.Generic;
using System.Text;

namespace CycloneDDSManager.Attr
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class TopicAttributes: Attribute
    {
        public TopicAttributes(string topicname , string topicdesc)
        {
            TopicName  = topicname;
            TopicDescription = topicdesc;
        }
        public TopicAttributes(string topicname)
        {
            TopicName  =topicname;
        }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DdsMemberAttribute : Attribute
    {
        public DdsMemberAttribute(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
        public bool IsKey { get; set; }
    }
}
