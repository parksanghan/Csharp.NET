using System;
using System.Collections.Generic;

namespace CycloneDDSManager.DDS
{
    internal sealed class DdsDynamicTypeGraph : IDisposable
    {
        private readonly List<DdsDynamicType> _ownedTypes = new List<DdsDynamicType>();
        private readonly Dictionary<Type, DdsDynamicType> _structures = new Dictionary<Type, DdsDynamicType>();
        private readonly Dictionary<Type, DdsDynamicType> _enumerations = new Dictionary<Type, DdsDynamicType>();
        private readonly DdsParticipant _participant;
        private readonly DdsObjectSchema _rootSchema;

        private DdsDynamicTypeGraph(DdsParticipant participant, DdsObjectSchema rootSchema)
        {
            _participant = participant;
            _rootSchema = rootSchema;
        }

        internal DdsDynamicType Root { get; private set; }

        internal static DdsDynamicTypeGraph Build(DdsParticipant participant, DdsObjectSchema schema)
        {
            if (participant == null) throw new ArgumentNullException(nameof(participant));
            if (schema == null) throw new ArgumentNullException(nameof(schema));

            var graph = new DdsDynamicTypeGraph(participant, schema);
            try
            {
                graph.Root = graph.BuildStructure(schema);
                return graph;
            }
            catch
            {
                graph.Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            for (int index = _ownedTypes.Count - 1; index >= 0; index--)
                _ownedTypes[index].Dispose();
            _ownedTypes.Clear();
            _structures.Clear();
            _enumerations.Clear();
            Root = null;
        }

        private DdsDynamicType BuildStructure(DdsObjectSchema schema)
        {
            DdsDynamicType existing;
            if (_structures.TryGetValue(schema.ManagedType, out existing)) return existing;

            var type = DdsDynamicType.CreateStructure(_participant, schema.DdsTypeName)
                .SetExtensibility(schema.Extensibility);
            if (!schema.IsTopicType) type.SetNested(true);
            _structures.Add(schema.ManagedType, type);
            _ownedTypes.Add(type);

            foreach (DdsMemberSchema member in schema.Members)
            {
                AddMember(type, member);
                if (member.IsKey) type.SetMemberKey(member.Id);
            }
            return type;
        }

        private void AddMember(DdsDynamicType owner, DdsMemberSchema member)
        {
            DdsValueSchema value = member.ValueType;
            switch (value.Kind)
            {
                case DdsValueKind.String8:
                    using (DdsDynamicType stringType = DdsDynamicType.CreateString8(
                        _participant, value.StringBound == 0 ? (uint?)null : value.StringBound))
                    {
                        owner.AddMember(member.Name, stringType, member.Id, member.Id);
                    }
                    break;

                case DdsValueKind.Enumeration:
                    owner.AddMember(member.Name, BuildEnumeration(value), member.Id, member.Id);
                    break;

                case DdsValueKind.Structure:
                    owner.AddMember(member.Name, BuildStructure(value.ObjectType), member.Id, member.Id);
                    break;

                default:
                    owner.AddPrimitiveMember(member.Name, value.DynamicKind, member.Id, member.Id);
                    break;
            }
        }

        private DdsDynamicType BuildEnumeration(DdsValueSchema schema)
        {
            DdsDynamicType existing;
            if (_enumerations.TryGetValue(schema.ManagedType, out existing)) return existing;

            string typeName = string.IsNullOrEmpty(_rootSchema.Module)
                ? schema.IdlTypeName
                : _rootSchema.Module + "::" + schema.IdlTypeName;
            var type = DdsDynamicType.CreateEnumeration(_participant, typeName);
            _enumerations.Add(schema.ManagedType, type);
            _ownedTypes.Add(type);
            foreach (DdsEnumLiteralSchema literal in schema.EnumLiterals)
            {
                type.AddEnumLiteral(
                    literal.Name,
                    DdsDynamicEnumLiteralValue.Explicit(literal.Value));
            }
            return type;
        }
    }
}
