# .NET NETWORK (.Net struct )

# **메모리에 대한 설명**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled.png)

**프로그램이 실행되기 위해서는 먼저 프로그램이 메모리에 로드(load)되어야 한다.**

**또한, 프로그램에서 사용되는 변수들을 저장할 메모리도 필요한데**

**이때, 컴퓨터의 운영체제는 프로그램의 실행을 위해 다양한 메모리 공간을 제공하고 있습니다.**

**프로그램이 운영체제로부터 할당받는 대표적인 메모리 공간은 4가지 있습니다.**

1. **코드(code) 영역**
2. **데이터(data) 영역**
3. **스택(stack) 영역**
4. **힙(heap) 영역**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%201.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%202.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%203.png)

**이전에서 win32api를 사용했다면 c#에서는 .Net Framework 를 사용한다.** 

**CTS(공용 형식 시스템)을 통해 견고한 코드를 보장하기
위해 형식에 대해 약속하고 있습니다. CTS는 관리되는 모든 형식은 자기 기술적이며 이
때문에 명확하게 개체들을 사용할 수 있습니다**

**CTS (type  타입)**

- **자기 기술적 ( 메타 데이터 ) : 본인이 누군지 설명**

**CLS(공용 언어 사양)**

**NET 프레임워크에서 동작 가능한 프로그램을 작성할 때에는 C#언어를 비롯하여 Managed C++, Visual Basic.NET을 비롯하여 다양한 언어로 개발이 가능하며 CLS를 따른다면 교차 언어 개발도 가능합니다.**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%204.png)

**c# 포인터가 있다. →  단, 순수한 c# 언어로만 코딩할 떄 사용  x → c# 언어에서 c/c++로 만든 DLL 을 사용할 수 있다. ⇒ 이경우 포인터 사용** 

**즉 , 순수 c# 코드에서는 사용하지 않는다…. → 데이터 파싱 /  데이터 오프셋 변환을 사용할 수가 없다….(억지 변환 불가능)**

**값을  저장하는 타입 : 값 형식** 

**주소 저장 타입 : 참조 형식 타입** 

## **네임스페이스**

**내가 만든 클래스도 이름이 있는 공간에 구현하는 것을 권장한다.**

**[c/c++언어] 파일단위로 코드 관리!**

**[C#언어]    이름이 있는 공간 단위로 코드 관리!**

 **다른 소스파일로 구성되어 있어도 namespace명이 동일하면 동일 공간!**

   **namespace명이 다른 공간에 있는 키워드를 사용하려면?

        using namespace명;**

## **값 형식 참조 방식  계산**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%205.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%206.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%207.png)

**값 형식의 연산**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%208.png)

**참조 형식의 연산**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%209.png)

**상 메서드의 사용 이점:**

**유연성: 자식 클래스에서 필요한 경우에 메서드를 재정의할 수 있어요.
다형성: 여러 클래스를 동일한 형식으로 다룰 수 있어요. 이는 유지보수 및 코드 이해를 쉽게 해주죠.
확장성: 새로운 클래스를 추가하거나 기존 클래스를 변경할 때 코드 수정이 적게 필요해져요.**

## **오버라이딩 + 오버로딩**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2010.png)

**c#에서는 virtual 키워드를 똑같이 사용한다. 하지만** 

## **object 클래스 비교**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2011.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2012.png)

**i.Equals(j), object.Equals(i,j) 둘다 값 비교이다.** 

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2013.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2014.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2015.png)

## **c#의 get set  메서드**

```cpp
internal class _03_Man

    {

        private string name;

        private int hp;

        public string Name

        {

            get { return name; }

            private set { name = value; }   //외부에서 접근 불가

        }

        public int HP

        {

            get { return hp; }

            set { hp = value; }

        }

    }//위 코드를 간결하게

    internal class _04_Man

    {

        public string Name { get; private set; } // 외부에서 접근 x 

        public int Hp      { get; set; }

    }

 public int HP

        {

            get { return hp; }

            set 

            { 

                if( value >=0 && value <=100) // 이런식으로 조건을 줄수 있다. 

                    hp = value; 

            }

        }
```

# **값 형식&참조형식**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2016.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2017.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2018.png)

# **인덱서**

**인덱서는 멤버 요소들로 구성된 컬렉션 개체의 요소에 쉽게 접근할 수 있게 해주는 멤버이다 .**

**인덱서의 매개 변수가 있다는 점을 제외하면 구현 방법이 속성과 매우 흡사하여 속성처럼 get 블록과 set 블록을 선택적으로 정의가 가능하다 .**

**arr[,]  = new arr[2,3]** 

```csharp
class MyDictionary
{
 string[] storage = new string[3];
}
public MyDictionary()
{
 for (int i = 0; i < storage.Length; i++)
 {
 storage[i] = string.Format("key{0}=value{0}",i,i);
 }
}
```

