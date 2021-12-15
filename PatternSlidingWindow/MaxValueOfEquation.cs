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
            Console.WriteLine($"The maximum number of points you can see is {output} and expected value is {expectedOutput}");
        }

        public static int FindMaxValueOfEquation(int[,] points, int k)
        {
           
            return default;
        }

    }
}
