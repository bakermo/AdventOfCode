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

        public static char [][] GetJaggedGrid(string[] input)
        {
            var grid = new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                grid[i] = input[i].ToCharArray();
            }
            return grid;
        }

        public static char[,] GetGrid(string[] input)
        {
            int rows = input.Length;
            int columns = input[0].Length;
            var grid = new char[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    grid[row, col] = input[row][col];
                }
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

        public static List<string> GetDiagonals(char[][] jaggedGrid, int diagonalLength)
        {
            int diagonalSafetyBound = diagonalLength - 1;
            var diagonals = new List<string>();
            for (int i = 0; i < jaggedGrid.Length; i++)
            {
                for (int j = 0; j < jaggedGrid[i].Length; j++)
                {
                    if (i + diagonalSafetyBound < jaggedGrid.Length && j + diagonalSafetyBound < jaggedGrid[i].Length)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += jaggedGrid[i + k][j + k];
                        }
                        diagonals.Add(diagonal);
                    }
                    if (i + diagonalSafetyBound < jaggedGrid.Length && j - diagonalSafetyBound >= 0)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += jaggedGrid[i + k][j - k];
                        }
                        diagonals.Add(diagonal);
                    }
                }
            }
            return diagonals;
        }

        public static List<string> GetDiagonals(char[,] grid, int diagonalLength)
        {
            int diagonalSafetyBound = diagonalLength - 1;
            var diagonals = new List<string>();
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);    
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (row + diagonalSafetyBound < rows && col + diagonalSafetyBound < columns)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += grid[row + k, col + k];
                        }
                        diagonals.Add(diagonal);
                    }
                    if (row + diagonalSafetyBound < rows && col - diagonalSafetyBound >= 0)
                    {
                        var diagonal = string.Empty;
                        for (int k = 0; k < diagonalLength; k++)
                        {
                            diagonal += grid[row + k, col - k];
                        }
                        diagonals.Add(diagonal);
                    }
                }
            }
            return diagonals;
        }


        public static List<Cross> GetCenteredCrosses(char[][] jaggedGrid, int crossLength)
        {
            int safetyBound = crossLength / 2;
            var crosses = new List<Cross>();
            for (int i = 0; i < jaggedGrid.Length; i++)
            {
                for (int j = 0; j < jaggedGrid[i].Length; j++)
                {
                    if (i + safetyBound < jaggedGrid.Length && j + safetyBound < jaggedGrid[i].Length && i - safetyBound >= 0 && j - safetyBound >= 0)
                    {
                        var cross = new Cross()
                        {
                            Row = i,
                            Column = j
                        };

                        for (int k = -safetyBound; k <= safetyBound; k++)
                        {
                            cross.TopToBottom += jaggedGrid[i + k][j + k];
                            cross.BottomToTop += jaggedGrid[i - k][j + k];
                        }
                        crosses.Add(cross);
                    }
                }
            }
            return crosses;
        }

        public static List<Cross> GetCenteredCrosses(char[,] grid, int crossLength)
        {
            int safetyBound = crossLength / 2;
            var crosses = new List<Cross>();

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (row + safetyBound < rows && col + safetyBound < columns && row - safetyBound >= 0 && col - safetyBound >= 0)
                    {
                        var cross = new Cross()
                        {
                            Row = row,
                            Column = col
                        };

                        for (int k = -safetyBound; k <= safetyBound; k++)
                        {
                            cross.TopToBottom += grid[row + k, col + k];
                            cross.BottomToTop += grid[row - k, col + k];
                        }
                        crosses.Add(cross);
                    }
                }
            }
            return crosses;
        }
    }
}
