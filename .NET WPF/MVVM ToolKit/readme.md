# MVVM TOOLKIT

# **MVVM 도구 키트 소개**

`CommunityToolkit.Mvvm` 패키지(이전의 이름이 MVVM `Microsoft.Toolkit.Mvvm`도구 키트)는 최신의 빠르고 모듈식 MVVM 라이브러리입니다. .NET 커뮤니티 도구 키트의 일부이며 다음 원칙을 기반으로 빌드됩니다.

- **Platform and Runtime Independent.NET - Standard 2.0**, **.NET Standard 2.1** 및 **.NET 6🚀**(UI Framework Agnostic)
- **간편한 선택 및 사용** - 애플리케이션 구조 또는 코딩 패러다임('MVVM'ness 외부)에 대한 엄격한 요구 사항, 즉 유연한 사용법이 없습니다.
- **일품요** 리 - 사용할 구성 요소를 자유롭게 선택할 수 있습니다.
- **참조 구현** - 기본 클래스 라이브러리에 포함되어 있지만 직접 사용할 구체적인 형식이 없는 인터페이스에 대한 구현을 제공하는 Lean 및 performant입니다.

![image.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/image.png)

 `CommunityToolkit.Mvvm;` 다운로드 

- 프로젝트 구조

```jsx
WpfMvvmToolkitDemo
 ┣ Models
 ┃ ┗ Person.cs
 ┣ ViewModels
 ┃ ┗ MainViewModel.cs
 ┣ Views
 ┃ ┗ MainWindow.xaml
 ┣ App.xaml / App.xaml.cs
 ┗ WpfMvvmToolkitDemo.csproj

```

대충 비슷하게 구성된다.

- 데이터 클래스 Model

```jsx
namespace WpfMvvmToolkitDemo.Models;

public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
```

- ViewModel

```jsx
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfMvvmToolkitDemo.Models;

namespace WpfMvvmToolkitDemo.ViewModels;

// MVVM Toolkit의 ObservableObject 상속
public partial class MainViewModel : ObservableObject
{
    // ObservableProperty → 자동으로 Property + INotifyPropertyChanged 구현
    [ObservableProperty]
    private string title = "CommunityToolkit.Mvvm 데모";

    [ObservableProperty]
    private Person? selectedPerson;

    // ObservableCollection → WPF 바인딩 친화적
    public ObservableCollection<Person> People { get; } = new()
    {
        new Person { Name = "홍길동", Age = 25 },
        new Person { Name = "김철수", Age = 30 }
    };

    // RelayCommand → 자동으로 ICommand 구현
    [RelayCommand]
    private void AddPerson()
    {
        People.Add(new Person { Name = "새 인물", Age = 20 });
    }

    [RelayCommand]
    private void RemovePerson()
    {
        if (SelectedPerson != null)
            People.Remove(SelectedPerson);
    }
}
```

## 규칙 하나

![image.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/image%201.png)

- 규칙 하나, `[ObservableProperty]` 는 속성에 대한 변경 알림을 자동으로 구현해줌 아마 change이벤트 invoke하는 걸 자동으로 해주는듯
- 이 Attribute 를 받은 속성은 Upper로 시작되선 아니한다.

![스크린샷 2025-09-18 170815.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7_2025-09-18_170815.png)

그 이유는 다음과 같다 

`ObservableProperty`  특성을 선언한 속성들은 컴파일 시 Source Generator…를 통해  대응 되는 프로퍼티 Lower{변수} ⇒ UPPER{변수}

```jsx
[ObservableProperty]
// 이렇게 선언하면 
private int count;
```

컴파일시 OberserverProperty 속성을 선언한 변수는 아래와 같이 변환 된다.

```jsx
public int Count
{
    get => count;
    set
    {
        if (!EqualityComparer<int>.Default.Equals(count, value))
        {
            OnPropertyChanging(value);
            count = value;
            OnPropertyChanged();
            OnCountChanged(value);
        }
    }
```

