using HomeSphere1.Services;
using HomeSphere1.ViewModels;
namespace HomeSphere1;

public partial class Repair : ContentPage
{
	public Repair()
	{
		InitializeComponent();
		this.BackgroundImageSource = "pr.png";
		var firestoreService = new FirestoreService();
		BindingContext = new RepairViewModel(firestoreService);
	}

}