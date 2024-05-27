using COP4870_A1.Models;
using System.Collections.ObjectModel;

namespace COP4870_A1.Services
{
    internal class ShoppingService
    {
        private Dictionary<int, int> cart;
        private static ShoppingService instance;
        private static readonly object lockObject = new object();

        private ShoppingService()
        {
            cart = new Dictionary<int, int>();
        }

        public static ShoppingService Current
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingService();
                    }
                    return instance;
                }
            }
        }

        public ReadOnlyCollection<KeyValuePair<Item, int>> GetCartItems(InventoryManagement inventory)
        {
            List<KeyValuePair<Item, int>> cartItems = new List<KeyValuePair<Item, int>>();
            foreach (var entry in cart)
            {
                Item item = inventory.GetAllItems().FirstOrDefault(i => i.ID == entry.Key);
                if (item != null)
                {
                    cartItems.Add(new KeyValuePair<Item, int>(item, entry.Value));
                }
            }
            return cartItems.AsReadOnly();
        }

        public void AddToCart(int itemId, int quantity, InventoryManagement inventory)
        {
            var item = inventory.GetAllItems().FirstOrDefault(i => i.ID == itemId);
            if (item != null && quantity > 0 && item.Quantity >= quantity)
            {
                if (cart.ContainsKey(itemId))
                {
                    cart[itemId] += quantity;
                }
                else
                {
                    cart[itemId] = quantity;
                }
                item.Quantity -= quantity;
            }
            else
            {
                Console.WriteLine("Invalid item ID, insufficient quantity in stock, or invalid quantity specified.");
            }
        }

        public void RemoveFromCart(int itemId, int quantity, InventoryManagement inventory)
        {
            if (cart.ContainsKey(itemId) && quantity > 0 && cart[itemId] >= quantity)
            {
                cart[itemId] -= quantity;
                if (cart[itemId] == 0)
                {
                    cart.Remove(itemId);
                }
                var item = inventory.GetAllItems().FirstOrDefault(i => i.ID == itemId);
                if (item != null)
                {
                    item.Quantity += quantity;
                }
            }
            else
            {
                Console.WriteLine("Invalid item ID, insufficient quantity in stock, or invalid quantity specified.");
            }
        }

        public void Checkout(InventoryManagement inventory)
        {
            double subtotal = 0;
            Console.WriteLine("Checking out the following items:");
            foreach (var entry in cart)
            {
                var item = inventory.GetAllItems().FirstOrDefault(i => i.ID == entry.Key);
                if (item != null)
                {
                    double totalItemPrice = item.Price * entry.Value;
                    Console.WriteLine($"{item.Name} - {entry.Value} x ${item.Price} = ${totalItemPrice}");
                    subtotal += totalItemPrice;
                }
            }
            double tax = subtotal * 0.07;
            double total = subtotal + tax;
            Console.WriteLine($"Subtotal: ${subtotal}, Tax: ${tax}, Total: ${total}");

            cart.Clear();
        }
    }
}
