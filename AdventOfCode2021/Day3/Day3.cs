using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day3
{
    internal static class Day3
    {

        public static void Day3Pt1()
        {
            var input = File.ReadAllLines("../../Day3/input.txt");

            var res = input.Select(i =>
                    i.ToCharArray()
                        .Select(c => (int)c == '1' ? 1 : 0)
                        .ToArray())
                .Aggregate(
                    new int[input[0].Length],
                    (arr, list) =>
                    {
                        for (var i = 0; i < arr.Length; i++)
                        {
                            arr[i] += list[i];
                        }
                        
                        return arr;
                    });
            int result = 0;
            for(int i =0;i<res.Length;i++)
            {
                result <<= 1;
                if(res[i]>input.Length/2)
                {
                    result |= 1;
                }
            }
            var epsilonMask = (1<< input[0].Length)-1;
            Console.WriteLine($"Gamma: {result}\n" +
                              $"Epsilon: {result ^epsilonMask}\n" +
                              $"Result: {result * (result ^ epsilonMask) }");
            
        }

        public static void Day3Pt2()
        {
            var input = File.ReadAllLines("../../Day3/input.txt");
            
            int width = input[0].Length;
            int samples = input.Length;
            
            var src = input.Select(s => Convert.ToInt32(s, 2));
            var counts =  input.Select(i =>
                    i.ToCharArray()
                        .Select(c => (int)c == '1' ? 1 : 0)
                        .ToArray())
                .Aggregate(
                    new int[input[0].Length],
                    (arr, list) =>
                    {
                        for (var i = 0; i < arr.Length; i++)
                        {
                            arr[i] += list[i];
                        }

                        return arr;
                    });
            var O2 = src.ToList();
            var CO2 = src.ToList();
            
            for(int i = 0;i <counts.Length;i++) 
            {
                if (O2.Count > 1)
                {
                    var count = O2.Where(nr => BitSet(nr, width - i - 1)).Count();
                    if (count << 1 >= O2.Count )
                    {
                        O2 = O2.Where(i1 => BitSet(i1, width - i - 1 )).ToList();
                    }
                    else
                    {
                        O2 = O2.Where(i1 =>!BitSet(i1, width - i - 1 )).ToList();
                    }
                }

                if (CO2.Count > 1)
                {
                    var count = CO2.Where(nr => !BitSet(nr, width - i - 1)).Count();
                    if (count << 1 <= CO2.Count)
                    {
                        CO2 = CO2.Where(i1 => !BitSet(i1, width - i - 1 )).ToList();
                    }
                    else
                    {
                        CO2 = CO2.Where(i1 => BitSet(i1, width - i - 1) ).ToList();
                    }
                }
            }
            Console.WriteLine($"O2: {O2[0]}\n"
            + $"CO2: {CO2[0]}\n" +
            $"Result: {O2[0] * CO2[0]}");
        }
        
        static bool BitSet (int i, int bit)
        {
            return (i & (1 << bit)) != 0;
        }

    }
}