using Common.Helpers;

namespace DailyCode.Year2021.Models
{
    public class DijkstraHelper : BaseDijkstraHelper<int>
    {
        private (int x, int y) _startingPos;

        public DijkstraHelper((int x, int y) startingPos)
        {
            _startingPos = startingPos;
        }

        protected override bool IsStartingPosition(int x, int y, int sourceVal)
            => x == _startingPos.x && y == _startingPos.y;
    }
}