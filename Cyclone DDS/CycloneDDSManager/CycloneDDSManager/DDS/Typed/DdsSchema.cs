using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CycloneDDSManager.Attr;

namespace CycloneDDSManager.DDS
{
    public sealed class DdsSchemaException : InvalidOperationException
    {
        public DdsSchemaException(string message) : base(message) { }
    }

    internal enum DdsValueKind
    {
        Boolean,
        Byte,
        Int8,
        Int16,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Float32,
        Float64,
        Char16,
        String8,
        Enumeration,
        Structure
    }

    internal sealed class DdsEnumLiteralSchema
    {
        internal string Name;
        internal int Value;
    }

    internal sealed class DdsValueSchema
    {
        internal DdsValueKind Kind;
        internal Type ManagedType;
        internal DdsDynamicTypeKind DynamicKind;
        internal string IdlTypeName;
        internal int NativeSize;
        internal int NativeAlignment;
        internal uint StringBound;
        internal DdsObjectSchema ObjectType;
        internal IReadOnlyList<DdsEnumLiteralSchema> EnumLiterals;
    }

    internal sealed class DdsMemberSchema
    {
        internal uint Id;
        internal string Name;
        internal bool IsKey;
        internal MemberInfo Member;
        internal DdsValueSchema ValueType;
        internal int NativeOffset;

        internal object GetValue(object instance)
        {
            var field = Member as FieldInfo;
            if (field != null) return field.GetValue(instance);
            return ((PropertyInfo)Member).GetValue(instance, null);
        }

        internal void SetValue(object instance, object value)
        {
            var field = Member as FieldInfo;
            if (field != null)
            {
                field.SetValue(instance, value);
                return;
            }
            ((PropertyInfo)Member).SetValue(instance, value, null);
        }
    }

    internal sealed class DdsObjectSchema
    {
        internal Type ManagedType;
        internal string TypeName;
        internal string DdsTypeName;
        internal string Module;
        internal string TopicName;
        internal string Description;
        internal DdsDynamicTypeExtensibility Extensibility;
        internal IReadOnlyList<DdsMemberSchema> Members;
        internal int NativeSize;
        internal int NativeAlignment;
        internal bool IsTopicType;

        internal object CreateInstance()
        {
            if (ManagedType.IsValueType)
                return Activator.CreateInstance(ManagedType);
            return Activator.CreateInstance(ManagedType, true);
        }
    }

    internal static class DdsSchemaCache
    {
        private static readonly object Sync = new object();
        private static readonly Dictionary<Type, DdsObjectSchema> Schemas
            = new Dictionary<Type, DdsObjectSchema>();

        internal static DdsObjectSchema Get<T>()
        {
            return Get(typeof(T));
        }

        internal static DdsObjectSchema Get(Type type)
        {
            lock (Sync)
            {
                DdsObjectSchema schema;
                if (!Schemas.TryGetValue(type, out schema))
                {
                    schema = DdsSchemaBuilder.Build(type);
                    Schemas.Add(type, schema);
                }
                return schema;
            }
        }
    }

    internal static class DdsSchemaBuilder
    {
        internal static DdsObjectSchema Build(Type rootType)
        {
            if (rootType == null) throw new ArgumentNullException(nameof(rootType));

            var topic = (TopicAttribute)Attribute.GetCustomAttribute(
                rootType, typeof(TopicAttribute), true);
            if (topic == null)
                throw Error(rootType, "must have [Topic] or [TopicAttributes].");

            string module = topic.Module ?? string.Empty;
            string typeName = string.IsNullOrWhiteSpace(topic.TypeName)
                ? rootType.Name
                : topic.TypeName;
            ValidateIdentifier(typeName, "DDS type", rootType);
            if (module.Length != 0) ValidateIdentifier(module, "IDL module", rootType);

            var cache = new Dictionary<Type, DdsObjectSchema>();
            var visiting = new HashSet<Type>();
            DdsObjectSchema root = BuildObject(rootType, module, cache, visiting);
            root.IsTopicType = true;
            root.TypeName = typeName;
            root.DdsTypeName = module.Length == 0 ? typeName : module + "::" + typeName;
            root.TopicName = topic.TopicName;
            root.Description = topic.TopicDescription;
            root.Extensibility = topic.Extensibility;
            return root;
        }

