namespace HomeSphere1;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
		this.BackgroundImageSource = "home.png";
	}
	private void OnLoginClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Home());
	}
}