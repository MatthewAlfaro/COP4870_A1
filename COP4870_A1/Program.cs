using COP4870_A1.Models;
using COP4870_A1.Services;
using System;

namespace Assignment1
{
    internal class Program
    {
        private static InventoryManagement inventoryManagement = InventoryManagement.Current;
        private static ShoppingService shoppingService = ShoppingService.Current;

        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("1. Manage Inventory");
                Console.WriteLine("2. Shop");
                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");
                string mainOption = Console.ReadLine();

                if (mainOption == "1")
                {
                    ManageInventory();
                }
                else if (mainOption == "2")
                {
                    ManageShopping();
                }
                else if (mainOption == "3")
                {
                    keepRunning = false; // Exit the loop to end the program
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        static void ManageInventory()
        {
            bool managing = true;
            while (managing)
            {
                Console.WriteLine("\nInventory Management Menu");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Update Item");
                Console.WriteLine("3. Delete Item");
                Console.WriteLine("4. Show Inventory");
                Console.WriteLine("5. Return to Main Menu");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    AddItem();
                }
                else if (option == "2")
                {
                    UpdateItem();
                }
                else if (option == "3")
                {
                    DeleteItem();
                }
                else if (option == "4")
                {
                    ShowInventory();
                }
                else if (option == "5")
                {
                    managing = false; // Return to the main menu
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        static void ManageShopping()
        {
            bool shopping = true;
            while (shopping)
            {
                Console.WriteLine("\nShopping Menu");
                Console.WriteLine("1. Add Item to Cart");
                Console.WriteLine("2. Remove Item from Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Return to Main Menu");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    AddItemToCart();
                }
                else if (option == "2")
                {
                    RemoveItemFromCart();
                }
                else if (option == "3")
                {
                    ViewCart();
                }
                else if (option == "4")
                {
                    Checkout();
                }
                else if (option == "5")
                {
                    shopping = false;
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        static void AddItem()
        {
            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            var item = inventoryManagement.AddItem(name, description, price, quantity);
            Console.WriteLine($"Item added successfully: {item}");
        }

        static void UpdateItem()
        {
            Console.Write("Enter Item ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter New Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter New Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            inventoryManagement.UpdateItem(id, name, description, price, quantity);
            Console.WriteLine("Item updated successfully.");
        }

        static void DeleteItem()
        {
            Console.Write("Enter the Item ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            inventoryManagement.DeleteItem(id);
            Console.WriteLine("Item deleted successfully if it existed.");
        }

        static void ShowInventory()
        {
            Console.WriteLine("\nCurrent Inventory:");
            var items = inventoryManagement.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void AddItemToCart()
        {
            Console.WriteLine("Available Inventory:");
            ShowInventory();

            Console.Write("Enter Item ID to add to cart: ");
            int itemId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity to add: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            shoppingService.AddToCart(itemId, quantity, inventoryManagement);
            Console.WriteLine("Item added to cart.");
            ViewCart();
        }

        static void RemoveItemFromCart()
        {
            Console.WriteLine("Current Cart:");
            ViewCart();

            Console.Write("Enter Item ID to remove from cart: ");
            int itemId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity to remove: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            shoppingService.RemoveFromCart(itemId, quantity, inventoryManagement);
            Console.WriteLine("Item removed from cart.");
            ViewCart();
        }

        static void ViewCart()
        {
            var cartItems = shoppingService.GetCartItems(inventoryManagement);
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                foreach (var pair in cartItems)
                {
                    Console.WriteLine($"{pair.Key.Name} - Quantity: {pair.Value}");
                }
            }
        }

        static void Checkout()
        {
            shoppingService.Checkout(inventoryManagement);
        }
    }
}
