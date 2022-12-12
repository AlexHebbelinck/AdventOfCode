using DailyCode.Base;
using DailyCode.Year2021.Models;

namespace DailyCode.Year2021.Days
{
    public class Day15 : BaseDay
    {
        private int[][] _fileInput = Array.Empty<int[]>();

        public Day15(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            _fileInput = fileInput.Select(line => line.Select(number => int.Parse(number.ToString())).ToArray()).ToArray();
        }

        protected override string RunPart1()
        {
            var dijkstraHelper = new DijkstraHelper((0, 0));
            var nodes = dijkstraHelper.CreateNodes(_fileInput);
            dijkstraHelper.RunAlgorithm(_fileInput, nodes);

            return nodes.Single(node => node.PosX == _fileInput[0].Length - 1 && node.PosY == _fileInput.Length - 1).Cost.ToString();
        }

        protected override string RunPart2()
        {
            CreateBiggerJaggedArray(5, 5);

            var dijkstraHelper = new DijkstraHelper((0, 0));
            var nodes = dijkstraHelper.CreateNodes(_fileInput);
            dijkstraHelper.RunAlgorithm(_fileInput, nodes);

            return nodes.Single(node => node.PosX == _fileInput[0].Length - 1 && node.PosY == _fileInput.Length - 1).Cost.ToString();
        }

        private void CreateBiggerJaggedArray(int maxX, int maxY)
        {
            var xLength = _fileInput[0].Length;
            for (int i = 1; i < maxX; i++)
            {
                for (var y = 0; y < _fileInput.Length; y++)
                {
                    var o = _fileInput[y];
                    for (var x = 0; x < xLength; x++)
                    {
                        var amount = (_fileInput[y][x] + i);
                        o = o.Append(amount > 9 ? amount - 9 : amount).ToArray();
                    }
                    _fileInput[y] = o;
                }
            }

            var l = new List<int[]>();
            for (int i = 1; i < maxY; i++)
            {
                for (var y = 0; y < _fileInput.Length; y++)
                {
                    var o = Array.Empty<int>();
                    foreach (var p in _fileInput[y])
                    {
                        var amount = (p + i);
                        o = o.Append(amount > 9 ? amount - 9 : amount).ToArray();
                    }
                    l.Add(o);
                }
            }
            l.ForEach(x => _fileInput = _fileInput.Append(x).ToArray());
        }
    }
}