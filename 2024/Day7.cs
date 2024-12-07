using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day7 : IDaySolver
    {
        public string Part1(string[] input)
        {
            long result = 0;
            foreach (var line in input)
            {
                var parts = line.Split(':');
                var target = long.Parse(parts[0]);
                var numbers = parts[1].Trim().Split(' ').Select(x => int.Parse(x)).ToList();

                bool isPossible = TestOperators(target, numbers);
                Console.WriteLine($"Target: {target} Possible: {isPossible}");
                if (isPossible)
                {
                    result += target;
                }
                if (result < 0)
                {
                    Console.WriteLine("Overflow");
                }
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            return input[0].Length.ToString();
        }

        private bool TestOperators(long target, List<int> numbers)
        {
            int numBits = numbers.Count - 1;
            int max = 1 << numBits;
            for (int i = 0; i < max; i++)
            {
                var operations = new bool[numBits];
                for (int bit = 0; bit < numBits; bit++)
                {
                    bool multiply = (i & (1 << bit)) != 0;
                    operations[bit] = multiply;
                }

                // we start with the first number because
                long result = numbers[0];
                for (int j = 1; j < numbers.Count; j++)
                {
                    bool multiply = operations[j - 1];
                    if (multiply)
                    {
                        result *= numbers[j];
                    }
                    else
                    {
                        result += numbers[j];
                    }

                    
                }
                if (result == target)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
