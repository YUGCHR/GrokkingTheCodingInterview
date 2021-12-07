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

            //int[] input = new int[] { 1, 3, -1, -3, 5, 3, 6, 7 };
            // output 3, 3, 5, 5, 6, 7
            int[] input = new int[] { 1, 9, 3, 1, 1, 1, 1, 1, 1, -1, -3, -3, -3, 9, 9, 9, 9, 9, -5, 3, -6, 7 };
            int k = 3;
            // output 9, 9, 3, 1, 1, 1, 1, 1, 1, -1, -3, 9, 9, 9, 9, 9, 9, 9, 3, 7
            //int k = 5;
            // output 9, 9, 3, 1, 1, 1, 1, 1, 1, 9, 9, 9, 9, 9, 9, 9, 9, 9


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
            int leftFrameSide = 0;

            int[] output = new int[outputLength];
            int outIndex = 0;

            List<int> sortedIndices = new();
            //sortedIndices.Add(0);
                                         
            // first K steps has completed
            bool firstKStepsHasCompleted = false;

            // предположим, что теперь это основной цикл
            // тогда после первых К символов, надо начинать вычитать символы из хвоста
            // точнее, как только окно вырастет до нужного размера, оно должно начать двигаться
            // для этого можно измерять длину окна в конце каждого цикла и, если размер (на 1) больше, чем надо, двигать левую сторону
            // (второй) while для этого не нужен, поскольку всегда двигаем на 1 символ
            // и потом не забыть выделить результат (максимум) текущего окна

            for (int i = 0; i < inputLength; i++)
            {
                bool isWritten = false;
                int n = 0;
                Console.WriteLine($"\n--- CYCLE loop {i} is started, isWritten = {isWritten}, n = {n}, leftFrameSide = {leftFrameSide}");

                while (!isWritten && n < sortedIndices.Count)
                {
                    // проверяем все числа списка и если нашлось меньшее (точнее, если новое оказалось больше), вставляем новое перед найденным
                    // test all the numbers in the list and if found a smaller one (more precisely, if the new one turned out to be larger), insert a new one before the found one
                    int element = input[sortedIndices[n]];
                    if (input[i] > element)
                    {
                        sortedIndices.Insert(n, i);
                        // запись произошла, устанавливаем признак и добавляем индекс из-за удлинившегося списка
                        // the write in has occurred, we set the tag isWritten and add the index due to the lengthened list
                        isWritten = true;
                        n++;
                        Console.WriteLine($"IF input[{i}] = {input[i]} > sortedIndices[{n}] = {sortedIndices[n]}, isWritten = {isWritten}, n={n}");

                    }
                    // will examine the next list element if exist
                    Console.WriteLine($"Cycle n {n} - sortedIndices.Count = {sortedIndices.Count}");
                    n++;
                }
                if (!isWritten)
                {
                    // список закончился, а меньшего елемента так и не нашлось, поэтому добавляем новое число в конец списка
                    // the list is over, but no smaller element was found, so we add a new number to the end of the list
                    sortedIndices.Add(i);

                }
                
                // calculate the length of the window at the end of each cycle
                int windowSize = i - leftFrameSide;
                Console.WriteLine($"calculate the length of the window - windowSize={windowSize} = i={i} - leftFrameSide={leftFrameSide} + 1");

                string sortedIndicesRearyToString = String.Join(", ", sortedIndices);
                Console.WriteLine($"LIST is ready - Cycle {i} - sortedIndices length is {sortedIndices.Count} = {sortedIndicesRearyToString} and k={k}");

                // if the window size has reached the specified K
                // it makes sense only for the first K steps, so then this condition can be disabled
                if (firstKStepsHasCompleted || windowSize == k-1)
                {
                    // take the maximum from the current position of the sliding window - this is the zero item in the list
                    output[outIndex] = input[sortedIndices[0]];

                    // once we got here, the window has already formed and it is no longer need to check its size
                    firstKStepsHasCompleted = true;

                    // move output index for the next result
                    outIndex++;

                    // remove from the list the index of the item to be removed 
                    // since the list is already in descending order, removing any element will not break its ordering (hopefully)
                    // нет! int index = sortedIndices[leftFrameSide]; ключевой момент - надо найти в списке элемент со значением leftFrameSide
                    // find an element in the list with the value leftFrameSide

                    string sortedIndicesRemovedToString = String.Join(", ", sortedIndices);                    
                    Console.WriteLine($"WILL try to remove = {leftFrameSide} from = {sortedIndicesRemovedToString} with length = {sortedIndices.Count} and k = {k}");

                    bool wasRemoved = sortedIndices.Remove(leftFrameSide);

                    sortedIndicesRemovedToString = String.Join(", ", sortedIndices);
                    Console.WriteLine($"WAS REMOVED = {wasRemoved} - Cycle {i} - sortedIndices element was removed and new length is {sortedIndices.Count} = {sortedIndicesRemovedToString} and k={k}");

                    // move the left side forward
                    leftFrameSide++;
                }






                string sortedIndicesToString = String.Join(", ", sortedIndices);
                Console.WriteLine($"Cycle {i} - sortedIndices with length {k} = {sortedIndicesToString}");
            }




            //string sortedIndicesToString = String.Join(", ", sortedIndices);
            //Console.WriteLine($"sortedIndices with length {k} = {sortedIndicesToString}");


            string outputToString = String.Join(", ", output);
            Console.WriteLine($"+++++++++++ SOLUTION IS --> maximums of sliding windows with size {k} are {outputToString}, input string length = {inputLength}, steps count = {stepCounter}");

            return output;
        }


    }
}