```csharp
public string this[int index]
{
 get;
}
public string this[string key]
{
 get;
}
public string this[int index]
{
 get
 {
 if (AvailIndex(index)) //유효한 인덱스일 때
 {
 return GetValue(storage[index]); //storage[index] 요소의 값을 반환
 }
 return string.Empty;
 }
}

private bool AvailIndex(int index)
{
 return (index >= 0) && (index < storage.Length);
}
요소 문자열은 "키=값"형태로 보관되어 있으므로 '='의 위치를 찾아 뒤쪽에 있는 부분
문자열이 값입니다.
private string GetValue(string s)
{
 int index = s.Index Of('='); //'='문자가 시작되는 위치를 찾는다.
 return s.Substring(index+1); //index+1 뒤에 있는 부분 문자열을 반환한다.
}

public string this[string key]
{
 get
 {
 string element =FindKey(key); //키에 해당하는 요소 문자열을 찾는다.
 return GetValue(element); //요소 문자열에서 값을 추출하여 반환한다.
 }
}

private string FindKey(string key)
{
 foreach (string s in storage) //storage 컬렉션에 보관된 각 요소에 대해 반복 수행
 {
 if (s.IndexOf(key) == 0) //요소 문자열이 key로 시작할 때
 {
 return s; //요소 문자열 반환
 }
 }
 return string.Empty;
}
```

- **예제 코드**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2019.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2020.png)

# **생성자와 소멸자**

- **생성자**

**기본 생성자는 입력 매개 변수가 없는 생성자를 말합니다. 기본 생성자는 클래스에서만
명시적으로 정의할 수 있으며 구조체는 매개 변수 있는 생성자만 정의할 수 있습니다. 또
한, 클래스나 구조체 내에 어떠한 생성자도 정의하지 않으면 묵시적으로 기본 생성자가
만들어지며 멤버들을 기본값으로 초기화하는 등의 작업을 수행합니다**

**그리고 매개 변수가 있는 생성자를 정의하고 기본 생성자를 정의하지 않았을 때 컴파일
러는 기본 생성자를 만들지 않으므로 생성할 때 인자를 전달하지 않으면 오류가 납니다.**

```csharp
class Parent{
	public int a;
	Parent(int value){
	a = Value;
}

}
class Child :Parent{
	Child(int value):base(vlaue) 
}
class Program
{
    static void Main(string[] args)
    {
		Parent p = new Child(3);
}
}
```

**→ 다음코드는 p객체는 기본적으로 부모 클래스인 생성자를 수행한후 Child 클래스 생성자를 수행함.**

**자식에서 또 다르게 수행하지 않기에 부모 생성자에서 생성한 정의문을 따름** 

- **소멸자**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2021.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2022.png)

```csharp
~_04_Dispose()

        {

            Console.WriteLine("소멸자 호출");

            Dispose();

        }

        public void Dispose()

        {

            Console.WriteLine("[비관리 자원 소멸처리] 파일 CLOSE");

            

            //GC(가비지컬렉터). 객체 소멸시 소멸자 호출X

            GC.SuppressFinalize(this);

        }
```

```csharp
static void Exam6()
        {
            _04_Dispose d1 = new _04_Dispose();            
        }
```

```csharp
static void Exam7()
        {
            _04_Dispose d1 = new _04_Dispose();
            d1.Dispose();
        }
```

```csharp
static void Exam8()
        {
            //만들어지는 객체의 사용범위를 설정
            using (_04_Dispose d1 = new _04_Dispose())
            {

            } //자동으로 Dispose를 호출!                
        }
```

# **Enum 열거형**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2023.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2024.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2025.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2026.png)

## **람다식**

**람다식 → 함수의 구조** 

 **코드 자체를 줄일 수 있다.** 

```csharp
Person person = list2.Find(x => x.Age >= 10);
        Console.WriteLine("결과 : " + person?.Name);
```

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        int foundNumber = numbers.Find(n => n == 3);

        Console.WriteLine(foundNumber); // Output: 3
```

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        int evenNumber = numbers.Find(IsEven);

        Console.WriteLine(evenNumber); // Output: 2
```

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2027.png)

# **Deligate**

**Deligate 의 장점**

1. **메서드를 참조할 수 있음: 델리게이트는 메서드의 주소(참조)를 저장할 수 있습니다. 이를 통해 메서드를 변수에 할당하고 다른 메서드로 전달할 수 있습니다.**
2. **타입 안전성: 델리게이트는 타입 안전성을 제공합니다. 즉, 델리게이트가 참조하는 메서드의 시그니처(매개변수 및 반환 값 형식)와 일치해야 합니다.**
3. **다중 콜백: 여러 메서드를 하나의 델리게이트에 연결하여 다중 콜백을 구현할 수 있습니다. 이를 통해 이벤트 처리 및 비동기 프로그래밍 등에 유용하게 사용됩니다**

