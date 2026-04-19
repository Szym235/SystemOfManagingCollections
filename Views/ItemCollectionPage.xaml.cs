using SystemOfManagingCollections.ViewModels;

namespace SystemOfManagingCollections.Views;

public partial class ItemCollectionPage : ContentPage
{
	public ItemCollectionPage()
	{
		InitializeComponent();
		BindingContext = new ItemCollectionPageViewModel();
	}
}