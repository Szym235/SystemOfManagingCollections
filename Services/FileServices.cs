using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOfManagingCollections.Models;

namespace SystemOfManagingCollections.Services
{
    internal class FileServices
    {

        public static bool saveItemCollection(ItemCollectionModel itemCollection, string path)
        {
            if(itemCollection == null) return false;

            StreamWriter writer = new StreamWriter(
                Path.Combine(path, itemCollection.Name + ".txt")
            );

            Debug.WriteLine("Saving in: " + Path.Combine(path, itemCollection.Name + ".txt"));

            foreach (ItemModel item in itemCollection.Items)
            {
                writer.WriteLine(item.Name + "\U0001f920" + 
                                item.Description + "\U0001f920" + 
                                item.ImagePath + "\U0001f920" + 
                                item.Price + "\U0001f920" + 
                                item.Status + "\U0001f920" + 
                                item.OwnerRating + "\U0001f920" + 
                                item.Comment + "\U0001f920" + 
                                item.Dimensions);
            }

            writer.Close();
            Debug.WriteLine("Collection " + itemCollection.Name + " saved");
            return true;
        }

        public static void loadAllItemsCollections()
        {
            AllItemCollectionsModel allItemCollections = AllItemCollectionsModel.Instance;
            String[] itemCollectionNames = Directory.GetFiles(FileSystem.AppDataDirectory);
            Debug.WriteLine("Loading from: " + FileSystem.AppDataDirectory);

            foreach (String itemCollectionName in itemCollectionNames)
            {
                if (itemCollectionName.EndsWith(".txt"))
                {
                    allItemCollections.ItemCollections.Add(loadItemCollection(FileSystem.AppDataDirectory, itemCollectionName));
                }
            }
        }

        public static ItemCollectionModel loadItemCollection(string folderPath, string itemCollectionName)
        {
            StreamReader reader = new StreamReader(Path.Combine(folderPath, itemCollectionName));
            ItemCollectionModel newItemCollection = new ItemCollectionModel(Path.GetFileNameWithoutExtension(itemCollectionName));

            string line = reader.ReadLine();
            while (line != null)
            {
                String[] itemData = line.Split("\U0001f920");
                if (!int.TryParse(itemData[5], out int ownerRating)) ownerRating = 0;
                ItemModel item = new ItemModel(
                    itemData[0],
                    itemData[1],
                    itemData[2],
                    itemData[3],
                    itemData[4],
                    ownerRating,
                    itemData[6],
                    itemData[7]
                );
                newItemCollection.addItem(item);
                line = reader.ReadLine();
            }

            reader.Close();
            Debug.WriteLine("Collection " + newItemCollection.Name + " loaded");
            return newItemCollection;
        }

        public static void deleteSaveFile(string name)
        {
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, name + ".txt"));
        }
    }
}
