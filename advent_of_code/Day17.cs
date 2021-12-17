using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace advent_of_code
{
    public class Day17 : IDay<long, long, long, long>
    {
        private const int Day = 17;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data);
        }

        public long Part2()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest2()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest1()
        {
            var data = File.ReadLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        class Area
        {
            public (int x, int y) StartPoint { get; set; }
            public (int x, int y) EndPoint { get; set; }

            public bool IsHit((int x, int y) point)
            {
                return StartPoint.x <= point.x && point.x <= EndPoint.x &&
                       StartPoint.y <= point.y && point.y <= EndPoint.y
                       ;
            }
        }

        private static long DoDay(List<string> data, bool isPt2 = false)
        {
            var targetArea = new Area();
            var target = data[0].Replace("target area: ", "");
            target = target.Replace("x=", "");
            target = target.Replace("y=", "");
            //target area: x=20..30, y=-10..-5
            var ranges = target.Split(",");
            var Xs = ranges[0].Split("..").Select(int.Parse).ToArray();
            var Ys = ranges[1].Split("..").Select(int.Parse).ToArray();
            targetArea.StartPoint = (Xs[0], Ys[0]);
            targetArea.EndPoint = (Xs[1], Ys[1]);

            (int x, int y) curPoint = (0, 0);
            
            long maxHeight = long.MinValue;
            long hitCounter = 0;
            int maxSteps = isPt2 ? 1000 : 100;
            for(int i = 0; i < maxSteps; i++)
            {
                for(int j = -maxSteps ; j < maxSteps ; j++){
                    (int x, int y) curSpeed = (i, j);

                    if (IsHitArea(curPoint, targetArea, curSpeed, out long height ))
                    {
                        hitCounter++;
                        if(isPt2){
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine($"{curSpeed.x},{curSpeed.y}");
                        }
                        if (height > maxHeight)
                        {
                            maxHeight = height;
                        }
                    };
                }
            }
            Console.ResetColor();
            if (isPt2)
                return hitCounter;
            return maxHeight;
        }

        private static bool IsHitArea((int x, int y) StartPoint, Area TargetArea, (int x, int y) StartSpeed, out long maxHeight)
        {
            maxHeight = StartPoint.y;
            bool print = false;
            if (StartSpeed.x == 6 && StartSpeed.y == -1)
            {
                //print = true;
            }
            while (StartPoint.x <= TargetArea.EndPoint.x && StartPoint.y >= TargetArea.StartPoint.y)
            {
                if(print)
                    Console.WriteLine($"{StartPoint.x},{StartPoint.y}");
                StartPoint.x += StartSpeed.x;
                StartPoint.y += StartSpeed.y;

                if (StartSpeed.x > 0)
                {
                    StartSpeed.x--;
                }
                if (StartPoint.y > maxHeight)
                {
                    maxHeight = StartPoint.y;
                }
       
                StartSpeed.y--;
                
                if (TargetArea.IsHit(StartPoint))
                {
                    return true;
                }
            }
            if(print)
                Console.WriteLine($"{StartPoint.x},{StartPoint.y}");

            return false;
        }
    }
}