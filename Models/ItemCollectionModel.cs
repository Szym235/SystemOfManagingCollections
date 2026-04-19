using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOfManagingCollections.Services;

namespace SystemOfManagingCollections.Models
{
    internal partial class ItemCollectionModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        private ObservableCollection<ItemModel> items;

        public ItemCollectionModel(string name)
        {
            this.name = name;
            this.items = new ObservableCollection<ItemModel>();
        }

        public ItemCollectionModel(string name, ObservableCollection<ItemModel> items)
        {
            this.name = name;
            this.items = items;
        }

        public void SortByStatus()
        {
            Items = Items.OrderBy(i => i.Status == "Sprzedane").ToObservableCollection();
        }

        public void addItem(ItemModel item)
        {
            Items.Add(item);

            item.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(ItemModel.Status)) SortByStatus();
                FileServices.saveItemCollection(this, FileSystem.AppDataDirectory);
            };
        }

        public void deleteItem(ItemModel item)
        {
            Items.Remove(item);
            FileServices.saveItemCollection(this, FileSystem.AppDataDirectory);
        }

        public bool checkIfItemNameExistsInCollection(String name)
        {
            String trimmedName = name.Replace(" ", "").ToLower();
            foreach(ItemModel item in Items)
            {
                if(item.Name.Replace(" ", "").ToLower() == trimmedName)
                {
                    return true;
                }
            }
            return false;
        }

        public static void mergeCollections(ItemCollectionModel existingItemCollection, ItemCollectionModel newItemCollection)
        {
            foreach (ItemModel item in newItemCollection.Items)
            {
                existingItemCollection.addItem(item);
            }
        }
    }
}
