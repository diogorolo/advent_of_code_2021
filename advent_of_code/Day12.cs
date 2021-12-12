namespace advent_of_code
{
    public class Day12 : IDay<long, long, long, long>
    {
        private const int Day = 12;
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

        class Node
        {
            public string Name { get; init; }
            public bool IsBig { get; init; }
            public List<Node> Connections { get; } = new();
        }

        private long DoDay(List<string> data, bool isPt2 = false)
        {
            var nodes = new List<Node>();
            foreach (var line in data)
            {
                var connections = line.Split("-");
                var node1 = nodes.FirstOrDefault(x => x.Name == connections[0]);
                var node2 = nodes.FirstOrDefault(x => x.Name == connections[1]);
                if (node1 == null)
                {
                    node1 = new Node()
                        { Name = connections[0], IsBig = connections[0][0] < 'a' || connections[0] == "end" };
                    nodes.Add(node1);
                }

                if (node2 == null)
                {
                    node2 = new Node()
                        { Name = connections[1], IsBig = connections[1][0] < 'a' || connections[1] == "end" };
                    nodes.Add(node2);
                }

                if (node2.Name != "start")
                    node1.Connections.Add(node2);
                if (node1.Name != "start")
                    node2.Connections.Add(node1);
            }

            var startNode = nodes.First(x => x.Name == "start");
            var endNode = nodes.First(x => x.Name == "end");
            endNode.Connections.Clear();
            return Go(startNode, new List<Node>(), isPt2, false);
        }

        private static long Go(Node node, List<Node> currentPath, bool isPt2, bool hasDoubleVisited)
        {
            long validPaths = 0;
            if (node.Name == "end")
            {
                currentPath.Add(node);
                return 1;
            }


            if (!node.IsBig && currentPath.Any(x => x.Name == node.Name))
            {
                if (isPt2 && !hasDoubleVisited)
                {
                    hasDoubleVisited = true;
                }
                else
                {
                    return 0;
                }
            }
            currentPath.Add(node);
            foreach (var connection in node.Connections)
            {
                validPaths += Go(connection, new List<Node>(currentPath), isPt2, hasDoubleVisited);
            }

            return validPaths;
        }
    }
}