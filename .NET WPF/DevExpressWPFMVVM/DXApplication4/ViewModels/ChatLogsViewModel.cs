using DXApplication4.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
 
using DevExpress.Mvvm.CodeGenerators;
using DXApplication4.Infrastructure;
using DXApplication4.Model;

namespace DXApplication4.ViewModels
{
    [GenerateViewModel]
    public partial class ChatLogsViewModel
    {
        private readonly IChatLogsService _svc;
        [GenerateProperty] ObservableCollection<ChatLogRow> items = new();
        [GenerateProperty] ChatLogRow? selected;
        [GenerateProperty] ObservableCollection<UserFilterDto> usersForFilter = new();
        [GenerateProperty] int? selectedUserId;
        [GenerateProperty] string? selectedLogType;
        [GenerateProperty] DateTime? from;
        [GenerateProperty] DateTime? to;
        [GenerateProperty] bool isBusy;
        public string[] LogTypes { get; } = new[] { "질의응답", "진단분석", "사용자설정" };
        public ChatLogsViewModel(IChatLogsService svc) => _svc = svc;

        [GenerateCommand]
        public async Task InitializeAsync()
        {
            UsersForFilter = new ObservableCollection<UserFilterDto>(await _svc.GetUsersForFilterAsync());
            await ApplyAsync();
        }
        [GenerateCommand]
        public async Task ApplyAsync()
        {
            IsBusy = true;
            try
            {
                var list = await _svc.QueryAsync(SelectedUserId, SelectedLogType, From, To);
                Items = new ObservableCollection<ChatLogRow>();
                foreach (var x in list) Items.Add(new ChatLogRow(x));
            }
            finally { IsBusy = false; }
        }
        [GenerateCommand] public async Task ResetAsync() { SelectedUserId = null; SelectedLogType = null; From = null; To = null; await ApplyAsync(); }
        [GenerateCommand] public async Task DeleteAsync(ChatLogRow row) { await _svc.DeleteAsync(row.Chat_Id); Items.Remove(row); }
    }
    public class ChatLogRow
    {
        public int Chat_Id { get; set; }
        public int? User_Id { get; set; }
        public string Username { get; set; } = ""
        ; public string Log_Type { get; set; } = "";
        public string? Message { get; set; }
        public string? Response { get; set; }
        public string? Diagnosis_Result { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message_Preview => (Message ?? "").Replace("\r\n", " ").Replace("\n", " ");
        public string Response_Preview => (Response ?? "").Replace("\r\n", " ").Replace("\n", " ");
        public ChatLogRow(ChatLogDto d) { Chat_Id = d.Chat_Id; User_Id = d.User_Id; Username = d.Username; Log_Type = d.Log_Type; Message = d.Message; Response = d.Response; Diagnosis_Result = d.Diagnosis_Result; Timestamp = d.Timestamp; }
    }
}
