using DailyCode.Base;
using DailyCode.Year2021.Models;
using DailyCode.Year2022.Models;
using System.Text.RegularExpressions;

namespace DailyCode.Year2022.Days
{
    internal class Day09 : BaseDay
    {
        private readonly Regex inputRgx = new("([A-Z]) (\\d+)");

        private List<(string direction, int steps)> _inputs = null!;

        public Day09(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
            => _inputs = fileInputs.ConvertAll(x =>
            {
                var match = inputRgx.Match(x);
                return (match.Groups[1].Value, Convert.ToInt32(match.Groups[2].Value));
            });

        protected override string RunPart1()
        {
            var rope = new Rope();
            foreach (var (direction, steps) in _inputs)
                rope.DoMotion(direction, steps);

            return rope.TailKnotCoordHistory.Count.ToString();
        }

        protected override string RunPart2()
        {
            var rope = new Rope(9);
            foreach (var (direction, steps) in _inputs)
                rope.DoMotion(direction, steps);

            return rope.TailKnotCoordHistory.Count.ToString();
        }
    }
}