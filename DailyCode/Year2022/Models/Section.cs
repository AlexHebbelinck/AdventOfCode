namespace DailyCode.Year2022.Models
{
    internal sealed class Section
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

        public int Start { get; private init; }
        public int End { get; private init; }
        public IEnumerable<int> CompleteSection { get; private init; }

        public bool FullyContains(Section otherSection)
            => Start <= otherSection.Start && End >= otherSection.End;

        public bool AnyOverlap(Section otherSection)
            => !(Start > otherSection.End || End < otherSection.Start);
    }
}