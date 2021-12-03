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
    public class MinimumWindowSubstringTests
    {
        [TestMethod()]
        [DataRow("ADOBECODEBANC", "ABC", "BANC")]
        [DataRow("a", "a", "a")]
        [DataRow("c", "a", "")]
        [DataRow("aa", "a", "a")]
        [DataRow("ac", "a", "a")]
        [DataRow("bb", "bb", "bb")]
        [DataRow("bbb", "bb", "bb")]
        [DataRow("ccc", "ccc", "ccc")]
        [DataRow("cccc", "ccc", "ccc")]
        [DataRow("a", "aa", "")]
        [DataRow("ADOBECODEBANC", "AABC", "ADOBECODEBA")]
        [DataRow("ADOBECODEBANC", "ABBC", "BECODEBA")]
        [DataRow("ADOBECODEBANC", "ABCC", "CODEBANC")]
        [DataRow("ADOBECODEBANC", "AABBCC", "ADOBECODEBANC")]
        [DataRow("caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr", "arc", "cira")]
        [DataRow("caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr", "aic", "aci")]
        [DataRow("caaarrraaarrraaaciiiiiiiiiiiiiiiiiicirarr", "aarrcc", "caaarrraaarrraaac")]

        public void FindMinimumWindowSubstringTest(string stringWhereToSearch, string searchingString, string expectedMinSubstring)
        {
            string foundMinSubstring = MinimumWindowSubstring.FindMinimumWindowSubstring(stringWhereToSearch, searchingString);

            Assert.AreEqual(expectedMinSubstring, foundMinSubstring);
        }
    }
}