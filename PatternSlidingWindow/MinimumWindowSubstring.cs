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

            //string stringWhereToSearch = "ADOBECODEBANC";
            //string searchingString = "ABC";

            string stringWhereToSearch = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr";
            string searchingString = "arc";

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


        // искомую строку сохраняем в исходный словарь, символы - это ключи, а значения - сколько раз встречается буква в ней
        // собираем подстроку из строки поиска в список - берём первый символ, проверяем что такой нужен
        // записываем его в рабочий словарь с таким же устройством - символы - это ключи, а значения - сколько раз нашли букву, нет - пока не собрали всю длину
        // вычитаем найденную букву из исходного словаря, пока он не опустеет - это значит, что все буквы собраны, надо измерить цепочку
        // цепочку в виде окна можно динамически хранить в списке, но может и не надо - наверное, это лишнее
        // после измерения цепочки, надо выкинуть (вычесть единицу) левую букву из рабочего словаря
        // если буква при этом исчезла (значение стало равно нулю и ключ удалил), то добавить ее в исходный словарь с единичкой
        // теперь двигать правую границу, пока исходный словарь опять не опустеет
        // если же после уменьшения последней (левой) буквы она не обнулилась, то правую границу не двигаем, а двигаем левую, вычитая значения в рабочем словаре, пока буква не удалится


        
        public static string FindMinimumWindowSubstring(string stringWhereToSearch, string searchingString) // 27 lines
        {
            int stepCounter = 0;
            int minLength = -1;
            int leftFrameSide = 0;
            int foundChars = 0;
            bool isListFull = false;
            string minSubstring = "";

            int inputLength = stringWhereToSearch.Length;
            int searchLength = searchingString.Length;

            if (stringWhereToSearch == null || stringWhereToSearch.Length == 0 || searchingString == null || searchingString.Length == 0)
            {
                Console.WriteLine($"String <<stringWhereToSearch>> OR <<searchingString>> is not defined or its length = 0, cannot use this data");
                return "";
            }

            // define Dictionary, where Key is char from stringWhereToSearch string and Value - its latest found index
            // предполагаем, что везде в int сейчас нули
            Dictionary<char, int> countDistinctCharacters = new();
            List<int> indicesDistinctCharacters = new();

            for (int windowEnd = 0; windowEnd < inputLength; windowEnd++)
            {
                char thisChar = stringWhereToSearch[windowEnd];
                // достаём текущий символ из строки и сравниваем со строкой поиска (string.Contain)
                bool isThisCharFound = searchingString.Contains(thisChar);
                Console.WriteLine($"<<<CYCLE START>>> - {windowEnd} loop of cycle, indexChar is {thisChar}, searching string contains it = {isThisCharFound}");

                // если такой есть, заносим символ в словарь (или список?), считаем количество каждого из найденных символов, если нашлись уже все, ставим отметку, что словарь полный
                if (isThisCharFound)
                {
                    // если в словаре такой уже есть, игнорируем (можно использовать TryAdd) - или можно не добавлять, а перезаписывать? тогда сложнее считать общее количество символов
                    //if (!countDistinctCharacters.ContainsKey(thisChar))

                    // считаем количество встреченных символов каждого сорта
                    if (countDistinctCharacters.ContainsKey(thisChar))
                    {
                        countDistinctCharacters[thisChar]++;
                    }
                    else
                    {
                        countDistinctCharacters.Add(thisChar, 1);
                    }

                    // еще надо сохранить список индексов всех полезных символов - в простом списке по возрастанию
                    indicesDistinctCharacters.Add(windowEnd);
                    Console.WriteLine($"Increment of countDistinctCharacters[{thisChar}] = {countDistinctCharacters[thisChar]}, saved CHAR index = {windowEnd}");

                    //foundChars++;
                    // пока нафиг считать, просто меряем длину словаря
                    foundChars = countDistinctCharacters.Count;
                    if (foundChars >= searchLength)
                    {
                        // нашлись все символы - сейчас скользящее окно захватывает весь набор искомых символов
                        isListFull = true;
                    }

                    // здесь рассмотрим ситуацию, когда встреченный повторно символ является еще и последним в списке индексов, а словарь еще не полный
                    if (!isListFull && countDistinctCharacters[thisChar] > 1)
                    {
                        char leftFrameSideChar = stringWhereToSearch[leftFrameSide];
                        if (thisChar == leftFrameSideChar)
                        {
                            // здесь удалить самый первый (нулевой) индекс из списка и присвоить leftFrameSide значение из нового нулевого индекса
                            Console.WriteLine($"Index {indicesDistinctCharacters[0]} in indicesDistinctCharacters[0] will be deleted");

                            indicesDistinctCharacters.RemoveAt(0);
                            // нет - после уменьшения значения в словаре, самый левый символ выбрасываем из работы, поэтому левый край окна сдвигаем на шаг вправо
                            // тут надо перескочить не на один символ, а перейти к самому маленькому индексу из словаря (а там - печаль - нет индексов)
                            leftFrameSide = indicesDistinctCharacters[0];
                            Console.WriteLine($"New index in indicesDistinctCharacters[0] is {leftFrameSide}");
                        }
                    }

                    // все символы могут найтись только на шаге, когда найден один из них, если пустышка, то сразу идем за следующим
                    // когда нашлись все символы, измеряем длину подстроки и сохраняем
                    if (isListFull)
                    {
                        int foundSubstringLength = windowEnd - leftFrameSide + 1;
                            Console.WriteLine($"*** Dictionary is full, calculate foundSubstringLength {foundSubstringLength} = windowEnd {windowEnd} - {leftFrameSide} + 1");

                        // можно на старте записать сюда maxInt
                        if (foundSubstringLength < minLength || minLength == -1)
                        {
                            // if more, save into it, otherwise disregard
                            minLength = foundSubstringLength;

                            // здесь надо выделить подстроку, соотвествующую текущему словарю, ее конец - это текущий index=14 (он уже на 1 больше, чем конец словаря), а длина - длина словаря = 3
                            // сначала получим стартовый индекс подстроки 14 - 3 = 11
                            int substringStart = windowEnd - (foundSubstringLength - 1);
                            Console.WriteLine($" calculate substringStart {substringStart} = windowEnd {windowEnd} - ({foundSubstringLength} - 1)");
                            minSubstring = stringWhereToSearch.Substring(substringStart, foundSubstringLength);
                            Console.WriteLine($" ------------------- new min substring found, min length now is {minLength}, min substring is {minSubstring}");
                        }

                        // удаляем из словаря символ, который с индексом leftFrameSide
                        // надо учесть вариант, что этот символ встречался в другом месте
                        // тогда в словаре надо хранить количество встреченных для каждого символа
                        // поэтому сразу не удаляем, а смотрим что там у него в значении, точнее, сразу вычитаем 1 из значения, если получился 0, то символ удаляем из словаря
                        char leftFrameSideChar = stringWhereToSearch[leftFrameSide];
                        int leftFrameSideCounter = countDistinctCharacters[leftFrameSideChar]--;
                        Console.WriteLine($"Decrement of countDistinctCharacters[{leftFrameSideChar}] = {countDistinctCharacters[leftFrameSideChar]}");

                        // здесь удалить самый первый (нулевой) индекс из списка и присвоить leftFrameSide значение из нового нулевого индекса
                        Console.WriteLine($"Index {indicesDistinctCharacters[0]} in indicesDistinctCharacters[0] will be deleted");

                        indicesDistinctCharacters.RemoveAt(0);
                        // нет - после уменьшения значения в словаре, самый левый символ выбрасываем из работы, поэтому левый край окна сдвигаем на шаг вправо
                        // тут надо перескочить не на один символ, а перейти к самому маленькому индексу из словаря (а там - печаль - нет индексов)
                        leftFrameSide = indicesDistinctCharacters[0];
                        Console.WriteLine($"New index in indicesDistinctCharacters[0] is {leftFrameSide}");

                        // наверное, правильнее меньше или равно нулю, а то страшно
                        if (leftFrameSideCounter == 0)
                        {
                            countDistinctCharacters.Remove(leftFrameSideChar);
                            //удалили символ, словарь перестал быть томным
                            isListFull = false;
                            Console.WriteLine($"Char {leftFrameSideChar} was removed from countDistinctCharacters, becouse it value became {leftFrameSideCounter}, dictionary if full = {isListFull}");
                        }
                    }
                }

                // здесь - только если еще/уже вообще ни одного символа не найдено - то есть, словарь сейчас пустой
                if (foundChars == 0)
                {
                    leftFrameSide++;
                }
            }

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {minLength}, max substring is {minSubstring}");

            return minSubstring;
        }
    }
}
