using System.Diagnostics;

namespace AdventOfCode._2024
{
    internal class Day6 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var grid = input.GetGrid();
            grid.PrintGrid();

            var currPos = GetStartPos(grid);
            var direction = Direction.Up;
            var visited = new HashSet<Position>();
            visited.Add(currPos);
            while (grid.IsInBounds(currPos.Row, currPos.Column))
            {
                var nextPos = currPos.GetNextPosition(direction);
                if (!grid.IsInBounds(nextPos.Row, nextPos.Column))
                {
                    break;
                }

                var space = grid[nextPos.Row, nextPos.Column];
                if (space == '#')
                {
                    direction = direction.TurnRight();
                }
                else
                {
                    grid[currPos.Row, currPos.Column] = 'X';
                    currPos = nextPos;
                    visited.Add(currPos);
                }
            }

            grid.PrintGrid();

            return visited.Count.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = input.GetGrid();
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            int count = 0;

            Console.WriteLine($"Rows: {rows} Cols: {cols}");
            int totalSearchSpace = rows * cols;
            Console.WriteLine($"Total Search Space: {totalSearchSpace}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int iterations = 0;
            int lastPercentage = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    iterations++;
                    var percentage = (int)(((double)iterations / totalSearchSpace) * 100);
                    if (percentage % 10 == 0 && percentage != lastPercentage)
                    {
                        lastPercentage = percentage;
                        Console.WriteLine($"Search space completed: {percentage}%");
                    }

                    var space = grid[row, col];
                    if (space != '#' && space != '^')
                    {
                        var copiedGrid = grid.CopyGrid();
                        copiedGrid[row, col] = '0';
                        if (TestMap(copiedGrid))
                        {
                            count++;
                        }
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");
            return count.ToString();
        }

        private bool TestMap(char[,] grid)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            var currentPos = GetStartPos(grid);
            var direction = Direction.Up;
            var boardCounts = new Dictionary<char[,], int>();
            while (grid.IsInBounds(currentPos.Row, currentPos.Column))
            {
                var nextPos = currentPos.GetNextPosition(direction);
                if (!grid.IsInBounds(nextPos.Row, nextPos.Column))
                {
                    break;
                }

                var space = grid[nextPos.Row, nextPos.Column];
                if (space == '#' || space == '0')
                {
                    direction = direction.TurnRight();
                }
                else
                {
                    grid[currentPos.Row, currentPos.Column] = 'X';
                    currentPos = nextPos;
                }
                if (boardCounts.ContainsKey(grid))
                {
                    boardCounts[grid]++;
                    if (boardCounts[grid] > rows * cols)
                    {
                        // we've been trying forever
                        return true;
                    }

                }
                else
                {
                    boardCounts.Add(grid, 1);
                }
            }

            return false;
        }

        private Position GetStartPos(char[,] grid)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row, col] =='^')
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null; // guess we're just fucked here
        }
    }
}
