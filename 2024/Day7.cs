
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
                var numbers = parts[1].Trim().Split(' ').Select(x => long.Parse(x)).ToList();

                bool oldIsPossible = TestOperationsBinaryCounting(target, numbers);
                bool isPossible = TestOperatorsRecursively(target, numbers);
                if (oldIsPossible != isPossible)
                {
                    Console.WriteLine($"Target: {target} Possible: {isPossible} Old: {oldIsPossible}");
                }   
                if (isPossible)
                {
                    result += target;
                }
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            long result = 0;
            foreach (var line in input)
            {
                var parts = line.Split(':');
                var target = long.Parse(parts[0]);
                var numbers = parts[1].Trim().Split(' ').Select(x => long.Parse(x)).ToList();

                //bool isPossible = TestOperationsBinaryCounting(target, numbers);
                bool isPossible = TestOperatorsRecursively(target, numbers, checkConcatentations: true);
                //Console.WriteLine($"Target: {target} Possible: {isPossible}");
                if (isPossible)
                {
                    result += target;
                }
            }

            return result.ToString();
        }

        private bool IsFactor(long target, long number)
        {
            return target % number == 0;
        }

       
        private long GetTruncatedTarget(long target, long numberToTest)
        {
            var numberToTestStr = numberToTest.ToString();
            // if we have 12345 as target and number to test is 45
            // legnth 45 is 2 so we divide by 10 twice
            // each time we drop a digit because int division truncates
            // so like , 12345 / 10 is 1234.5 in decimal but 1234 in int
            // so second iteration is 1234 / 10 = 123
            for (int i = 1; i <= numberToTestStr.Length; i++)
            {
                target /= 10;
            }

            return target;
        }

        private bool CanConcatentate(long left, long right)
        {
            var leftStr = left.ToString();
            var rightStr = right.ToString();

            // if we have like 12345: 123 45, we can cut 45
            // off of 123, so:   123: 123
            return leftStr.Length > rightStr.Length && leftStr.EndsWith(rightStr);
        }

        private bool TestOperatorsRecursively(long target, List<long> numbers, bool checkConcatentations = false)
        {
            /* 
             * for multiply test:
             *      check if the number is a factor of the target first
             *      in this case 27 is factor of 3267 because 3267 / 27 == 121 -> 3267 % 27 == 0
             *      3267: [x] * 27, x = 121, -> 121 : 81 ? 40
             *          121 : 81 x 40 -> 40 is not a factor of 121
             *          so we try adding
             *          121 : 40 + 81 -> 81 : 81 right side is just one value, now we are done.
             *          TRUE because 81 and 81 match
             *          
             * add test would also work on this case: 
             *      this only works if value on left side is greater than right side
             *      [y] : [x}, y > x
             *      3267: [x] + 27, x = 3240, -> 3240 : 81 ? 40
             *          3240 : 81 x 40 -> 40 is a factor of 3240
             *              81 : 81
             *              
             *     
            */

            var numberToTest = numbers.Last();
            if (numbers.Count == 1)
            {
                // we are on the last iteration
                // so either it matches at this point or it doesn't
                return numberToTest == target;
            }
            var newSet = numbers.Take(numbers.Count - 1).ToList();

            if (IsFactor(target, numberToTest) && TestOperatorsRecursively(target / numberToTest, newSet, checkConcatentations))
                return true;

            if (numberToTest < target && TestOperatorsRecursively(target - numberToTest, newSet, checkConcatentations))
                return true;

            if (checkConcatentations)
            {
                var truncatedTarget = GetTruncatedTarget(target, numberToTest);
                if (CanConcatentate(target, numberToTest) && TestOperatorsRecursively(truncatedTarget, newSet, checkConcatentations))
                    return true;
            }

            return false;
        }


        // This is how I initially solved it, but it doesn't work for part 2
        // because it involves 3 operations...trying to figure out looping
        // through all possible combinations of 3 operations broke my sleep
        // deprived and severely over-caffeinated fried senioritis brain
        private bool TestOperationsBinaryCounting(long target, List<long> numbers)
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
                // we will get conflicting results if we init
                // with either 1 or 0, depends if we multiply or add
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
