using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;

namespace VendingMachineApp.Constants
{
    public static class Constant
    {
        public static string ShowProductKeyword = "SHOW";

        public static short[] ValidCoins =
        {
            (short)Coin.FIVE_CENTS, (short)Coin.TEN_CENTS, (short)Coin.TWENTY_CENTS, (short)Coin.FIFTY_CENTS, (short)Coin.HUNDERED_CENTS,
            (short)Coin.TWO_HUNDERED_CENTS
        };

        public static short[] ProductList = { (short)Product.COLA, (short)Product.CHIPS, (short)Product.CANDY };

    }
}