```csharp
using System;

// 델리게이트 선언
public delegate void MyDelegate(string message);

public class MyClass
{
    public void DisplayMessage(string message)
    {
        Console.WriteLine("메시지: " + message);
    }

    public void AnotherMethod(string message)
    {
        Console.WriteLine("다른 메서드의 메시지: " + message);
    }
}

class Program
{
    static void Main()
    {
        MyClass myClass = new MyClass();

        // 델리게이트에 메서드 참조를 할당
        MyDelegate delegate1 = myClass.DisplayMessage;
        MyDelegate delegate2 = myClass.AnotherMethod;

        // 델리게이트를 사용하여 메서드 호출
        delegate1("Hello, World!"); // DisplayMessage 메서드 호출
        delegate2("안녕하세요!");    // AnotherMethod 메서드 호출
    }
}
```

```csharp
using System;

public delegate int MathOperation(int a, int b);

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();

        // 델리게이트에 메서드 참조를 할당
        MathOperation addDelegate = calculator.Add;
        MathOperation subtractDelegate = calculator.Subtract;

        int result1 = addDelegate(5, 3);       // 델리게이트를 사용하여 Add 메서드 호출
        int result2 = subtractDelegate(8, 4);  // 델리게이트를 사용하여 Subtract 메서드 호출

        Console.WriteLine("더하기 결과: " + result1);
        Console.WriteLine("빼기 결과: " + result2);
    }
}
```

# **싱글톤 vs 멀티톤**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2028.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2029.png)

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2030.png)

- **하나의 어셈블리에서만 사용**
- **internal 어셈블리에선 → public**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2031.png)

**base(name) 이 없으면 부모의 인자가 없는 생성자가 호출되므 로 오류** 

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2032.png)

**→ ㅡman 이가지고 있는 객체가 Stu 인가? ??**

**Student  stu = new Student ← 에서   재정의에서  왼쪽의 Student  stu만 중요 하다.**

**virtual   →  override  , new** 

**(클래스 선언에도 abstract Class 클래스명 )abstract → ovrrride , new** 

**생성된 객체  as  클래스 명  ⇒ 생성된 객체가 클래스명의 객체이냐 ?   라는 의미** 

**if (man is Stu)  → man 이 Stu 의 객체이냐 ? 결론 같네용** 

**"is" 연산자:**

- **"is" 연산자는 객체의 형식을 검사하는 데 사용됩니다. 주로 조건문에서 사용됩니다.**
- **"is" 연산자를 사용하면 특정 객체가 특정 형식으로 형변환이 가능한지를 검사합니다.**

**"as" 연산자:**

- **"as" 연산자는 형식 변환을 시도하고, 변환이 실패하면 `null`을 반환합니다.**
- **주로 형식 변환이 필요한 경우 사용됩니다.**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2033.png)

# **인터페이스와 컬렉션**

**인터페이스 :  구현에 대한  약속**

**추상 클래스** 

**캡슐화 대상 : 이벤트 , 속성 ,인덱서** 

**컬렉션 :** 

```jsx
interface IStudy 

{
 void Study(); // 접근지정자를 설정할 수 없다.
}

=> public virtual 
상속! (묵시적 표현)
class Student : IStudy
{
	public void Study(); // 내부적으로 오버라이드 
} // 여기서는 접근지정해야 함 .

class Student: IStudy{
	IStudy.Study();  // 명시적 방식
}
```

**→ 묵시적 인터페이스 구현** 

**다중 인터페이스 구현 약속** 

**인터페이스에서와 C++에서만 다중 상속을 사용가능**

**자식: 부모1 : 부모 2**

**다중상속 !**

```jsx
class Student : parent1 , parent2 
```

**→ 클래스로 클래스와 인터페이스를 상속받을때는 오류 x** 

**→ 인터페이스로 클래스와 인터페이스를 상속받으면 오류 o**

![                           **아빠… 나 어디로 가..?**](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2034.png)

                           **아빠… 나 어디로 가..?**

**→ 해당의 경우 , void IStduy.Work()  , void Teach.Work() 이런식으로 구현** 

**명시적 상속의 경우 반드시 인터페이스를 이용하여 멤버 사용 즉,  class 멤버로 사용 불가능** 

**→ 인터페이스 객체로 접근** 

```csharp
02_인터 sample = new 02_인터();
// 다운 캐스팅 
IStudy istudy = sample as Istudy ;

ITeach iteach = sample as ITeach;
```

**인터페이스에 멤버 속성과 인덱스를 캡슐화**

```csharp
interface IMemo
{
 int MaxCount { get; }
 string this[int index] { get; set; }
}
▶ 속성과 인덱스를 캡슐화한 인터페이스를 구현 약속
class Note : IMemo
{
 public int MaxCount
 {
 get { }
 }
 public string this[int index]
 {
 get{ }
 set{ }
 }
}
```

**구현** 

