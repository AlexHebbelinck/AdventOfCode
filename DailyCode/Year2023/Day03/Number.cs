using DailyCode.Common.Models;

namespace DailyCode.Year2023.Day03
{
    public class Number
    {
        public int Value { get; set; }
        public Coordinates StartCoordinates { get; set; } = null!;
        public Coordinates EndCoordinates { get; set; } = null!;
        public Symbol? AdjacentSymbol { get; set; }
    }
}
