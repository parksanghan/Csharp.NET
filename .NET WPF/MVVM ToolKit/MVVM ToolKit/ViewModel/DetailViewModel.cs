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