```csharp
class Note : IMemo
{
 string[] pages = null;
 public int MaxCount //IMemo에서 약속한 속성 구현
 {
 get; //IMemo에서 약속한 블록
 private set; //IMemo에서 약속하지 않은 블록이지만 추가했음
 }
 public string this[int index] //IMemo에서 약속한 인덱서 구현
 {
 get
 {
 if (AvailIndex(index))
 {
 return pages[index];
 }
 return null;
 }
 set
 {
 if (AvailIndex(index))
 {
 pages[index] = value;
 }
 }
 }
96
 public Note(int max_memo)
 {
 MaxCount = max_memo;
 pages = new string[max_memo];
 }
 private bool AvailIndex(int index)
 {
 return (index >= 0) && (index < MaxCount);
 }
}
```

# **나를 편하게 해주는 컬렉션**

**ICollection 인터페이스는 제네릭이 아닌 모든 컬렉션의 기반 인터페이스입니다**

**템플릿 기반   OR 일반 기반** 

**Icollection 을사용하는 자식들은 foreach 문 사용익가능하다 .**

**Icollection 은  IEnynerable 구현 상속이 되어 있기에 이안에서 foreach 문을 사용할수 있다.**

**→ 저장개수 확인 가능** 

**▶ IEnumerable, IEnumerator에 약속된 멤버들**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2035.png)

**▶ ICollection 인터페이스의 약속된 멤버**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2036.png)

**icollection  → object 타입** 

**collection.genetic**  

**foreach 문을 예로 icollection obect in something  이런형식이고**

**배열 또는 이중연결 리스트 사용** 

**데이터 클래스** 

**클래스가  정의 기본 정렬 기준설정**

**부가적 정렬 기준 추가 생성**

**ICompare vs ICompareable** 

**publci class Numberclass : ICompare<int>**

**public int Compare(int x, int ,y ){**

**return y - x ; 내림차순   // y 가크면 양수**

**return x - y  ; 오름차순  // x 가 크면 양수** 

# **대리자와 이벤트**

**대리자 → 메서드의 선형 원형을 정의하는 형식** 

- **1 . 대리자 객체 정의(컴파일러에 의해 클래스가 정의 됨)**
- **2. 대리자 객체 생성(new 연산자 사용 ), (대입 연산)**
- **3.객체 사용 (동기 , 생성된 객체를 함수처럼 호출 → 생성된 객체의 멤버함수 invoke 호출 )**
- **비동기**

```csharp
**delegate [리턴 타입] [대리자 형식 이름] [입력인자 리스트 ]**
```

**함수의 형태를 가지고 클래스를 정의하는 것 → 콜백 처리를 하기 위함** 

**이벤트 2가지 개념 감시자 , 처리자** 

```csharp
void Example()
{
 DemoDele dele1 = new DemoDele(SAdd);
 DemoDele dele2 = new DemoDele(Add);
 DemoDele dele3 = Add; // 이방식 사용 
-
}
static int SAdd(int a, int b)
{
 return a + b;
}
int Add(int a, int b)
{
 return a + b;
}
```

**→ 클래스를 가져와서 대입하여 대리자 객체를 생성** 

**Delegate를 넣는 과정에서 함수를 넣는 것** 

**⇒ Delegate를 함수처럼 사용하여 다형성을 제공** 

```csharp
class Program
{
 static void Main()
 {
 DemoDele dele = Add;
 Console.WriteLine("메서드처럼 호출:{0}", dele(2, 3)); // 이렇게해도 invoke 호출됨
 Console.WriteLine("Invoke 메서드 호출:{0} ", dele.Invoke(2, 3)); // 둘이 결국 똑같음
 } // 동기화 방식 호출 
// 객체 만들때 자동적으로 invoke 호출 ~
 static int Add(int a, int b)
 {
 return a + b;
 }
}
```

```csharp
namespace ConsoleApp3
{
    delegate int Demodel(int a, int b);
    internal class Program
    {
        void exam01()
        {

        }

        static void Main(string[] args)
        {
            // 이름이 없는 객체 생성 -> 한번만 메서드 호출할때
            new Program().exam01(); 
// 주소를 만들 필요없이 참조하여 한번만 사용 
        }
    }
}
```

**대리자에 여러개를 넣을수 있음.** 

- **동기 :호출된 함수가 자신의 일이 다 완료한 후 리턴**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2037.png)

**-대리자 사용방법 -** 

```csharp
delegate int DemoDel(int a, int b); // del 정의 -> 클래스화 
internal class Program
    {
        //2. 레퍼런스 변수 선언
        private DemoDel del = null;

        //3.1 동기방식 사용
        public void Exam1()
        {
            //FM 방식
            del = new DemoDel(Add); // 등록 
            Console.WriteLine("덧셈 : " +  del.Invoke(10, 20));

            //편리성 제공 방식
            del = Sub; // 등록 
            Console.WriteLine("뺄셈 : " + del(10, 20));
        }

        //3.2 여러개 등록 가능
        public void Exam2()
        {
            del = Add;
            del = del + Sub; // del 에 Add 가 있지만 또 추가 
						// 생성은 되지만 출력은 가장 최근 것출력 
            Console.WriteLine("결과 : " +  del(10, 20));  //-10

            del = del - Sub;
            Console.WriteLine("결과 : " + del(10, 20));  //30
        }
        public int Add(int n1, int n2) { Console.WriteLine("Add");  return n1 + n2; }
        public int Sub(int n1, int n2) { Console.WriteLine("Sub"); return n1 - n2;  }  

        static void Main(string[] args)
        {
            //이름이 없는 객체 생성
            //한번만 메서드 호출할 때
            new Program().Exam2();
        }
```

