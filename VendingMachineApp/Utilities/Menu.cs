using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Constants;
using VendingMachineApp.Models;
using VendingMachineApp.Services;

namespace VendingMachineApp.Utilities
{
    public class Menu
    {
        public List<Product> purchasedItems = new List<Product>();

        public void RunInterface(VendingMachine vendingMachine)
        {
            bool finished = false;
            Console.WriteLine("Press 1 to view items, or 2 to make a purchase:");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    vendingMachine.ShowProducts();
                    break;
                case "2":
                    while (finished == false)
                    {
                        finished = PurchaseMenu(vendingMachine);
                    }
                    break;
                default:
                    vendingMachine.message = MessageConstant.MenuInputNotRecognized;
                    Console.WriteLine(vendingMachine.message);
                    break;
            }
        }

        public bool PurchaseMenu(VendingMachine vendingMachine)
        {
            bool selectionMade = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Press 1 to insert money, 2 to select a product, or 0 to cancel transaction:");
            Console.WriteLine($"Current money provided: {ConvertValueToDisplay.ConvertCoinValueToDisplay(vendingMachine.balance)}");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    vendingMachine.Refund();
                    return true;
                case "1":
                    UserFeedCoin(vendingMachine);
                    return false;
                case "2":
                    if (vendingMachine.balance > 0)
                    {
                        while (selectionMade == false)
                        {
                            selectionMade = UserSelectProduct(vendingMachine);
                        }
                    }
                    else
                    {
                        vendingMachine.message = MessageConstant.MenuSelectProductWithoutInsertedCoin;
                        Console.WriteLine(vendingMachine.message);
                        Console.WriteLine();
                    }
                    return false;
                default:
                    vendingMachine.message = MessageConstant.MenuInputNotRecognized;
                    Console.WriteLine(vendingMachine.message);
                    return false;
            }
        }

        public void UserFeedCoin(VendingMachine vendingMachine)
        {
            Console.WriteLine("Please insert cents into the machine (100cents for €1):");
            string userInput = Console.ReadLine();

            if (Guard.ForNullOrEmpty(userInput, "insertedCoin")) return;
            var coin = Guard.IsConvertToShort(userInput, "insertedCoin");

            Console.WriteLine($"Inserted {ConvertValueToDisplay.ConvertCoinValueToDisplay(coin.convertedValue)}");
            vendingMachine.InsertMoney(coin.convertedValue);
        }

        public bool UserSelectProduct(VendingMachine vendingMachine)
        {
            Console.WriteLine("Please enter a product code. Press 9 to show all items or 0 to go back:");
            string userInput = Console.ReadLine()?.ToUpper();

            if (Guard.ForNullOrEmpty(userInput, "user select product"))
                return false;

            var input = Guard.IsConvertToShort(userInput, "user select product");
            if (!input.isSuccess)
                return false;

            switch (input.convertedValue)
            {
                case 0:
                    return true;
                case 9:
                    vendingMachine.ShowProducts();
                    return false;
                default:
                    {
                        if (!vendingMachine.itemInvertory.GetItems().ContainsKey((Product)input.convertedValue))
                        {
                            vendingMachine.message = MessageConstant.MenuSelectProductNotMacthItem;
                            Console.WriteLine(vendingMachine.message);
                            Console.WriteLine();
                            return false;
                        }
                        else
                        {
                            vendingMachine.SelectProduct((Product)input.convertedValue);
                            vendingMachine.DispanseProduct();
                            return true;
                        }
                    }
            }
        }

    }
}
