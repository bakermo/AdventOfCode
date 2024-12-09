using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day9 : IDaySolver
    {
        public string Part1(string[] input)
        {
            //var grid = input.GetGrid();
            //grid.PrintGrid();
            //return input[0];
            //string fileStream = "12345";
            //string fileStream = "2333133121414131402";
            string fileStream = input[0];
            var fileBlocks = new List<FileBlock>();
            for (int i = 0; i < fileStream.Length; i+= 2)
            {
                var blockSize = int.Parse(fileStream[i].ToString());
                var freeSpace = i + 1 < fileStream.Length ? int.Parse(fileStream[i + 1].ToString()) : 0;


                var fileBlock = new FileBlock
                {
                    Id = i / 2,
                     //Id = (char)(i / 2),
                    //Id = (char)('A' + (i / 2)),
                    BlockSize = blockSize, 
                    FreeSpace = freeSpace 
                };
                fileBlocks.Add(fileBlock);
            }

            //var blockMapBuilder = new StringBuilder();

            int indexOfLeftMostFreeSpace = fileBlocks.First().BlockSize;
            //for (int i = 0; i < fileBlocks.Count; i++)
            //{
            //    var fileBlock = fileBlocks[i];
            //    if (i == 0)
            //    {
            //        indexOfLeftMostFreeSpace = fileBlock.BlockSize;
            //    }
            //    for (int j = 0; j < fileBlock.BlockSize; j++)
            //    {
            //        blockMapBuilder.Append(fileBlock.Id);
            //    }
            //    for (int j = 0; j < fileBlock.FreeSpace; j++)
            //    {
            //        blockMapBuilder.Append('.');
            //    }
            //}

            //var blockMap = blockMapBuilder.ToString();
            //File.WriteAllText("output.txt", blockMap);
            //Console.WriteLine(blockMap);
            //var blockMapDict = new Dictionary<int, char>();
            //for (int i = 0; i < blockMap.Length; i++)
            //{
            //    blockMapDict.Add(i, blockMap[i]);
            //}

            var stack = new Stack<(int, int)>();
            var values = new List<int>();
            int counter = 0;

            foreach (var fileBlock in fileBlocks)
            {
                
                for (int j = 0; j < fileBlock.BlockSize; j++)
                {
                    stack.Push((counter, fileBlock.Id));
                    values.Add(fileBlock.Id);
                    counter++;

                }

                for (int i = 0; i < fileBlock.FreeSpace; i++)
                {
                    //stack.Push(-1);
                    values.Add(-1);
                    counter++;
                }
            }

            var indexOfSpaces = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == -1)
                {
                    indexOfSpaces.Add(i);
                }
            }

            var valueArray = values.ToArray();
            //int lastIndex = valueArray.Length - 1;
            foreach (var spaceIndex in indexOfSpaces)
            {
                if (IsComplete(valueArray, spaceIndex))
                {
                    break;
                }
                if (stack.Count > 0)
                {
                    var popped = stack.Pop();
                    valueArray[spaceIndex] = popped.Item2;
                    valueArray[popped.Item1] = -1;
                    //lastIndex--;
                }
            }

            //for (int i = 0; i < valueArray.Length; i++)
            //{
            //    var value = valueArray[i];
            //    if (value == -1 && stack.Count > 0)
            //    {
            //        var popped = -1;
            //        while (stack.Count > 0 && popped < 0)
            //        {
            //            popped = stack.Pop();
            //            valueArray[lastIndex] = -1;
            //            lastIndex--;
            //        }
            //        if (popped >= 0)
            //        {
            //            valueArray[i] = popped;
            //        }

            //    }
            //}

            long result = 0;
            for (int i = 0; i < valueArray.Length; i++)
            {
                var value = valueArray[i];
                if (value >= 0)
                {
                    result += value * i;
                }
            }
            //var blockMapDict = new Dictionary<int, int>();
            //int index = 0;
            //foreach (var fileBlock in fileBlocks)
            //{
                
            //    for (int j = 0; j < fileBlock.BlockSize; j++)
            //    {
            //        blockMapDict.Add(index, fileBlock.Id);
            //        index++;
            //        stack.Push(fileBlock.Id);

            //    }
            //    for (int j = 0; j < fileBlock.FreeSpace; j++)
            //    {
            //        blockMapDict.Add(index, -1);
            //        index++;
            //        stack.Push(-1);
            //    }
            //}   

            //int indexOfLastChar = blockMapDict.Count - 1; // blockMap[blockMap.Length - 1];
            //while (indexOfLastChar > indexOfLeftMostFreeSpace)
            //    //&& indexOfLeftMostFreeSpace < blockMapDict.Count && indexOfLastChar >= 0)
            //{
            //    var lastChar = blockMapDict[indexOfLastChar];
            //    while (lastChar == -1)
            //    {
            //        indexOfLastChar--;
            //        lastChar = blockMapDict[indexOfLastChar];
            //    }

                
            //    blockMapDict[indexOfLeftMostFreeSpace] = lastChar;
            //    blockMapDict[indexOfLastChar] = -1;

            //    //if (indexOfLastChar < indexOfLeftMostFreeSpace)
            //    //{
            //    //    break;
            //    //}

            //    //indexOfLeftMostFreeSpace++;
            //    var freeSpace = blockMapDict[indexOfLeftMostFreeSpace];
            //    while (freeSpace != -1 )
            //    {
            //        indexOfLeftMostFreeSpace++;
            //        freeSpace = blockMapDict[indexOfLeftMostFreeSpace];
            //    }

            //    //for (int i = 0; i < blockMapDict.Count; i++)
            //    //{
            //    //    Console.Write(blockMapDict[i]);
            //    //}
            //    //Console.WriteLine();
            //}

            ////var newBuilder = new StringBuilder();
            ////for (int i = 0; i < blockMap.Length; i++)
            ////{
            ////    newBuilder.Append(blockMapDict[i]);
            ////}

            ////var newBlockMap = newBuilder.ToString();
            ////File.WriteAllText("output.txt", newBlockMap);

            ////Console.WriteLine(newBlockMap);
            //BigInteger result = 0;
            //bool foundPeriod = false;
            //for (int i = 0; i < blockMapDict.Count; i++)
            //{
            //    var value = blockMapDict[i];
            //    if (value != -1)
            //    {
            //        if (foundPeriod)
            //        {
            //            Console.WriteLine($"Found period at {i}");
            //        }
            //        var before = result;
            //        var parsed = value;
            //        //var parsed = (int)(value - 'A');//char.Parse(value);
            //        result += (parsed * i);
            //        //Console.WriteLine($"{before} += {value} * {i} = {BigInteger.Parse(value) * i} RESULT: {result}");
            //    }
            //    else
            //    {
            //        foundPeriod = true;
            //    }
            //}


            Console.WriteLine();
            // 90542694500
            // 90542694500 is wrong
            // 6363692947304 is too low
            // 6367087069671 is wrong too
            //File.WriteAllText("output.txt", result.ToString());
            return result.ToString();
        }

        private bool IsComplete(int[] values, int startIndex)
        {
            for (int i = startIndex; i < values.Length; i++)
            {
                if (values[i] != -1)
                {
                    return false;
                }
            }
            return true;
        }

        public string Part2(string[] input)
        {
            string fileStream = input[0];
            var fileBlocks = new List<FileBlock>();
            for (int i = 0; i < fileStream.Length; i += 2)
            {
                var blockSize = int.Parse(fileStream[i].ToString());
                var freeSpace = i + 1 < fileStream.Length ? int.Parse(fileStream[i + 1].ToString()) : 0;


                var fileBlock = new FileBlock
                {
                    Id = i / 2,
                    BlockSize = blockSize,
                    FreeSpace = freeSpace
                };
                fileBlocks.Add(fileBlock);
            }

            int counter = 0;

            var linkedList = new LinkedList<FileBlock>();
            foreach (var fileblock in fileBlocks)
            {
                linkedList.AddLast(fileblock);
            }

            var freeSpaceNode = linkedList.First;
            var hasMoveBeenAttempted = new HashSet<int>();
            var blockToMove = linkedList.Last;
            while (blockToMove != null)
            {
                freeSpaceNode = linkedList.First;
                while (freeSpaceNode != null && freeSpaceNode.Value.FreeSpace < blockToMove.Value.BlockSize)
                {
                    freeSpaceNode = freeSpaceNode.Next;
                    if (freeSpaceNode != null && blockToMove == freeSpaceNode)
                    {
                        // we can only move blocks left
                        freeSpaceNode = null;
                        break;
                    }
                }

                var nextBlockToMove = blockToMove.Previous;
               
                if (!hasMoveBeenAttempted.Contains(blockToMove.Value.Id) && freeSpaceNode != null)
                {
                    if (blockToMove.Value.BlockSize <= freeSpaceNode.Value.FreeSpace)
                    {
                        var emptyBlock = new FileBlock
                        {
                            Id = -1,
                            BlockSize = blockToMove.Value.BlockSize,
                            FreeSpace = blockToMove.Value.FreeSpace
                        };
                        linkedList.AddBefore(blockToMove, emptyBlock);
                        blockToMove.Value.FreeSpace = freeSpaceNode.Value.FreeSpace - blockToMove.Value.BlockSize;


                        freeSpaceNode.Value.FreeSpace = 0;
                        var copy = new FileBlock
                        {
                            Id = blockToMove.Value.Id,
                            BlockSize = blockToMove.Value.BlockSize,
                            FreeSpace = blockToMove.Value.FreeSpace
                        };
                        hasMoveBeenAttempted.Add(blockToMove.Value.Id);
                        linkedList.Remove(blockToMove);
                        linkedList.AddAfter(freeSpaceNode, copy);
                    }

                }
                //PrintLinkedList(linkedList);
                blockToMove = nextBlockToMove;

            }

            long result = 0;
            foreach (var fileBlock in linkedList)
            {
                for (int i = 0; i < fileBlock.BlockSize; i++)
                {
                    if (fileBlock.Id >= 0)
                    {
                        result += fileBlock.Id * counter;
                    }
                    counter++;
                }

                for (int i = 0; i < fileBlock.FreeSpace; i++)
                {
                    counter++;
                }
            }

            Console.WriteLine();
            return result.ToString();
        }

        private void PrintLinkedList(LinkedList<FileBlock> fileBlocks)
        {
            var stringBuilder = new StringBuilder();
            foreach (var fileBlock in fileBlocks)
            {
                for (int i = 0; i < fileBlock.BlockSize; i++)
                {
                    if (fileBlock.Id >= 0)
                    {
                        stringBuilder.Append(fileBlock.Id);
                    }
                    else
                    {
                        stringBuilder.Append('.');

                    }
                }

                for (int i = 0; i < fileBlock.FreeSpace; i++)
                {
                    stringBuilder.Append('.');
                }
            }

            Console.WriteLine(stringBuilder.ToString());
        }
    }

    internal class FileBlock
    {
        public int Id { get; set; }
        public int BlockSize { get; set; }
        public int FreeSpace { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
