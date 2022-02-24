using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;
using VendingMachineApp.Utilities;
using Xunit;

namespace VendingMachineApp.Test
{
    public class GuardTest
    {
        [Fact]
        public void ForNullOrEmpty_Returns_True()
        {
            //Valid Coin
            var value = "";

            var isNullOrEmpty = Guard.ForNullOrEmpty(value,value);

            Assert.True(isNullOrEmpty);
        }


        [Fact]
        public void ForNullOrEmpty_Returns_False()
        {
            //Valid Coin
            var value = "value";

            var isNullOrEmpty = Guard.ForNullOrEmpty(value, value);

            Assert.False(isNullOrEmpty);
        }

        [Fact]
        public void IsValidCoin_ValidSituation()
        {
            //Valid Coin
            var coin = Coin.HUNDERED_CENTS;

            var isValid = Guard.IsValidCoin((short)coin);

            Assert.True(isValid);
        }

        [Fact]
        public void IsValidCoin_InvalidSituation()
        {
            //Valid Coin
            var coin = 2;

            var isValid = Guard.IsValidCoin(2);

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidProduct_ValidSituation()
        {
            //Valid Product
            var product = Product.COLA;

            var isValid = Guard.IsValidProduct((short)product);

            Assert.True(isValid);
        }

        [Fact]
        public void IsValidProduct_InvalidSituation()
        {
            //Valid Coin
            short product = 4;

            var isValid = Guard.IsValidProduct(product);

            Assert.False(isValid);
        }

        [Fact]
        public void IsConvertToShort_Returns_True() 
        {
            var value = "5";
            //Expected Value
            short shortValue = 5;
            var convertedValue = Guard.IsConvertToShort(value,"value");

            Assert.True(convertedValue.isSuccess);
            Assert.Equal(shortValue, convertedValue.convertedValue);
        }


        [Fact]
        public void IsConvertToShort_Returns_False()
        {
            var value = "a";
            //Expected Value
            
            var convertedValue = Guard.IsConvertToShort(value, "value");

            Assert.False(convertedValue.isSuccess);
            Assert.Equal(0, convertedValue.convertedValue);
        }



    }
}