- **비동기 : 함수가 호출시키면 바로 리턴 호출된 함수가 기능을 완료하면 해당 사실을 알려줌**

**(기능을 완료하면 호출한 함수에게 알려줘야 함 )** 

**→ 비동기 통지** 

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2038.png)

**대리가 개체의 BeginInbkoke    , → 3번째 인자 비동기 통지를 받는 인자 4번째 인자 통지에 들어가는 인자** 

**호출한 함수와 반환 함수는 다른 비동기에서** 

**여러개의 BeginInvoke 를 생성하여 하나의  3번째인자에 들어가는 비동기 작업이 완료되었을 때 호출되게 하여  사용도 가능하다 .**

```csharp
delegate int DemoDele(int s, int b); // del 정의 -> 클래스화 

    internal class Class1
    {
        //2. 레퍼런스 변수 선언
        DemoDele dele = null;

        //3. 비동기 호출
        public void Exam1()
        {
            //비동기 호출
            dele = Sum;
            dele.BeginInvoke(1, 20, EndSum, "첫번째");// 비동긱식 사용 

            dele.BeginInvoke(1, 50, EndSum, "두번째");
            dele.BeginInvoke(1, 30, EndSum, "세번째");
            dele.BeginInvoke(1, 100, EndSum, "네번째");

            //자신의 일을 수행
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Main:{0}", i);
                Thread.Sleep(100);
            }

            //종료 전 멈춤
            Console.ReadLine();
        }
        //4. 비동기 통지 수신
        public void EndSum(IAsyncResult iar)
        {
            object obj = iar.AsyncState;
            
            AsyncResult ar = iar as AsyncResult;
            DemoDele del = ar.AsyncDelegate as DemoDele;
            Console.WriteLine("비동기통지 : {0}, {1}",obj, del.EndInvoke(ar));
        }
        //[연산 함수]
        public int Sum(int s, int b)
        {
            int sum = 0;
            for(; s <=b; s++)
            {
                sum = sum + s;
                Console.WriteLine("Sum:{0}", s);
                Thread.Sleep(100);
            }
            return sum;
        }

        static void Main(string[] args)
        {
            //이름이 없는 객체 생성
            //한번만 메서드 호출할 때
            new Class1().Exam1();
        }
```

## **대리자 콜백**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2039.png)

```csharp
{
    #region Del 정의

    delegate void CalDel(int result);
    delegate void MessageDel(string msg);

    #endregion

    internal class Cal
    {
        public CalDel _CalDel { get; set; } = null;
        public MessageDel _MessageDel { get; set; } = null;

        #region 메서드
        public void Add(int n1, int n2)
        {
            int result = n1 + n2;
            _CalDel.Invoke(result); //_CalDel 에 등록된 함수를 호출
        }

        public void Sub(int n1, int n2)
        {
            int result = n1 - n2;
            _CalDel.Invoke(result); // _CalDel에 등록된 함수를 호출
        }

        public void Message(string msg)
        {
            _MessageDel(msg);
        }
        #endregion

    }

    //사용자 
    internal class Program
    {
        private Cal cal = new Cal();

        public void Run()
        {
            cal._CalDel     = GetCal;
            cal._MessageDel = GetMessag;

            cal.Add(10, 20);
            //Thread.Sleep(2000);

            cal.Sub(10, 20);
            //Thread.Sleep(2000);

            cal.Message("안녕하세요");
        }

        #region CallBack 메서드
        public void GetCal(int result)
        {
            Console.WriteLine("연산결과 : {0}", result);
        }
        public void GetMessag(string msg)
        {
            Console.WriteLine("Echo : {0}", msg);
        }
        #endregion 

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
```

## **이벤트**

**이벤트는 특정 사건이 발생하는 것을 감시하는 개체가 이를 처리하는 개체에게 이벤트가 발생하였을 때 필요한 인자들과 함께 발생 사실을 통보하기 위한 멤버이다 .**

         ****

**이벤트용 대리자 → 보통 void   obj 이벤트를 통보한 개체  , e : 이벤트 처리에 필요한 인자** 

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2040.png)

**→이벤트도 결국 CallBack 개념** 

**public CallDel CalDel { get; set; } = null;
public MessageDel MsgDel { get; set; } = null;
// 외부에서 접근가능한 delegate**

**이벤트 시스템의 의의?**

**이벤트 시스템은 안에 이벤트 안에 저장된 함수들을 같은 매개변수 호출하는 기능인데, 이게 순회 한번 돌려서 그냥 필요한 클래스만 골라다 쓰는거랑 뭐가 다르냐고 생각할 수 있다. 중간 과정도 더럽게 복잡하고.**

