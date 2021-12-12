using Common.Helpers.Extensions;
using DailyCode.Base;
using DailyCode.Year2021.Models;

namespace DailyCode.Year2021.Days
{
    public class Day11 : BaseDay
    {
        private DumboOctopus[][] _dumboOctopi = Array.Empty<DumboOctopus[]>();

        public Day11(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            _dumboOctopi = fileInput.Select(line => line.Select(letter => new DumboOctopus(int.Parse(letter.ToString()))).ToArray()).ToArray();
            for (int y = 0; y < _dumboOctopi.Length; y++)
            {
                for (int x = 0; x < _dumboOctopi[y].Length; x++)
                {
                    _dumboOctopi[y][x].AdjacentDumboOctopi = _dumboOctopi.GetAdjacent((y, x), true);
                }
            }
        }

        protected override long RunPart1()
        {
            const int steps = 100;
            for (int step = 1; step <= steps; step++)
            {
                HandleOctopi(step);
            }

            return _dumboOctopi.Sum(y => y.Sum(x => x.TotalFlashes));
        }

        protected override long RunPart2()
        {
            var step = 0;
            do
            {
                ++step;
                HandleOctopi(step);
            }
            while (!_dumboOctopi.All(y => y.All(x => x.FlashedOn == step)));

            return step;
        }

        private void HandleOctopi(int step)
        {
            for (int y = 0; y < _dumboOctopi.Length; y++)
            {
                for (int x = 0; x < _dumboOctopi[y].Length; x++)
                {
                    _dumboOctopi[y][x].IncreaseEnergyLevel(step);
                }
            }
        }
    }
}