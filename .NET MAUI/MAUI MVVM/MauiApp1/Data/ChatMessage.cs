using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    public class ChatMessage
    {
        public string? Sender { get; set; } // "user" 또는 "bot"
        public string? Text { get; set; }
        // sender 인자에 "sender" 라면 사용자
        // sender 인자에 bot이면 봇 

        // 각 메세지는 Text에 저장
        // ChatTemplateSelector 에서 데이터템플릿 적용
    }
}