**사실 이걸 쓰는 이유는 코드의 유연성 때문이다. 보통 사람들도 서로간에 너무 많은걸 공유하면 종속적인 관계가 되어버리는 것처럼 클래스들도 서로 너무 많은걸 공유하면 한쪽 없이는 사실상 쓰는게 불가능해지는데, 이벤트 시스템은 이런 문제를 해결해준다.**

**예를 들어, MemberManager와 Member간의 관계에서 우리는 지금까지 MemberManager가 Member를 '저장'하는 구성을 사용해왔다. 이러한 구성은 MemberManager에서 사용하는 자료구조에 저장된 Member를 '직접적으로 참조'해 함수를 호출하는 형태를 사용할 수 있었고, 지금까지 이러한 사용은 문제가 없었다.**

**하지만, MemberManager에서 Member를 저장하지 못하는 구성에서는 어떨까?**

**MemberManager는 더이상 Member에 접근할 수 없게 되고, 참조를 통해 함수를 호출하는 방식 또한 불가능하다. MemberManager가 Member를 저장하지 않는 시점부터 우리는 Member를 관리할 수 없는 것처럼 보인다.**

**하지만 MemberManager에서 Member들의 함수들을 자신의 이벤트에 구독시키면, Member들을 직접적으로 참조하지 않고도 Member내의 함수를 호출하는게 가능하다. 경우에 따라선 Member가 임의대로 자신의 구독을 해제하거나 다른 함수를 구독시키는 것이 가능하다.**

**Member는 함수 포인터 외에 다른 부분에서 MemberManager와의 접점은 거의 없어지고, MemberManager도 일일히 저장 자료구조를 순회하며 Member를 판별하고 적절한 함수를 찾을 필요가 없어진다.**

**요약 :**

1. **이벤트 시스템을 사용하면 '클래스 간의 연결을 느슨하'게 할 수 있음.**
2. **불필요한 순회 및 판별을 줄여 코드 효율을 높임.**
3. **피관리 객체들이 직접 호출된 함수들을 제어하는 등의 주동성을 높여줌.**

# **. Net 어셈블리**

# **C# 소켓 통신 및 기능화**

**기본 구성** 

- **초기화 및 할당**

```csharp
Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PORT);
								// 통신의 대상을 가지는 객체
                server.Bind(ipep); // 할당 

                server.Listen(20); // 대기 큐에 

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
```

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2041.png)

# **가변데이터 처리  (서버)**

- **바이트 수 먼저 보냄 → 본데이터 송신**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2042.png)

**→ 기존 구현에서 SendData , RecvData라는 Function을 추가해 가변길이에 대한 전송을 구현**

# **가변데이터 처리 (클라이언트 )**

![Untitled](NET%20NETWORK%20(%20Net%20struct%20)%207fc296c2922d43f78ec45804a21390fb/Untitled%2043.png)

## **쓰레드 사용 방식**

**→ 대리자를 통한 사용방식** 

 **Thread t = new Thread(new ThreadStart(함수));
가장 기본적인 스레드 생성, Foreground Thread 속성을 가진다.
Main Thread 종료 여부와 관계없이 해당 Thread는 계속 동작한다.
사용되는 대리자 : ThreadStart --> void .. (void);**

```csharp
Thread th = new Thread(new ThreadStart(ThreadFun1));
            //th.IsBackground = true; 
            th.Start();
```

**사용되는 대리자 : ParameterizedThreadStart --> void .. (object );**

```csharp
Thread th = new Thread(new ParameterizedThreadStart(ThreadFun2));
            //th.IsBackground = true; 
            th.Start(100);
```

# **서버 구현 방식**

- **대기 소켓 생성 → 주소할당 → 망연결   IPEndPoint  → Bind → Listen**

```csharp
server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PORT);
                server.Bind(ipep); // 할당 

                server.Listen(20); // 대기 소켓 버퍼 할당 
```

- **통신할 클라이언트  연결 수행 (Accept)**

```csharp
          			Console.WriteLine("서버 시작... 클라이언트 접속 대기중...");
                IPEndPoint myip = (IPEndPoint)server.LocalEndPoint;
                Console.WriteLine("자신의 주소 => {0}:{1}", myip.Address, myip.Port);

                Socket client = server.Accept();  // 클라이언트 접속 대기                                       
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine("클라이언트 주소 => {0}:{1}", ip.Address, ip.Port);
```

- **연결된 클라이언트 소켓에게 데이터 전송 및 수신**

```csharp
                        //2. 데이터 수신
                        byte[] data = new byte[1024];

                        int ret = client.Receive(data);
                        if (ret == 0)
                            throw new Exception("오류");
                        string recvmessage = Encoding.Default.GetString(data, 0, ret).TrimEnd('\0'); ;
                        Console.WriteLine("수신 메시지: {0}byte", ret);            
                        Console.WriteLine("수신 메시지: " + recvmessage);

                        //3. 데이터 송신(echo)
                        ret = client.Send(data, ret, SocketFlags.None); // 문자열 전송
                        Console.WriteLine("송신 메시지: {0}byte\n", ret);
```

