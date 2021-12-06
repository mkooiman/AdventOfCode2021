using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Day5
{
    internal static class Day5
    {
        public static void Day5Pt1()
        {
            //read input file
            var input = File.ReadAllLines("../../Day5/input.txt");
            var lines = new List<Line>();
            var maxPoint = new Point();
            foreach (var line in input)
            {
                var lineSplit = line.Split(" -> ");
                
                var lineToAdd = new Line
                {
                    Start = MapStringToPoint(lineSplit[0]),
                    End = MapStringToPoint(lineSplit[1])
                };
                if(lineToAdd.Start.X>maxPoint.X)
                    maxPoint.X = lineToAdd.Start.X;
                if(lineToAdd.Start.Y>maxPoint.Y)
                    maxPoint.Y = lineToAdd.Start.Y;
                
                if(lineToAdd.End.X>maxPoint.X)
                    maxPoint.X = lineToAdd.End.X;
                if(lineToAdd.End.Y>maxPoint.Y)
                    maxPoint.Y = lineToAdd.End.Y;
                
                if(lineToAdd.IsHorizontal() || lineToAdd.IsVertical() )
                    lines.Add(lineToAdd);
            }
            
            Console.WriteLine($"Maxpoint: {maxPoint.Y}, {maxPoint.X}");
            var grid = new int[maxPoint.Y+1, maxPoint.X+1];
            foreach(Line line in lines)
            {
                var points = line.GetPoints();
                foreach (var point in points)
                {
                    try
                    {
                        grid[point.Y, point.X]++;
                    }
                    catch (Exception e )
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            var higherThanTwo = 0;
            //print grid
            for (int y = 0; y <= maxPoint.Y; y++)
            {
                for (int x = 0; x <= maxPoint.X; x++)
                {
                    if(grid[y,x]>1)
                        higherThanTwo++;
                    Console.Write(grid[y,x]);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine($"Higher than two: {higherThanTwo}");
        }

        public static void Day5Pt2()
        {
            //read input file
            var input = File.ReadAllLines("../../Day5/input.txt");
            var lines = new List<Line>();
            var maxPoint = new Point();
            foreach (var line in input)
            {
                var lineSplit = line.Split(" -> ");
                
                var lineToAdd = new Line
                {
                    Start = MapStringToPoint(lineSplit[0]),
                    End = MapStringToPoint(lineSplit[1])
                };
                if(lineToAdd.Start.X>maxPoint.X)
                    maxPoint.X = lineToAdd.Start.X;
                if(lineToAdd.Start.Y>maxPoint.Y)
                    maxPoint.Y = lineToAdd.Start.Y;
                
                if(lineToAdd.End.X>maxPoint.X)
                    maxPoint.X = lineToAdd.End.X;
                if(lineToAdd.End.Y>maxPoint.Y)
                    maxPoint.Y = lineToAdd.End.Y;
                
                if(lineToAdd.IsHorizontal() || lineToAdd.IsVertical() || lineToAdd.IsDiagonal())
                    lines.Add(lineToAdd);
            }
            
            Console.WriteLine($"Maxpoint: {maxPoint.Y}, {maxPoint.X}");
            var grid = new int[maxPoint.Y+1, maxPoint.X+1];
            foreach(Line line in lines)
            {
                var points = line.GetPoints();
                foreach (var point in points)
                {
                    try
                    {
                        grid[point.Y, point.X]++;
                    } catch (Exception e )
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            var higherThanTwo = 0;
            //print grid
            for (int y = 0; y <= maxPoint.Y; y++)
            {
                for (int x = 0; x <= maxPoint.X; x++)
                {
                    if(grid[y,x]>1)
                        higherThanTwo++;
                    Console.Write(grid[y,x]);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine($"Higher than two: {higherThanTwo}");
        }
        
        public static Point MapStringToPoint(string input)
        {
            var split = input.Split(",");
            return new Point
            {
                X = int.Parse(split[0]),
                Y = int.Parse(split[1])
            };
        }
        
        // splits a string based on a string delimiter
        public static string[] Split(this string str, string delimiter)
        {
            var split = new List<string>();
            var start = 0;
            var end = 0;
            while (end < str.Length)
            {
                end = str.IndexOf(delimiter, start);
                if (end == -1)
                {
                    end = str.Length;
                }
                split.Add(str.Substring(start, end - start));
                start = end + delimiter.Length;
            }
            return split.ToArray();
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        
        public bool IsHorizontal()
        {
            return Start.X == End.X;
        }
        
        public bool IsVertical()
        {
            return Start.Y == End.Y;
        }
        
        public bool IsDiagonal()
        {
            return Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);
        }
        
        //returns a list of points the line crosses
        public List<Point> GetPoints()
        {
            var points = new List<Point>();
            var x = Start.X;
            var y = Start.Y;
            var dx = End.X - Start.X;
            var dy = End.Y - Start.Y;
            var stepX = Math.Sign(dx);
            var stepY = Math.Sign(dy);
            var steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            for (var i = 0; i <= steps; i++)
            {
                points.Add(new Point {X = x, Y = y});
                x += stepX;
                y += stepY;
            }
            return points;
        }
        
        public override string ToString()
        {
            return $"{Start.X},{Start.Y} -> {End.X},{End.Y}";
        }
    }
}