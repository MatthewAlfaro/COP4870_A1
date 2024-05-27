using COP4870_A1.Models;
using System.Collections.ObjectModel;

namespace COP4870_A1.Services
{
    internal class InventoryManagement
    {
        private List<Item> items;
        private static InventoryManagement instance;
        private static readonly object lockObject = new object();

        private InventoryManagement()
        {
            items = new List<Item>();
        }

        public static InventoryManagement Current
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new InventoryManagement();
                    }
                    return instance;
                }
            }
        }

        public ReadOnlyCollection<Item> GetAllItems()
        {
            return items.AsReadOnly();
        }

        public Item AddItem(string name, string description, double price, int quantity)
        {
            var newItem = new Item
            {
                ID = items.Any() ? items.Max(i => i.ID) + 1 : 1,
                Name = name,
                Description = description,
                Price = price,
                Quantity = quantity
            };
            items.Add(newItem);
            return newItem;
        }

        public void UpdateItem(int id, string name, string description, double price, int quantity)
        {
            var item = items.FirstOrDefault(i => i.ID == id);
            if (item != null)
            {
                item.Name = name;
                item.Description = description;
                item.Price = price;
                item.Quantity = quantity;
            }
        }

        public void DeleteItem(int id)
        {
            items.RemoveAll(i => i.ID == id);
        }
    }

}
