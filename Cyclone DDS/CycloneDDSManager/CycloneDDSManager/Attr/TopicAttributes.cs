using System;
using CycloneDDSManager.DDS;

namespace CycloneDDSManager.Attr
{
    /// <summary>Marks a class or struct as a DDS topic data type.</summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public class TopicAttribute : Attribute
    {
        public TopicAttribute(string topicName)
        {
            if (string.IsNullOrWhiteSpace(topicName))
                throw new ArgumentException("A DDS topic name is required.", nameof(topicName));

            TopicName = topicName;
        }

        public TopicAttribute(string topicName, string topicDescription)
            : this(topicName)
        {
            TopicDescription = topicDescription;
        }

        /// <summary>DDS topic name used to match writers and readers.</summary>
        public string TopicName { get; set; }

        /// <summary>
        /// DDS/IDL type name. The annotated CLR type name is used when omitted.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>Optional IDL module. It also prefixes the DDS type name.</summary>
        public string Module { get; set; }

        /// <summary>Human-readable comment written to generated IDL.</summary>
        public string TopicDescription { get; set; }

        /// <summary>Extensibility used by both generated IDL and Dynamic Type.</summary>
        public DdsDynamicTypeExtensibility Extensibility { get; set; }
            = DdsDynamicTypeExtensibility.Final;
    }

    /// <summary>
    /// Compatibility alias for the original plural attribute name. New code
    /// should use <see cref="TopicAttribute"/> (written as [Topic]).
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public sealed class TopicAttributes : TopicAttribute
    {
        public TopicAttributes(string topicName) : base(topicName) { }

        public TopicAttributes(string topicName, string topicDescription)
            : base(topicName, topicDescription) { }
    }

    /// <summary>Includes a field or property in the DDS wire type.</summary>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public sealed class DdsMemberAttribute : Attribute
    {
        public DdsMemberAttribute(uint id)
        {
            Id = id;
        }

        /// <summary>
        /// Member ID and serialization order. IDs must be contiguous from 0
        /// so generated IDL and runtime Dynamic Type are identical.
        /// </summary>
        public uint Id { get; private set; }

        public bool IsKey { get; set; }

        /// <summary>IDL/DDS member name. The CLR member name is used when omitted.</summary>
        public string Name { get; set; }

        /// <summary>Maximum UTF-8 string length; 0 means unbounded.</summary>
        public uint MaxLength { get; set; }
    }
}
