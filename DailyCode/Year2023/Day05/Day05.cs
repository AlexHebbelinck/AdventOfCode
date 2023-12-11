using DailyCode.Base;
using System.Text.RegularExpressions;
using Common.Models;

namespace DailyCode.Year2023.Day05
{
    public class Day05(string sessionId) : BaseDay(sessionId)
    {
        private List<long> _seeds = null!;
        private readonly List<MapBluePrintsPerType> _mapBluePrintsPerTypes = [];

        protected override void SetupData(FileInputCollection fileInputs)
        {
            var rgx = new Regex("(\\d+)");
            _seeds = rgx.Matches(fileInputs[0]).Select(x => long.Parse(x.Value)).ToList();

            var secondRgx = new Regex("(\\d+) (\\d+) (\\d+)");
            foreach (var fileInput in fileInputs.Skip(1).Select(x => x.Trim('\r')).Where(x => !string.IsNullOrEmpty(x)))
            {
                MapBluePrintsPerType? currentMapBluePrintsPerType = _mapBluePrintsPerTypes.LastOrDefault();

                var match = secondRgx.Match(fileInput);
                if (match?.Success == true)
                {
                    currentMapBluePrintsPerType!.MapBlueprints.Add(new MapBlueprint(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value), long.Parse(match.Groups[3].Value)));
                }
                else
                {
                    _mapBluePrintsPerTypes.Add(new MapBluePrintsPerType());

                    if (currentMapBluePrintsPerType != null)
                        currentMapBluePrintsPerType.NextMapBlueprintsPerType = _mapBluePrintsPerTypes.Last();
                }
            }
        }

        protected override string RunPart1()
            => _seeds.Min(seed => _mapBluePrintsPerTypes[0].Map(seed)).ToString();

        protected override string RunPart2()
            => _mapBluePrintsPerTypes[0].Map(GetSeedRanges()).Min(x => x.Start).ToString();

        private List<Range> GetSeedRanges()
        {
            var seedRanges = new List<Range>();
            for (int i = 0; i < (_seeds.Count) / 2; i++)
            {
                var seedPairs = _seeds.Skip(i * 2).Take(2).ToArray();
                seedRanges.Add(new Range(seedPairs[0], seedPairs[0] + seedPairs[1] - 1));
            }

            return seedRanges;
        }
    }

    public class MapBluePrintsPerType
    {
        public List<MapBlueprint> MapBlueprints { get; set; } = [];

        public MapBluePrintsPerType? NextMapBlueprintsPerType { get; set; }

        public long Map(long value)
        {
            var mapBluePrint = MapBlueprints.Find(x => x.IsMappable(value));
            var mappedValue = mapBluePrint != null ? mapBluePrint.Map(value) : value;

            return NextMapBlueprintsPerType != null
                ? NextMapBlueprintsPerType.Map(mappedValue)
                : mappedValue;
        }

        public List<Range> Map(List<Range> ranges)
        {
            List<Range> mappedRanges = [];
            List<Range> unmappedRanges = ranges;

            foreach (var mapBlueprint in MapBlueprints)
            {
                var newRanges = unmappedRanges.SelectMany(mapBlueprint.Map).ToList();
                mappedRanges.AddRange(newRanges.Where(x => x.IsMapped).Select(x => x.Range).ToList());
                unmappedRanges = newRanges.Where(x => !x.IsMapped).Select(x => x.Range).ToList();
            }

            mappedRanges.AddRange(unmappedRanges);
            if (NextMapBlueprintsPerType != null && NextMapBlueprintsPerType.MapBlueprints.Count != 0)
            {
                return NextMapBlueprintsPerType.Map(mappedRanges.Distinct().ToList());
            }

            return mappedRanges.Distinct().ToList();
        }

        //public IEnumerable<(Range startRange, Range endRange)> DoSomething(IEnumerable<(Range startRange, Range endRange)> mappedRanges)
        //{
        //    if (NextMapBlueprintsPerType != null && NextMapBlueprintsPerType.MapBlueprints.Count != 0)
        //    {
        //        //I dont fucking know, leave me wtf fuck
        //        //foreach(var mappedRange in mappedRanges)
        //        //{
        //        //    NextMapBlueprintsPerType.Map()
        //        //}
        //        foreach (var mapBluePrint in MapBlueprints)
        //        {
        //            NextMapBlueprintsPerType.Map(mapBluePrint.SourceRange, mapBluePrint.DestinationRange);
        //        }
        //    }

        //    return mappedRanges;
        //}

        //public IEnumerable<(Range startRange, Range endRange)> Map(Range sourceRange, Range destinationRange)
        //    => MapBlueprints.Select(x => x.Map(sourceRange, destinationRange));
    }

    public class MapBlueprint(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        public Range DestinationRange { get; init; } = new Range(destinationRangeStart, destinationRangeStart + (rangeLength - 1));

        public Range SourceRange { get; init; } = new Range(sourceRangeStart, sourceRangeStart + (rangeLength - 1));

        //Easy for debugging purposes
        public long RangeLength { get; init; } = rangeLength;

        public bool IsMappable(long value)
            => value >= SourceRange.Start && value <= SourceRange.End;

        public long Map(long value)
            => DestinationRange.Start + (value - SourceRange.Start);

        //public (Range startRange, Range endRange) Map(Range sourceRange, Range destinationRange)
        //{
        //    if ((destinationRange.Start >= SourceRange.Start && destinationRange.Start <= SourceRange.End)
        //        || (destinationRange.End >= SourceRange.Start && destinationRange.End <= SourceRange.End))
        //    {
        //        List<Range> ranges = [];

        //        var newRangeStart = Math.Max(SourceRange.Start, destinationRange.Start);
        //        var newRangeEnd = Math.Min(SourceRange.End, destinationRange.End);
        //        var differenceBetweenSourceDestination = DestinationRange.Start - SourceRange.Start;

        //        return (new Range(newRangeStart, newRangeEnd), new Range(newRangeStart + differenceBetweenSourceDestination, newRangeEnd + differenceBetweenSourceDestination));
        //    }

        //    return (sourceRange, destinationRange);
        //}

        public List<(bool IsMapped, Range Range)> Map(Range range)
        {
            if ((range.Start >= SourceRange.Start && range.Start <= SourceRange.End)
                || (range.End >= SourceRange.Start && range.End <= SourceRange.End))
            {
                List<(bool isMapped, Range range)> ranges = [];

                var newRangeStart = Math.Max(SourceRange.Start, range.Start);
                var newRangeEnd = Math.Min(SourceRange.End, range.End);
                var differenceBetweenSourceDestination = DestinationRange.Start - SourceRange.Start;

                ranges.Add((true, new Range(newRangeStart + differenceBetweenSourceDestination, newRangeEnd + differenceBetweenSourceDestination)));

                if (range.Start < SourceRange.Start) ranges.Add((false, new Range(range.Start, SourceRange.Start - 1)));
                if (range.End > SourceRange.End) ranges.Add((false, new Range(SourceRange.End + 1, range.End)));

                return ranges;
            }

            return [(false, range)];
        }
    }

    public record Range(long Start, long End);
}