namespace DailyCode.Year2022.Days.Day01
{
    internal class Elf
    {
        public List<int> Calories { get; init; } = new();

        public int Total => Calories.Sum();
    }
}