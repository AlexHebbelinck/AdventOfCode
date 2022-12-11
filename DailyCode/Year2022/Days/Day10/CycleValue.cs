namespace DailyCode.Year2022.Days.Day10
{
    public class CycleValue
    {
        public CycleValue(int cycle, int value)
        {
            Cycle = cycle;
            Value = value;
        }

        public int Cycle { get; init; }

        public int Value { get; init; }

        public int SignalStrength
            => Cycle * Value;
    }
}