**→ 송신은 사이즈 값이 필요하지만 수신은 필요없음** 

**⇒ 서버에서는 문자열입력이 아니고 받은 바이트 배열을 통해 수신 및 송신하므로 따로 함수를 쓰지않는다. (고정길이 )**

## **쓰레드 서버**

```csharp
**public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PORT);
                server.Bind(ipep);

                server.Listen(20);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }**
```

**초기화 작업 → Accept 마다 수행할 작업을 쓰레드에서  생성처리** 

## **가변 길이 서버**

```csharp
**public void Run()
        {
            while (true)
            {
                try
                {
                    //1. 연결처리
                    Console.WriteLine("서버 시작... 클라이언트 접속 대기중...");
                    IPEndPoint myip = (IPEndPoint)server.LocalEndPoint;
                    Console.WriteLine("자신의 주소 => {0}:{1}", myip.Address, myip.Port);

                    Socket client = server.Accept();  // 클라이언트 접속 대기                                       
                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                    Console.WriteLine("클라이언트 주소 => {0}:{1}", ip.Address, ip.Port);

                    Thread thread = new Thread(new ParameterizedThreadStart(ThreadFun));
                    thread.IsBackground = true;
                    thread.Start(client); // 소켓을 인자로 받음 
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Accept : " + ex.Message);
                }
            }
        }**
```

**→ 쓰레드 생성 후 받은 소켓을 통해 Recv , Send 처리** 

```csharp
public void ThreadFun(object obj)
        {
            Socket client = (Socket)obj;

            while (true)
            {
                try
                {
                    //2. 데이터 수신
                    byte[] data = new byte[1024];

                    int ret = client.Receive(data);
                    if (ret == 0)
                        throw new Exception("오류");
                    string recvmessage = Encoding.Default.GetString(data, 0, ret).TrimEnd('\0'); ;
                    Console.WriteLine("수신 메시지: {0}byte", ret);
                    Console.WriteLine("수신 메시지: " + recvmessage);

                    //3. 데이터 송신(echo)
                    ret = client.Send(data, ret, SocketFlags.None); // 문자열 전송
                    Console.WriteLine("송신 메시지: {0}byte\n", ret);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    //소켓 종료, 반복문 종료
                    client.Close();
                    break;
                }
            }
        }
```

# **클라이언트 구현 방식**

- **소켓 생성(IP , PORT ) → 주소 할당**

```csharp
			    const string SERVER_IP  = "220.90.179.75";  //"127.0.0.1"
          const int SERVER_PORT   = 7000;
			    private Socket server;
			    server = 		// 소켓 타입 생성
				  new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
					// 서버 IP , PORT 
					IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
				  server.Connect(ipep);
					      string msg = Console.ReadLine();
								int ret = client.SendData(msg);
                Console.WriteLine("{0}byte 전송", ret);

                //recv
                string recvmsg;
                ret = client.RecvData(out recvmsg);  // 데이터 받아옴 RecvData
```

- **송수신 함수**

```csharp
		  	public int SendData(string msg)
        {
            byte[] sendmessage = Encoding.Default.GetBytes(msg);
            int ret = server.Send(sendmessage);
            return ret;
        }
    
        public int RecvData(out string msg)
        {
            byte[] data = new byte[1024];

            int ret = server.Receive(data);
            msg = Encoding.Default.GetString(data).TrimEnd('\0');

            return ret; 
        }
```

## **쓰레드 클라이언트**

```csharp
						 const string SERVER_IP = "220.90.179.75";  //"127.0.0.1"
			       const int SERVER_PORT = 7000;

		         private Socket server;

        //2. 대리자 속성(사용자쪽에서 시작시 메서드 대입처리 )
		         public RecvDel RecvCallBack { get; set; } = null;	  				
				   // Run  메인 함수 
				    client = new Client();    
            //Del Callback등록!!***********************************
            client.RecvCallBack = RecvData; // 콜백함수로 받은 메시지 출력 

            if (client.Start() == false) // start 함수에서 쓰레드 처리 
                return;

            while (true)
            {
                Console.Write(">>");
                string msg = Console.ReadLine();
                if (msg == string.Empty)
                    break;

                //send
                int ret = client.SendData(msg);
                Console.WriteLine("{0}byte 전송", ret);
            }

            client.Stop();
        }
```

**→ 메인 함수에서 전송 처리** 

**→ start 함수  ⇒  Recv(수신) 처리** 

```csharp
public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
                server.Connect(ipep);

                Thread thread = new Thread(new ParameterizedThreadStart(RecvThread));
                thread.IsBackground = true;
                thread.Start(server);

                Console.WriteLine("서버에 접속...");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
```

→ RecvThread 함수 

```csharp
public void RecvThread(object obj)
        {
            try
            {
                while (true)
                {
                    string msg;
                    int ret = RecvData(out msg);
                    if (ret == 0)
                        throw new Exception("예외발생");

                    //3. Del을 사용한 동기화 호출
                    RecvCallBack.Invoke(msg); // 받은 바이트 출렬하는거
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
```

