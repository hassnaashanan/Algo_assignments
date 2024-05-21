using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PresentBaskets
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// fill the 2 baskets with the most expensive collection of fruits.
        /// </summary>
        /// <param name="W1">weight of 1st basket</param>
        /// <param name="W2">weight of 2nd basket</param>
        /// <param name="items">Pair of weight (Key) & cost (Value) of each item</param>
        /// <returns>max total cost to fill two baskets</returns>
        static public double PreparePresentBaskets(int W1, int W2, KeyValuePair<int, int>[] items)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            int W = W1 + W2;
            Array.Sort(items, (ax, by) => by.Value * ax.Key - ax.Value * by.Key);
            double c = 0;
            int i = 0;
            int v = items.Length;

            for (; W > 0 && i < v; i++)
            {
                int x = Math.Min(W, items[i].Key);
                if (items[i].Key == 0)
                {
                    c += x * 0;
                }
                else
                    c += x * ((double)items[i].Value / items[i].Key);
                W -= x;

            }

            return c;
        }
        #endregion
    }
}
