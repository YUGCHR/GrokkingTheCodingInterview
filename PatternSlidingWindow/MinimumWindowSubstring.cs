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
        // можно все сложить в модель и объявить поле одной строкой
        // первый словарь, а котором список всего нужного, которого нет
        private static Dictionary<char, int> sourceChars;
        // второй такой же со списком всего нужного, которое есть
        private static Dictionary<char, int> workChars;
        // третий словарь, в котором всё полезное, но не нужное
        private static Dictionary<char, int> thirdDict;
        // и список индексов всего этого полезного
        private static List<int> indicesDistinctCharacters;
        // просто счетчик всех циклов
        private static int stepCounter;
        // подстрока скользящего окна - для наглядности отладки, потом убрать
        private static StringBuilder winString;

        MinimumWindowSubstring()
        {

        }

        public static void MainMinimumWindowSubstring()
        {
            Console.WriteLine("Class - Minimum Window Substring");

            // A D O B E C O D E B  A  N  C
            // 0 1 2 3 4 5 6 7 8 9 10 11 12

            string stringWhereToSearch = "ADOBECODEBANC";
            string searchingString = "ABC";
            //string stringWhereToSearch = "caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr";
            //string searchingString = "aarrcc";

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

            //string output = FindMinimumWindowSubstring(stringWhereToSearch, searchingString);
            string output = FindMinimumWindowSubstringOriginal(stringWhereToSearch, searchingString);

            Console.WriteLine($"The minimum window substring is {output} which includes {searchingString}");
        }

        public static string FindMinimumWindowSubstringOriginal(string t, string s) // 27 lines
        {

            if (t == null || t.Length == 0 || s == null || s.Length == 0)
            {
                Console.WriteLine($"String <<stringWhereToSearch>> OR <<searchingString>> is not defined or its length = 0, cannot use this data");
                return "";
            }

            // Map<Character, Integer> dictT = new HashMap<Character, Integer>();
            Dictionary<char, int> dictT = new();

            for (int i = 0; i < t.Length; i++)
            {
                int count = dictT.GetValueOrDefault(t[i], 0);
                dictT[t[i]] = count + 1; //TryAdd?
            }
            DictIsVisual(dictT, "dictT");

            int required = dictT.Count;

            // Filter all the characters from s into a new list along with their index.
            // The filtering criteria is that the character should be present in t.
            // List<Pair<Integer, Character>> filteredS = new ArrayList<Pair<Integer, Character>>();
            // this is not a dictionary!
            List<KeyValuePair<int, char>> filteredS = new();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (dictT.ContainsKey(c))
                {
                    filteredS.Add(new KeyValuePair<int, char>(i, c));
                }
            }
            ListPairsIsVisual(filteredS, "filteredS");

            int l = 0, r = 0, formed = 0;

            // Map<Character, Integer> windowCounts = new HashMap<Character, Integer>();
            Dictionary<char, int> windowCounts = new();

            int[] ans = { -1, 0, 0 };

            // Look for the characters only in the filtered list instead of entire s.
            // This helps to reduce our search.
            // Hence, we follow the sliding window approach on as small list.

            while (r < filteredS.Count)
            {
                Console.WriteLine($"\n*** FIRST WHILE started *** r {r} must be < filteredS.Count {filteredS.Count}");

                //KeyValuePair<int, char> v = filteredS[r];
                //char c = filteredS.get(r).getValue();
                //int count = windowCounts.getOrDefault(c, 0);
                //windowCounts.put(c, count + 1);

                char c = filteredS[r].Value;
                int count = windowCounts.GetValueOrDefault(c, 0);
                windowCounts.Add(c, count + 1);
                Console.WriteLine($"windowCounts.Add(c, count + 1) {c} {count + 1}");
                DictIsVisual(windowCounts, "windowCounts");
                DictIsVisual(dictT, "dictT");

                // if (dictT.ContainsKey(c) && windowCounts.get(c).intValue() == dictT.get(c).intValue())
                Console.WriteLine($"if (c-{c} dictT.ContainsKey(c)-{dictT.ContainsKey(c)} && windowCounts[c]-{windowCounts[c]} == dictT[c]-{dictT[c]})  ==-{windowCounts[c] == dictT[c]}");
                if (dictT.ContainsKey(c) && windowCounts[c] == dictT[c])
                {
                    formed++;
                    Console.WriteLine($"formed++ - from {formed - 1} to {formed}");
                }

                // Try and contract the window till the point where it ceases to be 'desirable'.
                // while (l <= r && formed == required)
                while (l <= r && formed == required)
                {
                    Console.WriteLine($"*** SECOND WHILE started *** l{l} <= r-{r} && formed-{formed} == required-{required}");
                    // c = filteredS.get(l).getValue();
                    c = filteredS[l].Value;

                    // Save the smallest window until now.
                    int end = filteredS[r].Key;
                    int start = filteredS[l].Key;
                    if (ans[0] == -1 || end - start + 1 < ans[0])
                    {
                        ans[0] = end - start + 1;
                        ans[1] = start;
                        ans[2] = end;
                    }

                    windowCounts.Add(c, windowCounts[c] - 1);
                    if (dictT.ContainsKey(c) && windowCounts[c] < dictT[c])
                    {
                        formed--;
                    }
                    l++;
                }
                r++;
            }

            return ans[0] == -1 ? "" : s.Substring(ans[1], ans[2] + 1);
        }

        // тут заполняем три словаря, source - полезными символами с количеством, а work и third - этими же символами с нулями
        public static void CountCharsInDictionary(string searchingString)
        {
            // первый словарь, а котором список всего нужного, которого нет
            // второй такой же со списком всего нужного, которое есть
            // третий словарь, в котором всё полезное, но не нужное

            int searchLength = searchingString.Length;

            for (int i = 0; i < searchLength; i++)
            {
                stepCounter++;
                char thisChar = searchingString[i];
                // проверяем полученный символ в словаре, если нет, то создаем в трех словарях с нулем
                if (!sourceChars.ContainsKey(thisChar))
                {
                    sourceChars.Add(thisChar, 0);
                    workChars.Add(thisChar, 0);
                    thirdDict.Add(thisChar, 0);
                }
                // потом прибавляем 1 в словарь source
                sourceChars[thisChar]++;
            }

            DictIsVisual(sourceChars, "SOURCE");
            DictIsVisual(workChars, "WORK");
            DictIsVisual(thirdDict, "THIRD");
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

        public static void ListPairsIsVisual(List<KeyValuePair<int, char>> dict, string dictName)
        {
            StringBuilder dictString = new();

            foreach (var ch in dict)
            {
                dictString.Append($"({ch.Key}/{ch.Value}) ");
            }

            Console.WriteLine($"#                                                                            state of list of KeyValuePairs {dictName} # is {dictString}");
        }

        public static string FindMinimumWindowSubstring(string stringWhereToSearch, string searchingString) // 27 lines
        {
            int minLength = int.MaxValue;
            int leftFrameSide = 0;
            bool isCharFound;
            bool isUsefulCharFound = true;
            string minSubstring = "";

            int inputLength = stringWhereToSearch.Length;
            int searchLength = searchingString.Length;

            if (stringWhereToSearch == null || inputLength == 0 || searchingString == null || searchLength == 0)
            {
                Console.WriteLine($"String <<stringWhereToSearch>> OR <<searchingString>> is not defined or its length = 0, cannot use this data");
                return "";
            }

            if (stringWhereToSearch.Equals(searchingString))
            {
                Console.WriteLine($"String <<stringWhereToSearch>> AND <<searchingString>> are EQUAL");
                return stringWhereToSearch;
            }

            sourceChars = new();
            workChars = new();
            thirdDict = new();
            indicesDistinctCharacters = new();
            winString = new();
            stepCounter = 0;

            // init source Dictionary, where Key is char from stringWhereToSearch string and Value - this chars quantity in searchingString, work and third dicts init with the same keys and zero values
            CountCharsInDictionary(searchingString);

            // ************************************************************************************************************************************************************
            // в дальнейшем можно при превышении размера окна больше существующего минимума, сбрасывать словарь (заполнять исходный словарь полностью) и искать все заново
            // ************************************************************************************************************************************************************

            Console.WriteLine($"Three dictionaries and list were created, main cycle FOR is started\n");

            for (int windowEnd = 0; windowEnd < inputLength; windowEnd++)
            {
                stepCounter++;
                char thisChar = stringWhereToSearch[windowEnd];
                // сохраняем цепочку окна для наглядности отладки, потом удалить
                winString.Append(thisChar);
                Console.WriteLine($"\n<<<CYCLE START>>> - {windowEnd} loop of cycle, indexChar is {thisChar}, sliding window substring is {winString}");

                // находим любой пригодный/полезный символ (который есть в искомой строке)
                // собираем любые годные символы, а удаляем только при движении левой границы окна - внутри достигнутого счастья (найдены все символы искомой строки, исходный словарь пустой)
                isCharFound = searchingString.Contains(thisChar);

                if (isCharFound)
                {
                    Console.WriteLine($"!!! Char {thisChar} is usable and will be transferred from SOURCE to WORK dicts");
                    // после первого же полезного символа, левая граница переходит под управление словарей
                    isUsefulCharFound = false;

                    // обновляем словари за движением ПРАВОЙ границы вперед - переливаем символ из исходного в рабочий, добавляем в третий, индекс в список и проверяем уровень счастья - его и возвращаем
                    bool isHappinessComplete = RightFrameSideIsMoved(windowEnd, thisChar);

                    // если вдруг наступило полное счастье - собрана вся цепочка, производим замеры
                    while (isHappinessComplete)
                    {
                        stepCounter++;
                        Console.WriteLine($"*** WHILE started *** Dictionary empty is {isHappinessComplete}, calculate foundSubstringLength = windowEnd {windowEnd} - {leftFrameSide} + 1");

                        // если словарь опустел, время замерить цепочку
                        // можно на старте записать сюда maxInt
                        int foundSubstringLength = windowEnd - leftFrameSide + 1;

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

                        // после измерения цепочки, надо выкинуть (вычесть единицу) левую букву из ТРЕТЬЕГО словаря и посмотреть на остаток
                        // для этого сначала узнаем какой символ на левой границе окна
                        char leftFrameSideChar = stringWhereToSearch[leftFrameSide];

                        // обновляем словари за движением ЛЕВОЙ границы вперед - переливаем символ из рабочего в исходный, вычитаем из третьего, контролируем уровень счастья - его и возвращаем
                        isHappinessComplete = LeftFrameSideIsMoved(leftFrameSideChar);

                        // если запас полезных символов не истощился, то полное счастье продолжется и едет на следующий круг while
                        // если счастье закончилась, все равно надо выполнить эти же действия, только while больше не запустится
                        // двигаем восточную (левую) границу окна на запад
                        // для этого надо удалить самый первый (нулевой) индекс из списка и присвоить leftFrameSide значение из нового нулевого индекса
                        Console.WriteLine($"Index {indicesDistinctCharacters[0]} in indicesDistinctCharacters[0] will be deleted");
                        indicesDistinctCharacters.RemoveAt(0);

                        if (indicesDistinctCharacters.Count > 0)
                        {
                            // сохраняем про запас бывшую границу, чтобы рассчитать длину перехода для наглядной строки - потом удалить
                            int startChainToRemove = leftFrameSide;

                            // тут надо перескочить не на один символ, а перейти к следующему индексу из списка индексов
                            // таким образовм устанавливаем левую границу окна на следующий годный символ, минуя все лишние
                            leftFrameSide = indicesDistinctCharacters[0];
                            int lengthChainToRemove = leftFrameSide - startChainToRemove;
                            Console.WriteLine($"Symbols from 0 to {lengthChainToRemove} = leftFrameSide {leftFrameSide} - startChainToRemove {startChainToRemove} will be removed from sliding window substring is {winString}");

                            winString.Remove(0, lengthChainToRemove);
                            Console.WriteLine($"New index in indicesDistinctCharacters[0] is {leftFrameSide} - new left frame side, new sliding window substring is {winString}");
                        }

                        // если он там есть, то цикл while надо продолжить, подтягивая вправо левую границу, пока в рабочем словаре хватает букв
                        // это соотвествует ситуации, когда окно уменьшилось из-за сдвинутой левой границы, но в цепочке по прежнему есть все необходимые буквы в необходимом количестве
                        // тогда уменьшаем окно и на каждом шаге фиксируем минимум, пока не перестанет хватать букв - тогда опять начнем двигать правую границу вперед
                    }
                }

                // здесь - только если еще/уже вообще ни одного символа не найдено
                // (наверное, это может быть только в начале, когда еще не нашли символов)
                // в дальнейшем хоть одна буква всегда будет оставаться на левой границе
                if (isUsefulCharFound)
                {
                    leftFrameSide++;
                }
            }

            Console.WriteLine($"+++++++++++ SOLUTION IS --> max distinct substring length is {minLength}, max substring is {minSubstring}, input string length = {inputLength}, steps count = {stepCounter}");

            return minSubstring;
        }

        private static bool LeftFrameSideIsMoved(char leftFrameSideChar)
        {
            // вычтем его из рабочего словаря
            // вот! - берём удаляемый слева символ, удаляем его из третьего словаря, берём остаток и проверяем этот же символ в рабочем словаре
            int residueThird = --thirdDict[leftFrameSideChar];
            int residueWork = workChars[leftFrameSideChar];

            // если этот остаток больше или равен значению в рабочем (рабочий при этом не трогаем), то двигаем границу дальше на запад - полное счастье продолжается
            // если такого символа больше не осталось (0), то полное счастье закончилось
            // а если запас полезных символов не истощился, то полное счастье продолжется и едет на следующий круг while
            bool unhappy = residueThird < residueWork;

            DictIsVisual(workChars, "WORK");
            DictIsVisual(thirdDict, "THIRD");
            Console.WriteLine($"--- leftFrameSideChar {leftFrameSideChar} was sutract from THIRD and residue = {residueThird}, it <> residueWork {residueWork}, so unhappy is {unhappy}");

            if (unhappy)
            {
                // счастье закончилось, удаляем отнятый символ - переливаем этот символ из рабочего словаря в исходный
                // задаем направление перелива

                // не проверяем символ в словаре work - оттуда они не удаляются
                // сразу вычитаем, потом проверяем, что больше нуля
                // нет, лучше сначала проверить, что больше нуля, а потом вычитать
                // если больше 0, значит можно вычесть - и потом прибавить в source
                if (workChars[leftFrameSideChar] > 0)
                {
                    workChars[leftFrameSideChar]--;
                    // прибавим 1 в символ словаря source, тут надо проверять, что существует
                    if (!sourceChars.ContainsKey(leftFrameSideChar))
                    {
                        // если не существует, добавляем с нулем
                        sourceChars.Add(leftFrameSideChar, 0);
                    }
                    // потом по любому прибавляем 1
                    sourceChars[leftFrameSideChar]++;
                    // можно возвращаться, тут полного счастья не искали
                    DictIsVisual(workChars, "WORK");
                    Console.WriteLine($"#  - char {leftFrameSideChar} was overflowed (transferred) from Work to Source dictionary");
                    DictIsVisual(sourceChars, "SOURCE");
                    // он и так вернется из этого направления метода всегда false, но на всякий случай
                }
                return false;
            }
            return true;
        }

        private static bool RightFrameSideIsMoved(int windowEnd, char thisChar)
        {
            bool isHappinessComplete = false;
            int increment = 0;

            // сначала записываем его в третий словарь (считаем, что он тоже инициализирован годными символами с нулями)
            thirdDict[thisChar]++;
            DictIsVisual(thirdDict, "THIRD");

            // сохраняем индекс годного символа в список индексов
            indicesDistinctCharacters.Add(windowEnd);

            // проверяем символ в словаре source
            if (sourceChars.ContainsKey(thisChar))
            {
                // символ есть, надо вычесть - или удалить, если был последний
                // раз есть, что удалять, значит, будет что прибавлять
                increment = 1;
                // сначала вычитаем, потом проверяем, что больше нуля
                int val = --sourceChars[thisChar];
                if (val == 0)
                {
                    // удаляем символ из словаря
                    sourceChars.Remove(thisChar);
                    // проверяем длину словаря
                    int length = sourceChars.Count;
                    if (length == 0)
                    {
                        // словарь пустой - счастье стало полным
                        isHappinessComplete = true;
                    }
                }
            }
            // прибавим что-то в символ словаря work - наверху решили, что именно (0 или 1)
            workChars[thisChar] += increment;

            // можно возвращаться
            DictIsVisual(sourceChars, "SOURCE");
            Console.WriteLine($"##### Right Frame Side Is Moved - char {thisChar} was transferred from Source to Work dictionary");
            DictIsVisual(workChars, "WORK");

            return isHappinessComplete; ;
        }

        //public void RR()
        //{
        //    while (r < filteredS.size())
        //    {
        //        char c = filteredS.get(r).getValue();
        //        int count = windowCounts.getOrDefault(c, 0);
        //        windowCounts.put(c, count + 1);

        //        if (dictT.containsKey(c) && windowCounts.get(c).intValue() == dictT.get(c).intValue())
        //        {
        //            formed++;
        //        }

        //        // Try and contract the window till the point where it ceases to be 'desirable'.
        //        while (l <= r && formed == required)
        //        {
        //            c = filteredS.get(l).getValue();

        //            // Save the smallest window until now.
        //            int end = filteredS.get(r).getKey();
        //            int start = filteredS.get(l).getKey();
        //            if (ans[0] == -1 || end - start + 1 < ans[0])
        //            {
        //                ans[0] = end - start + 1;
        //                ans[1] = start;
        //                ans[2] = end;
        //            }

        //            windowCounts.put(c, windowCounts.get(c) - 1);
        //            if (dictT.containsKey(c) && windowCounts.get(c).intValue() < dictT.get(c).intValue())
        //            {
        //                formed--;
        //            }
        //            l++;
        //        }
        //        r++;
        //    }
        //}

    }
}
