using NetworkGenerator.Attributes;
using NetworkGenerator.MessageStructs;
using NetworkGenerator.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace NetworkGenerator.Binary
{
    public static class DataObjectRegistry
    {
        // Type 으로 그냥 자동화를하냐 아니면  DataPacketObject 상속받은 타입으로 하냐 
        // 데이터 파싱 시 헤더를 기준으로 반환할 패킷 클래스 
        //public static Dictionary<EMessageID, DataPacketObject> DataObjectDic = new Dictionary<EMessageID,  DataPacketObject>();  // 이렇게 쓰거나
                                                      
        // 메세지와  DataPacketObject를 상속받은 클래스를 관리
        public static Dictionary<EMessageID,Type> DataPacketObjDic = new Dictionary<EMessageID,Type>(); // 아래 처럼 타입으로 써서 해당타입을 반환 받거나  이 방식쓰면 아마 Registerall 에서 

        
        // 문제는 헤더를 기준으로 반환할 패킷 클래스에  언제 주입을 할 것인가 . App 진입점에서 ? 아니면 수동으로 패킷 클래스 강제로 ?
        public static void  RegistIDataObject(EMessageID id,Type value)
        {
            DataPacketObjDic.Add(key: id,value);
        }
        public static void DeRegistDataObject(EMessageID id)
        {
            DataPacketObjDic.Remove(key: id);
        }

        public static Type GetDataType(EMessageID id) {
            DataPacketObjDic.TryGetValue(key: id, out Type value );
            return value;
        }

        /// <summary> dj
        ///  On_Startup 등 메인 진입점시 호출되어야 하는 함수 
        ///  DataObjectAttribute 속성을 주입받은 IDataObject 타입의 클래스 순회하여 등록
        /// </summary>
        /// <param name="assembly"></param>
        //public static void RegisterAssembly(Assembly assembly)
        //{
        //    foreach (Type type in assembly.GetTypes())
        //    {
        //        DataPakcetObjectAttribute attribute =
        //            type.GetCustomAttribute<DataPakcetObjectAttribute>(
        //                inherit: false);

        //        if (attribute == null)
        //            continue;

        //        if (type.IsAbstract || type.IsInterface)
        //            continue;

        //        if (!typeof(IDataObject).IsAssignableFrom(type))
        //        {
        //            throw new InvalidOperationException(
        //                $"{type.FullName}에 DataObjectAttribute가 있지만 " +
        //                "IDataObject를 구현하지 않았습니다.");
        //        }

        //        if (type.GetConstructor(Type.EmptyTypes) == null)
        //        {
        //            throw new InvalidOperationException(
        //                $"{type.FullName}에 기본 생성자가 없습니다.");
        //        }

        //        int messageId = (int)attribute.MessageID;

        //        if (Factories.ContainsKey(messageId))
        //        {
        //            throw new InvalidOperationException(
        //                $"중복 MessageID입니다: {attribute.MessageID}");
        //        }

        //        Type packetType = type;

        //        Factories.Add(
        //            messageId,
        //            () => (IDataObject)Activator.CreateInstance(packetType));
        //    }
        //}
        public static void RegisterAssembly(
            Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // 인터페이스, 추상 클래스, 미완성 제네릭 제외
                if (type.IsInterface ||
                    type.IsAbstract ||
                    type.ContainsGenericParameters)
                {
                    continue;
                }

                DataPakcetObjectAttribute attribute =
                    type.GetCustomAttribute<DataPakcetObjectAttribute>(
                        inherit: false);

                // Attribute가 없는 클래스 제외
                if (attribute == null)
                    continue;

                // DataPacketObject<TData> 상속 여부 확인
                if (!InheritsFromGeneric(
                        type,
                        typeof(DataPacketObject<>)))
                {
                    throw new InvalidOperationException(
                        $"{type.FullName}에 DataObjectAttribute가 있지만 " +
                        "DataPacketObject<TData>를 상속하지 않았습니다.");
                }

                // 나중에 Activator로 생성하기 위한 조건
                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    throw new InvalidOperationException(
                        $"{type.FullName}에 기본 생성자가 없습니다.");
                }

                RegistDataObject(
                    attribute.MessageID,
                    type);
            }
        }
        private static bool InheritsFromGeneric(
            Type type,
            Type genericBaseType)
        {
            Type current = type;

            while (current != null &&
                   current != typeof(object))
            {
                if (current.IsGenericType &&
                    current.GetGenericTypeDefinition() ==
                    genericBaseType)
                {
                    return true;
                }

                current = current.BaseType;
            }

            return false;
        }

        public static void RegistDataObject(
            EMessageID id,
            Type packetType)
        {
            if (DataPacketObjDic.ContainsKey(id))
            {
                throw new InvalidOperationException(
                    $"중복 MessageID입니다: {id}");
            }

            DataPacketObjDic.Add(id, packetType);
        }

        

        public static Type GetDataObjectType(
            EMessageID id)
        {
            DataPacketObjDic.TryGetValue(
                id,
                out Type packetType);

            return packetType;
        }

        public static object CreateDataObject(
            EMessageID id)
        {
            Type packetType = GetDataObjectType(id);

            if (packetType == null)
            {
                throw new KeyNotFoundException(
                    $"등록되지 않은 MessageID입니다: {id}");
            }
             
            return Activator.CreateInstance(packetType);
        }
        // DataPacketObject를 상속 받은 클래스의 구조체의 타입을 반환해줌 EX)CntlCmdUdp(class):DataPackdetObject -> CntlCmdUdpData(struct) 구조 
        public static Type GetPayloadType(Type packetType) 
        { 
            Type current = packetType;

            while (current != null)
            {
                if (current.IsGenericType &&
                    current.GetGenericTypeDefinition() == typeof(DataPacketObject<>))
                {
                    return current.GetGenericArguments()[0];
                }

                current = current.BaseType;
            }

            throw new InvalidOperationException(
                $"{packetType.FullName} is not a DataPacketObject<TData>.");
        }
    }
}
