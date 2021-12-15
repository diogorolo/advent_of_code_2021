using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace advent_of_code
{
    public class Day15 : IDay<long, long, long, long>
    {
        private const int Day = 15;
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

        class Node
        {
            public List<Node> Nodes = new List<Node>();
            public long Cost { get; set; }
            public int X { get; init; }
            public int Y { get; init; }
            public long MinCostFromStart { get; set; } = long.MaxValue;
            public Node PreviousNode { get; set; }
        }

        private static int max = 0;

        private static long DoDay(List<string> data, bool isPt2 = false)
        {
            var nodes = new Dictionary<(int x, int y), Node>();
            FillData(data, nodes, isPt2);
            max = nodes.Max(x => x.Value.X);
            SetNeighbours(nodes);

            var start = nodes[(0, 0)];
            start.MinCostFromStart = 0;
            var end = nodes[(max, max)];

            //Dijkstra(nodes.Select(x => x.Value).ToList()); //new List<Node>(){start}));
            PriorityQueue<Node, int> q = new PriorityQueue<Node, int>();
            q.Enqueue(start,0);
            Dijkstra(q);
            var path = new List<Node>();
            path.Add(end);
            var curNode = end;
            while (curNode != null)
            {
                curNode = curNode.PreviousNode;
                path.Add(curNode);
            }

            /*if (isPt2)
                PrintIt(nodes, path);*/
            return end.MinCostFromStart;
        }

        private static void PrintIt(List<Node> nodes, List<Node> path)
        {
            for (int i = 0; i <= nodes.Max(x => x.X); i++)
            {
                for (int j = 0; j <= nodes.Max(x => x.Y); j++)
                {
                    var node = nodes.First(x => x.X == i && x.Y == j);
                    if (path.Contains(node))
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.Write(node.Cost);
                    // Console.Write($"   {node.MinCostFromStart:D4}   ");
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private static void Dijkstra(PriorityQueue<Node, int> nodes)
        {
            while (nodes.Count > 0)
            {
                var closest = nodes.Dequeue();
                foreach (var node in closest.Nodes)
                {
                    //if (nodes.Contains(node))
                    {
                        long dist = closest.MinCostFromStart + node.Cost;
                        if (dist < node.MinCostFromStart)
                        {
                            node.MinCostFromStart = dist;
                            node.PreviousNode = closest;
                            nodes.Enqueue(node, (int)dist);
                        }
                    }
                }
            }
        }

        private static void SetNeighbours(Dictionary<(int x, int y), Node> nodes)
        {
            foreach (var node in nodes.Values)
            {
                if (node.Y + 1 <= max)
                    node.Nodes.Add(nodes[(node.X, node.Y + 1)]);
                if (node.Y - 1 >= 0)
                    node.Nodes.Add(nodes[(node.X, node.Y - 1)]);
                if (node.X + 1 <= max)
                    node.Nodes.Add(nodes[(node.X + 1, node.Y)]);
                if (node.X - 1 >= 0)
                    node.Nodes.Add(nodes[(node.X - 1, node.Y)]);
            }
        }

        private static void FillData(List<string> data, Dictionary<(int, int), Node> nodes, bool isPt2)
        {
            var max = data.Count;
            for (var i = 0; i < max; i++)
            {
                if (string.IsNullOrEmpty(data[i]))
                    continue;

                for (var j = 0; j < max; j++)
                {
                    if (!isPt2)
                    {
                        var node = new Node
                        {
                            X = i,
                            Y = j,
                            Cost = data[i][j] - '0',
                            MinCostFromStart = long.MaxValue,
                        };
                        nodes.Add((node.X, node.Y), node);
                    }
                    else
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            for (int kk = 0; kk < 5; kk++)
                            {
                                var cost = (data[i][j] - '0' + k + kk);
                                if (cost >= 10)
                                    cost -= 9;
                                var node = new Node
                                {
                                    X = i + k * max,
                                    Y = j + kk * max,
                                    Cost = cost,
                                    MinCostFromStart = long.MaxValue,
                                };
                                nodes.Add((node.X, node.Y), node);
                            }
                        }
                    }
                }
            }
        }
    }
}