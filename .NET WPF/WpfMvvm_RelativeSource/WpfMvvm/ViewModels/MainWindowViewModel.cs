using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMvvm.Models;

namespace WpfMvvm.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public string SearchID
        {
            get => _searchID;
            set => SetProperty( ref _searchID, value );
        }
        private string _searchID;

        public MemberInfoModel MemberInfo
        {
            get => _memberInfo;
            set => SetProperty( ref _memberInfo, value );
        }
        private MemberInfoModel _memberInfo;

        public ICommand MainWindowCommand { get; }

        public MainWindowViewModel()
        {
            MainWindowCommand = new RelayCommand<string>( execute, canExecute );
        }

        private void execute( string paramerter )
        {
            switch ( paramerter )
            {
                case "Search":
                    if ( string.IsNullOrWhiteSpace( SearchID ) )
                    {
                        MessageBox.Show( "ID를 입력하세요." );
                    }
                    else
                    {
                        searchById( SearchID );
                    }
                    break;
            }
        }
        private bool canExecute( string parameter )
        {
            if ( parameter != null )
            {
                switch ( parameter )
                {
                    case "Search":
                        return true;
                }
            }

            return false;
        }

        private void searchById( string id )
        {
            var result = DataManager.FindMemberInfo( id );

            MemberInfo = result;
        }
    }
}
