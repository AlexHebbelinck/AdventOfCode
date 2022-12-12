using DailyCode.Base;
using DailyCode.Common.Models;

namespace DailyCode.Year2021.Days
{
    public class Day13 : BaseDay
    {
        private List<Coordinates> _dotCoordinates = new();
        private List<(char orientation, int position)> _foldInstructions = new();

        public Day13(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            var index = fileInput.IndexOf(string.Empty);

            _foldInstructions = fileInput.Skip(index + 1).Select(line =>
            {
                var splittedData = line.Split('=');
                return (splittedData[0].Last(), int.Parse(splittedData[1]));
            }).ToList();

            _dotCoordinates = fileInput.Take(index).Select(line =>
            {
                var dotcoordinates = line.Split(',');
                return new Coordinates(int.Parse(dotcoordinates[0]), int.Parse(dotcoordinates[1]));
            }).ToList();
        }

        protected override string RunPart1()
        {
            HandleFoldInstructions(true);

            return _dotCoordinates.Distinct().Count().ToString();
        }

        protected override string RunPart2()
        {
            HandleFoldInstructions(false);
            var uniqueDotCoordinates = _dotCoordinates.Distinct().OrderBy(x => x.PosX).ThenBy(x => x.PosY).ToList();

            var maxX = uniqueDotCoordinates.Max(coords => coords.PosX);
            var maxY = uniqueDotCoordinates.Max(coords => coords.PosY);

            var foldedPaper = string.Empty;
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    foldedPaper += uniqueDotCoordinates.Any(coords => coords.PosY == y && coords.PosX == x) ? "#" : ".";
                }

                foldedPaper += Environment.NewLine;
            }

            return foldedPaper;
        }

        private void HandleFoldInstructions(bool isOnyALlowedOneFold = false)
        {
            var totalFolds = isOnyALlowedOneFold ? 1 : _foldInstructions.Count;

            for (int i = 0; i < totalFolds; i++)
            {
                var maxX = _dotCoordinates.Max(coords => coords.PosX);
                var maxY = _dotCoordinates.Max(coords => coords.PosY);

                var (orientation, position) = _foldInstructions[i];
                if (orientation.Equals('x'))
                {
                    _dotCoordinates.ForEach(coords =>
                    {
                        if (coords.PosX > position)
                            coords.PosX -= (coords.PosX + (coords.PosX - maxX));
                    });
                }
                else
                {
                    _dotCoordinates.ForEach(coords =>
                    {
                        if (coords.PosY > position)
                            coords.PosY -= (coords.PosY + (coords.PosY - maxY));
                    });
                }
            }
        }
    }
}