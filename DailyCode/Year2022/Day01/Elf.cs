namespace DailyCode.Year2022.Day01
{
    internal class Elf
    {
        public List<int> Calories { get; init; } = new();

        public int Total => Calories.Sum();
    }
}