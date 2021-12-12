namespace DailyCode.Year2021.Models
{
    public class Cave
    {
        public Cave(string name)
        {
            Name = name;
            IsSmall = Name.All(char.IsLower) || Name.Equals("start", StringComparison.CurrentCultureIgnoreCase);
            IsStart = Name.Equals("start", StringComparison.CurrentCultureIgnoreCase);
            IsExit = Name.Equals("end", StringComparison.CurrentCultureIgnoreCase);
        }

        public string Name { get; } = string.Empty;

        public List<Cave> AdjacentCaves { get; set; } = new();

        public bool IsSmall { get; }

        public int TimesVisitted { get; set; }

        public bool IsStart { get; }

        public bool IsExit { get; }
    }
}