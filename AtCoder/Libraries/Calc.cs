using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder.Libraries
{
    public static class Calc
    {
        /// <summary>
        /// nCr 
        /// </summary>
        static decimal nCr(int c, int s)
        {
            if (c == s) return 1;

            var range1 = Enumerable.Range(c - s + 1, s).ToArray();
            var range2 = Enumerable.Range(1, s).ToArray();

            var dash1 = range1.Except(range2).Select(x => (decimal)x).ToArray();
            var dash2 = range2.Except(range1).Select(x => (decimal)x).ToArray();

            var multi1 = dash1.Aggregate((a, b) => a * b);
            var multi2 = dash2.Aggregate((a, b) => a * b);

            return (multi1 / multi2);
        }

        /// <summary>
        /// nPr 
        /// </summary>
        public static long nPr(int n, int r)
        {
            // naive: return Factorial(n) / Factorial(n - r);
            return FactorialDivision(n, n - r);
        }

        private static long FactorialDivision(int topFactorial, int divisorFactorial)
        {
            long result = 1;
            for (int i = topFactorial; i > divisorFactorial; i--)
                result *= i;
            return result;
        }

        private static long Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}
