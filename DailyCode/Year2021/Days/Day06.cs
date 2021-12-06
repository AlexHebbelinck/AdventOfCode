using DailyCode.Base;
using System.Diagnostics;

namespace DailyCode.Year2021.Days
{
    public class Day06 : BaseDay
    {
        private LinkedList<long> _totalFishesPerDay = new();

        private readonly Queue<long> _newFishesPerDay = new();

        public Day06(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            var collection = new List<long>(new long[7]);

            fileInput[0].Split(',')
                .Select(x => int.Parse(x))
                .ToList()
                .ForEach(x => ++collection[x]);

            _totalFishesPerDay = new LinkedList<long>(collection);
            _newFishesPerDay.Enqueue(0);
            _newFishesPerDay.Enqueue(0);
        }

        protected override string RunPart1()
            => Run(80);

        protected override string RunPart2()
            => Run(256);

        private string Run(int totalRuns)
        {
            for (int i = 0; i < totalRuns; i++)
            {
                HandleFishInternalTimer();
            }

            return (_totalFishesPerDay.Sum() + _newFishesPerDay.Sum()).ToString();
        }

        public void HandleFishInternalTimer()
        {
            var totalFishesMaxNode = _totalFishesPerDay.First;
            if (totalFishesMaxNode != null)
            {
                _newFishesPerDay.Enqueue(totalFishesMaxNode.Value);
                totalFishesMaxNode.Value += _newFishesPerDay.Dequeue();

                _totalFishesPerDay.RemoveFirst();
                _totalFishesPerDay.AddLast(totalFishesMaxNode);
            }
        }
    }
}