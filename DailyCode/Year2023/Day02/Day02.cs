using Common.Models;
using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2023.Day02
{
    public class Day02(string sessionId) : BaseDay(sessionId)
    {
        private readonly List<Game> _games = [];

        protected override void SetupData(FileInputCollection fileInputs)
        {
            var gameRgx = new Regex("Game (?<GameId>\\d*):");
            var subsetRgx = new Regex("\\d+ .*?(?=;|$)");
            fileInputs.ForEach(fileLine =>
            {
                var game = new Game(int.Parse(gameRgx.Match(fileLine).Groups["GameId"].Value));
                foreach (Match match in subsetRgx.Matches(fileLine))
                {
                    var cubesAmount = match.Value.Split(',');
                    game.Sets.Add(new Set
                    {
                        RevealedCubes = cubesAmount.Select(cubeAmount => {
                            cubeAmount = cubeAmount.Trim();
                            string[] splittedString = cubeAmount.Split(' ');
                            return new Cube(splittedString[1], int.Parse(splittedString[0])); 
                        }).ToList()
                    });
                }

                _games.Add(game);
            });
        }

        protected override string RunPart1()
        {
            List<(string Color, int MaxAmount)> maxPerColors =
            [
                ("red", 12),
                ("blue", 14),
                ("green", 13),
            ];

            return _games
                .Where(x => x.IsValid(maxPerColors)).Select(x => x.Id)
                .Sum()
                .ToString();
        }

        protected override string RunPart2()
            => _games.Select(x => x.GetLowestPerColor().Select(c => c.Amount).Aggregate((curr, next) => curr * next))
                .Sum()
                .ToString();
    }
}