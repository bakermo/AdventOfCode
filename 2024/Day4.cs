using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day4 : IDaySolver
    {
        public string Part1(string[] input)
        {
            int matchCount = 0;
            var rows = input.ToList();
       
            foreach (var row in rows)
            {
                matchCount += Regex.Matches(row, @"XMAS").Count;
                var reversed = new string(row.Reverse().ToArray());
                matchCount += Regex.Matches(reversed, @"XMAS").Count;

            }

            var columns = new List<string>();
            for (int i = 0; i < rows[0].Length; i++)
            {
                var column = string.Empty;
                for (int j = 0; j < rows.Count; j++)
                {
                    column += rows[j][i];
                }
                columns.Add(column.ToString());
            }

            foreach (var column in columns)
            {
                matchCount += Regex.Matches(column, @"XMAS").Count;
                var reversed = new string(column.Reverse().ToArray());
                matchCount += Regex.Matches(reversed, @"XMAS").Count;
            }

            var foursDiagonal = new List<string>();
            var grid = GetGrid(input);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (i + 3 < grid.Length && j + 3 < grid[i].Length)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < 4; k++)
                        {
                            diagonal += grid[i + k][j + k];
                        }
                        foursDiagonal.Add(diagonal);
                    }
                    if (i + 3 < grid.Length && j - 3 >= 0)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < 4; k++)
                        {
                            diagonal += grid[i + k][j - k];
                        }
                        foursDiagonal.Add(diagonal);
                    }
                }
            }

            foreach (var diagonal in foursDiagonal)
            {
                if (diagonal == "XMAS")
                {
                    matchCount++;
                }

                var resversed = new string(diagonal.Reverse().ToArray());
                if (resversed == "XMAS")
                {
                    matchCount++;
                }
            }

            return matchCount.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = GetGrid(input);
            var crosses = new List<Cross>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (i + 1 < grid.Length && j + 1 < grid[i].Length && i - 1 >= 0 && j - 1 >= 0)
                    {
                        var left = string.Concat(grid[i - 1][j - 1], grid[i][j], grid[i + 1][j + 1]);
                        var right = string.Concat(grid[i + 1][j - 1], grid[i][j], grid[i - 1][j + 1]);
                        var cross = new Cross()
                        {
                            Row = i,
                            Column = j,
                            Left = left,
                            Right = right
                        };
                        crosses.Add(cross);
                    }
                }
            }

            int xmasCount = 0;
            foreach (var cross in crosses)
            {
                var left = cross.Left;
                var leftReverse = new string(left.Reverse().ToArray());
                var right = cross.Right;
                var rightReverse = new string(right.Reverse().ToArray());

                if ((left == "MAS" || leftReverse == "MAS") && (right == "MAS" || rightReverse == "MAS"))
                {
                    xmasCount++;
                }
            }
            return xmasCount.ToString();
        }

        private char[][] GetGrid(string[] input)
        {
            var grid =  new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                grid[i] = input[i].ToCharArray();
            }

            return grid;
        }

        private class Cross
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
        }
    }
}
