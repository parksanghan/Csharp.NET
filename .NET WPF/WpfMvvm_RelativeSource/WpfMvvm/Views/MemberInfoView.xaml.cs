using System.Windows;
using System.Windows.Controls;
using WpfMvvm.Models;

namespace WpfMvvm.Views
{
    /// <summary>
    /// UserInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MemberInfoView : UserControl
    {
        public static readonly DependencyProperty MemberDataProperty = DependencyProperty.Register( "MemberData",
                                                                                                    typeof( MemberInfoModel ),
                                                                                                    typeof( MemberInfoView ),
                                                                                                    new PropertyMetadata( null ) );

        public MemberInfoModel MemberData
        {
            set => SetValue( MemberDataProperty, value );
            get => ( MemberInfoModel )GetValue( MemberDataProperty );
        }

        public MemberInfoView()
        {
            InitializeComponent();
        }
    }
}

