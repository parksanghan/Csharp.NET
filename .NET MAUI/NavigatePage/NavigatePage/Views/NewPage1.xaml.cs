namespace NavigatePage.Views;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}
	private async Task RequestCameraPermissionAsync()
	{
		var status=await Permissions.RequestAsync<Permissions.Camera>();
		if (status != PermissionStatus.Granted)
		{
			await DisplayAlert("���� �ʿ�", "������ �ʿ��մϴ�,", "Ȯ��");
        }
		else
		{
		 
		}
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await RequestCameraPermissionAsync();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		OnAppearing();
    }
}