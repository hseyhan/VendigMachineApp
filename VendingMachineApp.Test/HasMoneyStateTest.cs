using System;
using System.Linq;
using VendingMachineApp.Constants;
using VendingMachineApp.Models;
using VendingMachineApp.Services;
using VendingMachineApp.Utilities;
using Xunit;

namespace VendingMachineApp.Test
{
    public class HasMoneyStateTest
    {
        public VendingMachine vendingMachine;
        public HasMoneyStateService hasMoneyStateService;
        public ChangeCalculater changeCalculator;

        public HasMoneyStateTest()
        {
            vendingMachine = new VendingMachine();
            hasMoneyStateService = new HasMoneyStateService(vendingMachine);
            changeCalculator = new ChangeCalculater(vendingMachine);
        }

        [Fact]
        public void InsertMoney_Input_InValid_Coin()
        {
            vendingMachine.SetState(hasMoneyStateService);
            //Current Balance
            short balance = 100;
            vendingMachine.balance = balance;

            //Insert Invalid Coin
            short insertedCoin = 2;
            vendingMachine.InsertMoney(insertedCoin);

            //Balance does not change
            Assert.NotEqual(balance + insertedCoin, vendingMachine.balance);
        }

        [Fact]
        public void InsertMoney_Input_100Cents_Successful_Operation()
        {
            //Current Balance
            short balance = 50;
            vendingMachine.balance = balance;
            vendingMachine.SetState(hasMoneyStateService);
            //Insert Coin
            short insertedCoin = 100;
            vendingMachine.InsertMoney(insertedCoin);
            //Current balance equals inserted coin + balance
            Assert.Equal(insertedCoin + balance, vendingMachine.balance);
        }

        [Fact]
        public void SelectProduct_CoinInserted_Successful_Operation()
        {
            //Current Balance
            short balance = 100;
            vendingMachine.balance = balance;
            vendingMachine.SetState(hasMoneyStateService);


            //Select Product
            var selectedProduct = Product.COLA;
            vendingMachine.SelectProduct(selectedProduct);

            Assert.Equal(selectedProduct, vendingMachine.currentItem);
        }

        [Fact]
        public void SelectProduct_CoinInserted_Not_Enough_Coin()
        {
            //Current Balance
            short balance = 50;
            vendingMachine.balance = balance;
            vendingMachine.SetState(hasMoneyStateService);

            //Select Product
            var selectedProduct = Product.COLA;
            vendingMachine.SelectProduct(selectedProduct);

            var expectedMessage = MessageConstant.HasMoneySelectProductNotEnoughCoin;

            //Not Enough Money Current Product Unknown, Selected Item Count does not change
            Assert.NotEqual(selectedProduct, vendingMachine.currentItem);
            Assert.Equal(Product.UNKNOWN,vendingMachine.currentItem);
            Assert.Equal(expectedMessage,vendingMachine.message);
        }

        [Fact]
        public void DispenseProduct_Not_Returned_Coin_Successful_Operation()
        {
            //Current Balance
            short initialBalance = 100;
            vendingMachine.balance = initialBalance;
            vendingMachine.SetState(hasMoneyStateService);

            //Select Product
            var selectedProduct = Product.COLA;
            vendingMachine.currentItem = selectedProduct;
            short price = vendingMachine.itemInvertory.GetPrice(selectedProduct);

            //Current Cola Count
            var initialCount = vendingMachine.itemInvertory.Count(selectedProduct);

            vendingMachine.DispanseProduct();

            var expectedType = typeof(NoMoneyStateService);
            var actualType = vendingMachine.vendingMachineState;

            //Balance equals product 
            Assert.Equal(initialCount -1, vendingMachine.itemInvertory.Count(selectedProduct));
            Assert.Equal(initialBalance - price, vendingMachine.balance);
            Assert.Empty(vendingMachine.change);
            Assert.IsType(expectedType,actualType);
        }

        [Fact]
        public void DispenseProduct_Returned_Coin_Successful_Operation()
        {
            //Current Balance
            short initialBalance = 100;
            vendingMachine.balance = initialBalance;
            vendingMachine.SetState(hasMoneyStateService);

            //Select Product
            var selectedProduct = Product.CHIPS;
            vendingMachine.currentItem = selectedProduct;
            short price = vendingMachine.itemInvertory.GetPrice(selectedProduct);

            //Current Cola Count
            var initialCount = vendingMachine.itemInvertory.Count(selectedProduct);

            vendingMachine.DispanseProduct();

            var expectedType = typeof(NoMoneyStateService);
            var actualType = vendingMachine.vendingMachineState;
            short change = vendingMachine.change.Aggregate<Coin, short>(0, (current, c) => (short)(current + (short)c));


            //Balance equals product 
            Assert.Equal(initialCount - 1, vendingMachine.itemInvertory.Count(selectedProduct));
            Assert.Equal(initialBalance - price, change);
            Assert.NotEmpty(vendingMachine.change);
            Assert.Equal(50,change);
            Assert.IsType(expectedType, actualType);
        }

        [Fact]
        public void Refund_Return_All_Coins_Successful_Operation()
        {
            //Current Balance
            vendingMachine.balance = 100;
            vendingMachine.SetState(hasMoneyStateService);
            //Calculate Change
            var change = changeCalculator.GetChange(vendingMachine.balance);

            vendingMachine.Refund();

            var expectedType = typeof(NoMoneyStateService);
            var actualType = vendingMachine.vendingMachineState;

           
            var expectedMessage = MessageConstant.HasMoneyRefund +
                                  changeCalculator.CalculateChange(change);

            //Refund all coin 
            Assert.Equal(0,vendingMachine.balance);
            Assert.IsType(expectedType,actualType);
            Assert.Equal(expectedMessage, vendingMachine.message);
        }

        public void ShowProduct_SuccessfulOperation()
        {
            vendingMachine.SetState(hasMoneyStateService);
            vendingMachine.ShowProducts();
            //??

        }
    }
}