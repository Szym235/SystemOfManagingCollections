using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOfManagingCollections.Models;
using SystemOfManagingCollections.Services;
using SystemOfManagingCollections.Views;

namespace SystemOfManagingCollections.ViewModels
{
    [QueryProperty(nameof(ItemCollection), "itemCollection")]
    internal partial class ItemCollectionPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ItemCollectionModel itemCollection;
        [ObservableProperty]
        string newItemName = string.Empty;

        public ItemCollectionPageViewModel()
        {
            Debug.WriteLine(ItemCollection == null);
        }

        [RelayCommand]
        public async void AddItem()
        {
            if (NewItemName.Replace(" ", "") == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nazwa przedmiotu nie może być pusta", "Ok");
                return;
            }
            if (ItemCollection.checkIfItemNameExistsInCollection(NewItemName))
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Powielenie nazwy", "Przedmiot o takiej nazwie znajduje się już w kolekcji, czy pomimo tego chcesz go dodać?", "Tak", "Nie");
                if (answer == false) return;
            }
            ItemModel newItem = new ItemModel(NewItemName);
            ItemCollection.addItem(newItem);
            FileServices.saveItemCollection(ItemCollection, FileSystem.AppDataDirectory);
            NewItemName = string.Empty;
        }

        [RelayCommand]
        public void DeleteItem(ItemModel item)
        {
            ItemCollection.deleteItem(item);
            FileServices.saveItemCollection(ItemCollection, FileSystem.AppDataDirectory);
        }

        [RelayCommand]
        public async Task OpenEditItemPage(ItemModel item)
        {
            Debug.WriteLine("Odpalam item");
            await Shell.Current.GoToAsync(nameof(EditItemPage), new Dictionary<string, object>
            {
                ["item"] = item
            });
        }
    }
}
