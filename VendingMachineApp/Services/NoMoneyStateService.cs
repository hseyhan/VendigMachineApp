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
    public class NoMoneyStateService : IVendingMachineState
    {
        private VendingMachine vendingMachine;
        public NoMoneyStateService(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }
        public void InsertMoney(Coin coin)
        {
            if (Guard.IsValidCoin((short)coin))
            {
                vendingMachine.coinInvertory.Add(coin);
                vendingMachine.balance += (short)coin;
                vendingMachine.SetState(new HasMoneyStateService(vendingMachine));
            }
        }

        public void SelectProduct(Product product)
        {
            vendingMachine.message = MessageConstant.NoMoneySelectProduct;
            Console.WriteLine(vendingMachine.message);
        }

        public void DispenseProduct()
        {
            vendingMachine.message = MessageConstant.NoMoneyDispenseProduct;
            Console.WriteLine(vendingMachine.message);
        }

        public void ShowProducts()
        {
            var products = vendingMachine.itemInvertory.GetItems();

            foreach (var s in products)
            {
                var product = (Product)s.Key;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine((short)product + " " + product + " " + ConvertValueToDisplay.ConvertCoinValueToDisplay(vendingMachine.itemInvertory.GetPrice(product)) + " - " +
                                  (s.Value == 0 ? " Sold out" : s.Value + " Item Left"));
            }
        }

        public void Refund()
        {
            vendingMachine.message = MessageConstant.NoMoneyRefund;
            Console.WriteLine(vendingMachine.message);
        }
    }
}
