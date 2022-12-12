namespace Common.Models
{
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

    public class Node<TValue> : Node
    {
        public TValue Value { get; set; }

        public Node(int posX, int posY, int cost, TValue value)
            : base(posX, posY, cost)
        {
            Value = value;
        }
    }
}