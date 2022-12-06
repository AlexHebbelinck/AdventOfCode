using DailyCode.Base;
using System.Text;
using System.Text.RegularExpressions;

namespace DailyCode.Year2022.Days
{
    internal class Day06 : BaseDay
    {
        private string _fileInput = null!;

        public Day06(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
            => _fileInput = fileInputs[0];

        protected override string RunPart1()
            => GetResult(4);

        protected override string RunPart2()
            => GetResult(14);

        private string GetResult(int distinctCharacters)
        {
            var match = CreateMarkerRegex(distinctCharacters).Match(_fileInput);
            return (match.Index + match.Length).ToString();
        }

        private static Regex CreateMarkerRegex(int distinctCharacters)
        {
            if (distinctCharacters <= 0) throw new ArgumentException("Yeah go sabotage yourself, why not...");

            StringBuilder pattern = new();
            for(int counter = 0; counter < distinctCharacters; counter++)
            {
                pattern.Append($"({CreateNegativeLookaheadPattern(counter)}.)");
            }

            return new(pattern.ToString());
        }

        private static StringBuilder CreateNegativeLookaheadPattern(int outerCounter)
        {
            StringBuilder pattern = new();
            for (int innerCounter = 0; innerCounter < outerCounter; innerCounter++)
            {
                pattern.Append($"(?!\\{innerCounter + 1})");
            }

            return pattern;
        }
    }
}