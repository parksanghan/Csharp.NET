using System;

namespace NetworkGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DataPakcetObjectAttribute : Attribute
    {
        public DataPakcetObjectAttribute(int messageId)
        {
            MessageID = messageId;
        }

        public int MessageID { get; private set; }
    }

    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public sealed class PacketFieldAttribute : Attribute
    {
        public PacketFieldAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; private set; }
    }
}
