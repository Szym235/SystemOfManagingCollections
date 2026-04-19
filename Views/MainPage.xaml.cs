using Microsoft.Maui.Controls;
using System.Diagnostics;
using SystemOfManagingCollections.ViewModels;

namespace SystemOfManagingCollections.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            Debug.WriteLine(BindingContext.GetType());
        }
    }
}
