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
    public class LongestSubstringWithDistinctCharactersTests
    {
        [TestMethod()]
        [DataRow("abcabcbb", 3, "abc")]
        [DataRow("bbbbb", 1, "b")]
        [DataRow("pwwkew", 3, "wke")]
        [DataRow("", 0, "")]
        [DataRow("caaarrraaarrraaaciiiiiiiiiiiiiiiiiiirrr", 3, "aci")]

        public void FindLongestDistinctSubstringTest(string input, int expectedLongestSubstringLength, string expectedLongestSubstring)
        {
            int foundLongestSubstring = LongestSubstringWithDistinctCharacters.FindLongestDistinctSubstring(input);

            Assert.AreEqual(expectedLongestSubstringLength, foundLongestSubstring);
        }
    }
}