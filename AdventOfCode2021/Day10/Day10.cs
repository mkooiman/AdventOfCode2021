using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day10
{
    internal sealed class Day10
    {
        private const string TestFile = "../../Day10/input_test.txt";
        private const string RealFile = "../../Day10/input.txt";

        private const string InputFile = RealFile;

        public static void Day10Pt1()
        {
            var input = File.ReadAllLines(InputFile).Select(s => s.ToCharArray());
            var open = "([{<";
            var close = ")]}>";
            var points = new[] { 3, 57, 1197, 25137 };

            var stack = new LinkedList<char>();
            long score = 0;
            foreach (var chars in input)
            {
                foreach(var c in chars)
                {
                    if (open.IndexOf(c) >= 0)
                    {
                        stack.AddLast(c);
                    }
                    else
                    {
                        var index = close.IndexOf(c);
                        if(index==-1)
                            throw new ArgumentException($"Illegal character {c}");
                        if (open.IndexOf(stack.Last.Value) != index)
                        {
                            score += points[index];
                            break;
                        }
                        else
                        {
                            stack.RemoveLast();
                            
                        }
                    }
                }
            }

            Console.WriteLine(score);
        }

        public static void Day10Pt2()
        {
            var input = File.ReadAllLines(InputFile).Select(s => s.ToCharArray());
            var open = "([{<";
            var close = ")]}>";
            var points = new[] { 1ul, 2ul, 3ul, 4ul };

            var lineScores = new List<ulong>();
            foreach (var chars in input)
            {
                var stack = new LinkedList<char>();
                var skip = false;
                foreach(var c in chars)
                {
                    if (open.IndexOf(c) >= 0)
                    {
                        stack.AddLast(c);
                    }
                    else
                    {
                        var index = close.IndexOf(c);
                        if(index==-1)
                            throw new ArgumentException($"Illegal character {c}");
                        if (open.IndexOf(stack.Last.Value) != index)
                        {
                            skip = true;
                            break;
                        }
                        stack.RemoveLast();
                    }
                }
                if(skip )
                    continue;
                
                var lineScore = 0ul;
                foreach (var c in stack.Reverse())
                {
                    lineScore =  (lineScore*5) + points[open.IndexOf(c)];
                }
                lineScores.Add(lineScore);
            }

            lineScores.Sort();
            Console.WriteLine($"Result: {lineScores[lineScores.Count/2]}");
        }
    }
}