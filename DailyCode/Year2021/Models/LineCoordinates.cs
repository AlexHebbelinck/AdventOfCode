namespace DailyCode.Year2021.Models
{
    public class LineCoordinates
    {
        public (int x, int y) From { get; set; }
        public (int x, int y) To { get; set; }

        public bool IsDiagonal
            => From.x != To.x && From.y != To.y;

        public int HighestXCoordinate
            => Math.Max(From.x, To.x);

        public int HighestYCoordinate
            => Math.Max(From.y, To.y);

        public List<(int x, int y)> GetFullLineRange()
        {
            if (IsDiagonal)
                return GetFullDiagonalLineRange();

            if (From.x != To.x)
                return GetRange(From.x, To.x).Select(x => (x, From.y)).ToList();

            return GetRange(From.y, To.y).Select(y => (From.x, y)).ToList();
        }

        private IEnumerable<int> GetRange(int fromPos, int toPos)
        {
            var minPos = Math.Min(fromPos, toPos);
            var maxPos = Math.Max(fromPos, toPos);

            var range = Enumerable.Range(minPos, (maxPos - minPos) + 1);
            return fromPos < toPos
                ? range.Reverse()
                : range;
        }

        private List<(int x, int y)> GetFullDiagonalLineRange()
        {
            var xRanges = GetRange(From.x, To.x).ToList();
            var yRanges = GetRange(From.y, To.y).ToList();

            return xRanges.Join(yRanges, x => xRanges.IndexOf(x), y => yRanges.IndexOf(y), (x, y) => (x, y)).ToList();
        }

    }
}