        private static DdsObjectSchema BuildObject(
            Type type,
            string module,
            IDictionary<Type, DdsObjectSchema> cache,
            ISet<Type> visiting)
        {
            DdsObjectSchema cached;
            if (cache.TryGetValue(type, out cached)) return cached;
            if (!visiting.Add(type))
                throw Error(type, "contains a recursive member graph, which the attribute mapper does not support.");

            try
            {
                ValidateConstructible(type);
                ValidateIdentifier(type.Name, "nested DDS type", type);

                var discovered = DiscoverMembers(type);
                if (discovered.Count == 0)
                    throw Error(type, "does not contain any [DdsMember] fields or properties.");

                discovered.Sort((left, right) => left.Attribute.Id.CompareTo(right.Attribute.Id));
                for (int index = 0; index < discovered.Count; index++)
                {
                    uint expected = (uint)index;
                    if (discovered[index].Attribute.Id != expected)
                    {
                        throw Error(type,
                            "member IDs must be unique and contiguous from 0. Expected " + expected +
                            " but found " + discovered[index].Attribute.Id + " on " +
                            discovered[index].Member.Name + ".");
                    }
                }

                var schema = new DdsObjectSchema
                {
                    ManagedType = type,
                    TypeName = type.Name,
                    DdsTypeName = module.Length == 0 ? type.Name : module + "::" + type.Name,
                    Module = module,
                    Extensibility = DdsDynamicTypeExtensibility.Final
                };

                var members = new List<DdsMemberSchema>(discovered.Count);
                int offset = 0;
                int structureAlignment = 1;
                foreach (DiscoveredMember item in discovered)
                {
                    Type memberType = GetMemberType(item.Member);
                    DdsValueSchema valueType = BuildValue(
                        memberType, item.Attribute.MaxLength, module, cache, visiting);
                    if (item.Attribute.IsKey && valueType.Kind == DdsValueKind.Structure)
                    {
                        throw Error(type, "nested object member " + item.Member.Name +
                            " cannot be a key in the current mapper.");
                    }

                    string memberName = string.IsNullOrWhiteSpace(item.Attribute.Name)
                        ? item.Member.Name
                        : item.Attribute.Name;
                    ValidateIdentifier(memberName, "DDS member", type);
                    if (members.Any(member => member.Name == memberName))
                        throw Error(type, "contains duplicate DDS member name " + memberName + ".");

                    offset = Align(offset, valueType.NativeAlignment);
                    members.Add(new DdsMemberSchema
                    {
                        Id = item.Attribute.Id,
                        Name = memberName,
                        IsKey = item.Attribute.IsKey,
                        Member = item.Member,
                        ValueType = valueType,
                        NativeOffset = offset
                    });
                    offset += valueType.NativeSize;
                    structureAlignment = Math.Max(structureAlignment, valueType.NativeAlignment);
                }

                schema.Members = members;
                schema.NativeAlignment = structureAlignment;
                schema.NativeSize = Align(offset, structureAlignment);
                cache.Add(type, schema);
                return schema;
            }
            finally
            {
                visiting.Remove(type);
            }
        }

