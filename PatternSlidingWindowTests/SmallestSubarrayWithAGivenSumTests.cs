﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternSlidingWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternSlidingWindow.Tests
{
    [TestClass()]
    public class SmallestSubarrayWithAGivenSumTests
    {
        [TestMethod()]
        [DataRow(new int[] { 2, 1, 5, 2, 3, 2 }, 7, 2)]
        [DataRow(new int[] { 2, 1, 5, 2, 8 }, 7, 1)]
        [DataRow(new int[] { 3, 4, 1, 1, 6 }, 8, 3)]
        [DataRow(new int[] { 8, 2, 1, 4, 6 }, 8, 1)]
        [DataRow(new int[] { 3, 4, 1, 5, 3, 1, 9, 1, 2 }, 11, 3)]
        public void FindSmallestSubarrayTest(int[] input, int givenSum, int expectedSmallestSubarrayLength)
        {
            int foundSmallestSubarrayLength = SmallestSubarrayWithAGivenSum.FindSmallestSubarray(input, givenSum);

            Assert.AreEqual(expectedSmallestSubarrayLength, foundSmallestSubarrayLength);
        }

        [TestMethod()]
        [DataRow(new int[] { 2, 1, 5, 2, 3, 2 }, 7, 2)]
        [DataRow(new int[] { 2, 1, 5, 2, 8 }, 7, 1)]
        [DataRow(new int[] { 3, 4, 1, 1, 6 }, 8, 3)]
        [DataRow(new int[] { 8, 2, 1, 4, 6 }, 8, 1)]
        [DataRow(new int[] { 3, 4, 1, 5, 3, 1, 9, 1, 2 }, 11, 3)]
        public void FindSmallestSubarrayWithWhileTest(int[] input, int givenSum, int expectedSmallestSubarrayLength)
        {
            int foundSmallestSubarrayLength = SmallestSubarrayWithAGivenSum.FindSmallestSubarrayWithWhile(input, givenSum);

            Assert.AreEqual(expectedSmallestSubarrayLength, foundSmallestSubarrayLength);
        }
    }
}