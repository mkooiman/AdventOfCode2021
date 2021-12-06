using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day4
{
    internal static class Day4
    {

        public static void Day4Pt1()
        {
            // Read the input.txt file
            string[] input = File.ReadAllLines(@"../../Day4/input.txt");
            
            // read the first line of input as csv and parse to integers
            var inputArray = input[0].Split(',').Select(int.Parse).ToArray();

            int lineIndex = 1;
            var list = new List<int[][]>();
            while (lineIndex < input.Length)
            {
                lineIndex++;
                var board = new int[5][];
                for (int i = 0; i < 5; i++)
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
                    if (found[0] != -1)
                    {
                        board[found[0]][found[1]] = -1;
                        if (CheckArray(board, found[0], found[1]))
                        {
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

            }
        }  
        
        public static void Day4Pt2()
        {
            // Read the input.txt file
            string[] input = File.ReadAllLines(@"../../Day4/input.txt");
            
            var inputArray = input[0].Split(',').Select(int.Parse).ToArray();


            int lineIndex = 1;
            var list = new List<int[][]>();
            while (lineIndex < input.Length)
            {
                lineIndex++;
                var board = new int[5][];
                for (int i = 0; i < 5; i++)
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
                    if (found[0] != -1)
                    {
                        board[found[0]][found[1]] = -1;
                        if (CheckArray(board, found[0], found[1]))
                        {
                            toRemove.Add(board);
                            if (list.Count == 1)
                            {
                                var result = SumArray(list[0]);
                                Console.WriteLine($"losing board: {list.IndexOf(list[0])}\n"+
                                                  $"losing number: {i}\n" +
                                                  $"Sum: {result}\n" +
                                                  $"Result : {i * result}");
                                break;
                            }
                        }
                    }
                }
                foreach (var board in toRemove)
                {
                    list.Remove(board);
                }

                
            }
        }  
        
        //print a 2d array
        private static void PrintArray(int[][] array)
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
        
        public static int SumArray(int[][] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] != -1)
                    {
                        sum += array[i][j];
                    }
                }
            }
            return sum;
        }

        public static int[] FindNumber(int[][] array, int number)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == number)
                    {
                        return new int[] {i, j};
                    }
                }
            }
            return new int[] {-1, -1};
        }
        
        
        public static bool CheckArray(int[][] array, int column, int row)
        {
            if (array[column].All(i => i == -1))
                return true;
            for (int i = 0; i < 5; i++)
            {
                if (array[i][row] != -1)
                {
                    return false;
                }
            }
            return true;
        }

       public static string[] SplitString(string input)
        {
            var res =  input.Trim().Replace("  "," ").Split(' ');
            return res;
        }
        
    }
}