# WpfComObjectSample

WPF에서 COM 객체를 생성하고 호출하는 최소 예제입니다.

기본 ProgID는 Windows에 기본으로 등록되어 있는 `Scripting.Dictionary`입니다.

## 실행

```powershell
dotnet run --project .\WpfComObjectSample.csproj
```

## 핵심 코드

```csharp
var comType = Type.GetTypeFromProgID("Scripting.Dictionary", throwOnError: true);
dynamic dictionary = Activator.CreateInstance(comType!);

dictionary.Add("name", "COM from WPF");
var value = dictionary.Item("name");
```

사용이 끝난 COM 객체는 해제합니다.

```csharp
if (Marshal.IsComObject(comObject))
{
    Marshal.FinalReleaseComObject(comObject);
}
```

## 다른 COM 객체 사용

1. COM 서버가 Windows에 등록되어 있어야 합니다.
2. 앱의 `COM ProgID` 입력칸에 해당 ProgID를 입력합니다.
3. `Create`를 누릅니다.

32비트 COM 객체만 등록되어 있다면 프로젝트의 플랫폼 대상을 `x86`으로 맞춰 실행해야 합니다.
