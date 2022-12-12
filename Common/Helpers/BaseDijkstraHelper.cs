using Common.Models;

namespace Common.Helpers
{
    public abstract class BaseDijkstraHelper<TSourceVal, TNodeVal>
    {
        public void DoSomething(TSourceVal[][] source)
        {
            var nodes = new List<Node<TNodeVal>>();
            for (var y = 0; y < source.Length; y++)
            {
                for (var x = 0; x < source[y].Length; x++)
                {
                    nodes.Add(new Node(x, y, x == startingPos.x && y == startingPos.y ? 0 : int.MaxValue));
                }
            }
        }

        //nodes.Add();

        protected abstract Node CreateNode(int x, int y, TSourceVal sourceVal);
        public abstract Node CreateNode(int x, int y, TSourceVal sourceVal);
    }
}