using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOfManagingCollections.Models
{
    internal partial class ItemModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string imagePath;
        [ObservableProperty]
        string dimensions;
        [ObservableProperty]
        string price;
        [ObservableProperty]
        string status;
        //new, used, for sale, sold, want to buy, ...
        [ObservableProperty]
        int ownerRating;
        //1-10
        [ObservableProperty]
        string comment;

        public ItemModel(string name)
        {
            this.name = name;
        }
        public ItemModel(string name, string description, string imagePath, string price, string status, int ownerRating, string comment, string dimensions)
        {
            this.name = name;
            this.description = description;
            this.imagePath = imagePath;
            this.price = price;
            this.status = status;
            this.ownerRating = ownerRating;
            this.comment = comment;
            this.dimensions = dimensions;
        }
    }
}
