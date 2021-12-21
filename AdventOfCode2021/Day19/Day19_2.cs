using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day19
{
    internal sealed class Day19_2
    {
        private const string TestFile = "../../Day19/input_test.txt";
        private const string RealFile = "../../Day19/input.txt";

        private const string InputFile = TestFile;

        internal record Coord
        {
            public Coord( int[]coord)
            {
                
                X = coord[0];
                   Y = coord[1];
                   Z = coord[1];
            }

            public int Scanner { get; }
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
        }

        // public static void Day19pt1()
        // {
        //     var parsed = Parse(InputFile);
        //     var total = parsed.Select(p => p.Count).Sum();
        //
        //     for (int i1 = 0; i1 < parsed.Count; i1++)
        //     {
        //         for (int i2 = i1+1; i2 < parsed.Count; i2++)
        //         {
        //             total -= CountCommonDistances(parsed[i1], parsed[i2]);
        //         }
        //     }
        //     
        //     Console.WriteLine(total );
        // }

        // public static List<int[]> GetAllDistances(List<int[]> parsed)
        // {
        //     for (int i1 = 0; i1 < parsed.Count; i1++)
        //     {
        //         var point1 = parsed[i1];
        //         for (int i2 = i1 + 1; i2 < parsed.Count; i2++)
        //         {
        //             var point2 = parsed[i2];
        //             var distances = new[]
        //             {
        //                 point1[0] - point2[0],
        //                 point1[1] - point2[1],
        //                 point1[2] - point2[2]
        //             };
        //         }
        //     }
        // }

        public static int CountCommonDistances(List<int[]> one, List<int[]> two)
        {
            var total = one.Count + two.Count;
            var set = new HashSet<Coord>();
            for(int i1 = 0;i1<two.Count;i1++)
            {
                var point1 = two[i1];
                var lookup = false;
                for(int i2=i1+1;i2<two.Count;i2++)
                {
                    
                    var point2 = two[i2];
                    var translate = FindMatching(one, point1, point2);
                    if (translate != null)
                    {
                        set.Add(new Coord(translate.Value.Item1));
                        set.Add(new Coord(translate.Value.Item2));
                        Console.WriteLine(
                            $" {string.Join(",", point1)}\n" +
                            $" {string.Join(",", point2)}\n" +
                            $" {string.Join(",", translate.Value.Item1)}\n" +
                            $" {string.Join(",", translate.Value.Item2)}\n");
                        break;
                    }
                    
                }
            }

            return total-set.Count;
        }
        
        public static (int[],int[])? FindMatching(List<int[]> parsed, int[] point1, int[] point2)
        {
            var distances = new []
            {
                point1[0] - point2[0],
                point1[1] - point2[1],
                point1[2] - point2[2]
            };
            
            foreach (var point3 in parsed)
            {
                foreach( var point4 in parsed)
                {
                    if (point3 != point4)
                    {
                        
                        var res = new List<int>(new [] { 
                            point3[0] - point4[0],
                            point3[1] - point4[1],
                            point3[2] - point4[2] 
                        });
                        var xIndex = res.IndexOf(distances[0]);
                        if (xIndex == -1)
                        {
                            xIndex = res.IndexOf(distances[0]*-1);
                            if (xIndex != -1)
                                xIndex = -1 * xIndex;
                            else continue;
                        }
                        var yIndex = res.IndexOf(distances[1]);
                        if (yIndex == -1)
                        {
                            yIndex = res.IndexOf(distances[1] * -1);
                            if(yIndex!=-1) 
                                yIndex = -1 * yIndex;
                            else continue;
                            
                        }
                        var zIndex = res.IndexOf(distances[2]);
                        if (zIndex == -1)
                        {
                            zIndex = res.IndexOf(distances[2] * -1);
                            if(zIndex!=-1) 
                                zIndex = -1 * zIndex;
                            else continue;
                        }
                        if(Math.Abs(xIndex) == Math.Abs(yIndex)||
                           Math.Abs(yIndex) == Math.Abs(zIndex)||
                           Math.Abs(zIndex) == Math.Abs(xIndex))
                        {
                            Console.WriteLine();
                        }
                        var corr3 = new []
                        {
                            point3[Math.Abs(xIndex)] * (xIndex<0?-1:1),
                            point3[Math.Abs(yIndex)] * (yIndex<0?-1:1),
                            point3[Math.Abs(zIndex)] *(zIndex<0?-1:1),
                        };
                        
                        var corr4 = new []
                        {
                            point4[Math.Abs(xIndex)] * (xIndex<0?-1:1),
                            point4[Math.Abs(yIndex)] * (yIndex<0?-1:1),
                            point4[Math.Abs(zIndex)] * (zIndex<0?-1:1),
                        };

                        return new(point3, point4);
                    }
                }
            }

            return null;
        }
        
        private static List<List<int[]>> Parse(string file)
        {
            var input = File.ReadAllLines(file);
            List<int[]> scanner = null;
            List<List<int[]>> scanners = new();
            foreach (var line in input)
            {
                if (line.StartsWith("--- scanner "))
                {
                    if (scanner != null)
                    {
                        scanners.Add(scanner);
                    }

                    scanner = new ();
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    var coords = line.Split(',').Select(int.Parse).ToArray();
                    scanner.Add(coords);
                }
            }

            scanners.Add(scanner);
            return scanners;
        }
    }
}