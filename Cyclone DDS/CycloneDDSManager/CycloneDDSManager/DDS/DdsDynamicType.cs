using System;
using System.Runtime.InteropServices;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    /// <summary>
    /// Managed builder for Cyclone DDS Dynamic Type. It supports structures,
    /// enum/bitmask types and primitive or reusable-type members.
    /// </summary>
    public sealed class DdsDynamicType : IDisposable
    {
        private DdsDynamicTypeNative _native;
        private bool _registered;
        private bool _disposed;

        private DdsDynamicType(DdsDynamicTypeNative native, DdsDynamicTypeKind kind, string name)
        {
            _native = native;
            Kind = kind;
            Name = name;
            DdsError.Check(native.ReturnCode, "dds_dynamic_type_create");
        }

        public DdsDynamicTypeKind Kind { get; private set; }
        public string Name { get; private set; }

        public static DdsDynamicType CreateStructure(DdsParticipant participant, string name)
        {
            return CreateNamed(participant, DdsDynamicTypeKind.Structure, name);
        }

        public static DdsDynamicType CreateEnumeration(DdsParticipant participant, string name)
        {
            return CreateNamed(participant, DdsDynamicTypeKind.Enumeration, name);
        }

        public static DdsDynamicType CreateBitmask(DdsParticipant participant, string name)
        {
            return CreateNamed(participant, DdsDynamicTypeKind.Bitmask, name);
        }

        public static DdsDynamicType CreateString8(DdsParticipant participant, uint? bound = null)
        {
            if (participant == null) throw new ArgumentNullException(nameof(participant));
            IntPtr bounds = IntPtr.Zero;
            try
            {
                if (bound.HasValue)
                {
                    if (bound.Value == 0) throw new ArgumentOutOfRangeException(nameof(bound));
                    bounds = Marshal.AllocHGlobal(sizeof(uint));
                    Marshal.WriteInt32(bounds, unchecked((int)bound.Value));
                }

                var descriptor = new DdsDynamicTypeDescriptorNative
                {
                    Kind = DdsDynamicTypeKind.String8,
                    BoundCount = bound.HasValue ? 1u : 0u,
                    Bounds = bounds
                };
                DdsDynamicTypeNative value = DdsNative.dds_dynamic_type_create(participant.Handle, descriptor);
                return new DdsDynamicType(value, DdsDynamicTypeKind.String8, null);
            }
            finally
            {
                if (bounds != IntPtr.Zero) Marshal.FreeHGlobal(bounds);
            }
        }

        public static DdsDynamicType CreateSequence(
            DdsParticipant participant,
            string name,
            DdsDynamicTypeKind primitiveElementType,
            uint? bound = null)
        {
            if (participant == null) throw new ArgumentNullException(nameof(participant));
            using (var nativeName = new Utf8String(name))
            {
                IntPtr bounds = IntPtr.Zero;
                try
                {
                    if (bound.HasValue)
                    {
                        bounds = Marshal.AllocHGlobal(sizeof(uint));
                        Marshal.WriteInt32(bounds, unchecked((int)bound.Value));
                    }

                    var descriptor = new DdsDynamicTypeDescriptorNative
                    {
                        Kind = DdsDynamicTypeKind.Sequence,
                        Name = nativeName.Pointer,
                        BoundCount = bound.HasValue ? 1u : 0u,
                        Bounds = bounds,
                        ElementType = DdsDynamicTypeSpecNative.FromPrimitive(primitiveElementType)
                    };
                    DdsDynamicTypeNative value = DdsNative.dds_dynamic_type_create(participant.Handle, descriptor);
                    return new DdsDynamicType(value, DdsDynamicTypeKind.Sequence, name);
                }
                finally
                {
                    if (bounds != IntPtr.Zero) Marshal.FreeHGlobal(bounds);
                }
            }
        }

        public DdsDynamicType SetExtensibility(DdsDynamicTypeExtensibility value)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_type_set_extensibility(ref _native, value),
                "dds_dynamic_type_set_extensibility");
            return this;
        }

        public DdsDynamicType SetAutoId(DdsDynamicTypeAutoId value)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_type_set_autoid(ref _native, value),
                "dds_dynamic_type_set_autoid");
            return this;
        }

        public DdsDynamicType SetNested(bool value)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_type_set_nested(ref _native, value),
                "dds_dynamic_type_set_nested");
            return this;
        }

        public DdsDynamicType AddPrimitiveMember(
            string name,
            DdsDynamicTypeKind primitiveType,
            uint memberId = DdsConstants.DynamicMemberIdAuto,
            uint index = DdsConstants.DynamicMemberIndexEnd)
        {
            EnsureMutable();
            using (var nativeName = new Utf8String(name))
            {
                var descriptor = new DdsDynamicMemberDescriptorNative
                {
                    Name = nativeName.Pointer,
                    Id = memberId,
                    Index = index,
                    Type = DdsDynamicTypeSpecNative.FromPrimitive(primitiveType)
                };
                DdsError.Check(DdsNative.dds_dynamic_type_add_member(ref _native, descriptor),
                    "dds_dynamic_type_add_member");
            }
            return this;
        }

        public DdsDynamicType AddMember(
            string name,
            DdsDynamicType memberType,
            uint memberId = DdsConstants.DynamicMemberIdAuto,
            uint index = DdsConstants.DynamicMemberIndexEnd)
        {
            if (memberType == null) throw new ArgumentNullException(nameof(memberType));
            EnsureMutable();
            memberType.EnsureAlive();

            DdsDynamicTypeNative reference = DdsNative.dds_dynamic_type_ref(ref memberType._native);
            DdsError.Check(reference.ReturnCode, "dds_dynamic_type_ref");
            bool transferred = false;
            try
            {
                using (var nativeName = new Utf8String(name))
                {
                    var descriptor = new DdsDynamicMemberDescriptorNative
                    {
                        Name = nativeName.Pointer,
                        Id = memberId,
                        Index = index,
                        Type = DdsDynamicTypeSpecNative.FromType(reference)
                    };
                    DdsError.Check(DdsNative.dds_dynamic_type_add_member(ref _native, descriptor),
                        "dds_dynamic_type_add_member");
                    transferred = true;
                }
            }
            finally
            {
                if (!transferred)
                    DdsNative.dds_dynamic_type_unref(ref reference);
            }
            return this;
        }

        public DdsDynamicType SetMemberKey(uint memberId, bool value = true)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_member_set_key(ref _native, memberId, value),
                "dds_dynamic_member_set_key");
            return this;
        }

        public DdsDynamicType SetMemberOptional(uint memberId, bool value = true)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_member_set_optional(ref _native, memberId, value),
                "dds_dynamic_member_set_optional");
            return this;
        }

        public DdsDynamicType SetMemberExternal(uint memberId, bool value = true)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_member_set_external(ref _native, memberId, value),
                "dds_dynamic_member_set_external");
            return this;
        }

        public DdsDynamicType SetMemberMustUnderstand(uint memberId, bool value = true)
        {
            EnsureMutable();
            DdsError.Check(DdsNative.dds_dynamic_member_set_must_understand(ref _native, memberId, value),
                "dds_dynamic_member_set_must_understand");
            return this;
        }

        public DdsDynamicType AddEnumLiteral(
            string name,
            DdsDynamicEnumLiteralValue value,
            bool isDefault = false)
        {
            EnsureMutable();
            using (var nativeName = new Utf8String(name))
            {
                DdsError.Check(DdsNative.dds_dynamic_type_add_enum_literal(
                    ref _native, nativeName.Pointer, value, isDefault),
                    "dds_dynamic_type_add_enum_literal");
            }
            return this;
        }

        public DdsDynamicType AddBitmaskField(
            string name,
            ushort position = DdsConstants.DynamicBitmaskPositionAuto)
        {
            EnsureMutable();
            using (var nativeName = new Utf8String(name))
            {
                DdsError.Check(DdsNative.dds_dynamic_type_add_bitmask_field(
                    ref _native, nativeName.Pointer, position),
                    "dds_dynamic_type_add_bitmask_field");
            }
            return this;
        }

        /// <summary>Registers this type, creates a temporary descriptor, and creates a topic.</summary>
        public DdsTopic RegisterAndCreateTopic(
            DdsParticipant participant,
            string topicName,
            DdsQos qos = null,
            DdsListener listener = null,
            DdsFindScope scope = DdsFindScope.LocalDomain,
            TimeSpan? timeout = null)
        {
            if (participant == null) throw new ArgumentNullException(nameof(participant));
            EnsureAlive();

            IntPtr typeInfo = IntPtr.Zero;
            IntPtr descriptor = IntPtr.Zero;
            try
            {
                DdsError.Check(DdsNative.dds_dynamic_type_register(ref _native, out typeInfo),
                    "dds_dynamic_type_register");
                _registered = true;

                long wait = timeout.HasValue ? DdsConstants.ToNanoseconds(timeout.Value) : 0L;
                DdsError.Check(DdsNative.dds_create_topic_descriptor(
                    scope, participant.Handle, typeInfo, wait, out descriptor),
                    "dds_create_topic_descriptor");

                return participant.CreateTopic(descriptor, topicName, qos, listener);
            }
            finally
            {
                if (descriptor != IntPtr.Zero)
                    DdsNative.dds_delete_topic_descriptor(descriptor);
                if (typeInfo != IntPtr.Zero)
                    DdsNative.dds_free_typeinfo(typeInfo);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_native.Opaque0 != IntPtr.Zero || _native.Opaque1 != IntPtr.Zero)
                DdsNative.dds_dynamic_type_unref(ref _native);
        }

        private static DdsDynamicType CreateNamed(
            DdsParticipant participant,
            DdsDynamicTypeKind kind,
            string name)
        {
            if (participant == null) throw new ArgumentNullException(nameof(participant));
            using (var nativeName = new Utf8String(name))
            {
                var descriptor = new DdsDynamicTypeDescriptorNative
                {
                    Kind = kind,
                    Name = nativeName.Pointer
                };
                DdsDynamicTypeNative value = DdsNative.dds_dynamic_type_create(participant.Handle, descriptor);
                return new DdsDynamicType(value, kind, name);
            }
        }

        private void EnsureAlive()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DdsDynamicType));
            DdsError.Check(_native.ReturnCode, "Dynamic Type");
        }

        private void EnsureMutable()
        {
            EnsureAlive();
            if (_registered)
                throw new InvalidOperationException("A registered Dynamic Type is immutable.");
        }
    }
}
