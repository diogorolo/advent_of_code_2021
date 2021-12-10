using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day5 : IDay<int, int, int, int>
    {
        private const int Day = 5;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public int Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateMatrix(data);
        }

        public int Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateMatrix(data, true);
        }

        public int PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateMatrix(data, true);
        }

        public int PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateMatrix(data);
        }

        private int CalculateMatrix(List<string> data, bool diagonal = false)
        {
            var streamPoints = new Dictionary<(int, int), int>();
            foreach (var streamData in data)
            {
                var streamStartingPoints = streamData.Split(" -> ");
                var stream = new Stream(streamStartingPoints[0].Split(",").Select(int.Parse).ToArray(),
                    streamStartingPoints[1].Split(",").Select(int.Parse).ToArray());
                var streamLine = stream.GetStreamLine(diagonal);
                foreach (var streamPoint in streamLine)
                {
                    if (streamPoints.ContainsKey(streamPoint))
                    {
                        streamPoints[streamPoint]++;
                    }
                    else
                    {
                        streamPoints[streamPoint] = 1;
                    }
                }
            }

            return streamPoints.Count(x => x.Value > 1);
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