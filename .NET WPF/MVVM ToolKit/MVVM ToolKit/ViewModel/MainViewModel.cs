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
