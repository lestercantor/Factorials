using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;


namespace Factorials
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(RecursionFactorial(11));
            Console.WriteLine(IterativeFactorial(11));

            Console.WriteLine(ThreadedFactorial(15));
        }

        public static long RecursionFactorial(int n)
        {
            if (n == 0)
                return 1;

            return n * RecursionFactorial(n - 1);
        }

        public static long IterativeFactorial(int n)
        {
            if (n == 0)
                return 1;

            for (int i = (n - 1); i > 0; i--)
            {
                n *= i;
            }

            return n;
        }

        public static BigInteger ThreadedFactorial(int n)
        {
            BigInteger result = 1;
            int groupSize = 10;

            List<List<int>> groupedNum = new List<List<int>>();

            while (n > 0)
            {
                if (groupSize > n)
                    groupSize = n;

                List<int> nums = Enumerable.Range((n - groupSize + 1), groupSize).Reverse().ToList();
                groupedNum.Add(nums);
                n -= groupSize;
            }

            // check if sublists were properly made

            /*foreach (List<int> num in groupedNum)
            {
                Console.WriteLine(String.Join(",", num));
            }*/

            List<Thread> threads = new List<Thread>();
            foreach (List<int> num in groupedNum)
            {
               Thread thread = new Thread(() => { result *= Lists(num); });

               thread.Start();
               threads.Add(thread);
               thread.Join();
            }
            
            Console.WriteLine($"Total number of threads: {Process.GetCurrentProcess().Threads.Count}");

            return result;
        }

        public static BigInteger Lists(List<int> numList)
        {
            BigInteger n = numList.Max();
            for (int i = 1; i < numList.Count; i++)
            {
                n *= numList[i];
            }

            return n;
        }
    }
}
