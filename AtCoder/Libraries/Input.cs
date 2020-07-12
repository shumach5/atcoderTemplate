using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Libraries
{
    public class Input
    {
        static public char[,] GetChar2d(int h, int w) 
        {
            char[,] c = new char[h, w];
            for (int i = 0; i < h; i++)
            {
                var s = Console.ReadLine();
                for (int j = 0; j < s.Length; j++)
                {
                    c[i, j] = s[j];
                }
            }

            return c;
        }

        static public int[,] GetInt2d(int h, int w)
        {
            int[,] c = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                var s = Console.ReadLine();
                for (int j = 0; j < s.Length; j++)
                {
                    c[i, j] = s[j];
                }
            }

            return c;
        }

        static public long[,] GetLong2d(int h, int w)
        {
            long[,] c = new long[h, w];
            for (int i = 0; i < h; i++)
            {
                var s = Console.ReadLine();
                for (int j = 0; j < s.Length; j++)
                {
                    c[i, j] = s[j];
                }
            }

            return c;
        }
    }
}
