namespace Common.Helpers.Extensions
{
    public static class JaggedArrayExtensions
    {
        public static T[][] Pivot<T>(this T[][] source)
        {
            var numRows = source.Max(a => a.Length);
            var items = new List<List<T>>();

            for (int row = 0; row < source.Length; ++row)
            {
                for (int col = 0; col < source[row].Length; ++col)
                {
                    if (items.Count <= col)
                        items.Add(new List<T>());

                    var current = items[col];

                    current.Add(source[row][col]);
                }
            }

            return (from i in items
                    select i.ToArray()
                    ).ToArray();
        }

        public static List<T> GetAdjacent<T>(this T[][] source, (int y, int x) currentPos, bool includeDiagonally)
        {
            const int range = 3;
            var result = Enumerable.Range(currentPos.y - 1, range)
                 .SelectMany(_ => Enumerable.Range(currentPos.x - 1, range), (y, x) => new { y, x })
                 .Where(possiblPos => possiblPos.y >= 0 && possiblPos.x >= 0 && (possiblPos.y != currentPos.y || possiblPos.x != currentPos.x)
                     && possiblPos.y < source.Length && possiblPos.x < source[currentPos.y].Length
                     && (includeDiagonally || (possiblPos.y == currentPos.y || possiblPos.x == currentPos.x)))
                 .Select(possiblPos => source[possiblPos.y][possiblPos.x]);

            return result.ToList();
        }
    }
}