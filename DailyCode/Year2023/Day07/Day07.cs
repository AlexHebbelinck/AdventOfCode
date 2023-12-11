using DailyCode.Base;
using DailyCode.Year2023.Day07.Models;
using Common.Models;

namespace DailyCode.Year2023.Day07
{
    public class Day07(string sessionId) : BaseDay(sessionId)
    {
        private IEnumerable<CamelCardsMatch> _matches = [];

        protected override void SetupData(FileInputCollection fileInputs)
        {
            _matches = fileInputs.Select(x =>
            {
                var splitted = x.Split(' ');
                return new CamelCardsMatch(splitted[0], int.Parse(splitted[1]));
            });
        }

        protected override string RunPart1()
        {
            var mappedCamelCardsMatches = _matches.Select(x => new MappedCamelCardsMatch(CardMapping.Map(x.Hand), x.Bid, HandType.Get(x.Hand)));
            return CalculateTotalWinnings(mappedCamelCardsMatches).ToString();
        }

        protected override string RunPart2()
        {
            var mappedCamelCardsMatches = _matches.Select(x => {
                    var correctedHand =  x.Hand.Replace('J', '1');
                    return new MappedCamelCardsMatch(CardMapping.Map(correctedHand), x.Bid, HandType.Get(correctedHand));
                });

            return CalculateTotalWinnings(mappedCamelCardsMatches).ToString();
        }

        private static int CalculateTotalWinnings(IEnumerable<MappedCamelCardsMatch> mappedCamelCardsMatches)
            => mappedCamelCardsMatches.OrderByDescending(x => x.HandType.Rank)
              .GroupBy(x => x.HandType)
              .SelectMany(g => g.Select(x => new { Score = CalculateHandScore(x.Hand), x.Bid }).OrderByDescending(x => x.Score))
              .Select((x, i) => x.Bid * (i + 1))
              .Sum();

        private static long CalculateHandScore(string hand)
            => long.Parse(hand.ToCharArray().Select(x => (int)x).Aggregate("", (curr, next) => $"{curr}{next}"));
    }
}