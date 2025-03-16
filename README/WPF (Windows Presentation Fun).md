# WPF (Windows Presentation Fun)

## **WPF 주요 특징**

**WPF 의 주요 특징을 살펴보면 컨텐츠라 부르는 여러 요소들(컨트롤, 텍스트, 그래픽 등)을
하나의 모델로 통합하여 관리할 수 있다. 이들은 내부적으로 DirectX 기반으로 개발되어 DirectX 기술의 좋은 특징을 가질 뿐만 아니라 외형적으로는 DirectX 기반인지 알지 못하며 WinForm 과 같은 다른 .Net 환경의 기술들처럼 사용하기 쉽고 생산성 높게 개발할 수 있다.**

**→컨트롤들도 컨텐츠로 통합적으로 객체로 다룰 수 있다.**

## **데이터 바인딩**

- **데이터 속성 과 속성을 이어줌 .**

## **UI 를 표현하는 언어 XAML**

**메인을 숨김** 

**StartupUri="MainWindow.xaml">** 

**⇒ application 에서  경로를 제공하는 것을 통해 메인을 실행** 

```xml
<Application x:Class="MyWPFApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <!-- Resources here -->
    </Application.Resources>
</Application>
```

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled.png)

## **컨텐츠 모델**

**WPF 에서는 사용자 정의 컨트롤을 사용하지 않고독 외관을 아~주 유연하게 만들 수 있다.** 

**WPF 의 대부분의 컨트롤(ContentControl 로부터 파생된 요소)은 Content 라는 속성을 가지며 이 속성의 형식이 object 형식이므로 일반 문자열이나 .Net 객체라면 어떤 데이터든지 컨텐츠로 가질 수 있다**

**⇒  Panel 은 여러개의 컨텐츠 구조를 가질수 있다.** 

**⇒ 하나의 윈도우에서는 하나의 StackPanel가지고 그 안에는 3개의 버튼을 가짐**  

**xaml 코드로 자식 컨텐츠를 구성하는 방법이 여러 개이다 .**

**여러  개인 이유 ?** 

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%201.png)

## **의존 속성**

**의존에 의해서 나중에 써진 것이 의존성에 의해서 덮어 씌워짐** 

## **라우트 된 이벤트**

**WPF 는 ‘라우트된 이벤트’라는 새로운 개념의 이벤트 메커니즘을 제공한다.**

**또한 ,WPF 만의 기능(컨텐츠 모델)에 맞게 새로 정의된 이벤트로 크게 두 가지
버블링(bubbling) 이벤트와 터널링(tunneling) 이벤트가 있다**

**버블링 : 버튼 → 캔버스 → 창**
 

**터널링 : 창 → 캔버스 → 버튼**

  **버블링 이벤트는 마우스를 실제 위치한 자식 요소부터 부모 요소로 라우팅(전달)되며 발생하고**

 **터널링 이벤트는 말 그대로 부모 요소부터 자식 요소로 이벤트가 라우팅(전달)되며 연속하게 발생함**

****

**‘라우트된 이벤트’를 사용하면서 주의할 점은 핸들러에 전달되는 sender 는 이벤트를 등록한
객체의 참조이며 이벤트를 발생시킨 객체의 참조는 e.Source 로 얻을 수 있다.
다음은 핸들러로 전달된 이벤트 주체를 출력하는 예제다.**

**버블링이나 터널링에서**

**버블링 : 버튼 → 캔버스 → 창
일반적인 mousecliick 이 버블링**

**터널링 : 창 → 캔버스 → 버튼
=> preview 객체가 터널링
에서 터널링의 경우 mousedown**

**버블링의 경우:  mouseclick을하면 이건 버블링이니간 만아래 하위에서 클릭을 만나고 그 후 버튼 클릭을 만나면 라우팅 을 중지하니 컨버스 나 창에 대한 클릭은 라우팅이 되지않는다 .** 

**터널링의 경우 : 터널링에서는 창에서  클릭 만나고 그 후 캔버스 클릭 만나고 이후 버튼 클릭 만나니간 거기서 라우팅 중지를 해서 라우팅이 중지되기에 버튼 아래속성의 Rectangle 클릭은 라우팅이 되지 않는다**

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%202.png)

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%203.png)

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%204.png)

![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%205.png)

