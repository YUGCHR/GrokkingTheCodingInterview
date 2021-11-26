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
    public class LongestSubstringWithMaximumKDistinctCharactersTests
    {
        [TestMethod()]
        [DataRow("araaci", 2, 4)]
        [DataRow("araaci", 1, 2)]
        [DataRow("cbbebi", 3, 5)]
        [DataRow("cbbebi", 10, 6)]

        public void FindLongestSubstringTest(string input, int k, int expectedLongestSubstringLength)
        {
            int foundLongestSubstring = LongestSubstringWithMaximumKDistinctCharacters.FindLongestSubstring(input, k);

            Assert.AreEqual(expectedLongestSubstringLength, foundLongestSubstring);
        }
    }
}