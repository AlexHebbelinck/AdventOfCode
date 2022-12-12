using Common.Helpers;
using Common.Models;

namespace DailyCode.Year2021.Models
{
    public class DijkstraHelper : BaseDijkstraHelper<int>
    {
        public (int x, int y) StartingPos;

        public DijkstraHelper((int x, int y) startingPos)
        {
            StartingPos = startingPos;
        }

        public override Node CreateNode(int x, int y, int sourceVal)
            => new(x, y, x == StartingPos.x && y == StartingPos.y ? 0 : int.MaxValue);
    }
}