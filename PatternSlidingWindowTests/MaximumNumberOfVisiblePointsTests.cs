using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternSlidingWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternSlidingWindow.Tests
{
    [TestClass()]
    public class MaximumNumberOfVisiblePointsTests
    {
        [TestMethod()]
        [DataRow(new int [] { 2, 2, 3 }, new int[] { 1, 2, 3 }, 1, 1, 90, 3)] // { { 2, 1 }, { 2, 2 }, { 3, 3 } };
        [DataRow(new int [] { 2, 2, 3, 1 }, new int[] { 1, 2, 4, 1 }, 1, 1, 90, 4)] // { { 2, 1 }, { 2, 2 }, { 3, 4 }, { 1, 1 } };
        [DataRow(new int [] { 1, 2 }, new int[] { 0, 1 }, 1, 1, 13, 1)] // { { 1, 0 }, { 2, 1 } };
        [DataRow(new int [] { 1, 2, 2, 200, 300, 10000, 21, 2, 3, -10, -50, -30 }, new int[] { 2, 2, 3, 5, 6, 7, 22, 25, 28, 90, 30, -80 }, 1, 2, 180, 11)] // { { 1, 2 }, { 2, 2 }, { 2, 3 }, { 200, 5 }, { 300, 6 }, { 10000, 7 }, { 21, 22 }, { 2, 25 }, { 3, 28 }, { -10, 90 }, { -50, 30 }, { -30, -80 } };

        public void FindMaximumNumberOfVisiblePointsTest(int[] input0, int[] input1, int location0, int location1, int angle, int expectedOutput)
        {
            int length0 = input0.Length;

            int[,] input = new int[length0, 2];
            int[,] location = new int[,] { { location0, location1 } };

            for (int i = 0; i < length0; i++)
            {
                input[i, 0] = input0[i];
                input[i, 1] = input1[i];
                Console.Write($"[{input[i, 0]}, {input[i, 1]}], ");
            }
                Console.WriteLine($"\n---");
                Console.WriteLine($"[{location[0, 0]}, {input[0, 1]}], ");
                Console.WriteLine($" angle = {angle}");

            int output = MaximumNumberOfVisiblePoints.FindMaximumNumberOfVisiblePoints(input, location, angle);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}