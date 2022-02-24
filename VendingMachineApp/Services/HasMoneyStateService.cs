using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Constants;
using VendingMachineApp.Interfaces;
using VendingMachineApp.Models;
using VendingMachineApp.Utilities;

namespace VendingMachineApp.Services
{
    public class HasMoneyStateService : IVendingMachineState
    {
        private VendingMachine vendingMachine;
        private ChangeCalculater changeCalculater;
        public HasMoneyStateService(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
            changeCalculater = new ChangeCalculater(vendingMachine);
        }
        public void InsertMoney(Coin coin)
        {
            if (Guard.IsValidCoin((short)coin))
            {
                vendingMachine.coinInvertory.Add(coin);
                vendingMachine.balance += (short)coin;
            }
          
        }

        public void SelectProduct(Product product)
        {
            vendingMachine.currentItem = product;
            if (vendingMachine.itemInvertory.Count(product) >= 1)
            {
                if (vendingMachine.balance >= vendingMachine.itemInvertory.GetPrice(vendingMachine.currentItem))
                {
                    if (changeCalculater.HasChange((short)(vendingMachine.balance - vendingMachine.itemInvertory.GetPrice(vendingMachine.currentItem))))
                    {
                        vendingMachine.currentItem = product;
                    }
                    else
                    {
                        vendingMachine.currentItem = Product.UNKNOWN;
                        vendingMachine.message = MessageConstant.HasMoneySelectProductDoesNotHaveChange;
                        Console.WriteLine(vendingMachine.message);
                        Refund();
                    }
                }
                else
                {
                    vendingMachine.currentItem = Product.UNKNOWN;
                    vendingMachine.message = MessageConstant.HasMoneySelectProductNotEnoughCoin;
                    Console.WriteLine(vendingMachine.message);
                }
            }
            else
            {
                vendingMachine.currentItem = Product.UNKNOWN;
                vendingMachine.message = MessageConstant.HasMoneySelectProductSoldOut;
                Console.WriteLine(vendingMachine.message);
            }
        }

        public void DispenseProduct()
        {
            vendingMachine.balance = (short)(vendingMachine.balance - vendingMachine.itemInvertory.GetPrice(vendingMachine.currentItem));
            vendingMachine.itemInvertory.Dispense(vendingMachine.currentItem);
            vendingMachine.change = changeCalculater.GetChange(vendingMachine.balance);
            vendingMachine.balance = 0;
            vendingMachine.SetState(new NoMoneyStateService(vendingMachine));
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("|                                                |");
            Console.WriteLine("|                                                |");
            Console.WriteLine("THANK YOU!" + "\nProduct: " + vendingMachine.currentItem
                              + " \nChange: " + changeCalculater.CalculateChange(vendingMachine.change));
            Console.WriteLine("|                                                |");
            Console.WriteLine("|                                                |");
            Console.WriteLine("-------------------------------------------------");

        }

        public void ShowProducts()
        {
            var products = vendingMachine.itemInvertory.GetItems();

            foreach (var s in products)
            {
                var product = (Product)s.Key;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine((short)product + " " + product + " " + ConvertValueToDisplay.ConvertCoinValueToDisplay(vendingMachine.itemInvertory.GetPrice(product)) + "€ - " +
                                  (s.Value == 0 ? " Sold out" : s.Value + " Item Left"));
            }
        }

        public void Refund()
        {
            var returnMoney = changeCalculater.GetChange(vendingMachine.balance);
            vendingMachine.balance = 0;
            vendingMachine.message = MessageConstant.HasMoneyRefund + changeCalculater.CalculateChange(returnMoney);
            Console.WriteLine(vendingMachine.message);
            vendingMachine.SetState(new NoMoneyStateService(vendingMachine));
        }

    }
}
