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
        public static List<double> polarAngles;

        public static void MainMaximumNumberOfVisiblePoints()
        {
            Console.WriteLine("Class - Maximum Number of Visible Points");

            //int[,] input = new int[,] { { 1, 2 }, { 2, 2 }, { 2, 3 }, { 200, 5 }, { 300, 6 }, { 10000, 7 }, { 21, 22 }, { 2, 25 }, { 3, 28 }, { -10, 90 }, { -50, 30 }, { -30, -80 } };
            //int[,] location = new int[,] { { 1, 2 } };
            //int angle = 180;
            //int expectedOutput = 11;

            //int[,] input = new int[,] { { 2, 1 }, { 2, 2 }, { 3, 3 } };
            //int[,] location = new int[,] { { 1, 1 } };
            //int angle = 90;
            //int expectedOutput = 3;

            int[,] input = new int[,] { { 2, 1 }, { 2, 2 }, { 3, 4 }, { 1, 1 } };
            int[] location = new int[] { 1, 1 };
            //int[,] location = new int[,] { { 1, 1 } };
            int angle = 90;
            int expectedOutput = 4;

            //int[,] input = new int[,] { { 1, 0 }, { 2, 1 } };
            //int[,] location = new int[,] { { 1, 1 } };
            //int angle = 13;
            //int expectedOutput = 1;

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

            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Started point = [{location[0]}, {location[1]}], angle = {angle}");
            //Console.WriteLine($"Started point = [{location[0, 0]}, {location[0, 1]}], angle = {angle}");

            int output = FindMaximumNumberOfVisiblePointsOptimal(input, location, angle);
            //int output = FindMaximumNumberOfVisiblePoints(input, location, angle);

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

        public static int FindMaximumNumberOfVisiblePointsOptimal(int[,] points, int[] location, int targetAngle)
        {
            int n = points.GetLength(0);
            if (n == 0)
            {
                return 0;
            }

            int i, j, sameAsBase = 0, dx, dy;
            double angle;

            List<double> angles = new(n); //(n << 1);

            for (i = 0; i < n; i++)
            {
                dx = points[i, 0] - location[0];
                dy = points[i, 1] - location[1];
                if (dx == 0 && dy == 0)
                {
                    ++sameAsBase;
                }
                else
                {
                    angle = Math.Atan2(dy, dx) * 180.0 / Math.PI;
                    angles.Add(angle);
                }
            }

            int result = sameAsBase;

            if (angles.Any())
            {
                angles.Sort();
                n = angles.Count;
                for (i = 0; i < n; i++)
                {
                    angles.Add(angles[i] + 360.0); // for cyclic looping
                }

                n = (n << 1);

                for (i = 0; i + result < n; i++)
                {
                    j = BinarySearch(angles, i, n, angles[i] + targetAngle);
                    result = Math.Max(result, sameAsBase + j - i + 1);
                }
            }

            return result;
        }

        private static int BinarySearch(List<Double> angles, int start, int length, double target)
        {
            int left = start, right = length - 1;
            int mid;
            int result = -1;
            while (left <= right)
            {
                //mid = left + ((right - left) >> 1);
                mid = left + ((right - left) / 2);
                if (angles[mid] <= target)
                {
                    result = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return result;
        }

        public static int FindMaximumNumberOfVisiblePoints(int[,] input, int[,] location, int angle)
        {
            polarAngles = new();

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

            // еще можно сохранять не тяжелые значения, а легкие индексы
            List<double> slidingFrame = new();

            //int leftFrameSide = 0;
            double rightFrameSide = polarAngles[0] + angle;

            // так нельзя делать
            // список окна истощается и теряется - такой вариант возможен
            // надо формировать и двигать окно в едином цикле

            for (int i = 0; i < polarAngles.Count; i++)
            {
                stepCounter++;
                Console.WriteLine($"\n ++++++++ CYCLE FOR started with i = {i}, rightFrameSide = {rightFrameSide}, stepCounter = {stepCounter}");

                // добавляем текущий угол в скользящее окно
                slidingFrame.Add(polarAngles[i]);
                VisualList(slidingFrame, false);

                // если текущий угол больше, чем передний (бывший правый) луч угла обзора,
                // надо посчитать количество углов (вычесть один, как лишний спереди - он уже не попадает),
                // потом удалить самый ранний (бывший левый)
                // обновить переднюю границу угла обзора и идти на следующий круг
                if (polarAngles[i] > rightFrameSide)
                {
                    if (output < slidingFrame.Count - 1)
                    {
                        output = slidingFrame.Count - 1;
                        Console.WriteLine($"\n ****************** New minimum was found = {output}");

                    }

                    // самый ранний угол удаляем в цикле - пока последний угол в скользящем окне не поместится в угол обзора или окно не схлопнется до нуля
                    // меня мазать первым! - сначала проверяем, что есть где проверять (откуда удалять)
                    while (polarAngles[i] > rightFrameSide)
                    {
                        stepCounter++;

                        VisualList(slidingFrame, false);
                        // удаляем левое значение
                        // но нельзя удалять последнее значение - оно не наше (формально не входит в окно) - и к нему надо будет прицепиться, когда все окно схлопнется
                        if (slidingFrame.Count > 1)
                        {
                            slidingFrame.RemoveAt(0);
                            VisualList(slidingFrame, false);
                        }

                        // добавляем угол обзора к новому левому значению
                        // тут уже может не быть нового значения, принадлежащего окну - когда останется одно значение (оно за передним краем окна), оно станет началом нового окна
                        rightFrameSide = slidingFrame[0] + angle;
                        Console.WriteLine($"\n END FOR --- New rightFrameSide={rightFrameSide} = slidingFrame[0]={slidingFrame[0]} + angle={angle}");

                    }
                }
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
