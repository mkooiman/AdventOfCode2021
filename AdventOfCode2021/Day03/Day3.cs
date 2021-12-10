using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day03
{
    internal static class Day3
    {

        public static void Day3Pt1()
        {
            var input = File.ReadAllLines("../../Day03/input.txt");

            var res = input.Select(i =>
                    i.ToCharArray()
                        .Select(c => c == '1' ? 1 : 0)
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
            var result = 0;
            foreach (var t in res)
            {
                result <<= 1;
                if(t>input.Length/2)
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
            var input = File.ReadAllLines("../../Day03/input.txt");

            var width = input[0].Length;

            var src = input.Select(s => Convert.ToInt32(s, 2));
            var counts =  input.Select(i =>
                    i.ToCharArray()
                        .Select(c => c == '1' ? 1 : 0)
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
            var enumerable = src as int[] ?? src.ToArray();
            var o2 = enumerable.ToList();
            var co2 = enumerable.ToList();
            
            for(var i = 0;i <counts.Length;i++) 
            {
                if (o2.Count > 1)
                {
                    var count = o2.Count(nr => BitSet(nr, width - i - 1));
                    o2 = count << 1 >= o2.Count ? o2.Where(i1 => BitSet(i1, width - i - 1 )).ToList() : o2.Where(i1 =>!BitSet(i1, width - i - 1 )).ToList();
                }

                if (co2.Count <= 1) continue;
                {
                    var count = co2.Count(nr => !BitSet(nr, width - i - 1));
                    co2 = count << 1 <= co2.Count ? co2.Where(i1 => !BitSet(i1, width - i - 1 )).ToList() : co2.Where(i1 => BitSet(i1, width - i - 1) ).ToList();
                }
            }
            Console.WriteLine($"O2: {o2[0]}\n" +
                              $"CO2: {co2[0]}\n" +
                              $"Result: {o2[0] * co2[0]}");
        }

        private static bool BitSet (int i, int bit)
        {
            return (i & (1 << bit)) != 0;
        }

    }
}