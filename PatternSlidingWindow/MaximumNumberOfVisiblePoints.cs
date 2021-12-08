using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// You are given an array points, an integer angle, and your location, where location = [posx, posy] and points[i] = [xi, yi] both denote integral coordinates on the X-Y plane.
// Initially, you are facing directly east from your position.
// You cannot move from your position, but you can rotate.
// In other words, posx and posy cannot be changed.
// Your field of view in degrees is represented by angle, determining how wide you can see from any given view direction.
// Let d be the amount in degrees that you rotate counterclockwise.
// Then, your field of view is the inclusive range of angles [d - angle/2, d + angle/2].

namespace PatternSlidingWindow
{
    public static class MaximumNumberOfVisiblePoints
    {
        public static int stepCounter = 0;
        public static List<double> polarAngles = new();

        public static void MainMaximumNumberOfVisiblePoints()
        {
            Console.WriteLine("Class - Sliding Window Maximum");

            int[,] input = new int[,] { { 1, 2 }, { 2, 2 }, { 2, 3 }, { 200, 5 }, { 300, 6 }, { 10000, 7 }, { 21, 22 }, { 2, 25 }, { 3, 28 }, { -10, 90 }, { -50, 30 }, { -30, -80 } };
            int[,] location = new int[,] { { 1, 2 } };
            int angle = 30;
            int expectedOutput = 2;

            int length0 = input.GetLength(0);
            int length1 = input.GetLength(1);
            Console.WriteLine($"Array dimention [0, 1] = [{length0}, {length1}]");
            // Array dimention [0, 1] = [3, 2]

            Console.Write($"Input points are - ");
            for (int i = 0; i < length0; i++)
            {
                Console.Write($"[{input[i, 0]}, {input[i, 1]}], ");
            }
            Console.Write("\n");

            //Console.Write($"Dimension 0 indices  ");
            //for (int i = 0; i < length0; i++)
            //{
            //    string blank0 = "        ";
            //    if (input[i, 0] < 0 || input[i, 1] < 0)
            //    {
            //        blank0 = "         ";
            //    }
            //    Console.Write($"{i},{blank0}");
            //}
            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Started point = [{location[0, 0]}, {location[0, 1]}], angle = {angle}");

            int output = FindMaximumNumberOfVisiblePoints(input, location, angle);

            //string outputToString = String.Join(", ", output);
            Console.WriteLine($"The maximum number of points you can see is {output} and expected value is {expectedOutput}");
        }

        public static void VisualList(List<double> list2print, bool needConvertToInt)
        {
            string list2printToString;
            if (needConvertToInt)
            {
                int[] polarAnglesToInt = list2print.Select(x => (int)x).ToArray();
                list2printToString = String.Join(", ", polarAnglesToInt);
            }
            else
            {
                list2printToString = String.Join(", ", list2print);
            }
            Console.WriteLine($"\n polarAngles = {list2printToString}");
        }

