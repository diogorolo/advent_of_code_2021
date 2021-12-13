namespace advent_of_code
{
    public class Day13 : IDay<long, long, long, long>
    {
        private const int Day = 13;
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
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        enum direction
        {
            x,
            y
        }
        private static List<(int x, int y)> GetPoints(List<string> data)
        {
            var points = new List<(int x, int y)>();
            
            foreach (var line in data)
            {
                if(line.Contains("fold") || string.IsNullOrEmpty(line))
                    continue;
                var pointData = line.Split(",").Select(int.Parse).ToList();
                points.Add((pointData[0], pointData[1]));
            }

            return points;
        }
        
        private static List<(int fold, direction dir)> GetFolds(List<string> data)
        {
            var folds = new List<(int fold, direction dir)>();
            
            foreach (var line in data)
            {
                if(line.Contains("fold"))
                {
                    var foldData = line.Replace("fold along ","").Split("=").ToList();
                    folds.Add((int.Parse(foldData[1]), foldData[0] == "y" ? direction.y : direction.x));
                }
            }
            return folds;
        }

        private static void PrintIt(List<(int x, int y)> points)
        {
            var maxX = points.OrderByDescending(x => x.x).First().x;
            var maxY = points.OrderByDescending(x => x.y).First().y;
            Console.WriteLine("=================================");
            for (int i = 0; i <= maxY; i++)
            {
                for (int j = 0; j <= maxX; j++)
                {
                    if (points.Contains((j, i)))
                    {
                        Console.Write("x");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private static long DoDay(List<string> data, bool isPt2 = false)
        {
            var points = GetPoints(data);
            var folds = GetFolds(data);
            //PrintIt(points);
            foreach (var fold in folds)
            {
                if (fold.dir == direction.y)
                {
                    var fold1 = fold;
                    var foldedPoints = points.Where(pt => pt.y > fold1.fold).ToList();
                    foreach (var point in foldedPoints)
                    {
                        int newY = fold.fold - (point.y - fold.fold);
                        points.Add((point.x, newY));
                        points.Remove(point);
                    }
                }
                else
                {
                    var foldedPoints = points.Where(pt => pt.x > fold.fold).ToList();
                    foreach (var point in foldedPoints)
                    {
                        int newX = fold.fold - (point.x - fold.fold);
                        points.Add((newX, point.y));
                        points.Remove(point);
                    }
                }

                if (!isPt2)
                    break;
            }
            if(isPt2)
                PrintIt(points);
            return points.Distinct().Count();
        }

    }
}