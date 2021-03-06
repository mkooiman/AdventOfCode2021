using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day04
{
    internal static class Day4
    {

        public static void Day4Pt1()
        {
            // Read the input.txt file
            var input = File.ReadAllLines(@"../../Day04/input.txt");
            
            var inputArray = input[0].Split(',').Select(int.Parse).ToArray();

            var lineIndex = 1;
            var list = new List<int[][]>();
            while (lineIndex < input.Length)
            {
                lineIndex++;
                var board = new int[5][];
                for (var i = 0; i < 5; i++)
                {
                    board[i] = SplitString(input[lineIndex])
                        .Select(int.Parse)
                        .ToArray();
                    
                    lineIndex += 1;
                }
                list.Add(board);
            }
            Console.WriteLine($"{list.Count}");
            
            foreach (var i in inputArray)
            {
                foreach (var board in list)
                {
                    var found = FindNumber(board,i);
                    
                    if (found[0] == -1) 
                        continue;
                    
                    board[found[0]][found[1]] = -1;
                    
                    if (!CheckArray(board, found[0], found[1])) 
                        continue;
                    
                    PrintArray(board);
                    Console.WriteLine();
                            
                    var sum = SumArray(board);
                    Console.WriteLine($"Winning board: {list.IndexOf(board)}\n"+
                                      $"Winning number: {i}\n" +
                                      $"Sum: {sum}\n" +
                                      $"Result : {i * sum}");
                    return;
                }

            }
        }  
        
        public static void Day4Pt2()
        {
            // Read the input.txt file
            var input = File.ReadAllLines(@"../../Day04/input.txt");
            
            var inputArray = input[0].Split(',').Select(int.Parse).ToArray();


            var lineIndex = 1;
            var list = new List<int[][]>();
            while (lineIndex < input.Length)
            {
                lineIndex++;
                var board = new int[5][];
                for (var i = 0; i < 5; i++)
                {
                    board[i] = SplitString(input[lineIndex])
                        .Select(int.Parse)
                        .ToArray();
                    
                    lineIndex += 1;
                }
                list.Add(board);
            }
            Console.WriteLine($"{list.Count}");
            
            foreach (var i in inputArray)
            {
                var toRemove = new List<int[][]>();
                foreach (var board in list)
                {
                    var found = FindNumber(board,i);
                    
                    if (found[0] == -1) 
                        continue;
                    
                    board[found[0]][found[1]] = -1;
                    
                    if (!CheckArray(board, found[0], found[1]))
                        continue;
                    
                    toRemove.Add(board);
                    
                    if (list.Count != 1)
                        continue;
                    
                    var result = SumArray(list[0]);
                    
                    Console.WriteLine($"losing board: {list.IndexOf(list[0])}\n"+
                                      $"losing number: {i}\n" +
                                      $"Sum: {result}\n" +
                                      $"Result : {i * result}");
                    break;
                }
                foreach (var board in toRemove)
                {
                    list.Remove(board);
                }

                
            }
        }  
        
        private static void PrintArray(IEnumerable<int[]> array)
        {
            foreach (var i in array)
            {
                foreach (var j in i)
                {
                    Console.Write($"{j} ");
                }
                Console.WriteLine();
            }
        }
        
        private static int SumArray(IEnumerable<int[]> array)
        {
            return array.Sum(t => t.Where(t1 => t1 != -1).Sum());
        }

        private static int[] FindNumber(IReadOnlyList<int[]> array, int number)
        {
            for (var i = 0; i < array.Count; i++)
            {
                for (var j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == number)
                    {
                        return new [] {i, j};
                    }
                }
            }
            return new [] {-1, -1};
        }


        private static bool CheckArray(IReadOnlyList<int[]> array, int column, int row)
        {
            if (array[column].All(i => i == -1))
                return true;
            for (var i = 0; i < 5; i++)
            {
                if (array[i][row] != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private static IEnumerable<string> SplitString(string input)
        {
            return input.Trim().Replace("  ", " ").Split(' ');
        }
        
    }
}