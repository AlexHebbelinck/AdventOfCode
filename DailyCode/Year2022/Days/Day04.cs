using DailyCode.Base;
using DailyCode.Year2022.Models;
using System.Text.RegularExpressions;

namespace DailyCode.Year2022.Days
{
    internal class Day04 : BaseDay
    {
        private readonly Regex _fileInputRgx = new("(\\d+)-(\\d+),(\\d+)-(\\d+)");

        private List<(Section section1, Section section2)> _inputs = null!;

        public Day04(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
            => _inputs = fileInputs.ConvertAll(fileInput =>
            {
                var match = _fileInputRgx.Match(fileInput);
                return (new Section(match.Groups[1].Value, match.Groups[2].Value), new Section(match.Groups[3].Value, match.Groups[4].Value));
            });

        protected override string RunPart1()
            => _inputs.Sum(input => Convert.ToInt32((input.section1.Start <= input.section2.Start && input.section1.End >= input.section2.End)
                                                        || (input.section2.Start <= input.section1.Start && input.section2.End >= input.section1.End)))
               .ToString();

        protected override string RunPart2()
            => _inputs.Sum(input => Convert.ToInt32(input.section1.CompleteSection.Any(currentValue => input.section2.CompleteSection.Contains(currentValue)))).ToString();
    }
}