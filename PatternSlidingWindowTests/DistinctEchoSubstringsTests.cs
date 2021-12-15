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
    public class DistinctEchoSubstringsTests
    {
        [TestMethod()]
        [DataRow("abcabcabc", 3)]
        [DataRow("leetcodeleetcode", 2)]
        [DataRow("abcabcabcleetcodeleetcode", 5)]
        [DataRow("aba", 0)]
        [DataRow("aa", 1)]

        public void FindDistinctEchoSubstringsTest(string input, int expectedOutput)
        {
            int result = DistinctEchoSubstrings.FindDistinctEchoSubstrings(input);

            Assert.AreEqual(expectedOutput, result);
        }
    }
}