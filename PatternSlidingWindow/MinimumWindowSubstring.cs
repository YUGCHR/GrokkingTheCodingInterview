using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Hard
// Given two strings s and t of lengths m and n respectively.
// -------------------------------------------------------------------------------------------------------------------------
// Return the minimum window substring of s such that every character in t (including duplicates) is included in the window.
// -------------------------------------------------------------------------------------------------------------------------
// If there is no such substring, return the empty string "".
// The testcases will be generated such that the answer is unique.
// A substring is a contiguous sequence of characters within the string.

namespace PatternSlidingWindow
{
    public class MinimumWindowSubstring
    {
        public static void MainMinimumWindowSubstring()
        {
            Console.WriteLine("Class - Minimum Window Substring");

            // A D O B E C O D E B  A  N  C
            // 0 1 2 3 4 5 6 7 8 9 10 11 12

            string stringWhereToSearch = "ADOBECODEBANC";
            string searchingString = "ABC";

            string blank0 = "  ";

            for (int i = 0; i < stringWhereToSearch.Length; i++)
            {
                Console.Write($"  {stringWhereToSearch[i]},");
            }
            Console.Write("\n");
            for (int i = 0; i < stringWhereToSearch.Length; i++)
            {
                if (i > 9)
                {
                    blank0 = " ";
                }
                Console.Write($"{blank0}{i},");
            }
            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"String Where To Search is {stringWhereToSearch}");
            Console.WriteLine($"String Searching String is {searchingString}");

            string output = FindMinimumWindowSubstring(stringWhereToSearch, searchingString);

            Console.WriteLine($"The minimum window substring is {output} which includes {searchingString}");
        }

        public static string FindMinimumWindowSubstring(string stringWhereToSearch, string searchingString) // 27 lines
        {
            int stepCounter = 0;
            int maxLength = 0;

            if (stringWhereToSearch == null || stringWhereToSearch.Length == 0)
            {
                Console.WriteLine($"String <<stringWhereToSearch>> is not defined or its length = 0, cannot use this data");
                return "";
            }

            // define Dictionary, where Key is char from stringWhereToSearch string and Value - its latest found index
            Dictionary<char, int> countDistinctCharacters = new();

            int index = 0;
            string maxSubString = "";

            while (index < stringWhereToSearch.Length)
            {
                char indexChar = stringWhereToSearch[index];

                Console.WriteLine($"<<<CYCLE START>>> - {index} loop of cycle, indexChar is {indexChar}");

                // провеяем, если такой символ есть, просто? удаляем его из словаря, если нет - заносим
                // не просто - а удаляем из словаря все символы, индекс которых меньше этого, который встретился повторно
                // при этом словарь надо слить в массив/строку - в этот момент удобно искать нужные индексы
                // а в самом символе, который встретился, меняем индекс на новый
                // или удаляем всех (наверху разберутся, кто свой), и записываем новый символ

                // in a loop through the string, get the character, check if it is in the dictionary already
                if (countDistinctCharacters.ContainsKey(indexChar))
                {
                    //                           " c   a   r   a" - пока цепочка длится, индексы всегда идут подряд, это надо как-то использовать
                    // скажем, индексы могут быть 11, 12, 13, 14
                    // сейчас надо удалить все, раньше индекса a - включая его или нет, без разницы
                    // скажем, во второй словарь заносим индексы в качестве ключа, а значением - символы
                    // тогда спрашиваем в первом словаре индекс повторного символа, во втором словаре проверяем, если ли индексы меньше его, удаляем их и из первого словаря эти же символы
                    // символ встретился повторно, цепочка оборвалась, надо измерить ее длину и если макс, то сохранить подстроку
                    // в этот момент в словаре вся нужная цепочка "car", проверяем длину и, если надо, сохраняем

                    // maxLength = Math.Max(maxLength, countDistinctCharacters.Count); - отличная функция, но еще надо знать, что было обновление
                    // compare this frame size with max value stored in foundLongestSubstring
                    int countDistinctCharactersCount = countDistinctCharacters.Count;
                    Console.WriteLine($"Yes, char {indexChar} already exists in dictionary, DICTIONARY length = {countDistinctCharactersCount}, MAX length = {maxLength}");

                    if (countDistinctCharactersCount > maxLength)
                    {
                        // if more, save into it, otherwise disregard
                        maxLength = countDistinctCharactersCount;

                        // здесь надо выделить подстроку, соотвествующую текущему словарю, ее конец - это текущий index=14 (он уже на 1 больше, чем конец словаря), а длина - длина словаря = 3
                        // сначала получим стартовый индекс подстроки 14 - 3 = 11
                        int substringStart = index - countDistinctCharactersCount;
                        maxSubString = stringWhereToSearch.Substring(substringStart, countDistinctCharactersCount);

                        Console.WriteLine($" ------------------- new max found, max length now is {maxLength}, max substring is {maxSubString}");
                    }

                    // теперь взять индекс неудачливого символа и назначить его текущим, чтобы прямо сейчас символ добавился в словарь
                    // переставили индекс на предыдущее вхождение в словарь встретившегося повторно символа
                    int oldIndex = index;
                    index = countDistinctCharacters[indexChar] + 1;

                    // очистили словарь                    
                    countDistinctCharacters = new();
                    Console.WriteLine($"index = {index}, countDistinctCharacters was reset, oldIndex = {oldIndex}");
                }

                // все лишние удалены, добавляем символ, которого точно еще нет в словаре
                indexChar = stringWhereToSearch[index];
                countDistinctCharacters.Add(indexChar, index);
                Console.WriteLine($"new char {indexChar} on index {index} was added in dictionary with value {index}");
                index++;
                stepCounter++;
                Console.WriteLine($"index = {index}, stepCounter = {stepCounter}, is waiting a new cycle now");

                // ехать дальше по строке
                //Console.ReadKey();
            }

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {maxLength}, max substring is {maxSubString}");

            return "";
        }
    }
}
