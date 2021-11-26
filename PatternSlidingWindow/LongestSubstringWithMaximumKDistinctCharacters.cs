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

            string input = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiiirrr";
            // caaarrraaa r r r a a a c i i i i iiiiiiiiiiiiiiirrr;
            // 01234567891011121314151617181920;
            int k = 3;
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
                        
            Console.WriteLine($"String input is {input}");
            Console.WriteLine($"Necessary K Distinct Characters is {k}");

            int output = FindLongestSubstring(input, k);

            Console.WriteLine($"the length of the longest substring in input string with no more than {k} distinct characters is {output}");
        }

        public static int FindLongestSubstring(string input, int k)
        {
            // завести словарь, где ключ - символ, значение - индекс
            Dictionary<char, int> countDistinctCharacters = new();

            // можно индекс назвать rightFrameSide, а можно просто i
            int leftFrameSide = 0;
            int foundLongestSubstring = 0;
            bool isListFull = false; // Dictionary is emplty (or not full)

            for (int i = 0; i < input.Length; i++)
            {
                char indexChar = input[i];
                Console.WriteLine($"- {i} - loop of cycle, indexChar is {indexChar}, leftFrameSide = {leftFrameSide}, the longest substring length = {foundLongestSubstring}, dictionary is full = {isListFull}");

                // в цикле по строке достать символ, проверить, есть ли в словаре
                if (countDistinctCharacters.ContainsKey(indexChar))
                {
                    // если есть, записать значение нового индекса
                    Console.WriteLine($"Yes, artist IS at home today, for char {indexChar} new index {i} will be set in dictionary");

                    countDistinctCharacters[indexChar] = i;
                }
                // если такого символа в словаре нет, надо его добавить, а если нет места, сначала удалить самый ранний символ
                else
                {
                    // если нет, посмотреть на размер словаря (хранить в отдельной переменной?)
                    // можно размер хранить в bool - isListFull
                    Console.WriteLine($"New char {indexChar} with index {i} will be added in dictionary, which is full {isListFull}");

                    if (isListFull)
                    {
                        // если полный, найти минимальный индекс (как?), его ключ удалить
                        // c# find key with min value in dictionary
                        // https://stackoverflow.com/questions/23734686/c-sharp-dictionary-get-the-key-of-the-min-value
                        // https://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
                        // похоже, здесь l - это TAccumulate, а r - TSource (наоборот разницы нет?)
                        // хотя должно быть наоборот - https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-6.0
                        // никакой магии, просто тупо перебрать словарь
                        // похоже, этот индекс будет как раз в leftFrameSide
                        // нет, надо искать именно минимальный индекс (Value) в словаре (как получить сразу оба значения за один проход?)
                        char minChar = countDistinctCharacters.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                        int minValue = countDistinctCharacters[minChar];
                        Console.WriteLine($"dictionary is full {isListFull}, the farest char {minChar} was found with index {minValue} and will be removed from");

                        // определить, сравнить и, если надо, зафиксировать размер цепочки
                        // измерить размер окна - текущий индекс минус leftFrameSide
                        int thisChainLength = i - leftFrameSide;
                        Console.WriteLine($"new substring length {thisChainLength} was defined and will be compared with max found length {foundLongestSubstring}");

                        // сравнить его с максимальным foundLongestSubstring
                        if (thisChainLength > foundLongestSubstring)
                        {
                            // если больше, сохранить в него, иначе забыть
                            foundLongestSubstring = thisChainLength;
                            Console.WriteLine($"new max found length {foundLongestSubstring} was set");

                        }

                        // удаляем кандидата на выбывание - самый последний встреченный символ
                        countDistinctCharacters.Remove(minChar);
                        //isListFull = false;
                        Console.WriteLine($"dictionary is full {isListFull}, the farest char {minChar} was found with index {minValue} and will be removed from");

                        // опять найти минимальный индекс в словаре и записать его в leftFrameSide
                        // записать новый индекс в переменную leftFrameSide - (minValue + 1) будет новым началом окна
                        leftFrameSide = minValue + 1;

                        // словарь не полный, добавить в него новый символ 
                    }
                    //else // - else убрать, словарь всегда будет неполный, даже если был полный
                    // если влазит, добавить в словарь
                    countDistinctCharacters.Add(indexChar, i);
                    Console.WriteLine($"dictionary is full = {isListFull}, new char {indexChar} with index {i} was added, new dictionary length is {countDistinctCharacters.Count}");

                    if (!isListFull)
                    {
                        int countDistinctCharactersLenght = countDistinctCharacters.Count;
                        if (countDistinctCharactersLenght == k)
                        {
                            isListFull = true;
                            Console.WriteLine($"dictionary became full = {isListFull}, its length is {countDistinctCharacters.Count}");

                        }
                    }
                }
                // ехать дальше по строке
            }

            string output = String.Join("", countDistinctCharacters.Keys);
            Console.WriteLine($"kDistinctCharacters is {output}, the longest substring length = {foundLongestSubstring}");

            return foundLongestSubstring;
        }
    }
}