1. **터널링 이벤트 (예: Panel에서 처리):**
    - **Panel에서 이벤트 핸들러에서 `e.Handled = true`를 설정하면 이벤트 라우팅이 중단됩니다.**
    - **그로 인해 Panel 아래의 하위 요소인 Button 클릭 및 Button 아래의 Rectangle 클릭 이벤트는 라우팅되지 않으므로 실행되지 않습니다.**
2. **버블링 이벤트 (예: Rectangle에서 처리):**
    - **Rectangle에서 이벤트 핸들러에서 `e.Handled = true`를 설정하면 버블링 이벤트 라우팅이 중단됩니다.**
    - **이로 인해 Rectangle 아래의 하위 요소인 Button 클릭 이벤트가 상위로 버블링되지 않고, Button 클릭 이벤트에 대한 함수가 실행되지 않습니다.**
    
    ## **데이터 바인딩**
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%206.png)
    
    - **WPF 는 데이터 바인딩이라는 강력한 기술을 통하여 프로그램 내부의 데이터를 사용자에게 전달하거나 사용자가 입력한 데이터를 프로그램 내부의 데이터로 변환하는 자동화 기술을 제공한다 .**
    - **데이터 바인딩을 통해 데이터 변환 ( 수정 ) / 정렬 (sort) / 필터 /  그룹화에 대한 기능을 제공**
    
    ## **데이터 바인딩 미사용 - 일반적인 UI 다루기**
    
    ```xml
    <StackPanel>
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <StackPanel Grid.Column="0" Margin="5" Orientation="Horizontal">
     <Label >이름(_N):</Label>
     <TextBox Name="name" Width="120" />
     </StackPanel>
     <StackPanel Grid.Column="1" Margin="5" Orientation="Horizontal">
     <Label >전화(_P):</Label>
     <TextBox Name="phone" Width="120" />
     </StackPanel>
     </Grid>
     </StackPanel>
    ```
    
    ```csharp
    public partial class MainWindow : Window
     {
     private Person per = new Person() 
    { Name = "홍길동", Phone = "010-1111-1234" };
     public MainWindow()
     {
     InitializeComponent();
     UpdateNameToUI();
     UpdatePhoneToUI();
     }
     private void UpdateNameToUI()
     {
     name.Text = per.Name;
     }
     private void UpdatePhoneToUI()
     {
     phone.Text = per.Phone;
     }
     }
     public class Person
     {
     public string Name { get; set; }
     public string Phone { get; set; }
    }
    ```
    
    ## **TEXTBOX 변동을 통한 데이터 객체 갱신 → 양방향 통신**
    
     ****
    
    ```xml
    <StackPanel>
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <StackPanel Grid.Column="0" Margin="5" Orientation="Horizontal">
     <Label >이름(_N):</Label>
     <TextBox Name="name" Width="120" TextChanged="name_TextChanged" 
    />
     </StackPanel>
     <StackPanel Grid.Column="1" Margin="5" Orientation="Horizontal">
     <Label >전화(_P):</Label>
     <TextBox Name="phone" Width="120" TextChanged="phone_TextChanged" 
    />
     </StackPanel>
     </Grid>
     </StackPanel>
    ```
    
    ```csharp
    private void name_TextChanged(object sender, TextChangedEventArgs e)
     {
     per.Name = name.Text;
     }
     private void phone_TextChanged(object sender, TextChangedEventArgs e)
     {
     per.Phone = phone.Text;
    Title = per.Phone;
     }
    ```
    
    ### **이런식으로 데이터 바인딩을 사용하지 않는다면 리스트 박스 선택에서 텍스트 박스에 출력되게 하거나 삭제 시 리스트박스에서 값이 존재하는지 검사하는 등 기능이 많아질 수록 예외처리에 대한 작업이 너무 많아지면서 복잡도가 증가하게 된다.  ⇒ 성능이 후달림**
    
    ## **데이터 바인딩 사용**
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%207.png)
    
    - **데이터 사이의 관계를 정의 하여  EX)Binding path = Name 을통해  할당할 수 있다.**
    
    ```xml
    <StackPanel Name="panel">
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <StackPanel Grid.Column="0" Margin="5" Orientation="Horizontal">
     <Label >이름(_N):</Label>
     <TextBox Text="{Binding Path=Name}" Width="120"/>
     </StackPanel>
     <StackPanel Grid.Column="1" Margin="5" Orientation="Horizontal">
     <Label >전화(_P):</Label>
     <TextBox Text="{Binding Path=Phone}" Width="120"/>
     </StackPanel>
     </Grid>
     </StackPanel>
    ```
    
    ```csharp
    public partial class MainWindow : Window
     {
     private Person per = new Person() { Name = "홍길동", 
    Phone = "010-1111-1234" };
     public MainWindow()
     {
     InitializeComponent();
     panel.DataContext = per;
     }
     }
     public class Person
     {
     public string Name { get; set; }
     public string Phone { get; set; }
     }
    ```
    
    ### **데이터 바인딩은  UI에서 값을 바꾸면 내부 값도 바뀌지만 C# 코드 내부에서 바꾸면 바인딩에서 지원하지않기에 바뀌지않는다.**
    
    **⇒ 이런 경우 ,  WPF 에 통지를 통해 값이 변경되었음을 통지해야한다.**
    
    **즉, 이것은 [per.Name](http://per.name/) 이나 per.Phone 속성은
    일반 .Net 속성이므로 속성의 변경(데이터 원본의 변경) 사실을 UI 에 동기화하지 못한다.**
    
    ```csharp
    public class Person : INotifyPropertyChanged
    {
        private string name;
    
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name"); // 속성 변경을 알림
                }
            }
        }
    
        // INotifyPropertyChanged 인터페이스를 구현한 OnPropertyChanged 메서드
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
    }
    ```
    
    ```xml
    			  <Label Target="{Binding ElementName=name}">이름(_N):</Label>
            <TextBox Name="name" Width="100" />
            <Label Target="{Binding ElementName=phone}">전화(_P):</Label>
            <TextBox Name="phone" Width="100" />
    ```
    
    **데이터 바인딩 후 c#코드에서** 
    
    ```csharp
    panel.DataContext = per; // 데이터 바인딩 시작
                // 패널안에 있는 컨텐츠나 속성들도 모두 다 바인딩
    ```
    
    **바인딩 시스템에서 핵심 기능**
    
    - **UI의 변경을 데이터 객체로 데이터 객체의 변경을 UI의 변경을 데이터 객체로 데이터 객체의 변경을 UI로 자동 통신하는 기능**
    - 
    
    C# 코드에 정의된 Person 클래스를 XAML 에서 사용하기 위해 C#코드상의 네임스페이스를 XAML
    네임스페이스로 사용하기 위해 아래 코드를 사용하여 해당 네임스페이스를 local로 xaml에서 사용하며 
    
    ```csharp
    xmlns:local="clr-namespace:ex05_11"
    ```
    
    XAML 리소스를 사용한 데이터 원본 추출 
    
    ```xml
    <Window x:Class="ex05_11.MainWindow"
     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
     Title="MainWindow" Height="350" Width="525"
     xmlns:local="clr-namespace:ex05_11">
     <Window.Resources>
     <local:Person x:Key="person" Name="홍길동" Phone = "010-1111-1234" />
     </Window.Resources>
     <StackPanel DataContext="{StaticResource person}">
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <StackPanel Grid.Column="0" Margin="5" Orientation="Horizontal">
     <Label >이름(_N):</Label>
     <TextBox Text="{Binding Path=Name}" Width="120"/>
     </StackPanel>
     <StackPanel Grid.Column="1" Margin="5" Orientation="Horizontal">
     <Label >전화(_P):</Label>
     <TextBox Text="{Binding Path=Phone}" Width="120"/>
     </StackPanel>
     </Grid>
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <Border Grid.Column="0" Margin="2" BorderBrush="Black"
    BorderThickness="2">
     <Label Height="25" Content="{Binding Path=Name}"/>
     </Border>
     <Border Grid.Column="1" Margin="2" BorderBrush="Black"
    BorderThickness="2">
     <Label Height="25" Content="{Binding Path=Phone}"/>
     </Border>
     </Grid>
     <Grid >
     <Button Name="eraseButton" Margin="3" Height="30" Grid.Column="2"
    Content="Clear" Click="eraseButton_Click" />
     </Grid>
     </StackPanel>
    </Window>
    ```
    
    ```xml
    Window.Resources>
     <local:Person x:Key="person" Name="홍길동" Phone = "010-1111-1234" />
     </Window.Resources>
    ```
    
    로컬 키를 통해 리소스를 갖고온나.
    
    ```csharp
    public partial class MainWindow : Window
     {
     Person per = null;
     public MainWindow()
     {
     InitializeComponent();
     per = (Person)FindResource("person");
     }
     private void eraseButton_Click(object sender, RoutedEventArgs e)
     {
     per.Name = "";
     per.Phone = "";
     }
     }
    ```
    
    이렇게 하면 데이터 바인딩이 된 데이터를 가져올수 있다.
    
    하지만 C# 코드를 사용하지 않고 아래와 같이 한번에 할당할 수 있다.
    
    ```xml
    <StackPanel DataContext="{StaticResource person}">
    ```
    
    데이터 원본을 명시적으로 바인딩 문법을 사용하여 설정할 수 있으며 이때는 부모 요소의
    DataContext 에 설정된 데이터 원본보다 우선한다.
    다음은 TextBox 컨트롤은 부모 요소의 DataContext 에 설정된 데이터 원본을 사용하고 Label
    컨트롤은 명시적인 데이터 원본을 사용하는 예제다.
    
    ⇒ 즉 , 하위에 있는 DataContext 리소스가 우선이다.
    
    ```xml
    <Window x:Class="ex05_12.MainWindow"
     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
     Title="MainWindow" Height="350" Width="525"
     xmlns:local="clr-namespace:ex05_12">
     <Window.Resources>
     <local:Person x:Key="person1" Name="홍길동" Phone = "010-1111-1234" />
     <local:Person x:Key="person2" Name="일지매" Phone = "010-2222-1234" />
     </Window.Resources>
     <StackPanel DataContext="{StaticResource person1}">
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <StackPanel Grid.Column="0" Margin="5" Orientation="Horizontal">
     <Label >이름(_N):</Label>
     <TextBox Text="{Binding Path=Name}" Width="120"/>
     </StackPanel>
     <StackPanel Grid.Column="1" Margin="5" Orientation="Horizontal">
     <Label >전화(_P):</Label>
     <TextBox Text="{Binding Path=Phone}" Width="120"/>
     </StackPanel>
     </Grid>
     <Grid >
     <Grid.ColumnDefinitions>
     <ColumnDefinition/>
     <ColumnDefinition/>
     </Grid.ColumnDefinitions>
     <Border Grid.Column="0" Margin="2" BorderBrush="Black"
    BorderThickness="2">
     <Label Height="25" Content="{Binding Path=Name,
    Source={StaticResource person2}}"/>
     </Border>
     <Border Grid.Column="1" Margin="2" BorderBrush="Black"
    BorderThickness="2">
     <Label Height="25" Content="{Binding Path=Phone,
    Source={StaticResource person2}}"/>
     </Border>
     </Grid>
     <Grid >
     <Button Name="eraseButton" Margin="3" Height="30" Grid.Column="2"
    Content="Clear" Click="eraseButton_Click" />
     </Grid>
     </StackPanel>
    </Window>
    ```
    
    ## **라디오버튼의 데이터 바인딩**
    
    ```xml
    <local:Person x:Key="person" Name="이몽룡" Phone = "010-1111-1234"
    Male="True" />
    
    <RadioButton IsChecked="{Binding Path=Male}" Content="남"
    Margin="5,5,20,5" />
     <RadioButton IsChecked="{Binding Path=Male,
    Converter={StaticResource maleConverter}}" Content="여" Margin="5"/>
    ```
    
    ```csharp
    public partial class MainWindow : Window
     {
     Person per = null;
     public MainWindow()
     {
     InitializeComponent();
     per = (Person)FindResource("person");
     }
     private void eraseButton_Click(object sender, RoutedEventArgs e)
     {
     per.Name = "";
     per.Phone = "";
     per.Male = null;
     }
     }
     public class Person : INotifyPropertyChanged
     {
     public event PropertyChangedEventHandler PropertyChanged;
     private string name;
     public string Name
     {
     get { return name; }
     set
     {
     name = value;
     if (PropertyChanged != null)
     PropertyChanged(this, new PropertyChangedEventArgs("Name"));
     }
     }
     private string phone;
     public string Phone
     {
     get { return phone; }
     set
     {
     phone = value;
     if (PropertyChanged != null)
     PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
     }
     }
     private bool? male;
     public bool? Male
     {
     get { return male; }
     set
     {
     male = value;
     if (PropertyChanged != null)
     PropertyChanged(this, new PropertyChangedEventArgs("Male"));
     }
     }
     }
     [ValueConversion(/* 원본 형식 */ typeof(bool), /* 대상 형식 */ typeof(bool))]
     public class MaleToFemaleConverter : IValueConverter
     {
     // 데이터 속성을 UI 속성으로 변경할 때
     public object Convert(object value, Type targetType, object parameter, 
    System.Globalization.CultureInfo culture)
     {
     if (targetType != typeof(bool?)) 
     return null;
     bool? male = (bool?) value;
     if (male == null)
     return null;
     else
     return !(bool?)value;
     }
     // UI 속성을 데이터 속성으로 변경할 때
     public object ConvertBack(object value, Type targetType, object
    parameter, System.Globalization.CultureInfo culture)
     {
     if (targetType != typeof(bool?))
     return null;
     bool? male = (bool?)value;
     if (male == null)
     return null;
     else
     return !(bool?)value;
     }
     }
    ```
    
    object value 는 데이터의 속성이 전달디고   type 의 전달을 통해 타입을 가져옴
    
    ## 데이터 바인딩의 유효성 검사
    
    **기본 유효성 검사는 형식이 맞는지 검사만 하기에 상죵자 정의 유효성 검사를 정의** 
    
    다음은 ListBox 컨트롤의 IsSynchronizedWithCurrentItem="True" 속성을 상용하여 현재
    아이템이 단일 바인딩 UI 와 바인딩 되도록한 예제다.’
    
    WPF 는 current item을 가진다 
    
    컬렉션 뷰(collection view)라고 부르는 UI
    컨트롤과 일반 컬렉션 사이의 인터페이스를 수행하는 뷰를 얻어 사용해야 한다
    
    ⇒ Current item 와 컬렉션 객체와 동기화 연결을 통해 동작처리를 해준다 . 
    
    =⇒ 컬렉션 뷰 
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%208.png)
    
    ## **리스트 박스의 구조**
    
    **Items 속성은 아이템 컨테이너라고 하는 컬렉션에 각각의 매핑된 아이템 요소들을
    추가하는 형태다. 예로 ListBox 컨트롤의 Items 는 ListBoxItem 들을 추가하는 컬렉션이며
    ComboBox 컨트롤의 Items 는 ComboBoxItem 들을 추가하는 컬렉션이다. XAML 문법에서는 자식
    요소로 추가할 수 있으며 각각의 ListBoxItem 과 ComboBoxItem 들은 컨텐츠 컨트롤이며 다양한
    형태의 컨텐츠를 가질 수 있다. 다음은 ListBoxItem 의 상속 구조를 보인다.**
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%209.png)
    
    ## **MVVM 패턴**
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%2010.png)
    
    View : 사용자가 보는 화면 UI 를 구성 . XAML 로 구성되어 있으며 디자이너가 다루는 영역
    
    ViewModel : View , UI 실제 동작에 관련된 부분을 작성합니다 . 버튼읗 클릭하는 등의 작업을 여기서 구성한다 → 본래의 Control의 기능
    
    MOdel : View 모델에서 사용될 데이터를 정의 데이터는 DB에서 받아올 수도 있으며 필요한 데이터를 정의할 수 있다 .
    
    View : XAML = > UI 
    
    ViewModel :  실제 동작에 대한 부분 Control  클릭에 대한 처리 다 여기서함 
    
    Model : View모델에서 사용될 데이터 정의  EX : BOOK , PERSON
    
    참조를 위해서 폴더안에 넣어주고 cs 를 생성하여 프로젝트를 관리한다 .
    
    ## MVVM 데이터 바인딩
    
    ### 데이터를 UI에 바인딩 했을 떄 실시간으로 업데이트 하기 위한 사용
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%2011.png)
    
    ## Model  정의 ⇒ 데이터 정의
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%2012.png)
    
    ### ViewModel 정의 ⇒ Control 종류의 객체
    
    ![Untitled](WPF%20(Windows%20Presentation%20Fun)%205c2209bef23549138c94e30096513ade/Untitled%2013.png)
    
    view 모델 → 우리가 사용했던 Control 모델
    
    자동으로 처리  → 데이터바인딩이 기본적인 번뇌 기술
    
    ⇒ 여기서 deleagate를 통해 이벤
    
    view 자체에 코드가 안만들어짐 
    
    뷰에서 버튼이 있을 view 에서 버튼 을 객체 를 만들어서 다루는데 
    
    view 모델 구성에서는 대부분의 코드를 xaml에서 만드는 걸 목표로 한다 .
    
    버튼핸들러가  viewmodel 에 있다.
    
    ## **Shared Memoey**
    
     ****