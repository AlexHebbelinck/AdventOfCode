namespace DailyCode.Year2022.Models
{
    internal class Section
    {
        public Section(int start, int end)
        {
            Start = start;
            End = end;
            CompleteSection = Enumerable.Range(Start, End + 1 - Start);
        }

        public Section(string start, string end)
            : this(Convert.ToInt32(start), Convert.ToInt32(end))
        {
        }

        public int Start { get; init; }
        public int End { get; init; }

        public IEnumerable<int> CompleteSection { get; init; }
    }
}