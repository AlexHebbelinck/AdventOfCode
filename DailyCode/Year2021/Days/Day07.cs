using DailyCode.Base;
using System.Linq;

namespace DailyCode.Year2021.Days
{
    public class Day07 : BaseDay
    {
        private List<int> _sortedNumbers = new();

        public Day07(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            _sortedNumbers = fileInput[0].Split(',')
               .Select(x => int.Parse(x))
               .OrderBy(x => x)
               .ToList();
        }

        protected override string RunPart1()
        {
            var halfIndex = _sortedNumbers.Count / 2;
            var median = (_sortedNumbers.Count % 2) == 0
                ? ((_sortedNumbers[halfIndex] + _sortedNumbers[halfIndex - 1]) / 2)
                : _sortedNumbers[halfIndex];

            return _sortedNumbers.Select(x => Math.Abs(x - median)).Sum().ToString();
        }

        protected override string RunPart2()
        {
            var lowestFuelUse = long.MaxValue;

            for(int i = 0; i < _sortedNumbers.Max(); i++)
            {
                var fuelUse = _sortedNumbers.Select(x => Resolve(Math.Abs(x - i))).Sum();
                if (fuelUse < lowestFuelUse)
                    lowestFuelUse = fuelUse;
            }

            return lowestFuelUse.ToString();
        }

        public static int Resolve(int input)
            => input * (input + 1) / 2;

    }
}