using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOfManagingCollections.Models
{
    internal partial class AllItemCollectionsModel : ObservableObject
    {
        public static AllItemCollectionsModel Instance { get; } = new AllItemCollectionsModel();

        [ObservableProperty]
        public ObservableCollection<ItemCollectionModel> itemCollections;

        private AllItemCollectionsModel()
        {
            itemCollections = new ObservableCollection<ItemCollectionModel>();
        }

        public ItemCollectionModel findCollectionByName(string name)
        {
            foreach (ItemCollectionModel collection in ItemCollections)
            {
                if (collection.Name == name)
                {
                    return collection;
                }
            }
            return null;
        }
    }
}