## 가변길이 송신

```csharp
private bool SendData(Socket client, byte[] data)
        {
            try
            {
                int total       = 0;            
                int size        = data.Length;  //보낼 바이트 크기
                int left_data   = size;         
                int send_data   = 0;            //보낸 바이트 크기

                // [header]전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = client.Send(data_size);

                // [data] 전체데이터(size)가 전송될때까지 반복 전송
                while (total < size)
                {
                    send_data = client.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
                del_log(client, Log.Message_Send, Encoding.UTF8.GetString(data));
                return true;
            }
            catch (Exception ex)
            {
                del_log(client, Log.Message_Send, "에러 : " + ex.Message);
                return false;
            }
        }
```

## 가변 길이 수신

```csharp
private bool ReceiveData(Socket client, ref byte[] data)
        {
            try
            {
                int total       = 0;
                int size        = 0;    //실제 받을 데이터 크기
                int left_data   = 0;    //남은 데이터 
                int recv_data   = 0;    //수신한 바이트 크기

                //[header] 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = client.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                //[data] size만큼 반복해서 데이터 수신
                while (total < size)
                {
                    recv_data = client.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
```

## 패킷 처리  서버

```csharp
static class Packet
    {
        const string PACK_SHORTMESSAGE = "pack_shortmessage";
        public static string ShortMessage(string name, string msg)
        {
            string packet = string.Empty;

            packet += PACK_SHORTMESSAGE + "\a";
            packet += name + "#";
            packet += msg;

            return packet;
        }
    }
```

```csharp
string packet = Packet.ShortMessage("최창민", message);
            server.SendAllData(s, packet, false);
```

## 패킷 처리 클라이언트

```csharp
static class Packet
    {
        const string PACK_SHORTMESSAGE = "pack_shortmessage";
        public static string ShortMessage(string name, string msg)
        {
            string packet = string.Empty;

            packet += PACK_SHORTMESSAGE + "\a";
            packet += name + "#";
            packet += msg;

            return packet;
        }
    }
```

```csharp
while (true)
            {
                string name = "최창민";
                string msg = Console.ReadLine();
                if (msg == string.Empty)
                    break;

                //패킷 생성
                string packet = Packet.ShortMessage(name, msg);

                //send
                bool b = client.SendData(packet);
            }
```

 

## 확장 메서드

```sql
using System;
namespace Sample
{
 //Extension Method 선언을 위한 static 클래스 정의
 public static class ExtensionMethod
 {
 public static bool IsEven(this int number)
 {
 //모듈러연산(%)이 아닌 비트연산 이용
 bool isEven = ((number & 0x1) == 0) ? true : false;
 return isEven;
 }
 }
 class Program
 {
 static void Main(string[] args)
 {
 int number = 3;
 Console.WriteLine("{0} 짝수 ? {1}", number, number.IsEven());
 }
 }
}
```

string 객체는 임시문자열 저장공간 을 사용 ( 상수 )

var 은 컴파일 시점에 대응되는 타입으로 칭환하고 

dynamic 은 해당 프로그램이 실행되는 시점에 타입을 결정한다 .

연동싴키는방식 ironpthyon, ironruby 등등 

```csharp
var output = o?.ToString() ?? "Default Value";
 Console.WriteLine(output); //Default Valu
```

```csharp
public class Person {
 public int Age { get; set; }
 public string Name { get; set; }
 }
 class Program {
 static Person GetPerson1() { return null; }
 static Person GetPerson2() { return new Person() { Age = 10, Name = "ccm" }; }
 static void Main(string[] args) {
 Person person = GetPerson1();
 var age = person?.Age; // 'age' will be of type 'int?', even if 'person' is not null
 Console.WriteLine(age);
 }
```

- d익명 형식의 쓰레드 ㄹ마다식
- 

```csharp
Thread thread = new Thread(
 delegate (object obj)
 {
 Console.WriteLine("ThreadFunc in anonymous method called!");
 });
 thread.Start();
 thread.Join()
```

```csharp
var emp = new { BirthYear = 1978, Name = "김도현" };
 Console.WriteLine("=== 익명 형식 선언 ===");
 Console.WriteLine("BirthYear : {0}, Name : {1}", emp.BirthYear, emp.Name);
 //익명 형식으로 된 배열 선언
 var emps = new[] {
 new { BirthYear = 1978, Name = "김도현" },
 new { BirthYear = 1983, Name = "서정인" }

foreach (var item in emps) {
 Console.WriteLine("BirthYear : {0}, Name : {1}", item.BirthYear, item.Name)

 }
```

# 쓰레드 풀

상시 실행 - 스레드가 일단 생성되면 비교적 오랜 시간 동안 생성돼 있는 유형ㅍ 통신 쓰레드 

1회성의 임시 실행 특정 연산만을 수행하고 바로 종료 accept 객체