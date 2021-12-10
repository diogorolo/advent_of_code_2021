using System.Collections.Generic;
using System.Linq;

namespace advent_of_code
{
    public class Day1 : IDay<int, int, int, int>
    {
        private const int Day = 1;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public int Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").Select(int.Parse).ToList();
            return GetIncreasedCount(data);
        }

        public int Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").Select(int.Parse).ToList();
            return GetIncreasedCount(data,3);
        }

        public int PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").Select(int.Parse).ToList();
            return GetIncreasedCount(data);
        }

        public int PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").Select(int.Parse).ToList();
            return GetIncreasedCount(data,3);
        }

        private static int GetIncreasedCount(List<int>? data, int window = 1)
        {
            var prevValue = data.First();
            var increasedCount = 0;
            for (var index = 0; index < data.Count - window + 1; index++)
            {
                var valWindow = 0;
                for (int i = 0; i < window ; i++)
                {
                    valWindow += data[index + i];
                }

                if (index > 0 && valWindow > prevValue)
                {
                    increasedCount++;
                }

                prevValue = valWindow;
            }

            return increasedCount;
        }
    }
}