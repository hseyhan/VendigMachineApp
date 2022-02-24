using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.Utilities
{
    public static class ConvertValueToDisplay
    {
        public static string ConvertCoinValueToDisplay(short coin)
        {

            return ((decimal)coin / 100).ToString("€0.00");
        }
    }
}
