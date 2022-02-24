using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;
using VendingMachineApp.Services;
using VendingMachineApp.Utilities;
using Xunit;

namespace VendingMachineApp.Test
{
    public class ChangeCalculaterTest
    {
        public VendingMachine vendingMachine;
        public ChangeCalculater changeCalculater;
        public ChangeCalculaterTest()
        {
            vendingMachine = new VendingMachine();
            changeCalculater = new ChangeCalculater(vendingMachine);
        }
        [Fact]
        public void GetChange_Returns_Correct_Change()
        {
            short change = (short)Coin.HUNDERED_CENTS;
            var coins = changeCalculater.GetChange(change);
            var returnedChange = coins.Aggregate<Coin, short>(0, (current, c) => (short)(current + (short)c));

            Assert.Equal(change, returnedChange);
        }

        [Fact]
        public void HasChange_Returns_True() 
        {
            //Initialize coin inventory;
            vendingMachine.coinInvertory.RemoveAllCoin();

            vendingMachine.coinInvertory.Add(Coin.FIVE_CENTS,1);
            vendingMachine.coinInvertory.Add(Coin.TEN_CENTS,1);
            vendingMachine.coinInvertory.Add(Coin.TWENTY_CENTS,1);

            var coins = changeCalculater.HasChange(35);
            Assert.True(coins);
            
        }

        [Fact]
        public void HasChange_Returns_False()
        {
            //Initialize coin inventory;
            vendingMachine.coinInvertory.RemoveAllCoin();
            short change = 35;

            vendingMachine.coinInvertory.Add(Coin.FIVE_CENTS, 0);
            vendingMachine.coinInvertory.Add(Coin.TEN_CENTS, 0);
            vendingMachine.coinInvertory.Add(Coin.TWENTY_CENTS, 0);

            var coins = changeCalculater.HasChange(change);
            Assert.False(coins);

        }

        [Fact]
        public void GetValueFromCoinList_Successful_Operation() 
        {
            var coins = new List<Coin>();
            coins.Add(Coin.FIFTY_CENTS);
            coins.Add(Coin.TEN_CENTS);

            var expectedValue = (short)Coin.FIFTY_CENTS + (short)Coin.TEN_CENTS;

            var actualValue = changeCalculater.GetValueFromCoinList(coins);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