        public static int FindMaximumNumberOfVisiblePoints(int[,] input, int[,] location, int angle)
        {
            int output = 0;

            int length0 = input.GetLength(0);
            int length1 = input.GetLength(1);

            Console.WriteLine($"\n---------------------- Solution is started (length0 = {length0}, length1 = {length1}) \n");

            int[,] normalizedInput = new int[length0, length1];
            // пока, временно, для простоты - преобразуем массив точек для места зрения, вычтем из каждой координаты точек координату начальной точки

            for (int i = 0; i < length0; i++)
            {
                stepCounter++;
                Console.WriteLine($"Source point - [{input[i, 0]}, {input[i, 1]}], - start point [{location[0, 0]}, {location[0, 1]}]");

                normalizedInput[i, 0] = input[i, 0] - location[0, 0];
                normalizedInput[i, 1] = input[i, 1] - location[0, 1];

                Console.WriteLine($"Normalized point - [{normalizedInput[i, 0]}, {normalizedInput[i, 1]}], ");

                // преобразуем координаты в полярные - угол и длина относительно начала
                // сразу сортируем по возрастанию - в нулевом минимальный (для разнообразия)
                double polarAngle = Math.Atan2((double)normalizedInput[i, 1], (double)normalizedInput[i, 0]) * 180 / Math.PI;
                if (polarAngle < 0)
                {
                    polarAngle += 360;
                }
                Console.WriteLine($"\n polarAngle = {(int)polarAngle}, y = {normalizedInput[i, 1]}, x = {normalizedInput[i, 0]}");

                // сохраняем полученный угол в список, одновременно сортируя его по возрастанию (нулевой элемент минимальный)
                sortedPolarAngles(polarAngle, angle);
            }
            Console.WriteLine($"\n----------------------");

            VisualList(polarAngles, false);

            // теперь возьмем первый угол за опорный и прибавим поле зрения
            // посмотрим, сколько следущих углов меньше этой суммы
            // сохраним список этих точек в список скользящего окна
            // запомним число в максимальное значение
            // зачем выбросим первую точку
            // возьмем первую из оставшегося списка скользящего окна
            // прибавим к ней поле зрения
            // возьмем следующий угол из сортированного списка и проверим, меньше она текущей суммы или нет
            // если меньше, добавляем ее в список окна и идем за следующим углом
            // если не меньше - больше, то надо вычесть первую точку из окна
            // ----------------------------------------------------------------

            // можно сохранять не тяжелые значения, а легкие индексы
            List<double> slidingFrame = new();

            double startFrame = polarAngles[0] + angle;
            bool isFrameFull = false;
            int n = 0;

            while (!isFrameFull)
            {
                stepCounter++;
                if (startFrame < polarAngles[n])
                {
                    isFrameFull = true;
                }
                else
                {
                    slidingFrame.Add(polarAngles[n]);
                }
                n++;
            }

            VisualList(slidingFrame, false);

            // сформировали список скользящего окна, теперь двигаемся по нему
            // в начале цикла уже известно, что следующий угол не помещается, поэтому можно сразу начинать с удаления первого из окна
            int leftFrameSide = 0;


            // так нельзя делать
            // список окна истощается и теряется - такой вариант возможен
            // надо формировать и двигать окно в едином цикле


            for (int i = slidingFrame.Count; i < polarAngles.Count; i++)
            {
                stepCounter++;

                Console.WriteLine($"\n ++++++++ CYCLE FOR started with i = {i}, leftFrameSide = {leftFrameSide}");
                VisualList(slidingFrame, false);
                // удаляем левое значение
                slidingFrame.RemoveAt(leftFrameSide);

                // добавляем угол к новому левому значению
                double leftFrameSideAngle = slidingFrame[0] + angle;

                // проверяем следующий после правого края окна угол, попадает ли он теперь в окно
                // правый край окна - это i-1

                // это следущий после окна
                int m = i;
                // сравниваем с текущим полем зрения
                while (m < polarAngles.Count && polarAngles[m] < leftFrameSideAngle)
                {
                    stepCounter++;
                    // если меньше, то добавили и идем за следующим углом в следующем цикле
                    // нет, надо достать следующий угол в этом цикле - добавить while
                    slidingFrame.Add(polarAngles[m]);
                    m++;
                }
                // если следующий угол больше, то идем на новый цикл удалять левый угол из окна
                // в этот момент надо измерить длину окна и сравнить с максимальной
                if (output < slidingFrame.Count)
                {
                    output = slidingFrame.Count;
                }
                leftFrameSide++;
            }

            Console.WriteLine($"\n The maximum number of points = {output}, stepCounter = {stepCounter}");

            return output;
        }

        public static void sortedPolarAngles(double polarAngle, double angle)
        {

            bool isWritten = false;
            int n = 0;


            Console.WriteLine($"\n--- CYCLE loop polarAngle = {(int)polarAngle}, isWritten = {isWritten}, n = {n}");

            while (!isWritten && n < polarAngles.Count)
            {
                stepCounter++;
                // проверяем все числа списка и если нашлось меньшее (точнее, если новое оказалось больше), вставляем новое перед найденным
                // test all the numbers in the list and if found a smaller one (more precisely, if the new one turned out to be larger), insert a new one before the found one

                if (polarAngle < polarAngles[n])
                {
                    polarAngles.Insert(n, polarAngle);
                    // запись произошла, устанавливаем признак и добавляем индекс из-за удлинившегося списка
                    // the write in has occurred, we set the tag isWritten and add the index due to the lengthened list
                    isWritten = true;
                    n++;
                    Console.WriteLine($"IF polarAngle = {(int)polarAngle} < polarAngles[{n}] = {polarAngles[n]}, isWritten = {isWritten}, n={n}");

                }
                // will examine the next list element if exist
                Console.WriteLine($"Cycle n {n} - polarAngles.Count = {polarAngles.Count}");
                n++;
            }
            if (!isWritten)
            {
                // список закончился, а меньшего елемента так и не нашлось, поэтому добавляем новое число в конец списка
                // the list is over, but no smaller element was found, so we add a new number to the end of the list
                polarAngles.Add(polarAngle);
            }

            // если входное значение угла меньше угла зрения, то в конец? безусловно добавляем еще одно значение точки + 360
            if (polarAngle < angle)
            {
                polarAngles.Add(polarAngle + 360);
            }
        }
    }
}
