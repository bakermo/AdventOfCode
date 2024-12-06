namespace AdventOfCode
{
    internal class Position : Tuple<int, int>
    {
        public int Row { get => Item1; }
        public int Column { get => Item2; }
        public Position(int row, int column):base(row, column)
        {
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    internal class Cross
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string TopToBottom { get; set; } = string.Empty;
        public string BottomToTop { get; set; } = string.Empty;
    }

    internal static class Helpers
    {
        public static Direction TurnRight(this Direction direction)
        {
            return (Direction)(((int)direction + 1) % 4);
        }

        public static Direction TurnLeft(this Direction direction)
        {
            return (Direction)(((int)direction + 3) % 4);
        }

        public static Position GetNextPosition(int row, int col, Direction direction)
        {
            if (direction == Direction.Up)
            {
                return new Position(row - 1, col);
            }
            else if (direction == Direction.Right)
            {
                return new Position(row, col + 1);
            }
            else if (direction == Direction.Down)
            {
                return new Position(row + 1, col);
            }

            return new Position(row, col - 1);
        }

        public static Position GetNextPosition(this Position position, Direction direction)
        {
            return GetNextPosition(position.Row, position.Column, direction);
        }

        public static void PrintGrid(this char[,] grid)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            Console.WriteLine();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(grid[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

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

        public static char[,] GetGrid(this string[] input)
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

        public static bool IsInBounds(this char[,] grid, int row, int col)
        {
            return row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);
        }

        public static char[,] CopyGrid(this char[,] originalGrid)
        {
            var rows = originalGrid.GetLength(0);
            var cols = originalGrid.GetLength(1);
            var grid = new char[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < rows; col++)
                {
                    grid[row, col] = originalGrid[row, col];
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
