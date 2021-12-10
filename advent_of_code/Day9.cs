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
    public class Day9 : IDay<long, long, long, long>
    {
        private const int Day = 9;
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
            return isPt2 ? pt2(data) : pt1(data);
        }

        private static bool flood(int[,] heightData, int number, int i, int j)
        {
            bool val = false;
            if( i < 0 || i >=  heightData.GetLength(0)  || j < 0 || j >= heightData.GetLength(1) || heightData[i,j] != 0)
                return val;
            heightData[i, j] = number;
            val = true;
            flood(heightData,number,i-1, j);
            flood(heightData,number,i+1, j);
            flood(heightData,number,i, j-1);
            flood(heightData,number,i, j+1);
            return val;
        }
        
        private static long pt2(List<string> data)
        {
            var heightData = new int[data.Count, data[0].Length];
            for (var index = 0; index < data.Count; index++)
            {
                var line = data[index];
                for (var i = 0; i < line.Length; i++)
                {
                    var height = line[i];
                    heightData[index, i] = (int)char.GetNumericValue(height) == 9 ? int.MaxValue : 0;
                }
            }

            var lastUsedNumber = 1;
            for (int i = 0; i < heightData.GetLength(0); i++)
            {
                for (int j = 0; j < heightData.GetLength(1); j++)
                {
                    if (flood(heightData, lastUsedNumber,i,j))
                    {
                        lastUsedNumber++;
                    }
                }
            }
            /*Console.WriteLine(string.Join(Environment.NewLine, heightData.OfType<int>()
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / heightData.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}")));*/
            var output = 1;
            var finalData = heightData.Cast<int>().ToArray();
            var basinSizes = new List<int>();
            for (int i = 0; i < lastUsedNumber; i++)
            {
                basinSizes.Add(finalData.Count(x => x == i));
            }
            
            var largest = basinSizes.OrderByDescending(x => x).Take(3);
            foreach (var i in largest)
            {
                output *= i;
            }
            return output;
        }

        private static long pt1(List<string> data)
        {
            var heightData = new int[data.Count, data[0].Length];
            for (var index = 0; index < data.Count; index++)
            {
                var line = data[index];
                for (var i = 0; i < line.Length; i++)
                {
                    var height = line[i];
                    heightData[index, i] = (int)char.GetNumericValue(height);
                }
            }

            var output = 0;
            for (int i = 0; i < heightData.GetLength(0); i++)
            {
                for (int j = 0; j < heightData.GetLength(1); j++)
                {
                    if ((i - 1 < 0 || heightData[i, j] < heightData[i - 1, j]) &&
                        (i + 1 >= heightData.GetLength(0) || heightData[i, j] < heightData[i + 1, j]) &&
                        (j - 1 < 0 || heightData[i, j] < heightData[i, j - 1]) &&
                        (j + 1 >= heightData.GetLength(1) || heightData[i, j] < heightData[i, j + 1])
                       )
                    {
                        output += (1 + heightData[i, j]);
                    }
                }
            }

            /*Console.WriteLine(string.Join(Environment.NewLine, heightData.OfType<int>()
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / heightData.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}")));
                */
            return output;
        }
    }
}