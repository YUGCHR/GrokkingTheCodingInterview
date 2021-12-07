using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternSlidingWindow
{
    public static class SlidingWindowMaximum
    {
        public static void MainSlidingWindowMaximum()
        {
            Console.WriteLine("Class - Sliding Window Maximum");

            int[] input = new int[] { 1, 3, -1, -3, 5, 3, 6, 7 };
            int k = 3;

            for (int i = 0; i < input.Length; i++)
            {
                Console.Write($" {input[i]},");
            }
            Console.Write("\n");

            for (int i = 0; i < input.Length; i++)
            {
                string blank0 = " ";
                if (input[i] < 0)
                {
                    blank0 = "  ";
                }
                Console.Write($"{blank0}{i},");
            }
            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Sliding window size is {k}");

            int[] output = FindSlidingWindowMaximum(input, k);

            Console.WriteLine($"The sliding window maximums are {output}");
        }

        // считываем положенное (К) количество символов из входного массива
        // сортируя, складываем их индексы в список - в нулевой элемент индекс самого большого элемента и так далее
        // затем делаем шаг, для этого слева индекс надо удалить - находим индекс в списке как ключ и удаляем - список автоматически смыкает ряды
        // справа индекс надо добавить в список, достаем индекс из нулевой ячейки списка, достаём значение из основного, сравниваем - пока не поставим на нужное место
        // вместо простого списка индексов можно взять список пар, где индексы (входного массива) будут ключами, а значения - значениями, но большого смысла вроде бы нет
        // после размещения всех индексов в списке по убыванию (добавлению туда нового в общем случае),
        // засылаем в решение значение из нулевого элемента - достаём индекс нулевого элемента, по нему достаём значение из входного массива и кладём в массив решений

        // тут ещё скользкий момент при удалении и, особенно, при добавлении элемента в список - добавляем в произвольном месте, перед элементом, который оказался меньше того, для которого ищется место
        // честнее было бы сделать метод настоящей просейки с переупорядочиванием массива, но, насколько я понимаю эту жизнь, тип List это делает с недоступным мне изяществом
        public static int[] FindSlidingWindowMaximum(int[] input, int k) // 27 lines
        {
            int stepCounter = 0;
            int inputLength = input.Length;
            int outputLength = inputLength - k + 1; //наверное

            int[] output = new int[outputLength];

            List<int> sortedIndices = new();
            sortedIndices.Add(input[0]); // 1

            for (int i = 1; i < k; i++)
            {
                bool isWritten = false;
                int n = 0;

                while(!isWritten && n < sortedIndices.Count)
                {
                    if (input[i] > sortedIndices[n])
                    {
                        sortedIndices.Insert(n, input[i]);
                        isWritten = true;
                        n++;
                        Console.WriteLine($"IF input[{i}] = {input[i]} > sortedIndices[{n}] = {sortedIndices[n]}, isWritten = {isWritten}");

                    }
                    Console.WriteLine($"Cycle n {n} - sortedIndices.Count = {sortedIndices.Count}");
                    n++;
                }

                if (!isWritten)
                {
                    sortedIndices.Add(input[i]);

                }
                string sortedIndicesToString = String.Join(", ", sortedIndices);
                Console.WriteLine($"Cycle {i} - sortedIndices with length {k} = {sortedIndicesToString}");
            }




            //string sortedIndicesToString = String.Join(", ", sortedIndices);
            //Console.WriteLine($"sortedIndices with length {k} = {sortedIndicesToString}");

            

            Console.WriteLine($"+++++++++++ SOLUTION IS --> maximums of sliding windows with size {k} are {output}, input string length = {inputLength}, steps count = {stepCounter}");

            return output;
        }


    }
}
