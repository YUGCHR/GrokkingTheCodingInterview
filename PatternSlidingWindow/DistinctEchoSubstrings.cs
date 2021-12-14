using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Distinct Echo Substrings
// Return the number of distinct non-empty substrings of text that can be written as the concatenation of some string with itself
// (i.e. it can be written as a + a where a is some string).

namespace PatternSlidingWindow
{
    public static class DistinctEchoSubstrings
    {
        public static void MainDistinctEchoSubstrings()
        {
            Console.WriteLine("Class - Distinct Echo Substrings");

            string input = "abcabcabc";
            //string input = "leetcodeleetcode";
            int expectedOutput = 3;
            //int expectedOutput = 2;

            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Started string = {input}");

            int output = FindDistinctEchoSubstrings(input);

            //string outputToString = String.Join(", ", output);
            Console.WriteLine($"The number of distinct non-empty substrings is {output} and expected value is {expectedOutput}");
        }

        public static int FindDistinctEchoSubstrings(string input)
        {
            int output = 0;
            List<string> foundMatches = new();
            // расширяем окно от первой буквы до первой встреченной такой же буквы (как первой) - физически всё время держим подстроку, равную окну
            // играет роль только полное повторение подстроки, что сразу и проверяем - берём подстроку той же длины, начинающуюся с той же буквы и проверяем на совпадение
            // если совпали, то +1
            // когда добрались до половины и не встретили нужной буквы или опять не совпало (или совпало много раз), надо двигать левый край - переходим ко второй букве
            // собственно, первую букву ищем, пока не останется кусок меньше, чем набран, потом точно так же со второй буквой

            int inLength = input.Length;
            StringBuilder win = new();
            int winLenght = 0;

            Console.WriteLine($"win substring is {win}");

            for (int n = 0; n < inLength; n++)
            {
                char searchChar = input[n];
                win.Append(searchChar);
                winLenght++;
                int i = n;
                int reminder = inLength - n;
                Console.WriteLine($">>> FOR >>> Step {n}, search char is {searchChar}, win substring is {win}, length = {winLenght}, reminder of the input is {reminder}");

                while (reminder > winLenght)
                {
                    // проверить, что остаток еще больше, чем win, если меньше, то переходим к следующему символу с начала строки
                    // рассматриваем второй символ в сравнении с первым, значит остаток строки = длина - 1
                    reminder--;

                    // берем следуюший символ
                    i++;
                    char theChar = input[i];//.ToString();
                    Console.WriteLine($"--- WHILE --- Step {i}, this char is {theChar}, search char is {searchChar}, win substring is {win}");

                    // сравниваем с актуальным образцом
                    if (theChar == searchChar)
                    {
                        // выделяем подстроку длиной winLenght и начинающуюся с текущего символа
                        string sub2Compate = input.Substring(i, winLenght);
                        bool result = sub2Compate.Equals(win.ToString());
                        Console.WriteLine($"+++ first CHARS are equal +++ win substring is {win}, compared substring is {sub2Compate}, result of the comparing = {result}");

                        // сравниваем подстроки переменной длины, находящиеся рядом друг с другом (до тех пор, пока длина win не станет больше остатка)
                        if (result)
                        {
                            if (!foundMatches.Contains(sub2Compate))
                            {
                                foundMatches.Add(sub2Compate);
                                output++;
                                Console.WriteLine($"*** substrings were equal *** the result was incremented - {win}");
                            }
                        }
                    }

                    // добавляем текущий символ в окно
                    win.Append(theChar);
                    winLenght++;
                    Console.WriteLine($"... ADD this char to ... win substring is {win}, length = {winLenght}");
                }
                // если осталось меньше, чем собрано, сокращаем окно слева, нет, все сначала
                win = new();
                winLenght = 0;
                Console.WriteLine($"XXX RESET win XXX length = {winLenght}");
            }

            return output;
        }

    }
}
