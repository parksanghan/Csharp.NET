# CycloneDDSManager

Cyclone DDS 11.0.1 C API를 C#에서 사용하는 x64 P/Invoke 및 관리형 래퍼입니다.

## Attribute 기반 class/struct 패킷

`[Topic]`이 붙은 class 또는 struct에서 `[DdsMember]` 멤버만 DDS 데이터로 사용합니다.

```csharp
using CycloneDDSManager.Attr;

public enum DeviceState
{
    Offline = 0,
    Running = 5,
    Failed = 9
}

public sealed class PositionMeta
{
    [DdsMember(0)]
    public long Timestamp { get; set; }
}

[Topic(
    "PositionTopic",
    "장치 위치 데이터",
    Module = "MySystem",
    TypeName = "Position")]
public sealed class PositionPacket
{
    [DdsMember(0, IsKey = true)]
    public int DeviceId { get; set; }

    [DdsMember(1)]
    public double X { get; set; }

    [DdsMember(2)]
    public double Y { get; set; }

    [DdsMember(3)]
    public bool Enabled { get; set; }

    [DdsMember(4, MaxLength = 64)]
    public string Name { get; set; }

    [DdsMember(5)]
    public DeviceState State { get; set; }

    [DdsMember(6)]
    public PositionMeta Meta { get; set; }

    // DdsMember가 없으므로 DDS로 전송되지 않습니다.
    public DateTime ReceivedAt { get; set; }
}
```

멤버 ID는 0부터 중복 없이 연속되어야 합니다. 이 규칙으로 생성 IDL의 멤버 ID와 런타임 Dynamic Type의 멤버 ID를 동일하게 유지합니다.

## IDL 생성과 Topic 발행/구독

```csharp
using CycloneDDSManager.DDS;

using (var participant = DdsParticipant.Create())
// IDL 파일 생성과 Dynamic Type Topic 등록을 동시에 수행합니다.
using (var topic = participant.CreateTopic<PositionPacket>("idl/Position.idl"))
using (var writer = participant.CreateWriter(topic))
using (var reader = participant.CreateReader(topic))
{
    writer.Write(new PositionPacket
    {
        DeviceId = 2,
        X = 10.5,
        Y = 20.5,
        Enabled = true,
        Name = "RTC-2",
        State = DeviceState.Running,
        Meta = new PositionMeta { Timestamp = 123456789 }
    });

    IReadOnlyList<DdsReceivedSample<PositionPacket>> samples = reader.Take();
    foreach (DdsReceivedSample<PositionPacket> sample in samples)
    {
        if (sample.Info.ValidData)
            Console.WriteLine(sample.Data.Name);
    }
}
```

파일이 필요 없고 C# 양쪽에서 같은 Attribute 타입을 사용하는 경우에는 경로를 생략할 수 있습니다.

```csharp
using var topic = participant.CreateTopic<PositionPacket>();
```

IDL만 별도로 생성할 수도 있습니다.

```csharp
string text = DdsIdlGenerator.Generate<PositionPacket>();
string fullPath = DdsIdlGenerator.Save<PositionPacket>("idl/Position.idl");
```

생성 IDL을 런타임에 `idlc`로 다시 컴파일해서 C#이 사용하는 것은 아닙니다. 하나의 검증된 스키마에서 다음 두 결과를 함께 만듭니다.

```text
Attribute 스키마
├─ IDL 파일: 다른 PC/C/C++ 프로그램과 공유
└─ Dynamic Type: 현재 C# 프로세스에서 Topic 즉시 등록
```

따라서 생성 IDL과 C# 런타임 Topic 정의가 따로 어긋나지 않습니다.

## 현재 Attribute mapper 지원 타입

- class와 struct
- field와 property
- `bool`, 모든 정수형, `float`, `double`, `char`
- enum
- UTF-8 `string` (`MaxLength = 0`은 unbounded)
- 중첩 class/struct
- 상속받은 CLR 멤버의 평탄화

현재 배열, `List<T>`, nullable, optional, sequence, union은 Attribute mapper에서 거절합니다. 조용히 잘못된 데이터를 보내지 않고 `DdsSchemaException`을 발생시킵니다. 해당 타입은 이후 명시적인 sequence/optional 매핑을 추가하거나 IDL 생성 descriptor 경로를 사용해야 합니다.

## 저수준 API

기존 저수준 API도 그대로 사용할 수 있습니다.

- Participant, Topic, Publisher, Subscriber, Writer, Reader
- `write`, `write_ts`, `writedispose`, dispose, instance 등록/해제
- loan 기반 `read`, `take`, mask/instance 읽기와 `dds_return_loan`
- QoS, WaitSet, Guard/Read/Query Condition
- Listener callback과 Status getter
- 수동 Dynamic Type 생성

## 네이티브 DLL 경로

현재 기본 경로는 `F:\dev\cyclonedds`입니다. 다른 설치 경로에서는 다음과 같이 지정합니다.

```powershell
dotnet build -p:CycloneDdsRoot=C:\path\to\cyclonedds
```

현재 `ddsc.dll`이 x64이므로 소비 애플리케이션도 x64로 빌드해야 합니다.
