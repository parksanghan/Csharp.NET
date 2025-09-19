using CommunityToolkit.Mvvm.ComponentModel;
using MVVM_ToolKit.Model;
using System;
using System.Collections.Generic;
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
    }
}
