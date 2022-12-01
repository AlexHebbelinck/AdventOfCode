using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day15 : BaseDay
    {
        private int[][] _fileInput = Array.Empty<int[]>();

        public Day15(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            _fileInput = fileInput.Select(line => line.Select(number => int.Parse(number.ToString())).ToArray()).ToArray();
        }

        protected override string RunPart1()
        {
            var nodes = Dijkstra(_fileInput, (0, 0));
            return nodes.Single(node => node.PosX == _fileInput[0].Length - 1 && node.PosY == _fileInput.Length - 1).Cost.ToString();
        }

        protected override string RunPart2()
        {
            CreateBiggerJaggedArray(5, 5);
            var nodes = Dijkstra(_fileInput, (0, 0));
            return nodes.Single(node => node.PosX == _fileInput[0].Length - 1 && node.PosY == _fileInput.Length - 1).Cost.ToString();
        }

        private void CreateBiggerJaggedArray(int maxX, int maxY)
        {
            var xLength = _fileInput[0].Length;
            for (int i = 1; i < maxX; i++)
            {
                for (var y = 0; y < _fileInput.Length; y++)
                {
                    var o = _fileInput[y];
                    for (var x = 0; x < xLength; x++)
                    {
                        var amount = (_fileInput[y][x] + i);
                        o = o.Append(amount > 9 ? amount - 9 : amount).ToArray();
                    }
                    _fileInput[y] = o;
                }
            }

            var l = new List<int[]>();
            for (int i = 1; i < maxY; i++)
            {
                for (var y = 0; y < _fileInput.Length; y++)
                {
                    var o = Array.Empty<int>();
                    foreach (var p in _fileInput[y])
                    {
                        var amount = (p + i);
                        o = o.Append(amount > 9 ? amount - 9 : amount).ToArray();
                    }
                    l.Add(o);
                }
            }
            l.ForEach(x => _fileInput = _fileInput.Append(x).ToArray());
        }

        public List<Node> Dijkstra(int[][] source, (int x, int y) startingPos)
        {
            var nodes = new List<Node>();
            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source[y].Length; x++)
                {
                    nodes.Add(new Node(x, y, x == startingPos.x && y == startingPos.x ? 0 : int.MaxValue));
                }
            }
            DoSomething(source, nodes);

            return nodes;
        }

        private void DoSomething(int[][] source, List<Node> nodes)
        {
            var queue = new PriorityQueue<Node, int>();

            var startingNode = nodes.Single(x => x.Cost == 0);
            queue.Enqueue(startingNode, startingNode.Cost);

            while (nodes.Any(node => !node.IsVisitted))
            {
                var currentNode = queue.Dequeue();
                currentNode.IsVisitted = true;
                foreach (var node in GetAdjacentTiles(source, nodes, currentNode))
                {
                    if (currentNode.Cost + source[node.PosY][node.PosX] < node.Cost)
                        node.Cost = currentNode.Cost + source[node.PosY][node.PosX];

                    if (!node.IsQueued)
                        queue.Enqueue(node, node.Cost);

                    node.IsQueued = true;
                }
            }
        }

        private List<Node> GetAdjacentTiles(int[][] source, List<Node> nodes, Node currentNode)
        {
            var possibleTiles = new List<Node>();

            if (currentNode.PosY > 0)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY - 1 && node.PosX == currentNode.PosX);
                if (node != null)
                    possibleTiles.Add(node);
            }
            if (currentNode.PosY < source.Length - 1)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY + 1 && node.PosX == currentNode.PosX);
                if (node != null)
                    possibleTiles.Add(node);
            }

            if (currentNode.PosX > 0)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY && node.PosX == currentNode.PosX - 1);
                if (node != null)
                    possibleTiles.Add(node);
            }
            if (currentNode.PosX < source[currentNode.PosY].Length - 1)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY && node.PosX == currentNode.PosX + 1);
                if (node != null)
                    possibleTiles.Add(node);
            }

            return possibleTiles;
        }
    }

    public class Node
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Cost { get; set; }

        public bool IsQueued { get; set; }
        public bool IsVisitted { get; set; }

        public Node(int posX, int posY, int cost)
        {
            PosX = posX;
            PosY = posY;
            Cost = cost;
        }
    }
}