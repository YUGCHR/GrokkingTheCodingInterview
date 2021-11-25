using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternSlidingWindow
{
    public static class LongestSubstringWithMaximumKDistinctCharacters
    {
        public static void MainLongestSubstring()
        {
            Console.WriteLine("Class - Longest substring with maximum K distinct characters");

            string input = "aaarrraaarrraaaci";
            string blank0 = "  ";

            for (int i = 0; i < input.Length; i++)
            {
                Console.Write($"  {input[i]},");
            }
            Console.Write("\n");
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 9)
                {
                    blank0 = " ";
                }
                Console.Write($"{blank0}{i},");
            }
            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            int k = 2;
            Console.WriteLine($"String input is {input}");
            Console.WriteLine($"Necessary K Distinct Characters is {k}");

            int output = FindLongestSubstring(input, k);

            Console.WriteLine($"the length of the longest substring in input string with no more than {k} distinct characters is {output}");
        }

        public static int FindLongestSubstring(string input, int k)
        {
            int foundLongestSubstring = 0;
            int currentSubstringLength = 0;
            int countDistinctCharacters = 0;
            int index = -1;
            char[] kDistinctCharacters = new char[k];

            // insert in kDistinctCharacters first K distinct chars
            int i = 0;
            bool kDistinctCharactersIsFull = false;
            bool continueWhile = true;

            while (continueWhile)
            {
                index++;
                char indexChar = input[index];

                Console.WriteLine($"while step in string input {index} - i = {i}, k = {k}, indexChar = {indexChar}");

                bool charIsMissingInChain = true;
                for (int j = 0; j < k; j++)
                {
                    if (indexChar == kDistinctCharacters[j])
                    {
                        charIsMissingInChain = false;
                    }
                }

                if (charIsMissingInChain)
                {
                    if (!kDistinctCharactersIsFull)
                    {
                        kDistinctCharacters[i] = indexChar;
                        Console.WriteLine($"charIsMissingInChain is {charIsMissingInChain} - kDistinctCharacters[{i}] = {indexChar}");
                        i++;
                        if (i >= k)
                        {
                            kDistinctCharactersIsFull = true;
                        }
                    }
                    else
                    {
                        continueWhile = false;
                    }
                }
            }

            string output = String.Join("", kDistinctCharacters);
            Console.WriteLine($"kDistinctCharacters is {output}, index in input string stay on {index} position");

            return 0;
        }
    }
}
