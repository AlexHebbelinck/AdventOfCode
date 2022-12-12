using Common.Models;

namespace Common.Helpers
{
    public abstract class BaseDijkstraHelper<TSourceVal, TNodeVal>
    {
        //protected TSourceVal[][] Source = null!;

        //protected BaseDijkstraHelper(TSourceVal[][] source)
        //{
        //    Source = source;
        //}

        public List<Node<TNodeVal>> CreateNodes(int[][] source)
        {
            var nodes = new List<Node<TNodeVal>>();

            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source[y].Length; x++)
                {
                    nodes.Add(new Node<TNodeVal>(x, y, IsStartingPosition(x, y, source[x][y]) ? 0 : int.MaxValue, CreateNodeValue(source[x][y])));
                }
            }

            return nodes;
        }

        public void RunAlgorithm(int[][] source, List<Node<TNodeVal>> nodes) //Replace all the int[][] source.
        {
            var queue = new PriorityQueue<Node<TNodeVal>, int>();

            var startingNode = nodes.Single(x => x.Cost == 0);
            queue.Enqueue(startingNode, startingNode.Cost);

            while (nodes.Any(node => !node.IsVisitted))
            {
                var currentNode = queue.Dequeue();
                currentNode.IsVisitted = true;
                foreach (var node in BaseDijkstraHelper<TSourceVal, TNodeVal>.GetAdjacentTiles(source, nodes, currentNode))
                {
                    if (currentNode.Cost + source[node.PosY][node.PosX] < node.Cost)
                        node.Cost = currentNode.Cost + source[node.PosY][node.PosX];

                    if (!node.IsQueued)
                        queue.Enqueue(node, node.Cost);

                    node.IsQueued = true;
                }
            }
        }

        private static List<Node<TNodeVal>> GetAdjacentTiles(int[][] source, List<Node<TNodeVal>> nodes, Node<TNodeVal> currentNode)
        {
            var possibleTiles = new List<Node<TNodeVal>>();

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

        protected abstract bool IsStartingPosition(int x, int y, TSourceVal sourceVal);

        protected abstract TNodeVal CreateNodeValue(TSourceVal sourceVal);
    }

    public abstract class BaseDijkstraHelper<TSourceVal> : BaseDijkstraHelper<TSourceVal, TSourceVal>
    {
        protected override TSourceVal CreateNodeValue(TSourceVal sourceVal)
            => sourceVal;
    }
}