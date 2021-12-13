using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day13
{
    internal sealed class Day13
    {
        private const string TestFile = "../../Day13/input_test.txt";
        private const string RealFile = "../../Day13/input.txt";

        private const string InputFile = RealFile;

        public static void Day13Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            List<int[]> points = new();
            List<int> yFolds = new List<int>();
            List<int> xFolds = new List<int>();
            
            bool instructions = false;
            
            foreach( var str in input)
            {
                if (str == "")
                {
                    instructions = true;
                    continue;
                }

                if (!instructions)
                {
                    points.Add(str.Split(',').Select(int.Parse).ToArray());
                }
                else
                {
                    if (str.StartsWith("fold along y="))
                    {
                        yFolds.Add(Int32.Parse(str.Substring("fold along y=".Length)));
                        break;
                    } 
                    if( str.StartsWith("fold along x="))
                    {
                        xFolds.Add(Int32.Parse(str.Substring("fold along x=".Length)));
                        break;
                    }
                }
            }
            
            var minX = xFolds.Min();
            var minY = yFolds.Count == 0 ? points.Max( p => p[1])+1: yFolds.Min();
            
            var foldedPoints = new List<int[]>();
            
            foreach (var point in points)
            {
                var yLoc = yFolds.Aggregate( point[1], (val, y) =>
                {
                    var res = val;
                    if (val>=y)
                    {
                        res = val % y;
                        res = y - res;
                    }

                    return res % y;
                });
                var xLoc = xFolds.Aggregate( point[0], (val, x) =>
                {
                    var res = val;
                    if (val>x)
                    {
                        res = val % x;
                        res =  x - res;
                    }
                    return res % x;
                } );
                foldedPoints.Add(new[] {xLoc, yLoc});
            }
            
            var grid = new int[minY,minX];

            foreach (var point in foldedPoints)
            {
               grid[point[1], point[0]] = 1; 
            }
            PrintGrid(grid);

            var sum = 0;    
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    sum += grid[i,j];
                }
            }
            Console.WriteLine(sum);
        }

         public static void Day13Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            List<int[]> points = new();
            List<int> yFolds = new List<int>();
            List<int> xFolds = new List<int>();
            bool instructions = false;
            foreach( var str in input)
            {
                if (str == "")
                {
                    instructions = true;
                    continue;
                }

                if (!instructions)
                {
                    points.Add(str.Split(',').Select(int.Parse).ToArray());
                }
                else
                {
                    if (str.StartsWith("fold along y="))
                    {
                        yFolds.Add(Int32.Parse(str.Substring("fold along y=".Length)));
                    }
                    else if( str.StartsWith("fold along x="))
                    {
                        xFolds.Add(Int32.Parse(str.Substring("fold along x=".Length)));
                    }
                }
            }
            
            var minX = xFolds.Min();
            var minY = yFolds.Min();
            
            var foldedPoints = new List<int[]>();
            
            foreach (var point in points)
            {
                var yLoc = yFolds.Aggregate( point[1], (val, y) =>
                {
                    var res = val;
                    if (val>y)
                    {
                        res = val % y;
                        res = y - res;
                    }

                    return res % y;
                });
                var xLoc = xFolds.Aggregate( point[0], (val, x) =>
                {
                    var res = val;
                    if (val>x)
                    {
                        res = val % x;
                        res =  x - res;
                    }
                    return res % x;
                } );
                foldedPoints.Add(new[] {xLoc, yLoc});
            }
            
            var grid = new int[minY,minX];

            foreach (var point in foldedPoints)
            {
               grid[point[1], point[0]] = 1; 
            }
            PrintGrid(grid);

            var sum = 0;    
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    sum += grid[i,j];
                }
            }
            Console.WriteLine(sum);
        }

        
        private static void PrintGrid(int[,] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    Console.Write(grid[y, x]==1? 'X':' ');
                }
                Console.WriteLine();
            }
        }
    }
}