using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "PreparePresentBaskets"; } }

        public override void TryMyCode()
        {
            int W1, W2;
            double output, expected;
            KeyValuePair<int, int>[] items;

            //Same Values
            W1 = 2;
            W2 = 2;
            items = new KeyValuePair<int, int>[4];
            items[0] = new KeyValuePair<int, int>(1, 1);
            items[1] = new KeyValuePair<int, int>(2, 2);
            items[2] = new KeyValuePair<int, int>(3, 3);
            items[3] = new KeyValuePair<int, int>(4, 4);
            expected = 4;
            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
            PrintCase(W1, W2, items, output, expected);

            //Diff Values
            W1 = 3;
            W2 = 4;
            items = new KeyValuePair<int, int>[4];
            items[0] = new KeyValuePair<int, int>(1, 4);
            items[1] = new KeyValuePair<int, int>(2, 2);
            items[2] = new KeyValuePair<int, int>(3, 0);
            items[3] = new KeyValuePair<int, int>(4, 8);
            expected = 14;
            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
            PrintCase(W1, W2, items, output, expected);

            //Unsorted
            W1 = 4;
            W2 = 2;
            items = new KeyValuePair<int, int>[4];
            items[0] = new KeyValuePair<int, int>(6, 2);
            items[1] = new KeyValuePair<int, int>(4, 12);
            items[2] = new KeyValuePair<int, int>(5, 10);
            items[3] = new KeyValuePair<int, int>(2, 8);
            expected = 20;
            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
            PrintCase(W1, W2, items, output, expected);

            //Partial
            W1 = 5;
            W2 = 3;
            items = new KeyValuePair<int, int>[4];
            items[0] = new KeyValuePair<int, int>(3, 9);
            items[1] = new KeyValuePair<int, int>(6, 20);
            items[2] = new KeyValuePair<int, int>(2, 7);
            items[3] = new KeyValuePair<int, int>(1, 4);
            expected = 27.67;
            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
            PrintCase(W1, W2, items, output, expected);

            //Zeros
            W1 = 0;
            W2 = 3;
            items = new KeyValuePair<int, int>[4];
            items[0] = new KeyValuePair<int, int>(3, 0);
            items[1] = new KeyValuePair<int, int>(6, 0);
            items[2] = new KeyValuePair<int, int>(2, 0);
            items[3] = new KeyValuePair<int, int>(1, 0);
            expected = 0;
            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
            PrintCase(W1, W2, items, output, expected);
        }

        Thread tstCaseThr;
        bool caseTimedOut;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int W1 = 0, W2 = 0, N = 0;
            ushort w, c;
            KeyValuePair<int, int>[] items = null;
            double output, actualResult;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;

            int i = 1;
            while (testCases-- > 0)
            {
                W1 = br.ReadInt32();
                W2 = br.ReadInt32();
                N = br.ReadInt32();
                items = new KeyValuePair<int, int>[N];
                for (int j = 0; j < N; j++)
                {
                    w = br.ReadUInt16();
                    c = br.ReadUInt16();
                    items[j] = new KeyValuePair<int, int>(w, c);
                }
                actualResult = br.ReadDouble();

                //Console.WriteLine("W1 = {0}, W2 = {1} N = {2} Res = {3}", W1, W2, N, actualResult);
                output = 0;
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            Stopwatch sw = Stopwatch.StartNew();
                            output = PresentBaskets.PreparePresentBaskets(W1, W2, items);
                            sw.Stop();
                            Console.WriteLine("N = {0}, time in ms = {1}", items.Length, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = double.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
                    tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (Math.Round(output, 2) == Math.Round(actualResult, 2))    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0));
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int W1, int W2, KeyValuePair<int, int>[] items, double output, double expected)
        {
            Console.WriteLine("W1: {0}", W1);
            Console.WriteLine("W2: {0}", W2);
            Console.WriteLine("weight cost");

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(items[i].Key + "  " + items[i].Value);
            }
            Console.WriteLine("Output = " + Math.Round(output, 2));
            Console.WriteLine("Expected = " + Math.Round(expected, 2));
            Console.WriteLine();
        }
        #endregion

    }
}
