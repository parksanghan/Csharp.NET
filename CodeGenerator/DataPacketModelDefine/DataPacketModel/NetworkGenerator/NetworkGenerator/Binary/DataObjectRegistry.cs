using NetworkGenerator.Attributes;
using NetworkGenerator.Packets;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetworkGenerator.Binary
{
    public static class DataObjectRegistry
    {
        private static readonly object RegistryLock = new object();

        public static readonly Dictionary<int, Type> DataPacketObjDic =
            new Dictionary<int, Type>();

        private static readonly Dictionary<int, IDataPacketObject>
            DataPacketInstances =
                new Dictionary<int, IDataPacketObject>();

        public static void RegisterAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsInterface ||
                    type.IsAbstract ||
                    type.ContainsGenericParameters)
                {
                    continue;
                }

                DataPakcetObjectAttribute attribute =
                    type.GetCustomAttribute<DataPakcetObjectAttribute>(false);
                if (attribute == null)
                {
                    continue;
                }

                if (!typeof(IDataPacketObject).IsAssignableFrom(type))
                {
                    throw new InvalidOperationException(
                        type.FullName +
                        " has DataPakcetObjectAttribute but does not implement " +
                        nameof(IDataPacketObject) + ".");
                }

                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    throw new InvalidOperationException(
                        type.FullName +
                        " requires a public parameterless constructor.");
                }

                var packet =
                    (IDataPacketObject)Activator.CreateInstance(type);

                if (packet.MessageID != attribute.MessageID)
                {
                    throw new InvalidOperationException(
                        type.FullName +
                        " MessageID does not match its attribute.");
                }

                Register(packet);
            }
        }

        public static void Register(IDataPacketObject packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            lock (RegistryLock)
            {
                if (DataPacketInstances.ContainsKey(packet.MessageID))
                {
                    throw new InvalidOperationException(
                        "Duplicate MessageID: " + packet.MessageID + ".");
                }

                DataPacketInstances.Add(packet.MessageID, packet);
                DataPacketObjDic.Add(packet.MessageID, packet.GetType());
            }
        }

        public static void RegistIDataObject(int id, Type packetType)
        {
            RegistDataObject(id, packetType);
        }

        public static void RegistDataObject(
            int id,
            Type packetType)
        {
            if (packetType == null)
            {
                throw new ArgumentNullException(nameof(packetType));
            }

            if (!typeof(IDataPacketObject).IsAssignableFrom(packetType))
            {
                throw new InvalidOperationException(
                    packetType.FullName +
                    " does not implement " + nameof(IDataPacketObject) + ".");
            }

            var packet =
                (IDataPacketObject)Activator.CreateInstance(packetType);
            if (packet.MessageID != id)
            {
                throw new InvalidOperationException(
                    "Registered ID does not match packet.MessageID.");
            }

            Register(packet);
        }

        public static void DeRegistDataObject(int id)
        {
            lock (RegistryLock)
            {
                DataPacketObjDic.Remove(id);
                DataPacketInstances.Remove(id);
            }
        }

        public static Type GetDataType(int id)
        {
            return GetDataObjectType(id);
        }

        public static Type GetDataObjectType(int id)
        {
            lock (RegistryLock)
            {
                Type packetType;
                DataPacketObjDic.TryGetValue(id, out packetType);
                return packetType;
            }
        }

        public static IDataPacketObject GetInstance(int id)
        {
            lock (RegistryLock)
            {
                IDataPacketObject packet;
                if (!DataPacketInstances.TryGetValue(id, out packet))
                {
                    throw new KeyNotFoundException(
                        "Unregistered MessageID: " + id + ".");
                }

                return packet;
            }
        }

        public static TPacket GetInstance<TPacket>(int id)
            where TPacket : class, IDataPacketObject
        {
            TPacket packet = GetInstance(id) as TPacket;
            if (packet == null)
            {
                throw new InvalidCastException(
                    id + " is not " + typeof(TPacket).FullName + ".");
            }

            return packet;
        }

        public static object CreateDataObject(int id)
        {
            // Compatibility API: the registry-owned instance is returned.
            return GetInstance(id);
        }

        public static Type GetPayloadType(Type packetType)
        {
            if (packetType == null)
            {
                throw new ArgumentNullException(nameof(packetType));
            }

            Type current = packetType;
            while (current != null)
            {
                if (current.IsGenericType &&
                    current.GetGenericTypeDefinition() ==
                    typeof(DataPacketObject<>))
                {
                    return current.GetGenericArguments()[0];
                }

                current = current.BaseType;
            }

            throw new InvalidOperationException(
                packetType.FullName +
                " is not a DataPacketObject<TData>.");
        }
    }
}
