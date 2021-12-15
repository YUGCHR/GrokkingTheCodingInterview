using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 340 Longest Substring with At Most K Distinct Characters (46.8% Medium locked)
// Given a string, find the length of the longest substring in it with no more than K distinct characters.

namespace PatternSlidingWindow
{
    public static class LongestSubstringWithMaximumKDistinctCharacters
    {
        public static void MainLongestSubstring()
        {
            Console.WriteLine("Class - Longest substring with maximum K distinct characters");

            string input = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiiirrr";
            // caaarrraaa r r r a a a c i i i i i iiiiiiiiiiiiiirrr;
            // 0123456789101112131415161718192021;
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
            //int output = FindLongestSubstringOriginal(input, k);

            Console.WriteLine($"the length of the longest substring in input string with no more than {k} distinct characters is {output}");
        }

        // написать код по оригинальному решению
        public static int FindLongestSubstringOriginal(string input, int k) // 16-17 lines - FindLongestSubstringOriginal took 55 steps to execute input string with length 39 and K = 3
        {
            int stepCounter = 0;

            if (input == null || input.Length == 0)
            {
                Console.WriteLine($"String <<input>> is not defined or its length = 0, cannot use this data");
                return -1;
            }

            int windowStart = 0;
            int maxLength = 0;

            Dictionary<char, int> charFrequencyMap = new();

            for (int windowEnd = 0; windowEnd < input.Length; windowEnd++)
            {
                char rightChar = input[windowEnd];

                // получаем предыдущее сохраненное в словаре значение текущего (очередного в цикле) символа в строке - если такого символа в словаре нет, берем значение 0
                // charFrequencyMap.put(rightChar, charFrequencyMap.getOrDefault(rightChar, 0) + 1);
                int resultGetValueOrDefault = charFrequencyMap.GetValueOrDefault(rightChar, 0);

                // заносим символ rightChar в словарь cо значением +1 от уже существующего значения такого же ключа в словаре, если такого символа еще не было, берем значение 1
                charFrequencyMap[rightChar] = resultGetValueOrDefault + 1;

                // если длина словаря стала больше положенной (записали лишний символ), надо один удалить
                while (charFrequencyMap.Count > k)
                {
                    // достаем первый символ слева в скользящем окне (на первом шаге 0)
                    char leftChar = input[windowStart];
                    // уменьшаем значение ключа этого символа (как бы старим его на один шаг)
                    charFrequencyMap[leftChar] = charFrequencyMap[leftChar] - 1;

                    // если это значение стало нулевым, ключ совсем состарился и будет удален
                    if (charFrequencyMap[leftChar] == 0)
                    {
                        charFrequencyMap.Remove(leftChar);
                    }

                    // двигаемся на следующий символ скользящего окна
                    windowStart++; // shrink the window
                    stepCounter++; // count steps of execution
                }

                // сохраняем максимальную длину скользящего окна на каждом проходе цикла
                maxLength = Math.Max(maxLength, windowEnd - windowStart + 1);
                stepCounter++; // count steps of execution
            }

            Console.WriteLine($"FindLongestSubstringOriginal took {stepCounter} steps to execute input string with length {input.Length} and K = {k}");

            return maxLength;
        }

        public static int FindLongestSubstring(string input, int k) // 27 lines - FindLongestSubstringOriginal took 39 steps to execute input string with length 39 and K = 3
        {
            int stepCounter = 0;

            if (input == null || input.Length == 0)
            {
                Console.WriteLine($"String <<input>> is not defined or its length = 0, cannot use this data");
                return -1;
            }

            if (k >= input.Length)
            {
                Console.WriteLine($"Condition << K = {k} >> is more then string length, so the << string length = {input.Length} >> will be solution in this case");
                return input.Length;
            }

            // define Dictionary, where Key is char from input string and Value - its latest found index
            Dictionary<char, int> countDistinctCharacters = new();

            int leftFrameSide = 0;
            int foundLongestSubstring = 0;
            bool isListFull = false; // Dictionary is emplty (or not full)

            for (int i = 0; i < input.Length; i++)
            {
                char indexChar = input[i];
                Console.WriteLine($"- {i} - loop of cycle, indexChar is {indexChar}, leftFrameSide = {leftFrameSide}, the longest substring length = {foundLongestSubstring}, dictionary is full = {isListFull}");

                // in a loop through the string, get the character, check if it is in the dictionary already
                if (countDistinctCharacters.ContainsKey(indexChar))
                {
                    // if the char already exists, set new index value for it
                    Console.WriteLine($"Yes, artist IS at home today, for char {indexChar} new index {i} will be set in dictionary");

                    countDistinctCharacters[indexChar] = i;
                }
                // if there is no such symbol in the dictionary, add it, and if there is no place, first remove the farest symbol
                else
                {
                    Console.WriteLine($"New char {indexChar} with index {i} will be added in dictionary, which is full {isListFull}");

                    if (isListFull)
                    {
                        // if the dictionary is full, find the min index (Value) and remove Key for this Value
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
                        // calculate the frame size - it will be equal to subtract leftFrameSide from current index
                        int thisChainLength = i - leftFrameSide;
                        Console.WriteLine($"new substring length {thisChainLength} was defined and will be compared with max found length {foundLongestSubstring}");

                        // compare this frame size with max value stored in foundLongestSubstring
                        if (thisChainLength > foundLongestSubstring)
                        {
                            // if more, save into it, otherwise disregard
                            foundLongestSubstring = thisChainLength;
                            Console.WriteLine($"new max found length {foundLongestSubstring} was set");

                        }

                        // удаляем кандидата на выбывание - самый последний встреченный символ
                        // remove the elimination candidate - the most recently encountered character
                        countDistinctCharacters.Remove(minChar);
                        //isListFull = false;
                        Console.WriteLine($"dictionary is full {isListFull}, the farest char {minChar} was found with index {minValue} and will be removed from");

                        // set index of the removed Key plus 1 in leftFrameSide - this will be the new frame start index
                        // записать новый индекс в переменную leftFrameSide - (minValue + 1) будет новым началом окна
                        leftFrameSide = minValue + 1;

                        // словарь не полный, добавить в него новый символ 
                        // dictionary is not full now and can add new char in it
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
                stepCounter++; // count steps of execution
            }

            string output = String.Join("", countDistinctCharacters.Keys);
            Console.WriteLine($"kDistinctCharacters is {output}, the longest substring length = {foundLongestSubstring}");
            Console.WriteLine($"FindLongestSubstringOriginal took {stepCounter} steps to execute input string with length {input.Length} and K = {k}");

            return foundLongestSubstring;
        }
    }
}