        private static DdsValueSchema BuildValue(
            Type type,
            uint stringBound,
            string module,
            IDictionary<Type, DdsObjectSchema> cache,
            ISet<Type> visiting)
        {
            if (type == typeof(bool)) return Primitive(type, DdsValueKind.Boolean, DdsDynamicTypeKind.Boolean, "boolean", 1);
            if (type == typeof(byte)) return Primitive(type, DdsValueKind.Byte, DdsDynamicTypeKind.Byte, "octet", 1);
            if (type == typeof(sbyte)) return Primitive(type, DdsValueKind.Int8, DdsDynamicTypeKind.Int8, "int8", 1);
            if (type == typeof(short)) return Primitive(type, DdsValueKind.Int16, DdsDynamicTypeKind.Int16, "short", 2);
            if (type == typeof(ushort)) return Primitive(type, DdsValueKind.UInt16, DdsDynamicTypeKind.UInt16, "unsigned short", 2);
            if (type == typeof(int)) return Primitive(type, DdsValueKind.Int32, DdsDynamicTypeKind.Int32, "long", 4);
            if (type == typeof(uint)) return Primitive(type, DdsValueKind.UInt32, DdsDynamicTypeKind.UInt32, "unsigned long", 4);
            if (type == typeof(long)) return Primitive(type, DdsValueKind.Int64, DdsDynamicTypeKind.Int64, "long long", 8);
            if (type == typeof(ulong)) return Primitive(type, DdsValueKind.UInt64, DdsDynamicTypeKind.UInt64, "unsigned long long", 8);
            if (type == typeof(float)) return Primitive(type, DdsValueKind.Float32, DdsDynamicTypeKind.Float32, "float", 4);
            if (type == typeof(double)) return Primitive(type, DdsValueKind.Float64, DdsDynamicTypeKind.Float64, "double", 8);
            if (type == typeof(char)) return Primitive(type, DdsValueKind.Char16, DdsDynamicTypeKind.Char16, "wchar", 2);
            if (type == typeof(string))
            {
                int nativeSize = stringBound == 0
                    ? IntPtr.Size
                    : checked((int)stringBound + 1);
                return new DdsValueSchema
                {
                    Kind = DdsValueKind.String8,
                    ManagedType = type,
                    IdlTypeName = stringBound == 0 ? "string" : "string<" + stringBound + ">",
                    // idlc maps bounded string<N> to char[N+1] and an
                    // unbounded string to char*. Dynamic Type uses the same C layout.
                    NativeSize = nativeSize,
                    NativeAlignment = stringBound == 0 ? IntPtr.Size : 1,
                    StringBound = stringBound
                };
            }
            if (type.IsEnum)
            {
                var literals = new List<DdsEnumLiteralSchema>();
                var values = new HashSet<int>();
                foreach (string name in Enum.GetNames(type))
                {
                    object enumValue = Enum.Parse(type, name);
                    long raw;
                    try { raw = Convert.ToInt64(enumValue); }
                    catch (OverflowException) { throw Error(type, "enum values must fit in signed 32 bits."); }
                    if (raw < int.MinValue || raw > int.MaxValue)
                        throw Error(type, "enum values must fit in signed 32 bits.");
                    if (!values.Add((int)raw))
                        throw Error(type, "enum aliases are not supported by the IDL generator.");
                    ValidateIdentifier(name, "enum literal", type);
                    literals.Add(new DdsEnumLiteralSchema { Name = name, Value = (int)raw });
                }
                ValidateIdentifier(type.Name, "DDS enum", type);
                return new DdsValueSchema
                {
                    Kind = DdsValueKind.Enumeration,
                    ManagedType = type,
                    IdlTypeName = type.Name,
                    NativeSize = 4,
                    NativeAlignment = 4,
                    EnumLiterals = literals
                };
            }
            if (Nullable.GetUnderlyingType(type) != null || type.IsArray || IsGenericCollection(type))
            {
                throw Error(type,
                    "is not supported yet. Use scalar/string/nested [DdsMember] types; arrays, collections and nullable values require sequence/optional mapping.");
            }

            DdsObjectSchema nested = BuildObject(type, module, cache, visiting);
            return new DdsValueSchema
            {
                Kind = DdsValueKind.Structure,
                ManagedType = type,
                IdlTypeName = nested.TypeName,
                NativeSize = nested.NativeSize,
                NativeAlignment = nested.NativeAlignment,
                ObjectType = nested
            };
        }

