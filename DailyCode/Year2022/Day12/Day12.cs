using Common.Extensions;
using Common.Models;
using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2022.Day12
{
    internal class Day12 : BaseDay
    {
        private char[][] _inputs = null!;
        public Day12(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
            => _inputs = fileInputs.ToJaggedArray<char>();

        protected override string RunPart1()
        {
            var nodes = Dijkstra(_inputs);

            (int posX, int posY) position = (0,0);
            for (var y = 0; y < _inputs.Length; y++)
            {
                for (var x = 0; x < _inputs[y].Length; x++)
                {
                    if (_inputs[y][x] == 'E')
                        position = (x, y);
                }
            }

            return nodes.Single(node => node.PosX == position.posX && node.PosY == position.posY).Cost.ToString();
        }

        protected override string RunPart2()
        {
            (int posX, int posY) position = (0, 0);
            for (var y = 0; y < _inputs.Length; y++)
            {
                for (var x = 0; x < _inputs[y].Length; x++)
                {
                    if (_inputs[y][x] == 'E')
                        position = (x, y);
                }
            }

            var lowestCost = int.MaxValue;
            for (var y = 0; y < _inputs.Length; y++)
            {
                for (var x = 0; x < _inputs[y].Length; x++)
                {
                    if (_inputs[y][x] == 'S' || _inputs[y][x] == 'a')
                    {
                        var nodes = Dijkstra(_inputs, (x, y));
                        var cost = nodes.Single(node => node.PosX == position.posX && node.PosY == position.posY).Cost;
                        if (cost < lowestCost)
                            lowestCost = cost;
                    }
                       
                }
            }

            return lowestCost.ToString();
        }

        public List<Node<int>> Dijkstra(char[][] source, (int x, int y) startingPos)
        {
            var nodes = new List<Node<int>>();
            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source[y].Length; x++)
                {
                    nodes.Add(new Node<int>(x, y, x == startingPos.x && y == startingPos.y ? 0 : int.MaxValue, source[y][x] == 'S' ? 97 : source[y][x] == 'E' ? 'z' - 0 : source[y][x] - 0));
                }
            }

            DoSomething(source, nodes);

            return nodes;
        }

        public List<Node<int>> Dijkstra(char[][] source) //, (int x, int y) startingPos
        {
            var nodes = new List<Node<int>>();
            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source[y].Length; x++)
                {
                    nodes.Add(new Node<int>(x, y, source[y][x] == 'S' ? 0 : int.MaxValue, source[y][x] == 'S' ? 97 : source[y][x] == 'E' ? 'z' - 0 : source[y][x] - 0));
                }
            }

            DoSomething(source, nodes);

            return nodes;
        }

        private void DoSomething(char[][] source, List<Node<int>> nodes)
        {
            var queue = new PriorityQueue<Node<int>, int>();

            var startingNode = nodes.Single(x => x.Cost == 0);
            queue.Enqueue(startingNode, startingNode.Cost);

            while (nodes.Any(node => !node.IsVisitted))
            {
                if (queue.Count == 0)
                    break;

                var currentNode = queue.Dequeue();
                currentNode.IsVisitted = true;
                foreach (var node in GetAdjacentTiles(source, nodes, currentNode))
                {
                    if (currentNode.Cost + source[node.PosY][node.PosX] < node.Cost)
                        node.Cost = currentNode.Cost + 1;

                    if (!node.IsQueued)
                        queue.Enqueue(node, node.Cost);

                    node.IsQueued = true;
                }
            }
        }

        private List<Node<int>> GetAdjacentTiles(char[][] source, List<Node<int>> nodes, Node<int> currentNode)
        {
            var possibleTiles = new List<Node<int>>();

            if (currentNode.PosY > 0)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY - 1 && node.PosX == currentNode.PosX);
                if (node != null && DoSomething(currentNode, node))
                    possibleTiles.Add(node);
            }
            if (currentNode.PosY < source.Length - 1)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY + 1 && node.PosX == currentNode.PosX);
                if (node != null && DoSomething(currentNode, node))
                    possibleTiles.Add(node);
            }

            if (currentNode.PosX > 0)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY && node.PosX == currentNode.PosX - 1);
                if (node != null && DoSomething(currentNode, node))
                    possibleTiles.Add(node);
            }
            if (currentNode.PosX < source[currentNode.PosY].Length - 1)
            {
                var node = nodes.SingleOrDefault(node => node.PosY == currentNode.PosY && node.PosX == currentNode.PosX + 1);
                if (node != null && DoSomething(currentNode, node))
                    possibleTiles.Add(node);
            }

            return possibleTiles;
        }

        private bool DoSomething(Node<int> currentNode, Node<int> nextNode)
            => currentNode.Value >= nextNode.Value || currentNode.Value == (nextNode.Value - 1);
    }

  
}