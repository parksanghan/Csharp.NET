# Window Form

**<C에서의 DLL>**

**exe : Main 독립적 실행**

**DLL : exe 동작에 필요한 정보 (클래스, 기타)를 소유하는데  exe가 실행될 떄 DLL이 길이 메모리에 로딩된다.** 

**<C#에서의 DLL > → DLL 어셈블리** 

**솔루션  .Net 라이브러리** 

```csharp
public class Calc1

internal class Calc2
```

**ExE 자신이 사용하고자 하는 DLL 참조추가 
이름이 있는 공간 using 처리  → 이름이 잇는 공간 using 처리** 

**Internal 이라는 것은 자신의 어셈블리 내에서 자유롭게 사용할 수 있는 클래스** 

**Window32 API 에서 우리는 핸들을 얻어와서 이벤트를 제어하였다면 C# Window Form 에서는 스켈레톤 코드 , 핸들링(핸들을 얻어오는 과정)을 제공해준다 .** 

**이벤트 대리자 함수를 부모에게 등록시키면 대리자에서 호출되는 구조** 

![Untitled](Window%20Form%205219ce25fde14328b1889b4b4f62aa6e/Untitled.png)

![Untitled](Window%20Form%205219ce25fde14328b1889b4b4f62aa6e/Untitled%201.png)

![Untitled](Window%20Form%205219ce25fde14328b1889b4b4f62aa6e/Untitled%202.png)

속성 창을 건들일때 직접코딩 → initalized 다음에 수정 

[크로스 쓰레드 ]

특정 쓰레드에서 생성한 UI 를  다른 쓰레드에서  접근하려 할때 생기는 크로스 쓰레드 

textbox 1을 만든 쓰레드와 이  

⇒ 자신이 만든 쓰레드가 아닌 쓰레드에서 접근하는 경우이다.

eX) 쓰레드로 만들어 호출한 함수에서  메인에 대한 쓰레드에 대해 접근하는 경우 랜덤엑세스 오류가 발생 

해결방법 

```csharp
if(textbox1.InvokeRequired){
Action<string> del = SetText; // 동일한 형태의 함수 //대리자 등록
textbox1.invoke(del, new object[])
}

```

Action = > 대리자 : 템플릿 형태 

```csharp
if(textbox1.InvokeRequired){
Action<string> del =(msg)=> {textbox1.Text = msgs;}
textbox1.invoke(del, new object[])
}
```