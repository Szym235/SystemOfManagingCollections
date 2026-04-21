using SystemOfManagingCollections.ViewModels;

namespace SystemOfManagingCollections.Views;

public partial class EditItemPage : ContentPage
{
	public EditItemPage()
	{
		InitializeComponent();
        BindingContext = new EditItemPageViewModel();
    }
}