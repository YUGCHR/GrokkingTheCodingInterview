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

            //string stringWhereToSearch = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr";
            //string searchingString = "arc";

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

        // возвращаем - сам словарь, признак isLackingCharFound - был ли мальчик (символ) и isDictEmpty - стал ли словарь пустым
        public static (Dictionary<char, int>, bool, bool) DecimateCharFromDictionary(Dictionary<char, int> dict, char ch)
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

        public static void DictIsVisual(Dictionary<char, int> dict, string dictName)
        {
            StringBuilder dictString = new();

            foreach (var ch in dict)
            {
                dictString.Append(ch.Key, ch.Value);
            }

            Console.WriteLine($"#                                                                                       state of dictionary {dictName} # is {dictString}");
        }

        public static string FindMinimumWindowSubstring(string stringWhereToSearch, string searchingString) // 27 lines
        {
            int stepCounter = 0;
            int minLength = int.MaxValue;
            int leftFrameSide = 0;
            int foundChars = 0;
            bool isCharFound, isLackingCharFound, wasCharRemoved, isDictFull;
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

            DictIsVisual(sourceChars, "SOURCE");

            // sourceCharsFilling=searchLength и isDictFull=true - это одно и то же наполнение словаря
            // интересует только предельное состояние - не найдено ни одного символа, исходный словарь при этом полный
            // в дальнейшем надо будет решить, что из них проще использовать
            // пока этот признак нужен только для подтягивания левой стороны, если ни одного символа не найдено и обе границы окна синхронно двигаются вперед
            int sourceCharsFilling = searchLength;
            isDictFull = true;

            // ************************************************************************************************************************************************************
            // в дальнейшем можно при превышении размера окна больше существующего минимума, сбрасывать словарь (заполнять исходный словарь полностью) и искать все заново
            // ************************************************************************************************************************************************************

            // предполагаем, что везде в int сейчас нули - да, но нет
            Dictionary<char, int> workChars = new();

            // цепочку в виде окна можно динамически хранить в списке, но может и не надо - наверное, это лишнее
            // подстрока скользящего окна - для наглядности отладки, потом убрать
            StringBuilder winString = new();
            List<int> indicesDistinctCharacters = new();

            Console.WriteLine($"Source dictionary was created, main cycle FOR is started\n");

            for (int windowEnd = 0; windowEnd < inputLength; windowEnd++)
            {
                char thisChar = stringWhereToSearch[windowEnd];
                // сохраняем цепочку окна для наглядности отладки, потом удалить
                winString.Append(thisChar);
                Console.WriteLine($"\n<<<CYCLE START>>> - {windowEnd} loop of cycle, indexChar is {thisChar}, sliding window substring is {winString}");

                // находим любой пригодный символ (который есть в искомой строке)
                // собираем любые годные символы, а удаляем только при движении левой границы окна - внутри достигнутого счастья (найдены все символы искомой строки, исходный словарь пустой)
                isCharFound = searchingString.Contains(thisChar);

                if (isCharFound)
                {
                    sourceCharsFilling--;
                    isDictFull = false;

                    Console.WriteLine($"!!! Char {thisChar} is usable and will be add to work dict");

                    // записываем его в рабочий словарь с таким же устройством - символы - это ключи, а значения - сколько раз нашли букву, нет - пока не собрали всю длину
                    // если символ годный, добавляем его в рабочий словарь (считаем общее количество символов в значениях)
                    workChars = CountCharsInDictionary(workChars, thisChar);
                    DictIsVisual(workChars, "WORK");

                    // сохраняем индекс годного символа в список индексов
                    indicesDistinctCharacters.Add(windowEnd);


                    // собираем подстроку из строки поиска в список - берём первый символ, проверяем что такой нужен и вычитаем его из исходного словаря
                    // вычитаем найденную букву из исходного словаря, пока он не опустеет - это значит, что все буквы собраны, надо измерить цепочку

                    // выясняем, является ли найденный символ нехватающим (lacking) для счастья
                    // возвращаем -
                    // признак найденного --нехватающего!-- символа,
                    // признак, что символ был удален из словаря (и счастье стало неполным),
                    // обновленный словарь с уменьшенным или удаленным символом
                    (sourceChars, isLackingCharFound, wasCharRemoved) = DecimateCharFromDictionary(sourceChars, thisChar);
                    DictIsVisual(sourceChars, "SOURCE");

                    if (isLackingCharFound)
                    {
                        bool isDictEmpty = sourceChars.Count == 0;

                        // проверяем длину исходного словаря - isDictEmpty = true ?
                        while (isDictEmpty)
                        {
                            // если словарь опустел, время замерить цепочку
                            // можно на старте записать сюда maxInt
                            int foundSubstringLength = windowEnd - leftFrameSide + 1;
                            Console.WriteLine($"*** WHILE started *** Dictionary empty is {isDictEmpty}, calculate foundSubstringLength {foundSubstringLength} = windowEnd {windowEnd} - {leftFrameSide} + 1");

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
                            Console.WriteLine($"--- leftFrameSideChar is {leftFrameSideChar} now and it will be decimated");

                            // вычтем его из рабочего словаря
                            // теперь надо проверить символ на новой левой границе окна на наличие в рабочем словаре - это сделаем на следующем проходе while
                            (workChars, isCharFound, wasCharRemoved) = DecimateCharFromDictionary(workChars, leftFrameSideChar);
                            DictIsVisual(workChars, "WORK");

                            if (!isCharFound)
                            {
                                Console.WriteLine($"Something WENT WRONG - isCharFound = {isCharFound} and must be always true here");
                            }
                            //if (wasCharRemoved)
                            //{
                                Console.WriteLine($"--- leftFrameSideChar {leftFrameSideChar} was decimated from workChars, but will be added to sourceChars");

                                // если буква при этом исчезла (значение стало равно нулю и ключ удалил), то добавить ее в исходный словарь с единичкой
                                // сейчас же (в этом месте) исходный словарь пустой и в нем появится одна (значение 1) только что удаленная буква из рабочего словаря
                                // тут еще надо определить общее состояние словаря - если он полный (sourceCharsFilling=searchLength), то установить признак isDictFull=true
                                sourceChars = CountCharsInDictionary(sourceChars, leftFrameSideChar);
                                DictIsVisual(sourceChars, "SOURCE");

                                // после того, как хоть символ (нет! - исчез из рабочего словаря) появился в исходном словаре, надо прекратить while и идти по строке в поиске новых годных символов
                                isDictEmpty = false;
                            //}

                            // *******************************************
                            // сейчас сложный момент - куда передвинуть левую границу (где взять индекс следующего годного символа) - отложим на завтра
                            // можно запилить while и опять перебирать символы в строке до появления годного
                            // или опять хранить список индексов в списке, нулевой удалять вместе с символом и перескакивать на новый, ставший нулевым
                            // *******************************************

                            // здесь удалить самый первый (нулевой) индекс из списка и присвоить leftFrameSide значение из нового нулевого индекса
                            Console.WriteLine($"Index {indicesDistinctCharacters[0]} in indicesDistinctCharacters[0] will be deleted");
                            indicesDistinctCharacters.RemoveAt(0);
                            int startChainToRemove = leftFrameSide;

                            // тут надо перескочить не на один символ, а перейти к следующему индексу из списка индексов
                            // таким образовм устанавливаем левую границу окна на следующий годный символ, минуя все лишние
                            leftFrameSide = indicesDistinctCharacters[0];
                            int lengthChainToRemove = leftFrameSide - startChainToRemove;
                            Console.WriteLine($"Symbols from 0 to {lengthChainToRemove} = leftFrameSide {leftFrameSide} - startChainToRemove {startChainToRemove} will be removed from sliding window substring is {winString}");
                            winString.Remove(0, lengthChainToRemove);
                            Console.WriteLine($"New index in indicesDistinctCharacters[0] is {leftFrameSide} - new left frame side, new sliding window substring is {winString}");

                            //leftFrameSideChar = stringWhereToSearch[leftFrameSide];
                            // если он там есть, то цикл while надо продолжить, подтягивая вправо левую границу, пока в рабочем словаре хватает букв
                            // это соотвествует ситуации, когда окно уменьшилось из-за сдвинутой левой границы, но в цепочке по прежнему есть все необходимые буквы в необходимом количестве
                            // тогда уменьшаем окно и на каждом шаге фиксируем минимум, пока не перестанет хватать букв - тогда опять начнем двигать правую границу вперед
                            // возвращаем - признак найденного символа, признак, что символ был удален, обновленный словарь с уменьшенным или удаленным символом
                            // здесь эту операцию проделываем не над исходным словарем, а над рабочим - ожидая, когда появится признак, что символ удален
                            //(workChars, isCharFound, wasCharRemoved) = IsCharExistInDictionary(workChars, leftFrameSideChar);
                        }
                    }
                }

                // здесь - только если еще/уже вообще ни одного символа не найдено
                // (наверное, это может быть только в начале, когда еще не нашли символов)
                // в дальнейшем хоть одна буква всегда будет оставаться на левой границе
                if (isDictFull)
                {
                    leftFrameSide++;
                }
            }

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {minLength}, max substring is {minSubstring}");

            return minSubstring;
        }
    }
}
