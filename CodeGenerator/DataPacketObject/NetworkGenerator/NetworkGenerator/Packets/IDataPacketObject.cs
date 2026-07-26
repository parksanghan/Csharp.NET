using NetworkGenerator.Binary;
using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetworkGenerator.Packets
{
    public interface IDataPacketObject
    {
        EMessageID MessageID { get; }
        ushort MessageSync { get; }
        Type PayloadType { get; }
        object DataObject { get; }

        byte[] Serialize();
        void UpdateValue(byte[] payloadBytes);
    }

    public abstract class DataPacketObject<TData> : IDataPacketObject
        where TData : struct
    {
        private readonly object _stateLock = new object();

        public abstract EMessageID MessageID { get; }

        public virtual ushort MessageSync
        {
            get { return 5555; }
        }

        public abstract TData m_Data { get; set; }

        protected abstract Dictionary<string, double> m_Resolutions { get; }
        protected abstract Dictionary<string, double> m_MaxValues { get; }
        protected abstract Dictionary<string, double> m_MinValues { get; }

        public Type PayloadType
        {
            get { return typeof(TData); }
        }

        public object DataObject
        {
            get
            {
                lock (_stateLock)
                {
                    return m_Data;
                }
            }
        }

        public event EventHandler DataUpdated;

        public virtual void Validate()
        {
            TData snapshot;
            lock (_stateLock)
            {
                snapshot = m_Data;
            }

            Validate(snapshot);
        }

        public byte[] Serialize()
        {
            TData snapshot;
            lock (_stateLock)
            {
                snapshot = m_Data;
            }

            Validate(snapshot);

            byte[] bodyBytes = BinaryManager.SerializeStruct(snapshot);
            MESSAGEHEADER header = GetMessageHeader(bodyBytes.Length);
            return BinaryManager.SerializeWithHeader(header, bodyBytes);
        }

        public void UpdateValue(byte[] payloadBytes)
        {
            UpdateValue(payloadBytes, MessageID);
        }

        public TData UpdateValue(
            byte[] payloadBytes,
            EMessageID messageId)
        {
            if (messageId != MessageID)
            {
                throw new InvalidOperationException(
                    "MessageID mismatch: expected=" + MessageID +
                    ", actual=" + messageId + ".");
            }

            TData received =
                BinaryManager.DeserializeStruct<TData>(payloadBytes);

            Validate(received);

            lock (_stateLock)
            {
                m_Data = received;
            }

            EventHandler handler = DataUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            return received;
        }

        public MESSAGEHEADER GetMessageHeader(int bodyLength)
        {
            return new MESSAGEHEADER
            {
                snyc = MessageSync,
                messageid = (int)MessageID,
                messagesize = bodyLength
            };
        }

        protected virtual void Validate(TData data)
        {
            foreach (MemberInfo member in GetNumericMembers())
            {
                double max;
                double min;
                bool hasMax = m_MaxValues.TryGetValue(member.Name, out max);
                bool hasMin = m_MinValues.TryGetValue(member.Name, out min);

                if (!hasMax && !hasMin)
                {
                    continue;
                }

                object raw = GetMemberValue(member, data);
                if (!(raw is IConvertible))
                {
                    throw new InvalidOperationException(
                        member.Name + " is not a numeric member.");
                }

                // m_Data stores raw wire values. Range is checked in engineering units.
                double value = Convert.ToDouble(raw);
                double resolution;
                if (m_Resolutions.TryGetValue(member.Name, out resolution))
                {
                    if (resolution <= 0)
                    {
                        throw new InvalidOperationException(
                            member.Name + " resolution must be greater than zero.");
                    }

                    value *= resolution;
                }

                if (hasMax && value > max)
                {
                    throw new ArgumentOutOfRangeException(
                        member.Name,
                        value,
                        member.Name + " exceeds maximum " + max + ".");
                }

                if (hasMin && value < min)
                {
                    throw new ArgumentOutOfRangeException(
                        member.Name,
                        value,
                        member.Name + " is below minimum " + min + ".");
                }
            }
        }

        private static IEnumerable<MemberInfo> GetNumericMembers()
        {
            IEnumerable<MemberInfo> properties = typeof(TData)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .Cast<MemberInfo>();

            IEnumerable<MemberInfo> fields = typeof(TData)
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Cast<MemberInfo>();

            return properties.Concat(fields);
        }

        private static object GetMemberValue(MemberInfo member, object instance)
        {
            PropertyInfo property = member as PropertyInfo;
            if (property != null)
            {
                return property.GetValue(instance, null);
            }

            return ((FieldInfo)member).GetValue(instance);
        }
    }
}
