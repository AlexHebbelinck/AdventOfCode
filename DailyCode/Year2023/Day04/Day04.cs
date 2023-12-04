using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2023.Day04
{
    public partial class Day04(string sessionId) : BaseDay(sessionId)
    {
        [GeneratedRegex(@"Card.*\d+:")]
        private static partial Regex GameExtractionRgx();

        [GeneratedRegex(@"(?<!\d)(\d{1,2})(?!\d)(?=.+?\|.*[^\d]\1(?!\d))")]
        private static partial Regex WinningNumbersRgx();

        private List<string> _scratchcards = [];

        protected override void SetupData(List<string> fileInputs)
        {
            Regex rgx = GameExtractionRgx();
            _scratchcards = fileInputs
                .ConvertAll(x => rgx.Replace(x, string.Empty));
        }

        protected override string RunPart1()
        {
            Regex rgx = WinningNumbersRgx();

            return _scratchcards.Sum(x =>
            {
                var totalMatches = rgx.Count(x);
                return totalMatches == 0 ? 0 : Math.Pow(2, totalMatches - 1);
            }).ToString();
        }

        protected override string RunPart2()
        {
            Regex rgx = WinningNumbersRgx();

            var trackingScratchCards = _scratchcards.Select(_ =>  0).ToArray();

            int totalScratchCards = 0;

            for(int i = 0; i < _scratchcards.Count; i++)
            {
                totalScratchCards += trackingScratchCards[i] + 1;
                var totalMatches = rgx.Count(_scratchcards[i]);
                if (totalMatches > 0)
                {
                    for (int matchNumber = 1; matchNumber <= totalMatches; matchNumber++)
                    {
                        if((i + matchNumber) <= trackingScratchCards.Length)
                        trackingScratchCards[i + matchNumber] += trackingScratchCards[i] + 1;
                    }
                }
            }

            return totalScratchCards.ToString();
        }
    }
}