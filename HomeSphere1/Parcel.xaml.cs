using HomeSphere1.Services;
using HomeSphere1.ViewModels;


namespace HomeSphere1;

public partial class Parcel : ContentPage
{
	public Parcel()
	{
		InitializeComponent();
		this.BackgroundImageSource = "pr.png";
		var firestoreService = new FirestoreService();
        BindingContext = new ParcelViewModel(firestoreService);
	}

    internal static void Add(object item)
    {
        throw new NotImplementedException();
    }
}