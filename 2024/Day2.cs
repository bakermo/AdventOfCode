namespace AdventOfCode._2024
{
    internal class Day2 : IDaySolver
    {
        public string Part1(string[] input)
        {
            long result = 0;
            foreach (var line in input)
            {
                var numbers = line.Split(" ");
                var lineNumbers = new List<int>();
                foreach (var number in numbers)
                {
                    lineNumbers.Add(int.Parse(number));
                }
                bool safe = false;
                if (AllDecrease(lineNumbers.ToArray()) || AllIncrease(lineNumbers.ToArray()))
                {
                    // so far, its safe
                    var first = lineNumbers[0];
                    var current = first;
                    for (int i = 1; i < lineNumbers.Count; i++)
                    {
                        int difference = Math.Abs(lineNumbers[i] - lineNumbers[i - 1]);
                        if (difference >= 1 && difference <= 3)
                        {
                            safe = true;
                        }
                        else
                        {
                            safe = false;
                            break;
                        }
                    }
                }

                if (safe)
                {
                    result++;
                }
            }
            return result.ToString();
            
        }

        public string Part2(string[] input)
        {
            long result = 0;
            Queue<Level> levels = new Queue<Level>();
            foreach (var line in input)
            {
                var level = new Level()
                {
                    Input = line,
                    OriginalInput = line,
                    RemoveIndex = -1

                };
                levels.Enqueue(level);
            }


            while (levels.Count > 0)
            {
                var currentLevel = levels.Dequeue();
                var numbers = currentLevel.Input.Split(" ");
                var lineNumbers = new List<int>();
                foreach (var number in numbers)
                {
                    lineNumbers.Add(int.Parse(number));
                }

                bool safe = false;
                bool originalDirection = (lineNumbers[1] - lineNumbers[0]) < 0;
                for (int i = 1; i < lineNumbers.Count; i++)
                {
                    bool newDirection = (lineNumbers[i] - lineNumbers[i - 1]) < 0;
                    int difference = Math.Abs(lineNumbers[i] - lineNumbers[i - 1]);

                    safe = (newDirection == originalDirection) && (difference >= 1 && difference <= 3);
                    if (!safe)
                    {
                        var originalNumbers = currentLevel.OriginalInput.Split(" ");
                        int removeIndex = currentLevel.RemoveIndex + 1;
                        if (removeIndex < originalNumbers.Length)
                        {
                            var newNumbersToTest = new List<int>();
                            foreach (var number in originalNumbers)
                            {
                                newNumbersToTest.Add(int.Parse(number));
                            }
                            newNumbersToTest.RemoveAt(removeIndex);
                            var newLevel = new Level()
                            {
                                Input = string.Join(" ", newNumbersToTest),
                                OriginalInput = currentLevel.OriginalInput,
                                RemoveIndex = removeIndex
                            };
                            levels.Enqueue(newLevel);
                        }
                        break;

                    }
                }

                if (safe)
                {
                    result++;
                }
            }
            return result.ToString();
        }

        internal class Level
        {
            public string Input { get; set; }
            public string OriginalInput { get; set; }
            public int RemoveIndex { get; set; }
        }

        private bool AllDecrease(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < numbers[i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllIncrease(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
