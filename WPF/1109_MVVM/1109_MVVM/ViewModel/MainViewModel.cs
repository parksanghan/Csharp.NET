    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1109_MVVM.ViewModel
{ // control 의 역할 
    class MainViewModel 
    {
        public Model.MainModel Model { get; set; } // 모델객체를 생성해서 가지고 있음  => 아까 위의 메인 모델 
        // ="{Binding Model.Num1, 이거와 연결되어 바인딩됨 
        // trigger는 WPF 동기화처리때문에   필요없지만  직역하자면  데이터 소스가 수정되었을때의 상황자체를 야기함.
        public Command btn_cmd { get; set; }  // 재활용 객체  특정 버튼이 눌렸을때 재활용할 수 있게 만든 객체 
        // 아래 두개의 함수 delegate를 가지는 객체 
        public Command btn_insert { get; set; }
        public Command btn_Delete { get; set; } 

        public Command btn_Update { get; set; } 

        public Command btn_SelectAll { get; set; }  

        //Command="{Binding btn_cmd}" 이함수를 command와 연동시켜서 command시 바로 안에 있는 두 함수를 실행시킴 
        public MainViewModel()
        {
            Model = new Model.MainModel(); // 어떤 커맨드가 눌리면 
            btn_cmd = new Command(Execute_func, CanExecute_func); // 아래 함수들이 자동으로 호출     CanExcuteFunc 부터 출력 
            btn_insert = new Command(Insert_Func);
            btn_Delete = new Command(Delete_Func);
            btn_Update = new Command(Update_Func);
            btn_SelectAll = new Command(SelectAll_Func);
        }        

        public void Insert_Func(object obj)
        {

        }
        public void Delete_Func(object obj)
        {

        }
        public void Update_Func(object obj)
        {

        }
        public void SelectAll_Func(object obj)
        {

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
