namespace DailyCode.Year2022.Models
{
    internal class Elf
    {
        public List<int> Calories { get; init; } = new();

        public int Total => Calories.Sum();
    }
}