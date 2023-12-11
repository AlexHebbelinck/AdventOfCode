using DailyCode.Base;
using System.Text.RegularExpressions;
using Common.Models;

namespace DailyCode.Year2023.Day06
{
    //Note: Could be done with quadratic equation
    public class Day06(string sessionId) : BaseDay(sessionId)
    {
        private List<RaceData> _races = null!;

        protected override void SetupData(FileInputCollection fileInputs)
        {
            var rgx = new Regex("(\\d+)");
            var distances = rgx.Matches(fileInputs[1]).Select(x => long.Parse(x.Value)).ToArray();

            _races = rgx.Matches(fileInputs[0])
                .Select((x, i) => new RaceData(long.Parse(x.Value), distances[i]))
                .ToList();
        }

        protected override string RunPart1()
            => _races.Select(x => Race.GetTotalOptionsToBeatRecord(new ToyBoat(), x))
                .Aggregate((curr, next) => curr * next)
                .ToString();

        protected override string RunPart2()
            => Race.GetTotalOptionsToBeatRecord(new ToyBoat(), CreateNewRaceFromFaultyData())
            .ToString();

        private RaceData CreateNewRaceFromFaultyData()
        {
            var time = string.Empty;
            var distance = string.Empty;
            _races.ForEach(x =>
            {
                time += x.Time;
                distance += x.RecordDistance;
            });

            return new RaceData(long.Parse(time), long.Parse(distance));
        }
    }
}