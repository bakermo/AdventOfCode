using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    internal class Day15 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var gridLines = new List<string>();
            var directionLines = new List<string>();
            bool isGrid = true;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    isGrid = false;
                    continue;
                }

                if (isGrid)
                {
                    gridLines.Add(line);
                } 
                else
                {
                    directionLines.Add(line);
                }
            }

            var grid = gridLines.ToArray().GetGrid();
            grid.PrintGrid();
            string directionStream = Regex.Replace(string.Join("", directionLines), @"\t|\n|\r", "");
            
            Console.WriteLine(directionStream);

            var directions = new Queue<char>(directionStream);

            var startPostion = grid.GetStartPositions('@').First();
            var currentPosition = startPostion;
            while (directions.Count > 0)
            {
                Direction direction = Direction.Up;
                var d = directions.Dequeue();
                //Console.WriteLine(d);

                switch (d)
                {
                    case '^':
                        direction = Direction.Up;
                        break;
                    case 'v':
                        direction = Direction.Down;
                        break;
                    case '<':
                        direction = Direction.Left;
                        break;
                    case '>':
                        direction = Direction.Right;
                        break;
                }

                currentPosition = Push(grid, currentPosition, direction);

                //grid.PrintGrid();

            }

            return GetGridScore(grid).ToString();
        }

        public string Part2(string[] input)
        {
            return 0.ToString();
            var gridLines = new List<string>();
            var directionLines = new List<string>();
            bool isGrid = true;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    isGrid = false;
                    continue;
                }

                if (isGrid)
                {
                    var explodedLine = Regex.Replace(line, @"#", "##");
                    explodedLine = Regex.Replace(explodedLine, @"O", "[]");
                    explodedLine = Regex.Replace(explodedLine, @"\.", "..");
                    explodedLine = Regex.Replace(explodedLine, @"@", "@.");
                    gridLines.Add(explodedLine);
                }
                else
                {
                    directionLines.Add(line);
                }
            }

            var grid = gridLines.ToArray().GetGrid();
            grid.PrintGrid();
            string directionStream = Regex.Replace(string.Join("", directionLines), @"\t|\n|\r", "");

            Console.WriteLine(directionStream);

            var directions = new Queue<char>(directionStream);
            foreach (var direction in directionStream)
            {
                //directions.Append(direction);
            }

            var startPostion = grid.GetStartPositions('@').First();
            var currentPosition = startPostion;
            while (directions.Count > 0)
            {
                Direction direction = Direction.Up;
                var d = directions.Dequeue();
                //Console.WriteLine(d);

                switch (d)
                {
                    case '^':
                        direction = Direction.Up;
                        break;
                    case 'v':
                        direction = Direction.Down;
                        break;
                    case '<':
                        direction = Direction.Left;
                        break;
                    case '>':
                        direction = Direction.Right;
                        break;
                }

                currentPosition = Push(grid, currentPosition, direction);

                //grid.PrintGrid();
            }

            return GetGridScore(grid).ToString();
        }

        private Position Push(char[,] grid, Position currentPosition, Direction direction)
        {
            var currentUnit = grid[currentPosition.Row, currentPosition.Column];

            var nextPosition = currentPosition.GetNextPosition(direction);
            if (grid.IsInBounds(nextPosition.Row, nextPosition.Column))
            {
                if (IsLargeBoxLeft(grid, nextPosition))
                {
                    Push(grid, nextPosition, direction);
                    Push(grid, nextPosition.GetNextPosition(Direction.Right), direction);
                }

                if (IsLargeBoxRight(grid, nextPosition))
                {
                    Push(grid, nextPosition, direction);
                    Push(grid, nextPosition.GetNextPosition(Direction.Left), direction);
                }

                if (IsBox(grid, nextPosition))
                {
                    Push(grid, nextPosition, direction);
                }

                if (!IsWall(grid, nextPosition) && !IsBox(grid, nextPosition) && !IsLargeBox(grid, nextPosition))
                {
                    grid[nextPosition.Row, nextPosition.Column] = currentUnit;
                    grid[currentPosition.Row, currentPosition.Column] = '.';
                    currentPosition = nextPosition;
                }
            }
           
            return currentPosition;
        }

        private bool IsWall(char[,] grid, Position position)
        {
            return grid[position.Row, position.Column] == '#';
        }

        private bool IsBox(char[,] grid, Position position)
        {
            var value = grid[position.Row, position.Column];
            return value == 'O';// || value == '[' || value == ']';
        }

        private bool IsLargeBox(char[,] grid, Position position)
        {
            return IsLargeBoxLeft(grid, position) || IsLargeBoxRight(grid, position);
        }

        private bool IsLargeBoxLeft(char[,] grid, Position position)
        {
            var value = grid[position.Row, position.Column];
            return value == '[';
        }

        private bool IsLargeBoxRight(char[,] grid, Position position)
        {
            var value = grid[position.Row, position.Column];
            return value == ']';
        }

        private int GetGridScore(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);
            var sum = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (grid[row, col] == 'O' || grid[row, col] == '[')
                    {
                        sum += (100 * row) + col;
                    }
                }
            }
            return sum;
        }
    }
}
