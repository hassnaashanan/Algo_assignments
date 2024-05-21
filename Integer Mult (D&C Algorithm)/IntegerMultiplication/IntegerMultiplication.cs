//urns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
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
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();
            if (N <= 128)
            {
                int g = X.Length;
                byte[] result = Mulity(X, Y, g);
                return result;
            }
            else
            {
                if (N % 2 != 0)
                {
                    X = ex(X, 1);
                    Y = ex(Y, 1);               
                }
                 int w = X.Length;
                int m = w / 2;
                byte[] a = X.Take(m).ToArray();
                byte[] b = X.Skip(m).ToArray();
                byte[] c = Y.Take(m).ToArray();
                byte[] d = Y.Skip(m).ToArray();
                byte[] ab = AddArray(a, b);
                byte[] cd = AddArray(c, d);
                byte[] z = IntegerMultiply(ab, cd,m);
                byte[] m1 = IntegerMultiply(a, c,m);
                byte[] m2 = IntegerMultiply(b, d,m);
                byte[] t = SubArray(z, m1);
                byte[] n = SubArray(t, m2);
                byte[] s = expand(n, m);
                byte[] q = expand(m2, 2 * m);
                byte[] p = AddArray(s, q);
                byte[] r = AddArray(m1,p );
                int f = r.Length;
                Array.Resize(ref r,f-2 );
                return r;

            }
        }
        public static byte[] Mulity(byte[] x, byte[] y, int c)
        {
            int[] n = new int[2 * c];
            
            for(int k = 0; k < c;)
            {
                int j = 0;
                while ( j < c)
                {
                    n[k + j] += (int)(x[k] * y[j]);
                    j++;
                }
                k++;
            }
            int carry = 0;
            int i = 0;
           while( i < 2 * c)
            {
                n[i] += carry;
                carry = (int)(n[i] / 10);
                n[i] %= 10;
                 i++;
            }
            byte[] bytes = n.Select(m => (byte)m).ToArray();
            return bytes;
        }

        public static byte[] expand(byte[] arr, int n)
        {
            Array.Reverse(arr);
            int m = arr.Length;
            byte[] narr = new byte[m + n];
            for (int i = 0; i < m; i++)
            {
                narr[i] = arr[i];
            }
            Array.Reverse(narr);
            return narr;
        }
     
        public static byte[] AddArray(byte[] x, byte[] y)
        {
            int s = Math.Max(x.Length, y.Length);
            int v = Math.Min(x.Length, y.Length);
            int t = s - v;
            byte[] n = new byte[s + 1];
            int c = 0;
            if (x.Length > y.Length)
                y = ex(y, t);
            else if (y.Length > x.Length)
                x = ex(x, t);
            for (int i = 0; i < s; i++)
            {
                int r = (byte)(x[i] + y[i] + c);
                n[i] = (byte)(r % 10);
                c = (byte)(r / 10);
            }

            n[s] = (byte)c;
            return n;
        }

        public static byte[] SubArray(byte[] g, byte[] h)
        {
            int b = 0;
            int d = Math.Min(g.Length, h.Length);
            int c = Math.Max(h.Length, g.Length);
            byte[] n = new byte[c];
            int i = 0;
            while( i < d)
            {
                int hy = (g[i] - h[i] - b);
                if (hy < 0)
                {
                    hy = hy + 10;
                    b = 1;
                }
                else
                    b = 0;

                n[i] += (byte)(hy);
                i++;
            }
            for ( i = d; i < c; i++)
            {
                int pf = (g[i] - b);
                if (pf < 0)
                {
                    pf = pf + 10;
                    b = 1;
                }
                else
                    b = 0;
                
                n[i] += (byte)(pf);
            }
            return n;

        }
        static byte[] ex(byte[] arr, int n)
        {

            int m = arr.Length;
            byte[] narr = new byte[m + n];
            for (int i = 0; i < m; i++)
            {
                narr[i] = arr[i];
            }
            return narr;
        }
    }



    #endregion
}