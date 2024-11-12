namespace HomeSphere1;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
		this.BackgroundImageSource = "home.png";
	}
	private void OnRepairClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Repair());
	}
	private void OnParcelClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Parcel());
	}
}