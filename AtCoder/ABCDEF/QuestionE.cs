using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtCoder.Abcdef
{
    public class QuestionE
    {
        public static void Main(string[] args)
        {
            var sw = new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
            Console.SetOut(sw);

            //Input string
            string s = Console.ReadLine();

            //Input int
            int n = int.Parse(Console.ReadLine());

            //Input long
            long l = long.Parse(Console.ReadLine());

            //Input string array
            string[] inputStrArray = Console.ReadLine().Split(' ');

            //Input int array
            int[] inputArray = Console.ReadLine().Split(' ').Select(i => int.Parse(i)).ToArray();

            //Input long array
            long[] inputLongArray = Console.ReadLine().Split(' ').Select(i => long.Parse(i)).ToArray();


            Console.Out.Flush();
        }
    }
}
