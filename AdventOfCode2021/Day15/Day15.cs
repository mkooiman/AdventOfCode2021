using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day15
{
    internal sealed class Day15
    {
        private const string TestFile = "../../Day15/input_test.txt";
        private const string RealFile = "../../Day15/input.txt";

        private const string InputFile = RealFile;

        public static void Day15Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            
            var dict = input
                .Select(s => s.ToCharArray().Select(c => c - '0').ToArray())
                .ToArray();
           
            Solve(dict);
        }

       
        
        public static void Day15Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var dict = input
                .Select(s => s.ToCharArray().Select(c => c - '0').ToArray())
                .ToArray();
            var newInput = Make2dArray(dict[0].Length*5, dict.Length*5);
            for (int y = 0; y < 5; y++)
            {
                for( int x =0; x < 5; x++)
                {
                    Copy2DArray(dict, newInput, dict.Length * x,dict.Length*y,x+y );
                }
            }
            
            var sw = new Stopwatch();
            sw.Start();
            Solve(newInput);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds +"ms");
        }
        
        public static int[][] Make2dArray(int width, int height)
        {
            var array = new int[height][];
            for( int i =0;i<height;i++)
            {
                array[i] = new int[width];
            }

            return array;
        }

        
        public static void Copy2DArray(int[][] source, int[][] destination, int x, int y, int increment)
        {
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < source[i].Length; j++)
                {
                    destination[i + x][j + y] = (source[i][j] + increment - 1) % 9 + 1;
                }
            }
        }
        public static void Solve(int[][] matrix)
        {
            int[][] result = NewMatrix(matrix);
            result[0][0] = 0;
            bool changedouter;
            do
            {
                changedouter = false;
                for (int i = 1; i < matrix.Length; i++)
                {
                    bool changed;
                    do
                    {
                        changed = false;
                        for (int y = 0; y < i; y++)
                        {
                            var adjacent = Adjacent(result, y, i);
                            var min = adjacent.Where(i => i != -1).Min();
                            changed |= result[y][i] != min + matrix[y][i];
                            result[y][i] = min + matrix[y][i];

                            adjacent = Adjacent(result, i, y);
                            min = adjacent.Where(i => i != -1).Min();
                            changed |= result[i][y] != min + matrix[i][y];
                            result[i][y] = min + matrix[i][y];
                        }

                        {
                            var adjacent = Adjacent(result, i, i);
                            var min = adjacent.Where(i => i != -1).Min();
                            changed |= result[i][i] != min + matrix[i][i];
                            result[i][i] = min + matrix[i][i];
                        }
                        changedouter |= changed;
                    } while (changed);
                }
            } while (changedouter);
            
            Console.WriteLine(result[matrix.Length-1][matrix.Length-1]);
        }
        
        private static void PrintMatrix(int[][] matrix)
        {
            for (int y = 0; y < matrix.Length; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    Console.Write(matrix[y][x] + " ");
                }
                Console.WriteLine();
            }
        }
        
        public static int[] Adjacent(int[][] matrix, int y, int x)
        {
            var result = new List<int>();
            if (y > 0)
            {
                result.Add(matrix[y - 1][x]);
            }
            if (y < matrix.Length - 1)
            {
                result.Add(matrix[y + 1][x]);
            }
            if (x > 0)
            {
                result.Add(matrix[y][x - 1]);
            }
            if (x < matrix[y].Length - 1)
            {
                result.Add(matrix[y][x + 1]);
            }
            return result.ToArray();
        }
        
        public static int[][] NewMatrix(int[][] matrix, int initWith = -1)
        {
            int[][] result = new int[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = new int[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result[i][j] = initWith;
                }
            }
            return result;
        }
        
    }
}