나만 IIncremental SourceGenerator의 사용방향을 몰랐을뿐.. MS는 잘만 써먹었다..분명히 AOP 금기시한다고 했는데…  자바의 어노테이션과의 다른점은 java→class 이렇게 변경되는 것을 이용하는 것이기에 

어노테이션에서 들어가는 매개인자가 무엇을 뜻하지만 잘알면 어떻게 변화하는지 몰라도 작성할 수 있지만 

MVVM ToolKit의 경우 java→class 가 아니기에 아마…IIncrementalGenerator 이거 쓰는거 같은데  partial 클래스를 통해 구현하므로 다양한 사용조건과 어떠한 변환 과정(사실상 변환이라기 보단 추가 구현부가 맞을듯)을  거치는지 알아야 하는 것이 좋다. 

@socket.on 으로 매개인자만 잘 알고 넣어주면 그걸 기준으로 컴파일시 원하는 구조로 변경해서 .class 파일로 변경해줌 . 예시는 아래에서 찾아볼 수 있다.

- 어노테이션 선언부

[https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/annotation/src/main/java/org/sanghan/repository/annotation/SocketSupporter/SocketOn.java](https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/annotation/src/main/java/org/sanghan/repository/annotation/SocketSupporter/SocketOn.java)

[https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/annotation/src/main/java/org/sanghan/repository/annotation/SocketSupporter/SocketController.java](https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/annotation/src/main/java/org/sanghan/repository/annotation/SocketSupporter/SocketController.java)

- 어노테이션 리플렉션 매핑 구현부

