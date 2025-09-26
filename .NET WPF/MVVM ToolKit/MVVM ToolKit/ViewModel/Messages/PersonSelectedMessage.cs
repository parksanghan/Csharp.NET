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