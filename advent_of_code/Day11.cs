using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    public class Day11 : IDay<long, long, long, long>
    {
        private const int Day = 11;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data);
        }

        public long Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        private long DoDay(List<string> data, bool isPt2 = false)
        {
            var octopi = Parse(data);
            long output = 0;
            long maxStep = 100;
            if (isPt2)
            {
                maxStep = long.MaxValue;
            }
            for (long step = 0; step < maxStep; step++)
            {
                for (int i = 0; i < octopi.GetLength(0); i++)
                {
                    for (int j = 0; j < octopi.GetLength(1); j++)
                    {
                        if (octopi[i, j] == 9)
                        {
                            blink(octopi,i,j);
                        }
                        else
                        {
                            octopi[i, j]++;
                        }
                    }
                }

                if (isPt2)
                {
                    if(ClearBlinks(octopi) == octopi.GetLength(0)*octopi.GetLength(1))
                        return step + 1;
                }
                output+=ClearBlinks(octopi);
            }
            return output;
        }

        private static long ClearBlinks(int[,] octopi)
        {
            long clearedBlinks = 0;
            for (int i = 0; i < octopi.GetLength(0); i++)
            {
                for (int j = 0; j < octopi.GetLength(1); j++)
                {
                    if (octopi[i, j] > 9)
                    {
                        octopi[i, j] = 0;
                        clearedBlinks++;
                    }
                }
            }
            PrintIt(octopi);
            return clearedBlinks;
        }

        private static long blink(int[,] octopi, int i, int j)
        {
            long blinkCount = 0;
            if( i < 0 || i >=  octopi.GetLength(0)  || j < 0 || j >= octopi.GetLength(1))
                return blinkCount;
            octopi[i, j]++;
            blinkCount = 1;
            if (octopi[i, j] == 10)
            {
                blinkCount+=blink(octopi,i-1, j);
                blinkCount+=blink(octopi,i+1, j);
                blinkCount+=blink(octopi,i, j-1);
                blinkCount+=blink(octopi,i, j+1);
                blinkCount+=blink(octopi,i+1, j+1);
                blinkCount+=blink(octopi,i+1, j-1);
                blinkCount+=blink(octopi,i-1, j-1);
                blinkCount+=blink(octopi,i-1, j+1);  
            }

            return blinkCount;
        }
        private static void PrintIt(int[,] data)
        {
            return;
            Console.WriteLine("========================");
            Console.WriteLine(string.Join(Environment.NewLine, data.OfType<int>()
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / data.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}")));
            Console.WriteLine("========================");

        }

        private static int[,] Parse(List<string> data)
        {
            var octopi = new int[data.Count, data[0].Length];
            for (var index = 0; index < data.Count; index++)
            {
                var line = data[index];
                for (var i = 0; i < line.Length; i++)
                {
                    var height = line[i];
                    octopi[index, i] = (int)char.GetNumericValue(height);
                }
            }

            return octopi;
        }
    }
}