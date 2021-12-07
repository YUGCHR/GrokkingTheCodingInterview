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
    public class SlidingWindowMaximumTests
    {
        [TestMethod()]
        [DataRow(new int[] { 1, 9, 3, 1, 1, 1, 1, 1, 1, -1, -3, -3, -3, 9, 9, 9, 9, 9, -5, 3, -6, 7 }, 3, new int[] { 9, 9, 3, 1, 1, 1, 1, 1, 1, -1, -3, 9, 9, 9, 9, 9, 9, 9, 3, 7 })]
        [DataRow(new int[] { 1, 9, 3, 1, 1, 1, 1, 1, 1, -1, -3, -3, -3, 9, 9, 9, 9, 9, -5, 3, -6, 7 }, 5, new int[] { 9, 9, 3, 1, 1, 1, 1, 1, 1, 9, 9, 9, 9, 9, 9, 9, 9, 9 })]
        [DataRow(new int[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3, new int[] { 3, 3, 5, 5, 6, 7 })]
        [DataRow(new int[] { 9, 7, 1, 5, 2, 8, 6, 0 }, 3, new int[] { 9, 7, 5, 8, 8, 8 })]
        [DataRow(new int[] { 8, 5, 10, 7, 9, 4, 15, 12, 90, 13 }, 4, new int[] { 10, 10, 10, 15, 15, 90, 90, })]
        [DataRow(new int[] { 1 }, 1, new int[] { 1 })]
        [DataRow(new int[] { 1, -1 }, 1, new int[] { 1, -1 })]
        [DataRow(new int[] { 9, 11 }, 2, new int[] { 11 })]
        [DataRow(new int[] { 4, -2 }, 2, new int[] { 4 })]

        public void FindSlidingWindowMaximumTest(int[] input, int k, int[] expectedOutput)
        {
            int[] foundOutput = SlidingWindowMaximum.FindSlidingWindowMaximum(input, k);
            //int[] foundOutput = SlidingWindowMaximum.FindSlidingWindowMaximumOriginal(input, k);

            CollectionAssert.AreEqual(expectedOutput, foundOutput);
        }
    }
}