[https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/src/main/java/org/socketio/demo/domain/socket/config/SocketIoAddMappingSupporter.java](https://github.com/parksanghan/Spring-Netty-SocketIO/blob/master/src/main/java/org/socketio/demo/domain/socket/config/SocketIoAddMappingSupporter.java)

## 규칙 둘

- `RelayCommand`  속성을 받은 변수는  MVVM MainViewModel 내부에서 메서드명+Command로 증분형 소스 생성기를 통해 내부적으로 Command 객체가 생성된다.

![image.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/image%202.png)

위와 같이 [RelayCommand] 속성을 받은 AddPerson의 경우 

- 구세대 MVVM에서의 Change 알람 방식

```jsx
   class MainModel : INotifyPropertyChanged
   {
    private int num1 = 1;
    public int Num1
    {
        get { return num1; }
      
    }
      public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChange(string propertyName)
  {
      PropertyChangedEventHandler handler = PropertyChanged;

      if (handler != null)
      {
          handler(this, new PropertyChangedEventArgs(propertyName));
      }
  }
}
/// 대충 xaml ......
<TextBox text="{Binding Num1 , Mode=TwoWay}">
```

- `ObservableCollection`

컬렉션에 대한 변경을 자동으로 알림을 주어 UI에 자동으로 바인딩 

- `RelayCommand`  `RealyCommand(CanExecute =  nameof(메서드명))`

MVVM에서 버튼 클릭 같은 UI 이벤트를 ViewModel 메서드와 연결해줌 기존의 ICommand 인터페이스 상속받고  Command 객체로 실행할 메서드를 지정하는 구조⇒ RelayCommand를 통해  속성을 받은 메서드는 Command 객체를 자동으로 생성

```csharp
// Command.cs
using System;
using System.Windows.Input;

namespace _1109_MVVM
{
    class Command : ICommand
    {
        Action<object> ExecuteMethod;
        Func<object, bool> CanexecuteMethod;
        

        public Command(Action<object> e, Func<object, bool> c)
        {
            this.ExecuteMethod = e;
            this.CanexecuteMethod = c;
        }
        public Command(Action<object> e )
        {
            this.ExecuteMethod = e;
        }

        public event EventHandler CanExecuteChanged = null;
        public bool CanExecute(object parameter)
        {
            return CanexecuteMethod(parameter);
        }
        
        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
        // MainViewModel
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1109_MVVM.Model;
namespace _1109_MVVM.ViewModel
{ // control 의 역할 
    class MainViewModel 
    {
        public MainModel Model { get; set; } // 모델객체를 생성해서 가지고 있음  => 아까 위의 메인 모델 
        // ="{Binding Model.Num1, 이거와 연결되어 바인딩됨 
        // trigger는 WPF 동기화처리때문에   필요없지만  직역하자면  데이터 소스가 수정되었을때의 상황자체를 야기함.
        public Command btn_cmd { get; set; }  // 재활용 객체  특정 버튼이 눌렸을때 재활용할 수 있게 만든 객체 
        // 아래 두개의 함수 delegate를 가지는 객체 
     

        //Command="{Binding btn_cmd}" 이함수를 command와 연동시켜서 command시 바로 안에 있는 두 함수를 실행시킴 
        public MainViewModel()
        {
            Model = new Model.MainModel(); // 어떤 커맨드가 눌리면 
            btn_cmd = new Command(Execute_func, CanExecute_func); // 아래 함수들이 자동으로 호출     CanExcuteFunc 부터 출력 
         
        }        
        private void Execute_func(object obj)
        {
            Model.Num2 = Model.Num1 * 2;
        }
        private bool CanExecute_func(object obj) // 어떤 
        {
            if (Model.Num1 == 100)
                return false;
            return true;
        }
    }
}
// xaml
```

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM_ToolKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_ToolKit.ViewModel
{
    // partal 로선언 - 다른 파일에서 같은 이름의 partial class 를 선언하여 기능을 분리할 수 있다.
    partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        // 속성에 대한 변경 알림을 자동으로 구현해줌 
        // 아마 change이벤트 invoke하는 걸 자동으로 해주는듯
        public string title = "TITLE";
        [ObservableProperty]
        public Person? selectedPerson;

        public string? MyStr;

        // ObservableCollection - 컬렉션에 대한 변경 알림을 제공
        //추가되거나 변경되거나 삭제되거나 할때 자동알림
        public ObservableCollection<Person> People { get; } = new()
    {
        new Person { Name = "홍길동", Age = 25 },
        new Person { Name = "김철수", Age = 30 }
    };
        // ObervableCollection<Person> 로 생성시 자동으로 INotifyCollectionChanged 구현   

        [RelayCommand]
        private void AddPerson()
        {
            People.Add(new Person { Name = "새로운 사람", Age = 20 });
        }
        // RelayCommand - ICommand 인터페이스 구현을 자동으로 해줌

        [RelayCommand(CanExecute = nameof(CanRemovePerson))]
        // CanExecute 속성으로 명령이 실행될 수 있는지 여부를 결정하는 메서드 지정    
        private void RemovePerson()
        {
            if (SelectedPerson != null)
            {
                People.Remove(SelectedPerson);
            }
        }
        private bool CanRemovePerson()=> SelectedPerson != null;

    }
}
//...xaml
<Window x:Class="MVVM_ToolKit.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="clr-namespace:MVVM_ToolKit.ViewModel"
        Title="MVVM Toolkit Demo" Height="300" Width="400">

    <Window.DataContext>
        <vm:MainViewModel/>
    </Window.DataContext>

    <DockPanel Margin="10">
        <StackPanel DockPanel.Dock="Top" Orientation="Horizontal" Margin="0 0 0 10">
           
            
            <Button Content="추가" Command="{Binding AddPersonCommand}" Margin="0 0 10 0"/>
            <Button Content="삭제" Command="{Binding RemovePersonCommand}"/>
        </StackPanel>

        <ListBox ItemsSource="{Binding People}" 
                 SelectedItem="{Binding SelectedPerson}" 
             
                
                 Selected="ListBox_Selected"
                 DisplayMemberPath="Name" Height="180"/>

        <TextBlock Text="{Binding Title}" 
                   FontSize="16" 
                   HorizontalAlignment="Center" 
                   Margin="0 10 0 0"/>
    </DockPanel>
