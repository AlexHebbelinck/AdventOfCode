namespace DailyCode.Year2023.Day08.Models
{
    public class Node(string name, string leftName, string rightName)
    {
        public string Name { get; init; } = name;

        private string LeftName { get; init; } = leftName;

        public Node Left { get; set; } = null!;

        private string RightName { get; init; } = rightName;

        public Node Right { get; set; } = null!;

        public void CreateNetwork(List<Node> nodes)
        {
            if (Left == null)
            {
                Left = nodes.First(x => x.Name.Equals(LeftName));
                Left.CreateNetwork(nodes);
            }

            if (Right == null)
            {
                Right = nodes.First(x => x.Name.Equals(RightName));
                Right.CreateNetwork(nodes);
            }
        }

        public Node HandleInstruction(char instruction)
            => instruction == 'R' ? Right : Left;
    }

    public class NodeWrapper(Node node)
    {
        public Node StartNode { get; set; } = node;
        public int Total { get; set; } = 1;
        public bool FoundPattern { get; set; }
    }
}