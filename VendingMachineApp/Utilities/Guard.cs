using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Utilities
{
    public static class Guard
    {
        public static bool ForNullOrEmpty(string value, string parameterName)
        {
            if (!string.IsNullOrEmpty(value.Trim())) return false;
            Console.WriteLine($"{parameterName} cannot be Null or Empty");
            return true;

        }
        public static bool IsValidCoin(short coin)
        {
            if (Constant.ValidCoins.Contains(coin)) return true;
            Console.WriteLine("Inserted invalid coin. Please take your coin back");
            return false;
        }

        public static bool IsValidProduct(short product)
        {
            if (Constant.ProductList.Contains(product)) return true;
            Console.WriteLine("selected invalid product number");
            return false;
        }

        public static (short convertedValue, bool isSuccess) IsConvertToShort(string value, string parameterName)
        {
            try
            {
                var convertedValue = Convert.ToInt16(value);
                return (convertedValue, true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{parameterName} cannot be convert to short");
                return (0, false);
            }
        }
    }
}
