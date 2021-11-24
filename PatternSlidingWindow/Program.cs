using System;

namespace PatternSlidingWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solution - Grokking the Coding Interview");
            Console.WriteLine("Project - Pattern: Sliding Window\n");

            Console.WriteLine("Class - Smallest Subarray with a given sum");

            int[] input = new int[] { 2, 1, 5, 2, 3, 2 };
            int givenSum = 7;
            int output = SmallestSubarrayWithAGivenSum.FindSmallestSubarray(input, givenSum);
            
            Console.WriteLine($"Smallest Subarray with a given sum {givenSum} is {output}");

        }
    }
}
