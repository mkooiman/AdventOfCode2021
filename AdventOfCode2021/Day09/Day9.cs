using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day09
{
    public static class Day9
    {
        private const string TestFile = "../../Day09/input_test.txt";
        private const string RealFile = "../../Day09/input.txt";

        private const string InputFile = RealFile;

        public static void Day9Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            var result = new int[input.Length][];
            for(int i =0;i<input.Length;i++)
            {
                result[i] = input[i]
                    .ToCharArray()
                    .Select(c => c - '0')
                    .ToArray();
            }

            var lowestSum = 0; 
            for (int y = 0; y < result.Length; y++)
            {
                for (int x = 0; x < result[y].Length; x++)
                {
                    if ((y == 0 || result[y - 1][x] > result[y][x]) &&
                        (x == 0 || result[y][x - 1] > result[y][x]) &&
                        (y == result.Length - 1 || result[y + 1][x] > result[y][x]) &&
                        (x == result[y].Length-1 || result[y][x + 1] > result[y][x]))
                    {
                        lowestSum += (1 + result[y][x]);
                    }
                }
            }
            Console.WriteLine($"Result : {lowestSum}");
            
        }
        
        public static void Day9Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var result = new int[input.Length][];
            for(int i =0;i<input.Length;i++)
            {
                result[i] = input[i]
                    .ToCharArray()
                    .Select(c => c - '0')
                    .ToArray();
            }
            var seeds = new List<int[]>();
            for (var y = 0; y < result.Length; y++)
            {
                for (var x = 0; x < result[y].Length; x++)
                {
                    if ((y == 0 || result[y - 1][x] > result[y][x]) &&
                        (x == 0 || result[y][x - 1] > result[y][x]) &&
                        (y == result.Length - 1 || result[y + 1][x] > result[y][x]) &&
                        (x == result[y].Length-1 || result[y][x + 1] > result[y][x]))
                    {
                        seeds.Add(new []{ y, x });
                    }
                }
            }
            var solved = Mark(result, seeds);
            
            Console.WriteLine($"Result : {solved}");
            
        }
        
        //print 2d array
        private static void Print(int[][] input)
        {
            foreach (var t in input)
            {
                foreach (var t1 in t)
                {
                    Console.Write(t1);
                }

                Console.WriteLine();
            }
        }

        private static int Mark(int[][] input, List<int[]> seeds)
        {
            //copy 2d array
            var sizes = new List<int>();
            foreach(var seed in seeds)
            {
                var copy = Copy(input);

                copy[seed[0]][seed[1]] = -10 + copy[seed[0]][seed[1]];
                int changes;
                var count = 1;
                do
                {
                    changes = 0;
                    for (var y = 0; y < copy.Length; y++)
                    {
                        for (var x = 0; x < input[y].Length; x++)
                        {
                            if (copy[y][x] >= 0 && copy[y][x] != 9 &&
                                Neighbours(copy,y,x).Any(n => n< 0) ) 
                            {
                                copy[y][x] = -10 + copy[y][x];
                                changes++;
                               
                            }
                        }
                    }

                    count += changes;

                } while (changes != 0);    
                
                sizes.Add(count);
            }

            return sizes.OrderByDescending(x => x).Take(3).Aggregate(1, (x,y)=> x*y );
        }
        
        //copies 2d array
        private static int[][] Copy(int[][] input)
        {
            var copy = new int[input.Length][];
            for (var y = 0; y < input.Length; y++)
            {
                copy[y] = new int[input[y].Length];
                for (var x = 0; x < input[y].Length; x++)
                {
                    copy[y][x] = input[y][x];
                }
            }
            return copy;
        }
        

        public static List<int> Neighbours(int[][] matrix, int y, int x)
        {
            var result = new List<int>();
            if (y != 0)
            {
                result.Add( matrix[y - 1][x]);
            }

            if (y != matrix.Length - 1)
            {
                result.Add(matrix[y+1][x]);
            }

            if (x != 0)
            {
                result.Add(matrix[y][x-1]);
            }
            if(x != matrix[y].Length - 1)
            {
                result.Add(matrix[y][x+1]);
            }

            return result;
        }
        
    }
}