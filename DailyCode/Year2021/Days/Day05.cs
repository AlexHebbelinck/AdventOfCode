using DailyCode.Base;
using DailyCode.Year2021.Models;
using System.Text.RegularExpressions;

namespace DailyCode.Year2021.Days
{
    public class Day05 : BaseDay
    {
        private List<LineCoordinates> LinesCoordinates { get; set; } = new();

        public Day05(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            foreach (var line in fileInput)
            {
                var match = Regex.Match(line, @"(\d+),(\d+) -> (\d+),(\d+)");
                LinesCoordinates.Add(new LineCoordinates
                {
                    From = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                    To = (int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)),
                });
            }
        }

        protected override string RunPart1()
        {
            return Run(true);
        }

        protected override string RunPart2()
        {
            return Run();
        }

        private string Run(bool checkIfDiagonal = false)
        {
            var diagram = new int[LinesCoordinates.Max(x => x.HighestXCoordinate) + 1, LinesCoordinates.Max(y => y.HighestYCoordinate) + 1];

            foreach (var lineCoordinates in LinesCoordinates.Where(x => !checkIfDiagonal || x.IsDiagonal))
            {
                var fullLineRange = lineCoordinates.GetFullLineRange();
                foreach ((int x, int y) in fullLineRange)
                {
                    ++diagram[x, y];
                }
            }

            return diagram.Cast<int>().Count(x => x > 1).ToString();
        }
    }
}