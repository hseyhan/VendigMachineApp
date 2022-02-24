using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Constants;
using VendingMachineApp.Models;
using VendingMachineApp.Services;
using Xunit;

namespace VendingMachineApp.Test
{
    public class NoMoneyStateTest
    {
        public VendingMachine vendingMachine;
        public NoMoneyStateTest()
        {
            vendingMachine = new VendingMachine();
        }
        [Fact]
        public void InsertMoney_Input_InValid_Coin()
        {
            //Vending machine initialize with NoMoneyState
            var expectedType = typeof(NoMoneyStateService);
            var actualType = vendingMachine.vendingMachineState;

            //Insert Invalid Coin
            short insertedCoin = 2;
            vendingMachine.InsertMoney(insertedCoin);

            //Balance does not change
            Assert.NotEqual(insertedCoin, vendingMachine.balance);
            Assert.IsType(expectedType, actualType);
        }

        [Fact]
        public void InsertMoney_Input_100Cents_Successful_Operation()
        {
            //Vending machine initialize with NoMoneyState

            short insertedCoin = 100;
            vendingMachine.InsertMoney(insertedCoin);

            var expectedType = typeof(HasMoneyStateService);
            var actualType = vendingMachine.vendingMachineState;

            //Current balance equals inserted coin + balance
            Assert.Equal(insertedCoin, vendingMachine.balance);
            Assert.IsType(expectedType, actualType);
        }

        [Fact]
        public void SelectProduct_Not_Inserted_Coin()
        {
            //Vending machine initialize with NoMoneyState

            //Select Product
            var selectedProduct = Product.COLA;
            vendingMachine.SelectProduct(selectedProduct);

            var expectedMessage = MessageConstant.NoMoneySelectProduct;

            Assert.NotEqual(selectedProduct, vendingMachine.currentItem);
            Assert.Equal(Product.UNKNOWN,vendingMachine.currentItem);
            Assert.Equal(expectedMessage,vendingMachine.message);
        }

        [Fact]
        public void DispenseProduct_Not_Inserted_Coin()
        {
            //Vending machine initialize with NoMoneyState

            //Select Product
            var selectedProduct = Product.COLA;
            vendingMachine.currentItem = selectedProduct;
            //Dispense Product
            vendingMachine.DispanseProduct();

            //Initial count
            var initialCount = vendingMachine.itemInvertory.Count(selectedProduct);


            var expectedMessage = MessageConstant.NoMoneyDispenseProduct;

            Assert.Equal(expectedMessage, vendingMachine.message);
            Assert.NotEqual(initialCount -1 , vendingMachine.itemInvertory.Count(selectedProduct));
        }

        [Fact]
        public void Refund_Not_Inserted_Coin()
        {
            //Vending machine initialize with NoMoneyState

            //Dispense Product
            vendingMachine.Refund();

            //Message
            var expectedMessage = MessageConstant.NoMoneyRefund;

            Assert.Equal(expectedMessage, vendingMachine.message);
           
        }
    }
}
