using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Cross
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string TopToBottom { get; set; } = string.Empty;
        public string BottomToTop { get; set; } = string.Empty;
    }

    internal static class Helpers
    {
        public static string Reverse(string input)
        {
            return new string(input.Reverse().ToArray());
        }

        public static char [][] GetGrid(string[] input)
        {
            var grid = new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                grid[i] = input[i].ToCharArray();
            }
            return grid;
        }

        public static List<string> GetColumns(string[] input)
        {
            var rows = input.ToList();  
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
            return columns;
        }

        public static List<string> GetRows(string[] input)
        {
            return input.ToList();
        }

        public static List<string> GetDiagonals(char[][] grid, int diagonalLength)
        {
            int diagonalSafetyBound = diagonalLength - 1;
            var diagonals = new List<string>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (i + diagonalSafetyBound < grid.Length && j + diagonalSafetyBound < grid[i].Length)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += grid[i + k][j + k];
                        }
                        diagonals.Add(diagonal);
                    }
                    if (i + diagonalSafetyBound < grid.Length && j - diagonalSafetyBound >= 0)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += grid[i + k][j - k];
                        }
                        diagonals.Add(diagonal);
                    }
                }
            }
            return diagonals;
        }


        public static List<Cross> GetCenteredCrosses(char[][] grid, int crossLength)
        {
            int safetyBound = crossLength / 2;
            var crosses = new List<Cross>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (i + safetyBound < grid.Length && j + safetyBound < grid[i].Length && i - safetyBound >= 0 && j - safetyBound >= 0)
                    {
                        var cross = new Cross()
                        {
                            Row = i,
                            Column = j
                        };

                        for (int k = -safetyBound; k <= safetyBound; k++)
                        {
                            cross.TopToBottom += grid[i + k][j + k];
                            cross.BottomToTop += grid[i - k][j + k];
                        }
                        crosses.Add(cross);
                    }
                }
            }
            return crosses;
        }
    }
}
