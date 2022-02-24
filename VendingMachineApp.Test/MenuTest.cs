using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Constants;
using VendingMachineApp.Services;
using VendingMachineApp.Utilities;
using Xunit;

namespace VendingMachineApp.Test
{
    public class MenuTest
    {
        public VendingMachine vendingMachine;
        public Menu menu;

        public MenuTest()
        {
            vendingMachine = new VendingMachine();
            menu = new Menu();
        }

        [Fact]
        public void RunInterface_Invalid_Input()
        {
            var input = new StringReader("3");
            Console.SetIn(input);
            menu.RunInterface(vendingMachine);

            var expectedMessage = MessageConstant.MenuInputNotRecognized;
            Assert.Equal(expectedMessage,vendingMachine.message);
        }

        [Fact]
        public void PurchaseMenu_Invalid_Input()
        {
            var input = new StringReader("3");
            Console.SetIn(input);
            menu.PurchaseMenu(vendingMachine);

            var expectedMessage = MessageConstant.MenuInputNotRecognized;
            Assert.Equal(expectedMessage, vendingMachine.message);
        }

        [Fact]
        public void UserSelectProduct_Invalid_Input()
        {
            var input = new StringReader("5");
            Console.SetIn(input);
            menu.UserSelectProduct(vendingMachine);

            var expectedMessage = MessageConstant.MenuSelectProductNotMacthItem;
            Assert.Equal(expectedMessage, vendingMachine.message);
        }

        [Fact]
        public void UserFeedCoin_Successful_Operation()
        {

            //Coin value input
            var coinValue = new StringReader("100");
            Console.SetIn(coinValue);

            menu.UserFeedCoin(vendingMachine);

            var expectedBalance = 100;
            var expectedType = typeof(HasMoneyStateService);

            Assert.Equal(expectedBalance, vendingMachine.balance);
            Assert.IsType(expectedType,vendingMachine.vendingMachineState);

        }
     }
}
