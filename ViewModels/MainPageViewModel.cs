using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using SystemOfManagingCollections.Models;
using SystemOfManagingCollections.Services;
using SystemOfManagingCollections.Views;

namespace SystemOfManagingCollections.ViewModels
{

    internal partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public AllItemCollectionsModel allItemCollections;
        [ObservableProperty]
        public string newItemCollectionName = string.Empty;
        public MainPageViewModel()
        {
            allItemCollections = AllItemCollectionsModel.Instance;
            FileServices.loadAllItemsCollections();
        }

        [RelayCommand]
        public async Task OpenItemCollectionPage(ItemCollectionModel itemCollection)
        {
            Debug.WriteLine("Odpalam kolekcję");
            await Shell.Current.GoToAsync(nameof(ItemCollectionPage), new Dictionary<string, object>
            {
                ["itemCollection"] = itemCollection
            });
        }

        [RelayCommand]
        public void AddItemCollection()
        {
            if(NewItemCollectionName.Replace(" ", "") == string.Empty || AllItemCollections.findCollectionByName(NewItemCollectionName) != null)
            {
                Application.Current.MainPage.DisplayAlert("Błąd", "Nazwa kolekcji nie może być pusta, ani się powtarzać", "Ok");
                return;
            }
            ItemCollectionModel newItemCollection = new ItemCollectionModel(NewItemCollectionName);
            AllItemCollections.ItemCollections.Add(newItemCollection);
            FileServices.saveItemCollection(newItemCollection, FileSystem.AppDataDirectory);
            NewItemCollectionName = string.Empty;
        }

        [RelayCommand]
        public async Task ExportItemCollection(ItemCollectionModel itemCollection)
        {
            FolderPickerResult result = await FolderPicker.Default.PickAsync();
            if (result.Folder != null)
            {
                Debug.WriteLine("Zaczęto eksport do " + result.Folder.Path);
                FileServices.saveItemCollection(itemCollection, result.Folder.Path);
            }
            else
            {
                Debug.WriteLine("Brak wybranego folderu, nie rozpoczęto eksportu");
            }
        }

        [RelayCommand]
        public async Task ImportItemCollection()
        {
            FileResult result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (Path.GetExtension(result.FullPath) != ".txt")
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Plik nie jest w formacie .txt", "Ok");
                    return;
                }
                Debug.WriteLine("Zaczęto import z " + result.FullPath);
                ItemCollectionModel newItemCollection = FileServices.loadItemCollection(Path.GetDirectoryName(result.FullPath), Path.GetFileName(result.FullPath));

                ItemCollectionModel existingItemCollection = AllItemCollections.findCollectionByName(newItemCollection.Name);
                if (existingItemCollection != null)
                {
                    ItemCollectionModel.mergeCollections(existingItemCollection, newItemCollection);
                    existingItemCollection.SortByStatus();
                    FileServices.saveItemCollection(existingItemCollection, FileSystem.AppDataDirectory);
                    Application.Current.MainPage.DisplayAlert("Import", "Scalono importowaną kolekcję z istniejącą", "Ok");
                }
                else
                {
                    AllItemCollections.ItemCollections.Add(newItemCollection);
                    Application.Current.MainPage.DisplayAlert("Import", "Dodano nową kolekcję", "Ok");
                }
            }
            else
            {
                Debug.WriteLine("Brak wybranego pliku, nie rozpoczęto importu");
            }
        }

        [RelayCommand]

        public void DeleteItemCollection(ItemCollectionModel itemCollection)
        {
            AllItemCollections.ItemCollections.Remove(itemCollection);
            FileServices.deleteSaveFile(itemCollection.Name);
        }
    }
}
