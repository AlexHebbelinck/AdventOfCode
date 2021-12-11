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

        //TODO: Clean up later...
        public static List<T> GetAdjacent<T>(this T[][] source, (int y, int x) currentPos, bool includeDiagonally)
        {
            var adjacentNumbers = new List<T>();

            if (currentPos.y > 0)
                adjacentNumbers.Add(source[currentPos.y - 1][currentPos.x]);
            if (currentPos.y < source.Length - 1)
                adjacentNumbers.Add(source[currentPos.y + 1][currentPos.x]);

            if (currentPos.x > 0)
                adjacentNumbers.Add(source[currentPos.y][currentPos.x - 1]);
            if (currentPos.x < source[currentPos.y].Length - 1)
                adjacentNumbers.Add(source[currentPos.y][currentPos.x + 1]);


            if (includeDiagonally)
            {
                if (currentPos.y > 0)
                {
                    if(currentPos.x > 0)
                        adjacentNumbers.Add(source[currentPos.y - 1][currentPos.x - 1]);
                    if (currentPos.x < source[currentPos.y].Length - 1)
                        adjacentNumbers.Add(source[currentPos.y - 1][currentPos.x + 1]);
                }

                if (currentPos.y < source.Length - 1)
                {
                    if (currentPos.x > 0)
                        adjacentNumbers.Add(source[currentPos.y + 1][currentPos.x - 1]);
                    if (currentPos.x < source[currentPos.y].Length - 1)
                        adjacentNumbers.Add(source[currentPos.y + 1][currentPos.x + 1]);
                }
            }
            return adjacentNumbers;
        }
    }
}