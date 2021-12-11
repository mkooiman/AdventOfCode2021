using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day11
{
    internal sealed class Day11
    {
        private const string TestFile = "../../Day11/input_test.txt";
        private const string RealFile = "../../Day11/input.txt";

        private const string InputFile = TestFile;

        public static void Day11Pt1()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => s.ToCharArray()
                    .Select(c => c - '0').ToArray())
                .ToArray();
            var totalFlashes = 0;
            
            for( int i = 0; i < 100; i++)
            {
                IncreaseOne(input);
                var localFlashes = 0;
                do
                {
                    localFlashes = Flash(input);

                    totalFlashes += localFlashes;
                }while(localFlashes!=0);    
                ResetMinusOne(input);
                
            }
            Console.WriteLine($"Flashes: {totalFlashes}" );
        }
        
        public static void Day11Pt2()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => s.ToCharArray()
                    .Select(c => c - '0').ToArray())
                .ToArray();

            bool allFlashed = false;
            int count = 0;
            while(!allFlashed)
            {
                count++;
                var totalFlashes = 0;
                IncreaseOne(input);
                var localFlashes = 0;
                do
                {
                    localFlashes = Flash(input);

                    totalFlashes += localFlashes;
                }while(localFlashes!=0);    
                ResetMinusOne(input);
                allFlashed = totalFlashes == 100;
            }
            Console.WriteLine($"iteration: {count}" );
        }

        private static void Print(int[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    Console.Write(input[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void ResetMinusOne(int[][] input)
        {
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == -1)
                    {
                        input[y][x] = 0;        
                    }
                }
            }
        }

        private static int Flash(int[][] input)
        {
            int flash = 0;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] > 9)
                    {
                        flash++;
                        input[y][x] = -1;
                        IncreaseNeighbours(input, y, x);
                    }
                }
            }

            return flash;
        }

        private static void IncreaseNeighbours(int[][] input, int y, int x)
        {
            if (y != 0 && input[y - 1][x]!=-1)
            {
                input[y - 1][x]++;
            }

            if (y != input.Length - 1 && input[y + 1][x] != -1)
            {
                input[y + 1][x]++;
            }
            

            if (x != 0 && input[y][x-1] !=-1)
            {
                input[y][x-1]++;
            }
            
            
            if(x != input[y].Length - 1 && input[y][x+1] != -1)
            {
                input[y][x+1]++;
            }
            
            if (x != 0 && y!= 0 && input[y-1][x-1] != -1)
            {
                input[y-1][x-1]++;
            }
            
            if (x != input[y].Length-1 && y!= 0 && input[y-1][x+1] != -1)
            {
                input[y-1][x+1]++;
            }
            
            if (y != input.Length-1 && x!= 0 && input[y+1][x-1] != -1)
            {
                input[y+1][x-1]++;
            }
            
            if(x != input[y].Length-1 && y!=input.Length-1 && input[y+1][x+1] != -1)
            {
                input[y+1][x+1]++;
            }
            
        }
        
        private static void IncreaseOne(int[][] input)
        {
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    input[y][x]++;
                }
            }
        }
    }
}