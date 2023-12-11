using DailyCode.Base;
using System.Text.RegularExpressions;
using Common.Models;

namespace DailyCode.Year2022.Day04
{
    internal class Day04 : BaseDay
    {
        private readonly Regex _fileInputRgx = new("(\\d+)-(\\d+),(\\d+)-(\\d+)");

        private List<(Section section1, Section section2)> _inputs = null!;

        public Day04(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
            => _inputs = fileInputs.ConvertAll(fileInput =>
            {
                var match = _fileInputRgx.Match(fileInput);
                return (new Section(match.Groups[1].Value, match.Groups[2].Value), new Section(match.Groups[3].Value, match.Groups[4].Value));
            });

        protected override string RunPart1()
            => _inputs.Sum(input => Convert.ToInt32(input.section1.FullyContains(input.section2) || input.section2.FullyContains(input.section1)))
               .ToString();

        protected override string RunPart2()
            => _inputs.Sum(input => Convert.ToInt32(input.section1.AnyOverlap(input.section2))).ToString();
    }
}