        private static DdsValueSchema Primitive(
            Type type,
            DdsValueKind kind,
            DdsDynamicTypeKind dynamicKind,
            string idlName,
            int size)
        {
            return new DdsValueSchema
            {
                Kind = kind,
                ManagedType = type,
                DynamicKind = dynamicKind,
                IdlTypeName = idlName,
                NativeSize = size,
                NativeAlignment = size
            };
        }

        private static List<DiscoveredMember> DiscoverMembers(Type type)
        {
            var hierarchy = new Stack<Type>();
            for (Type current = type; current != null && current != typeof(object); current = current.BaseType)
                hierarchy.Push(current);

            var result = new List<DiscoveredMember>();
            while (hierarchy.Count > 0)
            {
                Type current = hierarchy.Pop();
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public |
                                           BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
                foreach (FieldInfo field in current.GetFields(flags))
                {
                    var attribute = (DdsMemberAttribute)Attribute.GetCustomAttribute(
                        field, typeof(DdsMemberAttribute), true);
                    if (attribute == null) continue;
                    if (field.IsStatic || field.IsInitOnly || field.IsLiteral)
                        throw Error(type, field.Name + " must be a writable instance field.");
                    result.Add(new DiscoveredMember(field, attribute));
                }
                foreach (PropertyInfo property in current.GetProperties(flags))
                {
                    var attribute = (DdsMemberAttribute)Attribute.GetCustomAttribute(
                        property, typeof(DdsMemberAttribute), true);
                    if (attribute == null) continue;
                    if (property.GetIndexParameters().Length != 0 ||
                        property.GetGetMethod(true) == null || property.GetSetMethod(true) == null ||
                        property.GetGetMethod(true).IsStatic)
                    {
                        throw Error(type, property.Name + " must be a readable/writable instance property.");
                    }
                    result.Add(new DiscoveredMember(property, attribute));
                }
            }
            return result;
        }

        private static void ValidateConstructible(Type type)
        {
            if (type.IsInterface || type.IsAbstract || type.IsPointer || type.ContainsGenericParameters)
                throw Error(type, "must be a concrete class or struct.");
            if (!type.IsValueType && type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null, Type.EmptyTypes, null) == null)
            {
                throw Error(type, "must have a parameterless constructor for DDS deserialization.");
            }
        }

        private static Type GetMemberType(MemberInfo member)
        {
            var field = member as FieldInfo;
            return field != null ? field.FieldType : ((PropertyInfo)member).PropertyType;
        }

        private static bool IsGenericCollection(Type type)
        {
            if (!type.IsGenericType) return false;
            Type definition = type.GetGenericTypeDefinition();
            return definition == typeof(List<>) || definition == typeof(IList<>) ||
                   definition == typeof(IEnumerable<>) || definition == typeof(IReadOnlyList<>);
        }

        private static int Align(int value, int alignment)
        {
            return (value + alignment - 1) & ~(alignment - 1);
        }

        private static void ValidateIdentifier(string value, string label, Type owner)
        {
            if (string.IsNullOrEmpty(value) || !IsIdentifierStart(value[0]) ||
                value.Skip(1).Any(character => !IsIdentifierPart(character)))
            {
                throw Error(owner, label + " name '" + value + "' is not a valid portable IDL identifier.");
            }
        }

        private static bool IsIdentifierStart(char value)
        {
            return value == '_' || (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
        }

        private static bool IsIdentifierPart(char value)
        {
            return IsIdentifierStart(value) || (value >= '0' && value <= '9');
        }

        private static DdsSchemaException Error(Type type, string message)
        {
            return new DdsSchemaException(type.FullName + " " + message);
        }

        private sealed class DiscoveredMember
        {
            internal DiscoveredMember(MemberInfo member, DdsMemberAttribute attribute)
            {
                Member = member;
                Attribute = attribute;
            }

            internal MemberInfo Member { get; private set; }
            internal DdsMemberAttribute Attribute { get; private set; }
        }
    }
}
