using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Infrastructure;
using VendingMachineApp.Models;

namespace VendingMachineApp.Interfaces
{
    public interface IVendingMachineState
    {
        public void InsertMoney(Coin coin);
        public void SelectProduct(Product product);
        public void DispenseProduct();
        public void ShowProducts();
        public void Refund();
    }
}
