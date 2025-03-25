using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Data; 
namespace MauiApp1.Model
{
    public class ChatViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<ChatMessage> Messages { get; set; } = new();

        // 메시지 전송 함수 (버튼 클릭 등에서 호출)
        public async Task simulation(string userText)
        {
            // 유저 메시지를 추가
            Messages.Add(new ChatMessage { Sender = "user", Text = userText });

            // 서버 또는 ChatGPT에게 응답 요청
            string response = await CallServerAsync(userText);

            // 봇 응답 메시지를 추가
            Messages.Add(new ChatMessage { Sender = "bot", Text = response });
        }

        private async Task<string> CallServerAsync(string message)
        {
            // 여기엔 실제 API 호출 또는 테스트용 가짜 응답을 넣을 수 있음
            await Task.Delay(500); // 응답 지연 시뮬레이션
            return $"[챗봇] '{message}'에 대한 응답입니다.";
        }
        //   public event PropertyChangedEventHandler? PropertyChanged; 사용예시
        //private string _someText;
        //public string SomeText
        //{
        //    get => _someText;
        //    set
        //    {
        //        if (_someText != value)
        //        {
        //            _someText = value;
        //            OnPropertyChanged(nameof(SomeText)); // ✅ 바뀐 걸 UI에 알림
        //        }
        //    }
        //}
        public event PropertyChangedEventHandler? PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //} 해당함수가 ui를 자동으로 갱신
    }
}
