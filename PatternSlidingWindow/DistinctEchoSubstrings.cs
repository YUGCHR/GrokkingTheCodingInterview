using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Distinct Echo Substrings
// Return the number of distinct non-empty substrings of text that can be written as the concatenation of some string with itself
// (i.e. it can be written as a + a where a is some string).


namespace PatternSlidingWindow
{
    public static class DistinctEchoSubstrings
    {
        public static void MainDistinctEchoSubstrings()
        {
            Console.WriteLine("Class - Distinct Echo Substrings");

            string input = "abcabcabc";            
            int expectedOutput = 3;

            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Started string = {input}");

            int output = FindDistinctEchoSubstrings(input);

            //string outputToString = String.Join(", ", output);
            Console.WriteLine($"The maximum number of points you can see is {output} and expected value is {expectedOutput}");
        }

        public static void VisualList(List<double> list2print, bool needConvertToInt)
        {
            string list2printToString;
            if (needConvertToInt)
            {
                int[] polarAnglesToInt = list2print.Select(x => (int)x).ToArray();
                list2printToString = String.Join(", ", polarAnglesToInt);
            }
            else
            {
                list2printToString = String.Join(", ", list2print);
            }
            Console.WriteLine($"\n polarAngles = {list2printToString}");
        }

        public static int FindDistinctEchoSubstrings(string input)
        {
            int result = 0;



            return result;
        }
        
    }
}
