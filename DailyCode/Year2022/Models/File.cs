namespace DailyCode.Year2022.Models
{
    internal class File
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public int Size { get; set; }
    }
}