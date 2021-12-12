using DailyCode.Base;
using DailyCode.Year2021.Models;
using Common.Helpers.Extensions;

namespace DailyCode.Year2021.Days
{
    public class Day12 : BaseDay
    {
        private List<Cave> _caves = new();

        public Day12(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            foreach(var input in fileInput)
            {
                var caves = input.Split("-").Select(name => _caves.Find(x => x.Name == name) ?? _caves.AddWithReturn(new Cave(name)));
                foreach(var cave in caves)
                {
                    cave.AdjacentCaves.Add(caves.Single(x => x.Name != cave.Name));
                }
            }
        }

        protected override long RunPart1()
            => GetTotalValidPaths(_caves.Single(x => x.IsStart));

        protected override long RunPart2()
            => GetTotalValidPaths(_caves.Single(x => x.IsStart), true);

        private int GetTotalValidPaths(Cave currentCave, bool allowVisitingSmallTwice = false, bool containsSmallCaveVisittedTwice = false)
        {
            var totalValidPaths = 0;

            ++currentCave.TimesVisitted;

            containsSmallCaveVisittedTwice = allowVisitingSmallTwice && (containsSmallCaveVisittedTwice || (currentCave.IsSmall && currentCave.TimesVisitted == 2));
            if (currentCave.IsExit)
            {
                --currentCave.TimesVisitted;
                return ++totalValidPaths;
            }

            var adjacentCaves = currentCave.AdjacentCaves.Where(x => !x.IsStart && (!x.IsSmall || (allowVisitingSmallTwice && !containsSmallCaveVisittedTwice) || x.TimesVisitted == 0));
            if (!adjacentCaves.Any())
            {
                --currentCave.TimesVisitted;
                return totalValidPaths;
            }

            foreach (var adjacentCave in adjacentCaves)
            {
                totalValidPaths += GetTotalValidPaths(adjacentCave, allowVisitingSmallTwice, containsSmallCaveVisittedTwice);
            }

            --currentCave.TimesVisitted;
            return totalValidPaths;
        }
    }
}