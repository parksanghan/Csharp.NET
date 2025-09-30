using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVM_Tool_K.ViewModel.Messages;
using MVVM_ToolKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_ToolKit.ViewModel
{
    // partal 로선언 - 다른 파일에서 같은 이름의 partial class 를 선언하여 기능을 분리할 수 있다.
    // ObservableObject - INotifyPropertyChanged 인터페이스 구현을 자동으로 해줌
    // ObservableValidator - INotifyDataErrorInfo 인터페이스 구현을 자동으로 해줌 
    // ObservableValidator 는 기본적으로  ObservableObject  상속함
    partial class MainViewModel :  ObservableValidator
    {       
        // 속성에 대한 변경 알림을 자동으로 구현해줌 
        // 아마 change이벤트 invoke하는 걸 자동으로 해주는듯
        [ObservableProperty]
        [Required(ErrorMessage ="Title Cant not be Empty")]
        [MaxLength(20, ErrorMessage ="Title Max Length is 20")]
        [MinLength (3, ErrorMessage ="Title Min Length is 3")]
        private string title = "TITLE";
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdatePersonNameCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemovePersonCommand))]
        private Person? selectedPerson;

        // [ObservableProperty0] 속성이 자동으로 해당메서드를 찾음
        partial void OnSelectedPersonChanged(Person? value)
        {
            if (value != null)
            {
                //
                WeakReferenceMessenger.Default.Send(new PersonSelectedMessage(value));
            }
           
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        [NotifyCanExecuteChangedFor(nameof(UpdatePersonNameCommand))]
        public string? personFirstName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        [NotifyCanExecuteChangedFor(nameof(UpdatePersonNameCommand))]
        public string? personLastName;
 
        
        public String FullName => $"{PersonFirstName} {PersonLastName}";    

        [RelayCommand (CanExecute = nameof(CanUpdatePersonName))]
        public void UpdatePersonName()
        {
            if (SelectedPerson != null)
            {
                SelectedPerson.Name = $"{PersonFirstName} {PersonLastName}";
                // SelectedPerson 속성 변경시 자동으로 PropertyChanged 이벤트 발생
           
            }
        }
        public bool CanUpdatePersonName() => SelectedPerson != null && !string.IsNullOrWhiteSpace(PersonFirstName) && !string.IsNullOrWhiteSpace(PersonLastName);   
        // ObservableCollection - 컬렉션에 대한 변경 알림을 제공
        //추가되거나 변경되거나 삭제되거나 할때 자동알림
        public ObservableCollection<Person> People { get; } = new()
    {
        new Person { Name = "홍길동", Age = 25 },
        new Person { Name = "김철수", Age = 30 }
    };
        // ObervableCollection<Person> 로 생성시 자동으로 INotifyCollectionChanged 구현   
        [RelayCommand]
        private void UpdateTitle()
        {
            Title = "Updated Title " + DateTime.Now.ToString("HH:mm:ss");
            // Title 속성 변경시 자동으로 PropertyChanged 이벤트 발생
        }

        [RelayCommand]
        private void AddPerson()
        {
            People.Add(new Person { Name = "새로운 사람", Age = 20 });
            Debug.WriteLine("AddPerson completed");
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
        [RelayCommand]
        private void Save()
        {
            ValidateAllProperties();
            if (HasErrors) return; // 에러 있으면 저장 막기
 
        }
    }
}
