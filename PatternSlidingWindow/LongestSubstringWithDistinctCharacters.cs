using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 3. Longest Substring Without Repeating Characters
// Given a string s, find the length of the longest substring without repeating characters.

namespace PatternSlidingWindow
{
    public class LongestSubstringWithDistinctCharacters
    {
        public static void MainLongestDistinctSubstring()
        {
            Console.WriteLine("Class - Longest Substring with Distinct Characters");

            //string input = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiiirarcr";
            // caaarrraaa r r r a a a c i i i i i iiiiiiiiiiiii  i  r  a  r  c  r;
            // 0123456789101112131415161718192021               35 36 37 38 39 40;

            string input = "caraarcrraccaariirraaacimibiibbirriiaaiicciiiciairibidiiirarcr";

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

            int output = FindLongestDistinctSubstring(input);

            Console.WriteLine($"the length of the longest substring in input string with all distinct characters is {output}");
        }

        // написать код по оригинальному решению
        // отсюда перевести третий вариант - https://www.geeksforgeeks.org/length-of-the-longest-substring-without-repeating-characters/
        public static int FindLongestDistinctSubstringOriginal(string input) // __ lines
        {
            int maxLength = 0;
            string maxSubString = "";

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {maxLength}, max substring is {maxSubString}");

            return maxLength;
        }

        public static int FindLongestDistinctSubstring(string input) // 23 lines
        {
            int stepCounter = 0;
            int maxLength = 0;

            if (input == null || input.Length == 0)
            {
                Console.WriteLine($"String <<input>> is not defined or its length = 0, cannot use this data");
                return 0;
            }

            // define Dictionary, where Key is char from input string and Value - its latest found index
            Dictionary<char, int> countDistinctCharacters = new();

            int index = 0;
            string maxSubString = "";

            while (index < input.Length)
            {
                char indexChar = input[index];

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
                        maxSubString = input.Substring(substringStart, countDistinctCharactersCount);

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
                indexChar = input[index];
                countDistinctCharacters.Add(indexChar, index);
                Console.WriteLine($"new char {indexChar} on index {index} was added in dictionary with value {index}");
                index++;
                stepCounter++;
                Console.WriteLine($"index = {index}, stepCounter = {stepCounter}, is waiting a new cycle now");

                // ехать дальше по строке
                //Console.ReadKey();
            }

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {maxLength}, max substring is {maxSubString}");

            return maxLength;
        }
    }
}
