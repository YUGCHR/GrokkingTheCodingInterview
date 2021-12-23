using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 1499. Max Value of Equation
// You are given an array points containing the coordinates of points on a 2D plane, sorted by the x-values,
// where points[i] = [xi, yi] such that xi < xj for all 1 <= i < j <= points.length.
// You are also given an integer k.
// Return the maximum value of the equation yi + yj + |xi - xj| where |xi - xj| <= k and 1 <= i < j <= points.length

namespace PatternSlidingWindow
{
    public static class MaxValueOfEquation
    {
        public static void MainMaxValueOfEquation()
        {
            Console.WriteLine("Class - Max Value of Equation");

            int[,] input = new int[,] { { 1, 3 }, { 2, 0 }, { 5, 10 }, { 6, -10 } };
            int k = 1;
            int expectedOutput = 4;

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

            int output = FindMaxValueOfEquation(input, k);

            //string outputToString = String.Join(", ", output);
            Console.WriteLine($"The maximum value of the equation is {output} and expected value is {expectedOutput}");
        }

        // модуль разницы по х - это фактически приращение dx, то есть, для любого приращения, меньшего К, надо найти максимум приращения по у
        // начинаем скользящее окно с начального значения х и двигаем правую границу, пока приращение меньше К
        // на каждом шагу считаем формулу yi + yj + |xi - xj| и сравниваем с максимумом
        // как только вышли за границу К, начинаем двигать левую границу, пока не вернёмся в К
        // как вернулись, опять двигаем правую

        public static int FindMaxValueOfEquation(int[,] input, int k)
        {
            int x = 0;
            int y = 1;
            int n = input.GetLength(0);
            int m = input.GetLength(1);

            int max = int.MinValue;

            int i = 0;
            int xi = input[i, x];
            int yi = input[i, y];
            Console.WriteLine($"input dims - n(x) = {n}({x}), m(y) = {m}({y}) - xi = {xi}, yi = {yi}");

            int xj = 0;
            int yj = 0;

            // начинаем цикл со следующего значения
            for (int j = 1; j < n; j++)
            {
                xj = input[j, x];
                yj = input[j, y];
                int dx = Math.Abs(xi - xj);
                Console.WriteLine($"--- FOR j = {j}, i = {i}, dx = {dx} ----- xi = {xi}, xj = {xj}, yi = {yi}, yj = {yj}");

                while (dx <= k && i < j)
                {
                    int res = yi + yj + Math.Abs(xi - xj);
                    Console.WriteLine($"   >>> WHILE j = {j}, i = {i}, dx = {dx}, res = {res}, max = {max}, ----- xi = {xi}, xj = {xj}, yi = {yi}, yj = {yj}");

                    if (res > max)
                    {
                        max = res;
                        Console.WriteLine($"      +++ NEW MAX j = {j}, i = {i}, dx = {dx}, res = {res}, max = {max}, ----- xi = {xi}, xj = {xj}, yi = {yi}, yj = {yj}");
                    }

                    i++;
                    xi = input[i, x];
                    dx = xj - xi;
                    Console.WriteLine($"   *** END WHILE j = {j}, i = {i}, dx = {dx}, res = {res}, max = {max}, ----- xi = {xi}, xj = {xj}, yi = {yi}, yj = {yj}");
                }
            }

            return max;
        }

    }
}
