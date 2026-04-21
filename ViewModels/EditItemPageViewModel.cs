using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOfManagingCollections.Models;

namespace SystemOfManagingCollections.ViewModels
{
    [QueryProperty(nameof(Item), "item")]
    internal partial class EditItemPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ItemModel item;


        [RelayCommand]
        public async void SetImage()
        {
            FileResult result = await FilePicker.Default.PickAsync();
            if (result == null) return;
            string extension = Path.GetExtension(result.FullPath);
            if (extension != ".png" && extension != ".jpg")
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Plik musi być w formacie .png lub .jpg", "Ok");
                return;
            }

            Item.ImagePath = result.FullPath;
        }
    }
}
