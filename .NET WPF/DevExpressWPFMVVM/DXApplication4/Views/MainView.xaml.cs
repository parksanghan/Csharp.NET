// 박상한 병장님,
using Microsoft.Data.SqlClient; // NuGet: Microsoft.Data.SqlClient
using Microsoft.Extensions.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace DXApplication4.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded; // 간단 스모크 테스트
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            try
            {
                var cs = DXApplication4.App.Configuration.GetConnectionString("PDB");
                using var con = new SqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new SqlCommand("SELECT 1", con);
                var x = (int)await cmd.ExecuteScalarAsync();
                MessageBox.Show(x == 1 ? "DB 연결 OK (SELECT 1)" : "DB 연결 실패");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"DB 오류: {ex.Message}");
            }
        }
    }
}
// 이상입니다 ! 필승 !
