using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day18 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var length = 71;
            //var length = 7;

            //var bytesFalling = 12;
            var bytesFalling = 1024;// 12;

            //var grid = new char[7, 7];
            var grid = new char[length, length];
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = '.';
                }
            }

            grid.PrintGrid();

            for (int i = 0; i < bytesFalling; i++)
            {
                var line = input[i];
                var parts = line.Split(',');
                var col = int.Parse(parts[0]);
                var row = int.Parse(parts[1]);
                grid[row, col] = '#';
            }

            grid.PrintGrid();


            var startNode = new Node()
            {
                Position = new Position(0, 0),
                Weight = 0,
                Direction = Direction.Right
            };


            var endPosition = new Node()
            {
                Position = new Position(length - 1, length -1),
                Weight = int.MaxValue
            };

            var path = new Dictionary<Position, Position>();

            Direction currentDirection = Direction.Right;
            var unvisited = new HashSet<Position>();
            unvisited.Add(startNode.Position);
            unvisited.Add(endPosition.Position);

            var validNodes = grid.GetPositionsByValue('.')
                .ToDictionary(x => x, x => (Previous: (Position)null, Distance: int.MaxValue));


            validNodes[startNode.Position] = (null, 0);

            var minQueue = new PriorityQueue<Node, int>();
            minQueue.Enqueue(startNode, startNode.Weight);
            while (minQueue.Count > 0)
            {
                var current = minQueue.Dequeue();

                // I think I need to figure out what the previous one was from here
                var nodes = new List<Node>();
                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction),
                    Weight = 1,
                    Direction = current.Direction
                });

                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction.TurnLeft()),
                    Weight = 1,
                    Direction = current.Direction.TurnLeft()
                });

                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction.TurnRight()),
                    Weight = 1,
                    Direction = current.Direction.TurnRight()
                });

                var currentNodeDistance = validNodes[current.Position].Distance;

                foreach (var node in nodes)
                {
                    if (validNodes.ContainsKey(node.Position))
                    {
                        var distance = validNodes[node.Position].Distance;

                        var cost = currentNodeDistance + node.Weight;

                        if (cost < distance)
                        {
                            validNodes[node.Position] = (current.Position, cost);
                            minQueue.Remove(node, out _, out _);
                            minQueue.Enqueue(node, cost);
                        }
                    }
                }

            }

            var currentNode = endPosition.Position;
            while (currentNode != null)
            {
                grid[currentNode.Row, currentNode.Column] = 'X';
                var distance = validNodes[currentNode].Distance;
                Console.WriteLine($"({currentNode.Row} {currentNode.Column}) {currentNode} {distance}");
                currentNode = validNodes[currentNode].Previous;
            }


            grid.PrintGrid();


            var result = validNodes[endPosition.Position].Distance;
            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var length = 71;
            //var length = 7;

            //var bytesFalling = 12;
            //var bytesFalling = 1024;// 12;

            //var grid = new char[7, 7];

            var bytesFalling = new List<(int, int)>();
            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                var parts = line.Split(',');
                var col = int.Parse(parts[0]);
                var row = int.Parse(parts[1]);
                //THIS GETS REVERSED AT THE END!
                bytesFalling.Add((row, col));
            }

            var grid = new char[length, length];
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] != '#')
                    {
                        grid[row, col] = '.';
                    }
                }
            }

            foreach (var bf in bytesFalling)
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (grid[row, col] != '#')
                        {
                            grid[row, col] = '.';
                        }
                    }
                }

                grid[bf.Item1, bf.Item2] = '#';

                Console.WriteLine("testing " + bf);
                //grid.PrintGrid();

                var startNode = new Node()
                {
                    Position = new Position(0, 0),
                    Weight = 0,
                    Direction = Direction.Right
                };


                var endPosition = new Node()
                {
                    Position = new Position(length - 1, length - 1),
                    Weight = int.MaxValue
                };

                var path = new Dictionary<Position, Position>();

                Direction currentDirection = Direction.Right;
                var unvisited = new HashSet<Position>();
                unvisited.Add(startNode.Position);
                unvisited.Add(endPosition.Position);

                var validNodes = grid.GetPositionsByValue('.')
                    .ToDictionary(x => x, x => (Previous: (Position)null, Distance: int.MaxValue));

                validNodes[startNode.Position] = (null, 0);

                var minQueue = new PriorityQueue<Node, int>();
                minQueue.Enqueue(startNode, startNode.Weight);
                while (minQueue.Count > 0)
                {
                    var current = minQueue.Dequeue();

                    // I think I need to figure out what the previous one was from here
                    var nodes = new List<Node>();
                    nodes.Add(new Node()
                    {
                        Position = current.Position.GetNextPosition(current.Direction),
                        Weight = 1,
                        Direction = current.Direction
                    });

                    nodes.Add(new Node()
                    {
                        Position = current.Position.GetNextPosition(current.Direction.TurnLeft()),
                        Weight = 1,
                        Direction = current.Direction.TurnLeft()
                    });

                    nodes.Add(new Node()
                    {
                        Position = current.Position.GetNextPosition(current.Direction.TurnRight()),
                        Weight = 1,
                        Direction = current.Direction.TurnRight()
                    });

                    var currentNodeDistance = validNodes[current.Position].Distance;

                    foreach (var node in nodes)
                    {
                        if (validNodes.ContainsKey(node.Position))
                        {
                            var distance = validNodes[node.Position].Distance;

                            var cost = currentNodeDistance + node.Weight;

                            if (cost < distance)
                            {
                                validNodes[node.Position] = (current.Position, cost);
                                minQueue.Remove(node, out _, out _);
                                minQueue.Enqueue(node, cost);
                            }
                        }
                    }

                }

                if (validNodes[endPosition.Position].Distance == int.MaxValue)
                {
                    return $"{bf.Item2},{bf.Item1}";
                }
            }


            return "every value tested";
        }

        private void Run()
        {

        }

        internal class Node
        {
            public Position Position { get; set; }
            public int Weight = int.MaxValue;
            public Direction Direction = Direction.Right;
        }
    }
}
