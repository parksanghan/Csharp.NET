# CycloneDDSManager

Cyclone DDS 11.0.1 C API를 C#에서 사용하기 위한 x64 P/Invoke 및 관리형 래퍼입니다.

## 구현 범위

- Participant, Topic, Publisher, Subscriber, Writer, Reader
- `write`, `write_ts`, `writedispose`, `dispose`, instance 등록/해제/키 조회
- loan 기반 `read`, `take`, mask/instance 읽기 및 `dds_return_loan`
- QoS 생성/복사/병합과 주요 정책 setter
- WaitSet, GuardCondition, ReadCondition, QueryCondition
- Listener의 13개 공개 callback과 Status getter
- Dynamic Type의 structure/enum/bitmask/sequence 생성, member 속성, 등록 및 Topic 생성

`DDSI` 내부 API, Security plugin API, PSMX, CDR collector, 통계 등은 공개 관리형 API에 포함하지 않았습니다.

## Dynamic Type 예제

```csharp
using System;
using System.Runtime.InteropServices;
using CycloneDDSManager.DDS;

[StructLayout(LayoutKind.Sequential)]
public struct Message
{
    public int Id;
    public double Value;
}

using (var participant = DdsParticipant.Create())
using (var type = DdsDynamicType.CreateStructure(participant, "Message")
    .AddPrimitiveMember("id", DdsDynamicTypeKind.Int32, 0)
    .AddPrimitiveMember("value", DdsDynamicTypeKind.Float64, 1)
    .SetMemberKey(0))
using (var topic = type.RegisterAndCreateTopic(participant, "MessageTopic"))
using (var writer = participant.CreateWriter(topic))
using (var reader = participant.CreateReader(topic))
{
    writer.Write(new Message { Id = 1, Value = 3.14 });

    using (DdsLoanedSamples samples = reader.Take(32))
    {
        for (int i = 0; i < samples.Count; i++)
        {
            DdsSampleInfo info = samples.GetInfo(i);
            if (info.ValidData)
            {
                Message value = samples.Get<Message>(i);
                Console.WriteLine($"{value.Id}: {value.Value}");
            }
        }
    } // dds_return_loan
}
```

## IDL 생성 descriptor 사용

`idlc`가 생성한 `*_desc`는 네이티브 `dds_topic_descriptor_t`입니다. C#에 같은 구조체를 다시 선언하지 말고, 생성된 C 코드를 DLL로 빌드한 뒤 descriptor 주소를 반환하는 작은 export를 추가하는 방식이 안전합니다.

```c
__declspec(dllexport)
const dds_topic_descriptor_t *get_Message_desc(void)
{
  return &Message_desc;
}
```

그 함수의 반환값(`IntPtr`)을 다음과 같이 넘깁니다.

```csharp
using (var topic = participant.CreateTopic(get_Message_desc(), "MessageTopic"))
{
    // writer / reader 생성
}
```

IDL의 string, sequence, optional 같은 포인터 기반 필드는 단순 `StructLayout`만으로 충분하지 않을 수 있습니다. 이 경우 생성된 C 타입에 맞는 별도 marshaller를 두고, loan이 반환되기 전에 문자열/sequence를 복사해야 합니다.

## 네이티브 DLL 경로

프로젝트는 현재 설치 경로인 `F:\dev\cyclonedds\bin`의 DLL을 출력 폴더로 복사합니다. 다른 위치에서는 빌드 속성을 지정합니다.

```powershell
dotnet build -p:CycloneDdsRoot=C:\path\to\cyclonedds
```

현재 `ddsc.dll`이 x64이므로 소비 애플리케이션도 x64로 빌드해야 합니다.
