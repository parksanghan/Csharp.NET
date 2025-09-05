using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Data; // 👈 이거 꼭 있어야 함!
namespace MauiApp1.Data
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? UserTemplate { get; set; }  // 우측 유저 데이터 템플릿

        public DataTemplate? BotTemplate { get; set; }  // 좌측 봇 데이터 템플릿
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as ChatMessage;
            DataTemplate? datatemp = null;
            if (message != null && message.Sender == "user")
            {
                datatemp = UserTemplate;
            }
            else if (message != null && message.Sender == "bot")
            {
               datatemp = BotTemplate;  
            }
            return datatemp;

             
        }
    }
}
