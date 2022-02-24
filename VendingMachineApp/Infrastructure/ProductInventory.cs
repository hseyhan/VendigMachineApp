using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;

namespace VendingMachineApp.Infrastructure
{
    public class ProductInventory
    {
        #region Properties
        private Dictionary<Product, short> Products { get; set; }
        #endregion

        #region Constructor
        public ProductInventory()
        {
            Products = new Dictionary<Product, short>();
        }
        #endregion

        #region Methods
        internal void Insert(Product product, short num)
        {
            if (Products.ContainsKey(product))
            {
                Products[product] += num;
            }
            else
            {
                Products.Add(product, num);
            }
        }

        internal void Dispense(Product product)
        {
            short count = Count(product);
            if (count > 0)
            {
                Products[product] = (short)(count - 1);
            }
        }

        public short Count(Product product)
        {
            short count = 0;
            if (Products.ContainsKey(product))
            {
                count = Products[product];
            }

            return count;
        }
        public short GetPrice(Product product)
        {
            short price = 0;
            if (product == Product.COLA)
            {
                price = 100;
            }
            else if (product == Product.CHIPS)
            {
                price = 50;
            }
            else if (product == Product.CANDY)
            {
                price = 65;
            }

            return price;
        }

        public Dictionary<Product, short> GetItems()
        {
            return Products;
        }
        #endregion
    }
}
