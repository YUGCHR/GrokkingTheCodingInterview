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

        public static Dictionary<char, int> CountCharsInDictionary(Dictionary<char, int> dict, char ch)
        {
            // считаем полученный символ в значении ключа - если ключ уже есть, то +1, если ключа нет - создаем со значением 1
            if (dict.ContainsKey(ch))
            {
                dict[ch]++;
            }
            else
            {
                dict.Add(ch, 1);
            }

            return dict;
        }

        // возвращаем - сам словарь, признак isCharFound - был ли мальчик (символ) и isDictEmpty - стал ли словарь пустым
        public static (Dictionary<char, int>, bool, bool) IsCharExistInDictionary(Dictionary<char, int> dict, char ch)
        {
            // проверяем, есть ли такой символ вообще в словаре
            if (dict.ContainsKey(ch))
            {
                // ищем значение в ключе полученного символ и вычитаем 1, если в результате получится 0, удаляем ключ
                int val = dict[ch];
                if (val <= 1)
                {
                    dict.Remove(ch);
                    // нет, пусть снаружи проверяют
                    // нет - проверяем длину словаря
                    //int count = dict.Count;
                    //if (count == 0)
                    //{
                    // называем выход wasCharRemoved вместо isDictEmpty
                    // нет - если словарь опустел, возвращаем isCharFound = true и isDictEmpty = true
                    // если ключ удалили, возвращаем isCharFound = true и wasCharRemoved = true
                    return (dict, true, true);
                    //}
                    // словарь не пустой, возвращаем isCharFound = true и isDictEmpty = false
                    //return (dict, true, false);
                }
                else
                {
                    dict[ch] = val - 1;
                    // нет - словарь не пустой, возвращаем isCharFound = true и isDictEmpty = false
                    // ключ не удаляли, возвращаем isCharFound = true и isDictEmpty = false
                    return (dict, true, false);
                }
            }
            else
            {
                // если такого символа нет, возвращаем isCharFound = false
                return (dict, false, false);
            }
            //return dict;
        }

        public static string FindMinimumWindowSubstring(string stringWhereToSearch, string searchingString) // 27 lines
        {
            int stepCounter = 0;
            int minLength = int.MaxValue;
            int leftFrameSide = 0;
            int foundChars = 0;
            bool isCharFound, wasCharRemoved, isDictEmpty;
            string minSubstring = "";

            int inputLength = stringWhereToSearch.Length;
            int searchLength = searchingString.Length;

            if (stringWhereToSearch == null || stringWhereToSearch.Length == 0 || searchingString == null || searchingString.Length == 0)
            {
                Console.WriteLine($"String <<stringWhereToSearch>> OR <<searchingString>> is not defined or its length = 0, cannot use this data");
                return "";
            }

            // define source Dictionary, where Key is char from stringWhereToSearch string and Value - this chars quantity in searchingString
            Dictionary<char, int> sourceChars = new();

            // искомую строку сохраняем в исходный словарь, символы - это ключи, а значения - сколько раз встречается буква в ней            

            for (int i = 0; i < searchLength; i++)
            {
                char thisChar = searchingString[i];
                sourceChars = CountCharsInDictionary(sourceChars, thisChar);
                stepCounter++;
            }

            // предполагаем, что везде в int сейчас нули - да, но нет
            Dictionary<char, int> workChars = new();

            // подстрока скользящего окна - для наглядности отладки, потом убрать
            StringBuilder winString = new();
            //List<int> indicesDistinctCharacters = new();

            // цепочку в виде окна можно динамически хранить в списке, но может и не надо - наверное, это лишнее

            for (int windowEnd = 0; windowEnd < inputLength; windowEnd++)
            {
                char thisChar = stringWhereToSearch[windowEnd];
                winString.Append(thisChar);
                Console.WriteLine($"<<<CYCLE START>>> - {windowEnd} loop of cycle, indexChar is {thisChar}, sliding window substring is {winString}");

                // нет - достаём текущий символ из строки и сравниваем со строкой поиска (string.Contain)
                //bool isThisCharFound = searchingString.Contains(thisChar);

                // собираем подстроку из строки поиска в список - берём первый символ, проверяем что такой нужен и вычитаем его из исходного словаря
                // вычитаем найденную букву из исходного словаря, пока он не опустеет - это значит, что все буквы собраны, надо измерить цепочку
                (sourceChars, isCharFound, wasCharRemoved) = IsCharExistInDictionary(sourceChars, thisChar);

                //if (wasCharRemoved)
                //{
                //    // проверяем длину словаря
                //    int count = sourceChars.Count;
                //    if (count == 0)
                //    {
                //        isDictEmpty = true;
                //    }
                //}

                if (isCharFound)
                {
                    // записываем его в рабочий словарь с таким же устройством - символы - это ключи, а значения - сколько раз нашли букву, нет - пока не собрали всю длину
                    // если символ годный, добавляем его в рабочий словарь (считаем общее количество символов в значениях)
                    workChars = CountCharsInDictionary(workChars, thisChar);
                }

                // проверяем длину исходного словаря - isDictEmpty = true ?
                if (sourceChars.Count == 0)
                {
                    // если словарь опустел, время замерить цепочку
                    // можно на старте записать сюда maxInt
                    int foundSubstringLength = windowEnd - leftFrameSide + 1;
                    Console.WriteLine($"*** Dictionary is full, calculate foundSubstringLength {foundSubstringLength} = windowEnd {windowEnd} - {leftFrameSide} + 1");

                    if (foundSubstringLength < minLength)
                    {
                        // if more, save into it, otherwise disregard
                        minLength = foundSubstringLength;

                        // здесь надо выделить подстроку, соотвествующую текущему словарю, ее конец - это текущий index=14 (он уже на 1 больше, чем конец словаря), а длина - длина словаря = 3
                        // сначала получим стартовый индекс подстроки 14 - 3 = 11
                        int substringStart = windowEnd - (foundSubstringLength - 1);
                        Console.WriteLine($" calculate substringStart {substringStart} = windowEnd {windowEnd} - ({foundSubstringLength} - 1)");

                        minSubstring = stringWhereToSearch.Substring(substringStart, foundSubstringLength);
                        Console.WriteLine($" ------------------- new min substring found, min length now is {minLength}, min substring is {minSubstring}");
                        //сравнить с текущей подстрокой скользящего окна (только для отладки)
                        Console.WriteLine($" min substring is {minSubstring}, sliding window substring is {winString} - must be the same");
                    }

                    // после измерения цепочки, надо выкинуть (вычесть единицу) левую букву из рабочего словаря
                    // узнаем какой символ на левой границе окна
                    char leftFrameSideChar = stringWhereToSearch[leftFrameSide];
                    // вычтем его из рабочего словаря
                    (workChars, isCharFound, wasCharRemoved) = IsCharExistInDictionary(workChars, leftFrameSideChar);

                    // если буква при этом исчезла (значение стало равно нулю и ключ удалил), то добавить ее в исходный словарь с единичкой
                    // сейчас же (в этом месте) исходный словарь пустой и в нем появится одна (значение 1) только что удаленная буква из рабочего словаря
                    if (wasCharRemoved)
                    {
                        sourceChars = CountCharsInDictionary(sourceChars, leftFrameSideChar);

                    }


                    // *******************************************
                    // сейчас сложный момент - куда передвинуть левую границу (где взять индекс следующего годного символа) - отложим на завтра
                    // можно запилить while и опять перебирать символы в строке до появления годного
                    // или опять хранить список индексов в списке, нулевой удалять вместе с символом и перескакивать на новый, ставший нулевым
                    // *******************************************


                    int leftFrameSideCounter = workChars[leftFrameSideChar]--;
                    Console.WriteLine($"Decrement of countDistinctCharacters[{leftFrameSideChar}] = {workChars[leftFrameSideChar]}");

                    // теперь двигать правую границу, пока исходный словарь опять не опустеет
                    // если же после уменьшения последней (левой) буквы она не обнулилась, то правую границу не двигаем,
                    // тогда двигаем левую, вычитая значения в рабочем словаре, пока буква не удалится










                }


                // если такой есть, заносим символ в словарь (или список?), считаем количество каждого из найденных символов, если нашлись уже все, ставим отметку, что словарь полный
                if (isThisCharFound)
                {
                    // если в словаре такой уже есть, игнорируем (можно использовать TryAdd) - или можно не добавлять, а перезаписывать? тогда сложнее считать общее количество символов
                    //if (!countDistinctCharacters.ContainsKey(thisChar))

                    // считаем количество встреченных символов каждого сорта
                    if (workChars.ContainsKey(thisChar))
                    {
                        workChars[thisChar]++;
                    }
                    else
                    {
                        workChars.Add(thisChar, 1);
                    }

                    // еще надо сохранить список индексов всех полезных символов - в простом списке по возрастанию
                    indicesDistinctCharacters.Add(windowEnd);
                    Console.WriteLine($"Increment of countDistinctCharacters[{thisChar}] = {workChars[thisChar]}, saved CHAR index = {windowEnd}");

                    //foundChars++;
                    // пока нафиг считать, просто меряем длину словаря
                    foundChars = workChars.Count;
                    if (foundChars >= searchLength)
                    {
                        // нашлись все символы - сейчас скользящее окно захватывает весь набор искомых символов
                        isListFull = true;
                    }

                    // здесь рассмотрим ситуацию, когда встреченный повторно символ является еще и последним в списке индексов, а словарь еще не полный
                    if (!isListFull && workChars[thisChar] > 1)
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
                        int leftFrameSideCounter = workChars[leftFrameSideChar]--;
                        Console.WriteLine($"Decrement of countDistinctCharacters[{leftFrameSideChar}] = {workChars[leftFrameSideChar]}");

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
                            workChars.Remove(leftFrameSideChar);
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
