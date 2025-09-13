using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm.CodeGenerators;
using DXApplication4.Services;

namespace DXApplication4.ViewModels
{
    [GenerateViewModel]
    public partial class UsersViewModel
    {
        private readonly IUsersService _svc;
        [GenerateProperty] ObservableCollection<UserRow> users = new();
        [GenerateProperty] UserRow? selected;
        [GenerateProperty] bool isBusy;
        public UsersViewModel(IUsersService svc) => _svc = svc;

        [GenerateCommand]
        public async Task LoadAsync()
        {
            IsBusy = true;
            try
            {
                var list = await _svc.GetAllAsync();
                Users = new ObservableCollection<UserRow>();
                foreach (var x in list) Users.Add(new UserRow { User_Id = x.User_Id, Username = x.Username, Password = x.Password });
            }
            finally { IsBusy = false; }
        }
        [GenerateCommand]
        public async Task SaveAsync(UserRow row)
        {
            if (row.User_Id == 0)
                await _svc.InsertAsync(new UserDto(0, row.Username, row.Password));
            else
                await _svc.UpdateAsync(new UserDto(row.User_Id, row.Username, row.Password));
            await LoadAsync();
        }
        [GenerateCommand]
        public async Task DeleteAsync(UserRow row)
        {
            await _svc.DeleteAsync(row.User_Id);
            Users.Remove(row);
        }
    }
    public class UserRow { public int User_Id { get; set; } public string Username { get; set; } = ""; public string Password { get; set; } = ""; }
}