using Common.Helpers.Extensions;
using DailyCode.Base;
using DailyCode.Year2021.Models;

namespace DailyCode.Year2021.Days
{
    public class Day11 : BaseDay
    {
        private DumboOctopus[][] _fileInput = Array.Empty<DumboOctopus[]>();

        public Day11(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            _fileInput = fileInput.Select(line => line.Select(letter => new DumboOctopus(int.Parse(letter.ToString()))).ToArray()).ToArray();
            for (int y = 0; y < _fileInput.Length; y++)
            {
                for (int x = 0; x < _fileInput[y].Length; x++)
                {
                    _fileInput[y][x].AdjacentDumboOctopi = _fileInput.GetAdjacent((y, x), true);
                }
            }
        }

        protected override string RunPart1()
        {
            const int steps = 100;
            for (int step = 0; step <= steps; step++)
            {
                HandleOctopi(step);
            }

            return _fileInput.Sum(y => y.Sum(x => x.TotalFlashes)).ToString();
        }

        protected override string RunPart2()
        {
            var step = 0;
            do
            {
                ++step;
                HandleOctopi(step);
            }
            while (!_fileInput.All(y => y.All(x => x.FlashedOn == step)));

            return step.ToString();
        }

        private void HandleOctopi(int step)
        {
            for (int y = 0; y < _fileInput.Length; y++)
            {
                for (int x = 0; x < _fileInput[y].Length; x++)
                {
                    _fileInput[y][x].IncreaseEnergyLevel(step);
                }
            }
        }
    }
}