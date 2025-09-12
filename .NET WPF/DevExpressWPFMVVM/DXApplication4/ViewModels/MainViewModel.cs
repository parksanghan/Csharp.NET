using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm.CodeGenerators;
using DXApplication4.Infrastructure;
using DXApplication4.Model;

namespace DXApplication4.ViewModels
{
    [GenerateViewModel]
    public partial class MainViewModel
    {

        [GenerateProperty]
        ObservableCollection<Customer>? customers;

        [GenerateProperty]
        bool isInitialized;

        readonly IDataService dataService;
        public MainViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [GenerateCommand]
        async Task InitializeAsync()
        {
            Customers = new ObservableCollection<Customer>(await dataService.GetCustomersAsync());
            IsInitialized = true;
        }

        bool CanInitialize() => !IsInitialized;

        void OnUsernameChanged(string oldUsername) => InitializeAsyncCommand.RaiseCanExecuteChanged();
    }
}