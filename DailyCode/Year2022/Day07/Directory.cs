namespace DailyCode.Year2022.Day07
{
    internal class Directory
    {
        public Directory(string name, Directory? parent = null)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; set; }

        public Directory? Parent { get; set; }

        public List<Directory> SubDirectories { get; set; } = new();

        public List<File> Files { get; set; } = new();

        private int? _size;

        public int Size
            => _size ?? (_size = Files.Sum(x => x.Size) + SubDirectories.Sum(x => x.Size)) ?? 0;
    }
}