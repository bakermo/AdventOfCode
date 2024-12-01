using AdventOfCode;

namespace AdventOfCode._2024
{
    internal class Day1 : IDaySolver
    {
        public long Part1(string[] input)
        {
            long result = 0;
            var tuples = new List<Tuple<int, int>>();
            var leftColumn = new List<int>();
            var rightColumn = new List<int>();
            int lines = 0;
            foreach (var line in input)
            {
                var split = line.Split(" ");
                //Console.WriteLine(split);
                var left = split[0];
                var right = split[split.Length - 1];

                leftColumn.Add(int.Parse(left));
                rightColumn.Add(int.Parse(right));
                //Console.WriteLine(left + " " + right);
                lines++;
            }

            leftColumn = leftColumn.OrderBy(x => x).ToList();
            rightColumn = rightColumn.OrderBy(x => x).ToList();

            for (int i = 0; i < lines; i++)
            {
                result += Math.Abs(leftColumn[i] - rightColumn[i]);

            }
            return result;
        }

        public long Part2(string[] input)
        {
            long result = 0;
            var tuples = new List<Tuple<int, int>>();
            var leftColumn = new List<int>();
            var rightColumn = new List<int>();
            int lines = 0;
            foreach (var line in input)
            {
                var split = line.Split(" ");
                var left = split[0];
                var right = split[split.Length - 1];

                leftColumn.Add(int.Parse(left));
                rightColumn.Add(int.Parse(right));
                //Console.WriteLine(left + " " + right);
                lines++;
            }

            // This is a really shit O(n^2) and we could probably solve with
            // hashsets/dictionaries somehow, but I need first place
            long similarity = 0;
            for (int i = 0; i < lines; i++)
            {
                var left = leftColumn[i];

                for (int j = 0; j < lines; j++)
                {
                    int counter = 0;
                    var right = rightColumn[j];
                    if (left == right)
                    {
                        counter++;
                    }

                    similarity += (left * counter);
                }

            }
            return similarity;
        }

    }
}
