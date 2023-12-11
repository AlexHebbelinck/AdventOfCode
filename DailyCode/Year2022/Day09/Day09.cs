using DailyCode.Base;
using System.Text.RegularExpressions;
using Common.Models;

namespace DailyCode.Year2022.Day09
{
    internal class Day09 : BaseDay
    {
        private readonly Regex inputRgx = new("([A-Z]) (\\d+)");

        private List<(string direction, int steps)> _inputs = null!;

        public Day09(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
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