using Common.Helpers.Extensions;
using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day09 : BaseDay
    {
        private int[][] _fileInput = Array.Empty<int[]>();

        public Day09(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
            => _fileInput = fileInput.Select(x => x.Select(letter => int.Parse(letter.ToString())).ToArray()).ToArray();

        protected override long RunPart1()
        {
            var lowestPoints = new List<int>();

            for (var x = 0; x < _fileInput.Length; x++)
            {
                for (var y = 0; y < _fileInput[x].Length; y++)
                {
                    var currentNumber = _fileInput[x][y];
                    if (currentNumber < 9)
                    {
                        var adjacentNumbers = _fileInput.GetAdjacent((x, y), false);

                        if (adjacentNumbers.Min() > currentNumber)
                            lowestPoints.Add(currentNumber);
                    }
                }
            }

            return lowestPoints.Sum() + (lowestPoints.Count * 1);
        }

        protected override long RunPart2()
        {
            var basinTotals = new List<int>();
            for (var x = 0; x < _fileInput.Length; x++)
            {
                for (var y = 0; y < _fileInput[x].Length; y++)
                {
                    var currentNumber = _fileInput[x][y];
                    if (currentNumber < 9)
                    {
                        var adjacentNumbers = _fileInput.GetAdjacent((x, y), false);

                        if (adjacentNumbers.Min() > currentNumber)
                            basinTotals.Add(CollectBasin((x, y)).Distinct().Count());
                    }
                }
            }

            return basinTotals.OrderByDescending(x => x).Take(3).Aggregate((a, x) => a * x);
        }

        private List<int> GetAdjecentNumbers((int x, int y) currentPos)
        {
            var adjacentNumbers = new List<int>();

            if (currentPos.x > 0)
                adjacentNumbers.Add(_fileInput[currentPos.x - 1][currentPos.y]);
            if (currentPos.x < _fileInput.Length - 1)
                adjacentNumbers.Add(_fileInput[currentPos.x + 1][currentPos.y]);

            if (currentPos.y > 0)
                adjacentNumbers.Add(_fileInput[currentPos.x][currentPos.y - 1]);
            if (currentPos.y < _fileInput[currentPos.x].Length - 1)
                adjacentNumbers.Add(_fileInput[currentPos.x][currentPos.y + 1]);

            return adjacentNumbers;
        }

        private List<int> CollectBasin((int x, int y) currentPos)
        {
            var currentValue = _fileInput[currentPos.x][currentPos.y];
            if(currentValue < 9)
            {
                var adjacentBasinNumbers = new List<int> { string.GetHashCode($"{currentPos.x}{currentPos.y}") };

                if (currentValue < 8)
                {
                    if (currentPos.x > 0 && _fileInput[currentPos.x - 1][currentPos.y] > currentValue)
                        adjacentBasinNumbers.AddRange(CollectBasin((currentPos.x - 1, currentPos.y)));

                    if (currentPos.x < _fileInput.Length - 1 && _fileInput[currentPos.x + 1][currentPos.y] > currentValue)
                        adjacentBasinNumbers.AddRange(CollectBasin((currentPos.x + 1, currentPos.y)));

                    if (currentPos.y > 0 && _fileInput[currentPos.x][currentPos.y - 1] > currentValue)
                        adjacentBasinNumbers.AddRange(CollectBasin((currentPos.x, currentPos.y - 1)));

                    if (currentPos.y < _fileInput[currentPos.x].Length - 1 && _fileInput[currentPos.x][currentPos.y + 1] > currentValue)
                        adjacentBasinNumbers.AddRange(CollectBasin((currentPos.x, currentPos.y + 1)));
                }
                return adjacentBasinNumbers;
            }

            return new List<int>();
        }
    }
}