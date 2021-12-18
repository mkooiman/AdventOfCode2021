using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day18
{
    internal static class Day18
    {
        private const string TestFile = "../../Day18/input_test.txt";
        private const string RealFile = "../../Day18/input.txt";

        private const string InputFile = RealFile;

        public static void Day18Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            var num1 = Parse(input[0]);
            for(int i =1;i<input.Length;i++)
            {
                var num2 = Parse(input[i]);
                num1 = Add(num1, num2);
                num1 = Reduce(num1);
                // Console.WriteLine(num1.ToString());
            }
            Console.WriteLine(num1.ToString());
            Console.WriteLine(num1.Magnitude());
        }

        public static void Day18Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var max = 0;
            for(int i =1;i<input.Length;i++)
            {
                for (int i1 = 0; i1 < input.Length; i1++)
                {
                    var num1 = Parse(input[i]);
                    if(i1 == i) continue;
                    var num2 = Parse(input[i1]);
                    var res =Add(num1, num2);
                    res = Reduce(res);
                    var mag = res.Magnitude();
                    if (mag > max)
                    {
                        max = mag;
                    }

                    // Console.WriteLine(num1.ToString());
                }
            }
            Console.WriteLine(max);
        }
        
        private static SNumber Add(SNumber left, SNumber right)
        {
            var num =  new SNumber()
            {
                Left = left,
                Right = right
            };
            left.Parent = num;
            right.Parent = num;
            return num;
        }

        private static SNumber Reduce(SNumber number)
        {
            
            while (Explode(number) || Split(number))
            {
                // Console.WriteLine(number.ToString());
            }

            return number;

        }

        private static bool Split(SNumber number)
        {
            foreach (var sNumber in number)
            {
                if (sNumber.IsLiteral() && sNumber.Literal > 9)
                {
                    double val = (double)sNumber.Literal / 2d;
                    sNumber.Left = new SNumber()
                    {
                        Literal = (int)Math.Floor(val),
                        Parent = sNumber
                    };
                    sNumber.Right = new SNumber()
                    {
                        Literal = (int)Math.Ceiling(val),
                        Parent = sNumber
                    };
                    sNumber.Literal = null;
                    return true;
                }
            }

            return false;
        }

        private static bool Explode(SNumber number)
        {
            var toExplode = FindFirstWithDepth(number, 5);
            if(toExplode==null)
                return false;
            // Console.WriteLine($"Explode: number {toExplode.ToString()}");
            var left = FindNumberToTheLeft(number, toExplode.Left);

            var right = FindNumberToTheRight(number,toExplode.Right);
            if (left == null)
            {
                toExplode.Parent.Left.Literal = 0;
            }
            else
            {
                left.Literal += toExplode.Left.Literal;
            }
            if (right == null)
            {
                toExplode.Parent.Right.Literal = 0;
            }
            else
            {
                right.Literal += toExplode.Right.Literal;
            }
            //remove ToExplode from parent
            if (toExplode.Parent.Left == toExplode)
            {
                toExplode.Parent.Left = new SNumber(){Literal = 0, Parent = toExplode.Parent};
            }
            else
            {
                toExplode.Parent.Right = new SNumber(){Literal = 0, Parent = toExplode.Parent};
            }
            return true;
        }

        private static SNumber? FindNumberToTheRight(SNumber root, SNumber number)
        {
            var isNext = false;
            foreach (var sNumber in root)
            {
                if (isNext && sNumber.IsLiteral()) return sNumber;
                if (sNumber == number)
                {
                    isNext = true;
                }
            }

            return null;
        }
        private static SNumber? FindNumberToTheLeft(SNumber root, SNumber number)
        {
            var reverse= root.Reverse();
            var isNext = false;
            foreach (var sNumber in reverse)
            {
                if (isNext) return sNumber;
                if (sNumber == number)
                {
                    isNext = true;
                }
            }

            return null;
        }
        
        private static SNumber? FindFirstWithDepth(SNumber number, int depth = 5)
        {
            return (from sNumber in number where sNumber.GetDepth() == depth select sNumber.Parent).FirstOrDefault();
        }


        private static SNumber Parse(string s)
        {
            return Parse(new ReadStream(s), 0);
        }
        
        private static SNumber Parse(ReadStream ps, int depth = 0)
        {
            char c = ps.Read();
            if (c == '[')
            {
                var n = new SNumber();
                n.Left = Parse(ps, depth +1);
                n.Left.Parent = n;
                c = ps.Read();
                if(c !=',') throw new Exception("Expected ','");
                n.Right = Parse(ps, depth +1);
                n.Right.Parent = n;
                c = ps.Read();
                if(c !=']') throw new Exception("Expected ']'");
                return n;
                
            }
            
            if (c is >= '0' and <= '9')
            {
                return new SNumber{Literal = c-'0'};
            }
            
            throw new Exception("");
            
        }
    }

    class SNumber: IEnumerable<SNumber>
    {
        public SNumber? Parent{get; set; }
        public SNumber? Left{get; set; }
        public SNumber? Right{get; set; }
        public int? Literal { get; set; }

        public bool IsLiteral()
        {
            return Literal.HasValue;
        }

        public int GetDepth()
        {
            var node = this;
            int depth = 0;
            while(node.Parent!=null)
            {
                node = node.Parent;
                depth++;
            }

            return depth;
        }
        public IEnumerator<SNumber> GetEnumerator()
        {
            if (IsLiteral())
            {
                yield return this;
            }
            else
            {
                foreach (SNumber value in Left!)
                {
                    yield return value;
                }

                foreach (SNumber value in Right!)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string ToString()
        {
            if (IsLiteral()) return Literal.Value.ToString();
            return $"[{Left?.ToString()},{Right?.ToString()}]";
        }

        public int Magnitude()
        {
            if (IsLiteral()) return Literal.Value;
            return (3*Left!.Magnitude()) + (2*Right!.Magnitude());
        }
    }
    
    public class ReadStream
    {
        private readonly string _input;
        private int _index;

        public ReadStream(string input)
        {
            _input = input;
        }

        public char Read()
        {
            return _input[_index++];
        }
        public char PeekNext()
        {
            return _input[_index];
        }

    }

    
}