</Window>

```

MVVM ToolKit으로 간단하게 바인딩이 가능하며 축약된 단계로 바인딩할 수 있다.

## DI 주입  (HostBuilder)

```csharp
// mainWindows.xaml

    <Window.DataContext>
        <vm:MainViewModel/>
    </Window.DataContext>
```

View에서  네임스페이스로 지정한 vm에 DataContext로 MainViewModel을  참조하도록 하는 구조에서 

App에서의 DI 주입으로 간단하게 설정이 가능하다.

![image.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/image%203.png)

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM_ToolKit.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
namespace MVVM_ToolKit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;       
        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices(s =>
            {
                // DI 등록
                s.AddSingleton<MainViewModel>(); //프로그램에서 하나만 존재하는 MainViewModel 등록
                s.AddSingleton<MainWindow>(); // 메인 윈도우는 앱 생명주기 동안 딱 하나만 사용
            })
            .Build();

            var main = _host.Services.GetRequiredService<MainWindow>(); // 서비스 컨테이너에서 MainWindow 인스턴스 가져오기
            // 해당 시점에서 WPF 창이 생성
            main.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            // MainViewModel 인스턴스를 DataContext로  컨테이너에서 꺼내서 주입
           
            main.Show();

            base.OnStartup(e);
        }

    }

}

```

아래와 같이 주입하고 XAML에서 DataContext를 지우고 사용이 가능하다.

## NotifyProPertyChangedFor - 종속 속성 알림

- 특정 속성이 바뀔 때 다른 속성도 같이 알림이 가도록 할 수 있다.

아래의 경우  FirstName은 자동으로 FirstName 가 변경 시 FullName도 자동으로 PropertyChanged 이벤트가  전파되어 갱신되게 한다.

ObservableProperty 는 다른 프로퍼티 속성에 전파되어 쓰인다.

```csharp
[ObservableProperty]
[NotifyPropertyChangedFor(nameof(FullName))]
private string firstName = "";

[ObservableProperty]
[NotifyPropertyChangedFor(nameof(FullName))]
private string lastName = "";

public string FullName => $"{FirstName} {LastName}";

```

## NotifyProPertyChangedFor - CanExecute 자동 갱신

- ViewModel 속성이 변경 때마다 특정 Command의 CanExecute 갱신을 자동으로 바인딩

아래의 경우 NotifyProPertyChangedFor 는  selectedPerson 이 변경 시  RemovePersonCommand 로 이벤트가 전파되어 해당 Command 메서드가 실행하도록하고  Command 객체에서는 CanExecute 를 통해  실행여부를 정한다.

```csharp
[ObservableProperty]
[NotifyCanExecuteChangedFor(nameof(RemovePersonCommand))]
private Person? selectedPerson;

[RelayCommand(CanExecute = nameof(CanRemovePerson))]
private void RemovePerson() => People.Remove(SelectedPerson!);

private bool CanRemovePerson() => SelectedPerson is not null;

```

## AsyncRelayCommand - 비동기 Command 객체

- 해당 속성은 RelayCommand를 기본으로 사용하며  async Task 함수로 선언한다.

AsyncRelayCommand의 경우 메서드명을 LoadPeopleAsync로 선언하더라도  Attribute 바인딩 과정에서 Async를 제외하고 메서드가 재생성되어 LoadPeopleCommand로 생성된다.

```csharp
     [RelayCommand]
     private async Task LoadPeopleAsync()
     {
         Title = "로딩 중…";              
         await Task.Delay(2000);
         People.Clear();
         People.Add(new Person { Name = "비동기 사람1", Age = 28 });
         People.Add(new Person { Name = "비동기 사람2", Age = 35 });
         Title = "로딩 완료";
         Debug.WriteLine("LoadPeopleAsync completed" ); 

     }
```

![image.png](MVVM%20TOOLKIT%20272e5df1bb8a806386f1f300270a77f5/image%204.png)

