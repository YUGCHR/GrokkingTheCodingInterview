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
            // завести словарь, где ключ - символ, значение - индекс
            // можно индекс назвать rightFrameSide, а можно просто i
            // в цикле по строке достать символ, проверить, есть ли в словаре
            // если есть, записать значение нового индекса
            // записать этот индекс в переменную leftFrameSide
            // если нет, посмотреть на размер словаря (хранить в отдельной переменной?)
            // можно размер хранить в bool - isListFull
            // если влазит, добавить в словарь
            // если полный, найти минимальный индекс (как?), его ключ удалить
            // c# find key with min value in dictionary
            // https://stackoverflow.com/questions/23734686/c-sharp-dictionary-get-the-key-of-the-min-value
            // https://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
            // var min = tempDictionary.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
            // похоже, здесь l - это TAccumulate, а r - TSource (наоборот разницы нет?)
            // хотя должно быть наоборот - https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-6.0
            // никакой магии, просто тупо перебрать словарь
            // похоже, этот индекс будет как раз в leftFrameSide
            // измерить размер окна - текущий индекс минус leftFrameSide
            // сравнить его с максимальным foundLongestSubstring
            // если больше, сохранить в него, иначе забыть
            // опять найти минимальный индекс в словаре и записать его в leftFrameSide
            // словарь не полный, добавить в него новый символ 
            // ехать дальше



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
