using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;
using VendingMachineApp.Services;

namespace VendingMachineApp.Utilities
{
    public class ChangeCalculater
    {
        private readonly VendingMachine vendingMachine;
        public ChangeCalculater(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        public List<Coin> GetChange(short balance)
        {
            List<Coin> change = new List<Coin>();
            while (balance != 0)
            {
                switch (balance)
                {
                    case >= (short)Coin.TWO_HUNDERED_CENTS when vendingMachine.coinInvertory.Has(Coin.TWO_HUNDERED_CENTS):
                        balance -= (short)Coin.TWO_HUNDERED_CENTS;
                        change.Add(Coin.TWO_HUNDERED_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.TWO_HUNDERED_CENTS);
                        break;
                    case >= (short)Coin.HUNDERED_CENTS when vendingMachine.coinInvertory.Has(Coin.HUNDERED_CENTS):
                        balance -= (short)Coin.HUNDERED_CENTS;
                        change.Add(Coin.HUNDERED_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.HUNDERED_CENTS);
                        break;
                    case >= (short)Coin.FIFTY_CENTS when vendingMachine.coinInvertory.Has(Coin.FIFTY_CENTS):
                        balance -= (short)Coin.FIFTY_CENTS;
                        change.Add(Coin.FIFTY_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.FIFTY_CENTS);
                        break;
                    case >= (short)Coin.TWENTY_CENTS when vendingMachine.coinInvertory.Has(Coin.TWENTY_CENTS):
                        balance -= (short)Coin.TWENTY_CENTS;
                        change.Add(Coin.TWENTY_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.TWENTY_CENTS);
                        break;
                    case >= (short)Coin.TEN_CENTS when vendingMachine.coinInvertory.Has(Coin.TEN_CENTS):
                        balance -= (short)Coin.TEN_CENTS;
                        change.Add(Coin.TEN_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.TEN_CENTS);
                        break;
                    case >= (short)Coin.FIVE_CENTS when vendingMachine.coinInvertory.Has(Coin.FIVE_CENTS):
                        balance -= (short)Coin.FIVE_CENTS;
                        change.Add(Coin.FIVE_CENTS);
                        vendingMachine.coinInvertory.Take(Coin.FIVE_CENTS);
                        break;
                }
                // SelectProduct Step catch this exception and does not let continue
                if (vendingMachine.coinInvertory.IsEmpty() && balance > 0)
                {
                    return new List<Coin>();
                }
            }
            return change;
        }

        public bool HasChange(short change)
        {
            List<Coin> coins = GetChange(change); //returning coins back to inventory          

            foreach (Coin c in coins)
            {
                vendingMachine.coinInvertory.Add(c);
            }

            return GetValueFromCoinList(coins) == change;

        }

        public string CalculateChange(List<Coin> coin)
        {
            var fiveCent = coin.Count(s => s == Coin.FIVE_CENTS);
            var tenCent = coin.Count(s => s == Coin.TEN_CENTS);
            var twentyCent = coin.Count(s => s == Coin.TWENTY_CENTS);
            var fiftyCent = coin.Count(s => s == Coin.FIFTY_CENTS);
            var hundredCent = coin.Count(s => s == Coin.HUNDERED_CENTS);
            var twoHundredCent = coin.Count(s => s == Coin.TWO_HUNDERED_CENTS);
            return
                   "\nFIVE CENT- " + fiveCent + "\nTEN CENT- " + tenCent + "\nTWENTY CENT- " + twentyCent +
                   "\nFIFTYCENT- " + fiftyCent + "\nHUNDREDCENT- " + hundredCent + "\nTWOHUNDREDCENT- " + twoHundredCent;
        }

        public short GetValueFromCoinList(List<Coin> coins) 
        {
            return coins.Aggregate<Coin, short>(0, (current, c) => (short)(current + (short)c));
        }
    }
}
