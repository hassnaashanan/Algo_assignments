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
    public static class SchoolQuizII
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the minimum number of integers whose sum equals to ‘N’
        /// </summary>
        /// <param name="N">number given by the teacher</param>
        /// <param name="numbers">list of possible numbers given by the teacher (starting by 1)</param>
        /// <returns>minimum number of integers whose sum equals to ‘N’</returns>
        public static int SolveValue(int N, int[] numbers)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            int[] hass = new int[N + 1];

            int i = 1;
            while (i <= N)
            {
                hass[i] = int.MaxValue;
                i++;
            }
            int r = numbers.Length;
            int s = hass.Length;
            int h = 1;
            while (h <s)
            {
                for (int j = 1; j <= r; j++)
                {
                    if (numbers[j - 1] <= h)
                    {
                        int w = hass[h - numbers[j - 1]] + 1;
                        if (w < hass[h])
                            hass[h] = w;
                    }
                }
                h++;
            }

            return hass[N];
        }
        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>the numbers themselves whose sum equals to ‘N’</returns>
        public static int[] ConstructSolution(int N, int[] numbers)
        {
            //REMOVE THIS LINE BEFORE START CODING
           // throw new NotImplementedException();

            int[] hass = new int[N + 1];

            int i = 1;
            while (i <= N)
            {
                hass[i] = int.MaxValue;
                i++;
            }
            int[] solution = new int[N + 1];
            int r= numbers.Length;
            int k = 1;
            while (k <= N)
            {
                for (int h = 0; h < r; h++)
                {
                    if (numbers[h] <= k)
                    {
                        int y = hass[k - numbers[h]] + 1;
                        if (y < hass[k])
                        {
                            hass[k] = y;
                            solution[k] = numbers[h];
                        }
                    }
                }
                k++;
            }
            LinkedList<int> ouput = new LinkedList<int>();
            while (N > 0)
            {
                ouput.AddLast(solution[N]);
                N -= solution[N];
            }
            return ouput.ToArray();
        }
        #endregion

        #endregion
    }
}
