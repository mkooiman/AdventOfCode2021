using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day20
{
    internal sealed class Day20
    {
        private const string TestFile = "../../Day20/input_test.txt";
        private const string RealFile = "../../Day20/input.txt";

        private const string InputFile = RealFile;

        public static void Day20Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            var filter = input[0].Select(c => c == '#').ToArray();

            var result = input.Skip(2).Select(s => s.ToCharArray().Select(c => c == '#').ToArray()).ToArray();
            var target = Pad(result, 40,false);
            
            var res = Transform(target, filter);
            // res = Pad(res, 1, filter[0] );
            res = Transform(res, filter);
            Print(res);
            Console.WriteLine(CountTrue(res, true));
        }

        public static void Day20Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var filter = input[0].Select(c => c == '#').ToArray();

            var result = input.Skip(2).Select(s => s.ToCharArray().Select(c => c == '#').ToArray()).ToArray();
            
            var target = Pad(result,40);

            for (int i = 0; i < 50; i++)
            {
                target = Transform(target, filter);
                target = Shrink(target, 1);
                target = Pad(target,2,filter[0]?i%2==0:false);    
            }
            Console.WriteLine(CountTrue(target, false));
        }

        public static int CountTrue(bool[,] array, bool skipEdges)
        {
            var res = 0;
            for (var y = 1; y < array.GetLength(0)-1; y++)
            {
                
                for (var x = 1; x < array.GetLength(1)-1; x++)
                {
                    if(array[y,x])
                        res++;
                }

            }

            return res;
        }

        public static void Print(bool[,] array)
        {
            for (var y = 0; y < array.GetLength(0); y++)
            {
                for (var x = 0; x < array.GetLength(1); x++)
                {
                    Console.Write(array[y, x] ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static bool[,] Transform(bool[,] source, bool[] filter)
        {
            var target = new bool[source.GetLength(0), source.GetLength(1)];
            
            for (int y = 1; y < source.GetLength(0)-1; y++)
            {
                for (int x = 1; x < source.GetLength(1)-1; x++)
                {
                    target[y,x] = filter[NumberAt(source, y, x, 1)];
                }
            }

            return target;
        }

        public static int NumberAt(bool[,] source, int y, int x, int borderSize)
        {
            var result = 0;
            for (var bY = 0 - borderSize; bY <= borderSize; bY++)
            {
                for (var bX = 0 - borderSize; bX <= borderSize; bX++)
                {
                    result <<= 1;
                    result |= source[y + bY, x + bX] ? 1 : 0;
                }
            }

            return result;
        }

        public static bool[,] Pad(bool[][] source, int border = 1, bool initWith = false)
        {
            var result = new bool[source.Length+2* border, source[0].Length+2* border];
            for (var y = 0; y < result.GetLength(0); y++)
            {
                for (var x = 0; x < result.GetLength(1); x++)
                {
                    result[y,x] = initWith;
                }
            }

            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source.Length; x++)
                {
                    result[y+border,x+border] = source[y][x];
                }
            }

            return result;
        }

        public static bool[,] Shrink(bool[,] source, int count)
        {
            var result = new bool[source.GetLength(0) - (2 * count), source.GetLength(1)-(2* count) ];
            // Print(source);
            for (var y=0;y<result.GetLength(0);y++)
            {
                for (var x=0;x<result.GetLength(1);x++)
                {
                    result[y,x] = source[y+count,x+count];
                }
            
            }

            // Print(result);

            return result;
        }
        
        public static bool[,] Pad(bool[,] source, int border = 1, bool initWith = false)
        {
            var result = new bool[source.GetLength(0) +2 * border, source.GetLength(1)+2* border];
            for (var y = 0; y < result.GetLength(0); y++)
            {
                for (var x = 0; x < result.GetLength(1); x++)
                {
                        result[y,x] = initWith;
                }
            }

            for (var y = 0; y < source.GetLength(0); y++)
            {
                for (var x = 0; x < source.GetLength(1); x++)
                {
                    
                    result[y+border,x+border] = source[y,x];
                }
            }

            return result;
        }
    }
}