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
            //grid.PrintGrid();
            string directionStream = Regex.Replace(string.Join("", directionLines), @"\t|\n|\r", "");
            
            //Console.WriteLine(directionStream);

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

                currentPosition = PushBoxes(grid, currentPosition, direction);

                //grid.PrintGrid();

            }

            return GetGridScore(grid).ToString();
        }

        public string Part2(string[] input)
        {
            //1473203 // too high
            //return 0.ToString();
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

            var startPostion = grid.GetStartPositions('@').First();
            var currentPosition = startPostion;
            while (directions.Count > 0)
            {
                Direction direction = Direction.Up;
                var d = directions.Dequeue();
               // Console.WriteLine(d);
               // Console.ReadLine();
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

                currentPosition = PushLargeBoxes(grid, currentPosition, direction);

              // grid.PrintGrid();
            }

            grid.PrintGrid();

            return GetGridScore(grid).ToString();
        }

        private Position PushLargeBoxes(char[,] grid, Position currentPosition, Direction direction, Position? tetheredPosition = null)
        {
            var currentUnit = grid[currentPosition.Row, currentPosition.Column];
            char? tetheredUnit = null;

            var nextPosition = currentPosition.GetNextPosition(direction);
            Position nextTetheredPosition = null;
            if (tetheredPosition != null)
            {
                nextTetheredPosition = tetheredPosition.GetNextPosition(direction);
                tetheredUnit = grid[tetheredPosition.Row, tetheredPosition.Column];
            }

            if (grid.IsInBounds(nextPosition.Row, nextPosition.Column))
            {
                if (IsLargeBoxLeft(grid, nextPosition))
                {
                    if (direction == Direction.Up || direction == Direction.Down)
                    {
                        var currentAdjacentPosition = nextPosition.GetNextPosition(Direction.Right);
                        //var updatedAdjacentPosition = PushLargeBoxes(grid, currentAdjacentPosition, direction);
                        //if (currentAdjacentPosition != updatedAdjacentPosition)
                        //{
                        //    PushLargeBoxes(grid, nextPosition, direction);
                        //}
                        PushLargeBoxes(grid, nextPosition, direction, currentAdjacentPosition);
                        //if (nextTetheredPosition != null)
                        //{
                        //    PushLargeBoxes(grid, nextTetheredPosition, direction);
                        //}
                    }
                    else
                    {
                        PushLargeBoxes(grid, nextPosition, direction);
                    }
                }


                if (IsLargeBoxRight(grid, nextPosition))
                {
                    if (direction == Direction.Up || direction == Direction.Down)
                    {
                        var currentAdjacentPosition = nextPosition.GetNextPosition(Direction.Left);
                        //var updatedAdjacentPosition = PushLargeBoxes(grid, currentAdjacentPosition, direction);
                        //if (currentAdjacentPosition != updatedAdjacentPosition)
                        //{
                        //    PushLargeBoxes(grid, nextPosition, direction);
                        //}
                        PushLargeBoxes(grid, nextPosition, direction, currentAdjacentPosition);
                        //if (nextTetheredPosition != null)
                        //{
                        //    PushLargeBoxes(grid, nextTetheredPosition, direction);
                        //}
                    }
                    else
                    {
                        PushLargeBoxes(grid, nextPosition, direction);
                    }
                }
                
                if (nextTetheredPosition != null)
                {
                    if (IsLargeBoxLeft(grid, nextTetheredPosition))
                    {
                        if (direction == Direction.Up || direction == Direction.Down)
                        {
                            var currentAdjacentPosition = nextTetheredPosition.GetNextPosition(Direction.Right);
                            //var updatedAdjacentPosition = PushLargeBoxes(grid, currentAdjacentPosition, direction);
                            //if (currentAdjacentPosition != updatedAdjacentPosition)
                            //{
                            //    PushLargeBoxes(grid, nextPosition, direction);
                            //}
                            PushLargeBoxes(grid, nextTetheredPosition, direction, currentAdjacentPosition);
                            //if (nextTetheredPosition != null)
                            //{
                            //    PushLargeBoxes(grid, nextTetheredPosition, direction);
                            //}
                        }
                        else
                        {
                            PushLargeBoxes(grid, nextTetheredPosition, direction);
                        }
                    }


                    if (IsLargeBoxRight(grid, nextTetheredPosition))
                    {
                        if (direction == Direction.Up || direction == Direction.Down)
                        {
                            var currentAdjacentPosition = nextTetheredPosition.GetNextPosition(Direction.Left);
                            //var updatedAdjacentPosition = PushLargeBoxes(grid, currentAdjacentPosition, direction);
                            //if (currentAdjacentPosition != updatedAdjacentPosition)
                            //{
                            //    PushLargeBoxes(grid, nextPosition, direction);
                            //}
                            PushLargeBoxes(grid, nextTetheredPosition, direction, currentAdjacentPosition);
                            //if (nextTetheredPosition != null)
                            //{
                            //    PushLargeBoxes(grid, nextTetheredPosition, direction);
                            //}
                        }
                        else
                        {
                            PushLargeBoxes(grid, nextTetheredPosition, direction);
                        }
                    }
                }
                //if (IsBox(grid, nextPosition))
                //{
                //    PushLargeBoxes(grid, nextPosition, direction);
                //}

                if (!IsWall(grid, nextPosition) && (nextTetheredPosition == null || !IsWall(grid, nextTetheredPosition)) && 
                    !IsLargeBox(grid, nextPosition) && (nextTetheredPosition == null || !IsLargeBox(grid, nextTetheredPosition)))
                {
                    grid[nextPosition.Row, nextPosition.Column] = currentUnit;
                    grid[currentPosition.Row, currentPosition.Column] = '.';

                    if (tetheredPosition != null)
                    {
                        grid[nextTetheredPosition.Row, nextTetheredPosition.Column] = tetheredUnit.Value;
                        grid[tetheredPosition.Row, tetheredPosition.Column] = '.';
                    }
                    tetheredPosition = nextTetheredPosition;
                    currentPosition = nextPosition;
                }
            }

            return currentPosition;
        }

        private Position PushBoxes(char[,] grid, Position currentPosition, Direction direction)
        {
            var currentUnit = grid[currentPosition.Row, currentPosition.Column];

            var nextPosition = currentPosition.GetNextPosition(direction);
            if (grid.IsInBounds(nextPosition.Row, nextPosition.Column))
            {
                if (IsBox(grid, nextPosition))
                {
                    PushBoxes(grid, nextPosition, direction);
                }

                if (!IsWall(grid, nextPosition) && !IsBox(grid, nextPosition))
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
