using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day12 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var grid = input.GetGrid();
            grid.PrintGrid();

            //var unique = GetUniquePlotKeys(grid);

            //foreach (var plot in unique)
            //{
            //    Console.WriteLine($"{plot.Key} at {plot.Value.Row},{plot.Value.Column}");
            //}
            var seenPositions = new HashSet<Position>();
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);   
            var plots = new Dictionary<Position, HashSet<Position>>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var position = new Position(row, col);
                    if (!seenPositions.Contains(position))
                    {
                        var plotMembers = new HashSet<Position>();
                        Traverse(grid, new Position(row, col), plotMembers, seenPositions);
                        plots.Add(position, plotMembers);
                    }
                }
            }

            var result = 0;
            foreach (var plot in plots)
            {
                Console.WriteLine($"start: {plot.Key.Row},{plot.Key.Column} key:");
                Console.WriteLine($"members (area): {plot.Value.Count}");
                int perimeter = 0;
                foreach (var member in plot.Value)
                {
                    perimeter += CountPositionBounds(grid, member);
                }
                Console.WriteLine($"bounds (perimeter): {perimeter}");
                var price = plot.Value.Count * perimeter;
                result += price;

            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = input.GetGrid();
            grid.PrintGrid();

            //var unique = GetUniquePlotKeys(grid);

            //foreach (var plot in unique)
            //{
            //    Console.WriteLine($"{plot.Key} at {plot.Value.Row},{plot.Value.Column}");
            //}
            var seenPositions = new HashSet<Position>();
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            var plots = new Dictionary<Position, HashSet<Position>>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var position = new Position(row, col);
                    if (!seenPositions.Contains(position))
                    {
                        var plotMembers = new HashSet<Position>();
                        Traverse(grid, new Position(row, col), plotMembers, seenPositions);
                        plots.Add(position, plotMembers);
                    }
                }
            }

            var result = 0;
            foreach (var plot in plots)
            {
                //var boundingBox = GetBoundingBox(grid, plot.Value);

                var plotMembers = plot.Value;
                var minRow = plotMembers.Min(r => r.Row);
                var maxRow = plotMembers.Max(r => r.Row);
                var minCol = plotMembers.Min(c => c.Column);
                var maxCol = plotMembers.Max(c => c.Column);
                //var boundingRows = boundingBox.GetLength(0);
                //var boundingCols = boundingBox.GetLength(1);
                int edges = 0;
                for (int row = minRow; row <= maxRow; row++)
                {
                    bool foundUpEdge = false;
                    bool foundDownEdge = false;
                    for (int col = minCol; col <= maxCol; col++)
                    {
                        var position = new Position(row, col);
                        bool localUpEdge = false;
                        bool localDownEdge = false;
                        if (plotMembers.Contains(position))
                        {
                            var up = position.GetNextPosition(Direction.Up);
                            if (!plotMembers.Contains(up))
                            {
                                localUpEdge = true;
                                //this is an edge   
                            }
                      

                            var down = position.GetNextPosition(Direction.Down);
                            if (!plotMembers.Contains(down))
                            {
                                localDownEdge = true;
                                //this is an edge   
                            }
                        }

                        if (localUpEdge == true )
                        {
                            if (foundUpEdge == false)
                            {
                                foundUpEdge = true;
                                edges++;
                            }
                        }
                        else
                        {
                            foundUpEdge = false;
                        }

                        if (localDownEdge == true)
                        {
                            if (foundDownEdge == false)
                            {
                                foundDownEdge = true;
                                edges++;
                            }
                        }
                        else
                        {
                            foundDownEdge = false;
                        }
                    }
                }


                for (int col = minCol; col <= maxCol; col++)

                {
                    bool foundLeftEdge = false;
                    bool foundRightEdge = false;
                    for (int row = minRow; row <= maxRow; row++)
                    {


                        var position = new Position(row, col);
                        bool localLeftEdge = false;
                        bool localRightEdge = false;
                        if (plotMembers.Contains(position))
                        {
                            var left = position.GetNextPosition(Direction.Left);
                            if (!plotMembers.Contains(left))
                            {
                                localLeftEdge = true;
                                //this is an edge   
                            }


                            var right = position.GetNextPosition(Direction.Right);
                            if (!plotMembers.Contains(right))
                            {
                                localRightEdge = true;
                                //this is an edge   
                            }
                        }

                        if (localLeftEdge == true)
                        {
                            if (foundLeftEdge == false)
                            {
                                foundLeftEdge = true;
                                edges++;
                            }
                         
                        }
                        else
                        {
                            foundLeftEdge = false;
                        }

                        if (localRightEdge == true)
                        {
                            if (foundRightEdge == false)
                            {
                                foundRightEdge = true;
                                edges++;
                            }
                        }
                        else
                        {
                            foundRightEdge = false;
                        }
                    }
                   
                }

                


                Console.WriteLine($"start: {plot.Key.Row},{plot.Key.Column} key:");
                Console.WriteLine($"members (area): {plot.Value.Count}");
                //int perimeter = 0;
                //foreach (var member in plot.Value)
                //{
                //    perimeter += CountPositionBounds(grid, member);
                //}
                //Console.WriteLine($"bounds (perimeter): {perimeter}");
                //var price = plot.Value.Count * perimeter;
                Console.WriteLine("edges: {0}", edges); 
                var price = plot.Value.Count * edges;
                result += price;

             
            }

            return result.ToString();
        }


        private Dictionary<char, Position> GetUniquePlotKeys(char[,] grid)
        {
            var unique = new Dictionary<char, Position>();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    var value = grid[row, col];
                    if (!unique.ContainsKey(value)) {
                        // this fails the "can be in multiple regions" though
                        unique.Add(value, new Position(row, col));
                    }
                }
            }
            return unique;
        }


        private void Traverse(char[,] grid, Position startPosition, HashSet<Position> plotMembers, HashSet<Position> seenPositions)
        {
            plotMembers.Add(startPosition);
            var value = grid[startPosition.Row, startPosition.Column];
            var testPositions = new List<Position>();
            testPositions.Add(startPosition.GetNextPosition(Direction.Up));
            testPositions.Add(startPosition.GetNextPosition(Direction.Down));
            testPositions.Add(startPosition.GetNextPosition(Direction.Left));
            testPositions.Add(startPosition.GetNextPosition(Direction.Right));

            foreach (var position in testPositions)
            {
                if (seenPositions.Contains(position) || plotMembers.Contains(position))
                {
                    continue;
                }

                if (grid.IsInBounds(position.Row, position.Column))
                {
                   
                    var testValue = grid[position.Row, position.Column];
                    if (testValue == value)
                    {
                        plotMembers.Add(position);
                        seenPositions.Add(position);
                        Traverse(grid, position, plotMembers, seenPositions);
                    }
                }
            }
        }

        private int CountPositionBounds(char[,] grid, Position position)
        {
            int boundCount = 0;
            var testPositions = new List<Position>();
            var value = grid[position.Row, position.Column];

            testPositions.Add(position.GetNextPosition(Direction.Up));
            testPositions.Add(position.GetNextPosition(Direction.Down));
            testPositions.Add(position.GetNextPosition(Direction.Left));
            testPositions.Add(position.GetNextPosition(Direction.Right));
            
            foreach (var testPosition in testPositions)
            {
                if (grid.IsInBounds(testPosition.Row, testPosition.Column))
                {
                    var testValue = grid[testPosition.Row, testPosition.Column];
                    if (testValue != value)
                    {
                        boundCount++;
                    }
                }
                else
                {
                    boundCount++;
                }
            }
            return boundCount;
        }

        private char[,] GetBoundingBox(char[,] grid, HashSet<Position> plotMembers)
        {
            var minRow = plotMembers.Min(r => r.Row);
            var maxRow = plotMembers.Max(r => r.Row);   
            var minCol = plotMembers.Min(c => c.Column);
            var maxCol = plotMembers.Max(c => c.Column);

            var subSet = new char[maxRow - minRow + 1, maxCol - minCol + 1];

            for (int row = minRow; row <= maxRow; row++)
            {
                for (int col = minCol; col <= maxCol; col++)
                {
                    var position = new Position(row, col);
                    subSet[row, col] = grid[row, col];
                }
            }

            return subSet;
        }
        
        private bool HasEdge(char[,] grid, Position position, Direction direction)
        {
            var value = grid[position.Row, position.Column];
            var borderPosition = position.GetNextPosition(direction);
            if (!grid.IsInBounds(borderPosition.Row, borderPosition.Column))
                return true;
           
            var borderValue = grid[borderPosition.Row, borderPosition.Column];
            if (borderValue != value)
                return true;

            return false;
        }
    }
}
