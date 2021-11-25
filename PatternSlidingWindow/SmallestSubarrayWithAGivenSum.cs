using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternSlidingWindow
{
    public static class SmallestSubarrayWithAGivenSum
    {
        public static int FindSmallestSubarrayWithWhile(int[] input, int givenSum)
        {
            PrintArrayInLine(input, givenSum);

            int countedUpSum = 0;
            int minSumLength = int.MaxValue;            
            int leftFrameSide = 0;

            for (int i = 0; i < input.Length; i++)
            {
                countedUpSum += input[i];
                Console.WriteLine($"cycle step {i} - countedUpSum = {countedUpSum}");

                while (countedUpSum >= givenSum)
                {
                    if (i - leftFrameSide + 1 < minSumLength)
                    {
                        Console.WriteLine($"minSumLength was updated {minSumLength} - with new sumLength = {i - leftFrameSide + 1}");
                        minSumLength = i - leftFrameSide + 1;
                    }

                    countedUpSum -= input[leftFrameSide];
                    leftFrameSide++;
                    Console.WriteLine($"while step {leftFrameSide} - countedUpSum = {countedUpSum}");
                }
            }

            Console.WriteLine($"minSumLength was found {minSumLength}");
            return minSumLength == int.MaxValue ? 0 : minSumLength;
        }

        public static void PrintArrayInLine(int[] input, int givenSum)
        {
            if (input.Length < 100)
            {
                // need to limit the length of the printed array
                string joinInputArray = String.Join(", ", input);
                Console.WriteLine($"Array input is {joinInputArray}");
                Console.WriteLine($"Given sum is {givenSum}");
            }
            else
            {
                Console.WriteLine($"Array input is to big to print, length = {input.Length}");
            }
        }

        public static int FindSmallestSubarray(int[] input, int givenSum)
        {
            PrintArrayInLine(input, givenSum);

            //int foundSmallestSubarrayLength = -1; //temp name is output
            int output = -1;

            // check if input[0] >= givenSum            
            if (input[0] >= givenSum)
            {
                output = 1;
                Console.WriteLine($"Smallest Subarray was found on stage 0 - it length is {output}");
                return output;
            }

            // get start sum based on input[0]
            int lastCountedSum = input[0];
            int lastCountedIndex = 0;
            (int countedUpSum, int lastTakenIndex) = CoutSubarraySum(lastCountedSum, input, lastCountedIndex, givenSum);

            int currentSum = countedUpSum;
            int currentSumLength = lastTakenIndex + 1; //because start index = 0
            output = currentSumLength;
            lastCountedIndex = lastTakenIndex; // new start index will be the last taken index in CoutSubarraySum method

            for (int i = 0; i < input.Length; i++)
            {
                // move the left side of the sliding window on one step and decrement sum lehgth
                currentSum -= input[i];
                currentSumLength--;
                Console.WriteLine($"move the left side of the sliding window - subtract input[{i}] {input[i]}, now currentSum is {currentSum} and its length = {currentSumLength}");

                if (currentSum < givenSum)
                {
                    Console.WriteLine($"currentSum {currentSum} < givenSum {givenSum}, CoutSubarraySum will be called with lastCountedIndex = {lastCountedIndex}");

                    (countedUpSum, lastTakenIndex) = CoutSubarraySum(currentSum, input, lastCountedIndex, givenSum);

                    Console.WriteLine($"!!! where is it ???");

                    // need to check if new sum was found
                    if (countedUpSum < 0)
                    {
                        // if new sum was NOT found, the output is minimal and will be returned as a result
                        Console.WriteLine($"currentSum {currentSum} is negative, the search is finished and the result {output} will be returned");
                        return output;
                    }

                    currentSum = countedUpSum;
                    // add to the sum length steps count in CoutSubarraySum method
                    currentSumLength += lastTakenIndex - lastCountedIndex;
                    Console.WriteLine($"new currentSum is {currentSum} and the new its lenght is {currentSumLength}");

                    lastCountedIndex = lastTakenIndex;

                }
                // compare new sum length with minimal saved and upgrade if needed
                if (currentSumLength < output)
                {
                    Console.WriteLine($"compare new sum length {currentSumLength} with minimal saved {output} and upgrade it");
                    output = currentSumLength;
                }
                // if sum still more then given one, do new cycle loop (and subtract next element from left side)
            }

            // sum of all array is less then given sum, so 0 will be returned as a result
            Console.WriteLine($"unfortunately, sum {currentSum} of all array is less then given sum {givenSum}, so 0 will be returned as a result");
            return 0;
        }

        public static (int, int) CoutSubarraySum(int lastCountedSum, int[] input, int lastCountedIndex, int givenSum)
        {
            int sum = lastCountedSum;
            int startIndexToAdd = lastCountedIndex + 1;
            Console.WriteLine($"method CoutSubarraySum started with lastCountedSum {lastCountedSum}, lastCountedIndex {lastCountedIndex} and givenSum {givenSum}");

            for (int j = startIndexToAdd; j < input.Length; j++)
            {
                sum += input[j];
                Console.WriteLine($"input[{j}] was added to sum and it is {sum} now");

                if (sum >= givenSum)
                {
                    Console.WriteLine($"method CoutSubarraySum counted up sum {sum} >= givenSum {givenSum} and will be returned with last used index {j}");
                    return (sum, j);
                }
            }

            Console.WriteLine($"smallest subarray was NOT found in array, (-1, -1) will be returned");
            return (-1, -1);
        }
    }
}
