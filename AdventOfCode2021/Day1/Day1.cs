using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day1
{
    internal class Day1
    {

        private static readonly int _windowSize = 3;
        
        public static void Day1Pt1(string[] args)
        {
            var input = File.ReadAllLines("../../Day1/input.txt");
            var result = 0;
            input
                .Select(int.Parse)
                .Aggregate((x, y) =>
                {
                    result += x < y ? 1 : 0;
                    return y;
                });

            Console.WriteLine(result);
        }
        
        public static void Day1Pt2(string[] args)
        {
            var input = File.ReadAllLines("../../Day1/input.txt");
            
            var ints = input.Select(int.Parse).ToList();

            var buffer = new LinkedList<int>();
            var count = 0;
            int? prev = null;
            foreach (var i in ints)
            {
                buffer.AddLast(i);
                if (buffer.Count < _windowSize) continue;

                if (prev == null)
                {
                    prev = buffer.Sum();
                    continue;
                }

                var nextVal = prev - buffer.First.Value + buffer.Last.Value;
                if (nextVal > prev)
                    count++;
                buffer.RemoveFirst();
            }

            Console.WriteLine(count);
        }
        
    }
}