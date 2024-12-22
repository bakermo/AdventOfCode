using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day16 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var grid = input.GetGrid();
            grid.PrintGrid();

            var startNode = new Node()
            {
                Position = grid.GetPositionsByValue('S').First(),
                Weight = 0,
                Direction = Direction.Right
            };

            var endPosition = new Node()
            {
                Position = grid.GetPositionsByValue('E').First(),
                Weight = int.MaxValue
            };

            var path = new Dictionary<Position, Position>();

            Direction currentDirection = Direction.Right;

            var validNodes = grid.GetPositionsByValue('.')
                .ToDictionary(x => x, x => (Previous: (Position)null, Distance: int.MaxValue));

            var seen = new HashSet<(Position, Direction)>();

            validNodes.Add(startNode.Position, (null, 0));
            validNodes.Add(endPosition.Position, (null, int.MaxValue));

            var minQueue = new PriorityQueue<Node, int>();

            minQueue.Enqueue(startNode, startNode.Weight);
            while (minQueue.Count > 0)
            {
                //var current = minQueue.Dequeue();
                minQueue.TryDequeue(out var current, out var distance);

                seen.Add((current.Position, current.Direction));
                Console.WriteLine("current position: " + current.Position);
                if (current.Position.Row == endPosition.Position.Row && current.Position.Column == endPosition.Position.Column)
                {
                    //TODO: why the fuck doesn't this respect equality operator?
                    Console.WriteLine("distance: " + distance);
                    break;
                }
                //if (current.Position == endPosition.Position)
                //{
                //    Console.WriteLine("distance: ", distance);
                //    break;
                //}
                // I think I need to figure out what the previous one was from here
                var nodes = new List<Node>();
                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction),
                    Weight = current.Weight + 1,

                    //Weight = 1,
                    Direction = current.Direction
                });

                //nodes.Add(new Node()
                //{
                //    Position = current.Position,
                //    Weight = 1000,
                //    Direction = current.Direction.TurnLeft()
                //});

                //nodes.Add(new Node()
                //{
                //    Position = current.Position,
                //    Weight = 1000,
                //    Direction = current.Direction.TurnRight()
                //});

                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction.TurnLeft()),
                    Weight = current.Weight + 1001,
                    //Weight = 1001,
                    Direction = current.Direction.TurnLeft()
                });

                nodes.Add(new Node()
                {
                    Position = current.Position.GetNextPosition(current.Direction.TurnRight()),
                    Weight = current.Weight + 1001,

                    //Weight = 1001,
                    Direction = current.Direction.TurnRight()
                });

                var currentNodeDistance = validNodes[current.Position].Distance;

                foreach (var node in nodes)
                {
                    if (validNodes.ContainsKey(node.Position))
                    {
                        if (seen.Contains((node.Position, node.Direction)))
                        {
                            continue;
                        }

                        //var newCost = current.Weight + node.Weight;
                        //node.Weight = newCost;
                        minQueue.Remove(node, out _, out _);
                        minQueue.Enqueue(node, node.Weight);

                        //var distance = validNodes[node.Position].Distance;

                        //var cost = currentNodeDistance + node.Weight;

                        //validNodes[node.Position] = (current.Position, cost);
                        ////node.Weight = cost;
                        ////if (path.ContainsKey(current.Position))
                        ////{
                        ////    path[current.Position] = node.Position;
                        ////}
                        ////else
                        ////{
                        ////    path.Add(current.Position, node.Position);
                        ////}

                        //minQueue.Remove(node, out _, out _);
                        //minQueue.Enqueue(node, cost);

                        //if (cost < distance)
                        //{
                        //    validNodes[node.Position] = (current.Position, cost);
                        //    //node.Weight = cost;
                        //    //if (path.ContainsKey(current.Position))
                        //    //{
                        //    //    path[current.Position] = node.Position;
                        //    //}
                        //    //else
                        //    //{
                        //    //    path.Add(current.Position, node.Position);
                        //    //}

                        //    minQueue.Remove(node, out _, out _);
                        //    minQueue.Enqueue(node, cost);
                        //}
                    }
                }

            }

            var currentNode = endPosition.Position;
            //while (currentNode != null && currentNode != startNode.Position)
            //{
            //    grid[currentNode.Row, currentNode.Column] = 'X';
            //    var distance = validNodes[currentNode].Distance;
            //    Console.WriteLine($"({currentNode.Row} {currentNode.Column}) {currentNode} {distance}");
            //    currentNode = validNodes[currentNode].Previous;
            //    //if (path.ContainsKey(currentNode))
            //    //{
            //    //    var next = path[currentNode];
            //    //    grid[currentNode.Row, currentNode.Column] = 'X';
            //    //    currentNode = next;
            //    //}
            //    //else
            //    //{
            //    //    currentNode = null;
            //    //}

            //}

            //foreach (var node in path)
            //{
            //    grid[node.Value.Row, node.Value.Column] = 'X';
            //}

            grid.PrintGrid();


            var result = validNodes[endPosition.Position].Distance;
            // 7029 vs 7036 expected, 7 off
            // 11037 vs 11048 expected, 11 off
            // 101395 too low!
            // 101496/101495 too high!
            // 101391 incorrect (duh because 101395 is too low
            return result.ToString();
        }

        public string Part2(string[] input)
        {
            return input[0].Length.ToString();
        }

        internal class Node
        {
            public Position Position { get; set; }
            public int Weight = int.MaxValue;
            public Direction Direction = Direction.Right;
        }
    }
}
