using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day25 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var lines = new List<string>();
            var grids = new List<char[,]>();
            var keys = new List<char[,]>();
            var locks = new List<char[,]>();
            foreach (var line in input)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    lines.Add(line);
                }
                else
                {
                    grids.Add(lines.ToArray().GetGrid());
                    lines.Clear();
                }
            }

            grids.Add(lines.ToArray().GetGrid());
            foreach (var grid in grids)
            {
                if (IsLock(grid))
                {
                    locks.Add(grid);
                }
                else
                {
                    keys.Add(grid);
                }
            }

            int countFits = 0;
            foreach (var k in keys)
            {
                var keyHeights = GetKeyHeights(k);
                foreach (var l in locks)
                {
                    var lockHeights = GetLockHeights(l);
                    if (KeyFits(keyHeights, lockHeights))
                    {
                        countFits++;
                        Console.WriteLine("Key: " + string.Join(", ", keyHeights) + " fits lock: " + string.Join(", ", lockHeights));
                    }
                }
            }

            return countFits.ToString();
        }

        private bool IsLock(char[,] grid)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[0, j] == '.')
                {
                    return false;
                }
            }
            return true;
        }

        public string Part2(string[] input)
        {
            return input[0].Length.ToString();
        }

        private int[] GetLockHeights(char[,] grid)
        {
            var result = new int[5];
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);

            for (int col = 0; col < cols; col++)
            {
                int count = 0;
                // we intentially start at the second row
                for (int row = 1; row < rows; row++)
                {
                    if (grid[row, col] == '#')
                    {
                        count++;
                    }
                    else
                    {
                        result[col] = count;
                        continue;
                    }
                }
               
            }
            
            return result;
        }

        private int[] GetKeyHeights(char[,] grid)
        {
            var result = new int[5];
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int count = 0;
                // we intentially start from the second from bottom 
                // row because the first row would be the lock anyway
                for (int row = rows - 2; row >= 0; row--)
                {
                    if (grid[row, col] == '#')
                    {
                        count++;
                    }
                    else
                    {
                        result[col] = count;
                        continue;
                    }
                }

            }

            return result;
        }

        private bool KeyFits(int[] keyHeights, int[] lockHeights)
        {
            for (int i = 0; i < keyHeights.Length; i++)
            {
                if (keyHeights[i] + lockHeights[i] > 5)
                {
                    return false;
                }
            }

            return true;
        }
    }
}