## ObservableValidator - 유효성 검증

- ObservableValidator 는 유효성 검사에 사용되며 DataAnnotation 속성을 사용해 폼 검증 기능을 사용할 수 있다.

```csharp
        [ObservableProperty]
        [Required(ErrorMessage ="Title Cant not be Empty")]
        [MaxLength(20, ErrorMessage ="Title Max Length is 20")]
        [MinLength (3, ErrorMessage ="Title Min Length is 3")]
        private string title = "TITLE";
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdatePersonNameCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemovePersonCommand))]
        private Person? selectedPerson;
```

## Messenger - ViewModel 간 메시지 통신

다른 뷰끼리 이벤트 전파를 할때는  ViewModel간의 메세지 통신을 사용한다.

```csharp
//..\ViewModel\Messages\PersonSelectedMessage.cs
using MVVM_ToolKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Tool_K.ViewModel.Messages
{
    // 메세지 타입
    // 서로 다른 뷰끼리 통신하기 위해 사용 
    // 한쪽에서는 WeakReferenceMessenger.Default.Send(new PersonSelectedMessage(person));로 이벤트 전송
    // 다른쪽에서는 MessengerHelper.Register<PersonSelectedMessage>(this, (r, m) => { ... });로 이벤트 수신
    public sealed record PersonSelectedMessage(Person Person);
}
```

```csharp
// //..\ViewModel\ MainViewModel.cs

[ObservableProperty]
[NotifyCanExecuteChangedFor(nameof(UpdatePersonNameCommand))]
[NotifyCanExecuteChangedFor(nameof(RemovePersonCommand))]
private Person? selectedPerson;
   
   // [ObservableProperty0] 속성이 자동으로 해당메서드를 찾음
   partial void OnSelectedPersonChanged (Person? value)
   {
       if (value != null)
       {
           //
           WeakReferenceMessenger.Default.Send(new PersonSelectedMessage(value));
       }
      
   }
```

위와 같이 이벤트 전파를 위해 WeakReferenceMessenger.Default.Send(new PersonSelectedMessage(value)); 를 통해 이벤트를 알리고  이때  observableProperty 속성을 받은 selectedPerson 은  아래와 같은 과정을 거친다.

```csharp
public Person? SelectedPerson
{
    get => selectedPerson;
    set
    {
        if (!EqualityComparer<Person?>.Default.Equals(selectedPerson, value))
        {
            OnSelectedPersonChanging(value); // 변경 전 훅 (구현 선택 가능)

            selectedPerson = value;

            OnPropertyChanged(nameof(SelectedPerson)); // UI에 알림

            OnSelectedPersonChanged(value); // 변경 후 훅 (구현 선택 가능)
        }
    }
}
```

ObservableProperty 는 Setter 내부에서 OnSelectedPersonChanged 이 자동으로 생성되고 호출되기 때문에 아래와 같이 partial 로선언하면  ObservableProperty 에서 자동으로 해당 메서드를 찾아 실행하는 구조가 된다.

```csharp
// 이 두 메서드는 partial로만 선언됨
partial void OnSelectedPersonChanging(Person? value);
partial void OnSelectedPersonChanged(Person? value);
```

이후 이벤트 전파를 Send를 통해 송신알림을 받은 PersonSelectedMessage은 수신측에서는 아래와 같이 구현한다.

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MVVM_Tool_K.ViewModel.Messages;
using MVVM_ToolKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_ToolKit.ViewModel
{
    public partial class DetailViewModel: ObservableObject, IRecipient<PersonSelectedMessage>, IDisposable
    {
        private Person? current;
        public string? CurrentName => current?.Name;

        public DetailViewModel()
        {
            // 이 VM이 살아있는 동안 메시지 수신
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        // 메시지 도착 시 호출
        public void Receive(PersonSelectedMessage message)
        {
            current = message.Person;
            OnPropertyChanged(nameof(CurrentName)); // UI 갱신
        }

        public void Dispose()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }   
    }
}

```