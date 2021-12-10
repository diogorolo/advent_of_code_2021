using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day6 : IDay<long, long, long, long>
    {
        private const int Day = 6;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculatePopulation(data,80);
        }

        public long Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculatePopulation(data, 256);
        }

        public long PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculatePopulation(data, 256);
        }

        public long PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculatePopulation(data,80);
        }

        private long CalculatePopulation(List<int> data, int days)
        {
            Dictionary<int, long> daysLeft = new Dictionary<int, long>();
            for (int i = 0; i <= 8; i++)
            {
                daysLeft[i] = 0;
            }
            foreach (var fish in data)
            {
                daysLeft[fish]++;
            }
            for (int i = 0; i < days; i++)
            {
                var aux = daysLeft[0];
                daysLeft[0] = daysLeft[1];
                daysLeft[1] = daysLeft[2];
                daysLeft[2] = daysLeft[3];
                daysLeft[3] = daysLeft[4];
                daysLeft[4] = daysLeft[5];
                daysLeft[5] = daysLeft[6];
                daysLeft[6] = daysLeft[7]+ aux;
                daysLeft[7] = daysLeft[8];
                daysLeft[8] = aux;
            }

            return daysLeft.Sum(x => x.Value);
        }

        class Stream
        {
            private (int x, int y) from;
            private (int x, int y) to;

            internal Stream(int[] from, int[] to)
            {
                this.from = (from[0], from[1]);
                this.to = (to[0], to[1]);
            }

            internal List<(int, int)> GetStreamLine(bool diagonal = false)
            {
                var streamLine = new List<(int, int)>();

                if (@from.x != to.x && @from.y != to.y && !diagonal)
                {
                    return streamLine;
                }

                (int x, int y) direction = (to.x - @from.x, to.y - @from.y);

                var length = Math.Max(Math.Abs(direction.x), Math.Abs(direction.y));
                for (var i = 0; i <= length; i++)
                {
                    streamLine.Add((from.x + i * Math.Sign(direction.x), from.y +  i * Math.Sign(direction.y)));
                }
                
                return streamLine;
            }
        }
    }
}