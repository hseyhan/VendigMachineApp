using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Infrastructure;
using VendingMachineApp.Interfaces;
using VendingMachineApp.Models;

namespace VendingMachineApp.Services
{
    public class VendingMachine
    {
        public IVendingMachineState vendingMachineState { get; set; }
        public ProductInventory itemInvertory = new ProductInventory();
        public CoinInventory coinInvertory = new CoinInventory();
        public Product currentItem;
        public short balance;
        public string message;
        public short moneyPaid { get; set; }

        public List<Coin> change = new List<Coin>();
        public VendingMachine()
        {
            vendingMachineState = new NoMoneyStateService(this);
            Initialize();
        }

        public void InsertMoney(short coin)
        {
            vendingMachineState.InsertMoney((Coin)coin);
        }
        public void SelectProduct(Product product)
        {
            vendingMachineState.SelectProduct(product);
        }
        public void DispanseProduct()
        {
            if (currentItem == Product.UNKNOWN)
                Refund();
            else
                vendingMachineState.DispenseProduct();
        }

        public void ShowProducts()
        {
            vendingMachineState.ShowProducts();
        }
        public void SetState(IVendingMachineState newState)
        {
            vendingMachineState = newState;
        }

        public void Refund()
        {
            vendingMachineState.Refund();
        }
        private void Initialize()
        {
            LoadItems();
            LoadCoins();
        }

        private void LoadItems()
        {
            itemInvertory.Insert(Product.COLA, 15);
            itemInvertory.Insert(Product.CHIPS, 10);
            itemInvertory.Insert(Product.CANDY, 20);
        }

        private void LoadCoins()
        {
            coinInvertory.Add(Coin.FIVE_CENTS, 100);
            coinInvertory.Add(Coin.TEN_CENTS, 100);
            coinInvertory.Add(Coin.TWENTY_CENTS, 100);
            coinInvertory.Add(Coin.FIFTY_CENTS, 100);
            coinInvertory.Add(Coin.HUNDERED_CENTS, 100);
            coinInvertory.Add(Coin.TWO_HUNDERED_CENTS, 100);
        }


    }
}
