using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;

namespace VendingMachineApp.Infrastructure
{
    public class CoinInventory
    {
        private Dictionary<Coin, short> store = new Dictionary<Coin, short>();
        public short GetCount(Coin item)
        {
            var count = store.FirstOrDefault(s => s.Key.Equals(item)).Value;
            return count;
        }

        public void Add(Coin item, short count)
        {
            store.Add(item, count);
        }

        public void Add(Coin item)
        {
            short count = GetCount(item);
            if (store.ContainsKey(item))
            {
                count = (short)(count + 1);
                store[item] = count;
            }
            else
            {
                store.Add(item, ++count);
            }

        }

        public Dictionary<Coin, short> GetItems()
        {
            return store;
        }


        public void Take(Coin item)
        {
            short count = GetCount(item);
            if (store.ContainsKey(item))
            {
                count = (short)(count - 1);
                store[item] = count;
            }
        }
        public bool IsEmpty()
        {
            foreach (var item in store.Values) 
            {
                if (item != 0)
                    return false;
            }
            return true;
        }
        public bool Has(Coin item)
        {
            return GetCount(item) > 0;
        }

        //Method for TestProject
        public void RemoveAllCoin() 
        {
            store.Clear();
        }

    }
}
