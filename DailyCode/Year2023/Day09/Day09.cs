using Common.Helpers;
using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2023.Day09
{
    public class Day09(string sessionId) : BaseDay(sessionId)
    {
        private IEnumerable<long[]> _lines;

        protected override void SetupData(FileInputCollection fileInputs)
            => _lines = fileInputs.ConvertAll(x => x.Trim('\r').Split(' ').Select(n => long.Parse(n)).ToArray());

        protected override string RunPart1()
            => _lines.Select(x => MathHelper.Extrapolate(x)).Sum().ToString();

        protected override string RunPart2()
            => _lines.Select(x => MathHelper.Extrapolate(x.Reverse().ToArray())).Sum().ToString();
    }
}