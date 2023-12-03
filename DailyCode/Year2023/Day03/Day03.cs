using DailyCode.Base;
using DailyCode.Common.Models;

namespace DailyCode.Year2023.Day03
{
    public class Day03(string sessionId) : BaseDay(sessionId)
    {
        private List<Number> _numbers = [];
        private List<Symbol> _symbols = [];

        protected override void SetupData(List<string> fileInputs)
        {
            fileInputs = fileInputs.ConvertAll(x => x.Trim('\r'));

            for (int y = 0; y < fileInputs.Count; y++)
            {
                string partialNumber = string.Empty;
                for (int x = 0; x < fileInputs[y].Length; x++)
                {
                    var character = fileInputs[y][x];
                    if (char.IsDigit(character))
                    {
                        partialNumber += character;

                        if (x + 1 == fileInputs[y].Length)
                        {
                            _numbers.Add(new Number
                            {
                                Value = int.Parse(partialNumber),
                                StartCoordinates = new Coordinates(x - (partialNumber.Length - 1), y),
                                EndCoordinates = new Coordinates(x, y)
                            });
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(partialNumber))
                        {
                            _numbers.Add(new Number
                            {
                                Value = int.Parse(partialNumber),
                                StartCoordinates = new Coordinates(x - partialNumber.Length, y),
                                EndCoordinates = new Coordinates(x - 1, y)
                            });

                            partialNumber = string.Empty;
                        }

                        if (character != '.')
                        {
                            _symbols.Add(new Symbol
                            {
                                Value = character.ToString(),
                                Coordinates = new Coordinates(x, y)
                            });
                        }
                    }
                }
            }

            foreach (var symbol in _symbols)
            {
                //No number seems to be larger than 3 digits, so we don't care about the middle one
                foreach (var adjacentNumber in _numbers.Where(x => x.StartCoordinates.IsAdjacent(symbol.Coordinates, true) || x.EndCoordinates.IsAdjacent(symbol.Coordinates, true)))
                {
                    adjacentNumber.AdjacentSymbol = symbol;
                }
            }
        }

        protected override string RunPart1()
            => _numbers.Where(x => x.AdjacentSymbol != null).Sum(x => x.Value).ToString();

        protected override string RunPart2()
        {
            var c = _numbers.Where(x => x.AdjacentSymbol?.Value == "*")
                .GroupBy(x => x.AdjacentSymbol)
                .Select(x => x.Select(g => g.Value))
                .Where(x => x.Count() == 2)
                .ToList();

            var result = 0;
            foreach (var b in c)
            {
                result += b.Aggregate((curr, next) => curr * next);
            }
            return result.ToString();
        }

        public class Number
        {
            public int Value { get; set; }
            public Coordinates StartCoordinates { get; set; } = null!;
            public Coordinates EndCoordinates { get; set; } = null!;
            public Symbol? AdjacentSymbol { get; set; }
        }

        public class Symbol
        {
            public string Value { get; set; } = null!;
            public Coordinates Coordinates { get; set; } = null!;
        }
    }
}