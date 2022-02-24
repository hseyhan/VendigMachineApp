using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.Constants
{
    public static class MessageConstant
    {
        #region NoMoneyStateMessages

        public static string NoMoneySelectProduct = "You can not select item without inserting coin";
        public static string NoMoneyDispenseProduct = "You can not get item without inserting coin";
        public static string NoMoneyRefund = "There is no money to refund";

        #endregion

        #region HasMoneyStateMessages

        public static string HasMoneySelectProductDoesNotHaveChange = "Machine doesn't have sufficient change, Please take refund";
        public static string HasMoneySelectProductNotEnoughCoin = "Your current balance can not afford the product :(";
        public static string HasMoneySelectProductSoldOut = "Product sold out, Please try another product";
        public static string HasMoneyRefund = "Please take your refund coin :";

        #endregion

        #region Menu

        public static string MenuInputNotRecognized = "Input not recognized";
        public static string MenuSelectProductWithoutInsertedCoin = "Please insert money before selecting a product";
        public static string MenuSelectProductNotMacthItem = "Input does not match any item";

        #endregion
    }
}
