using DailyCode.Base;
using System.Text.RegularExpressions;
using Common.Models;

namespace DailyCode.Year2022.Day07
{
    internal class Day07 : BaseDay
    {
        private readonly Regex cmdRgx = new("\\$ (\\w{2}) {0,1}(.*?)(?:\n|\r|$)");

        private readonly List<Directory> _directories = new();

        public Day07(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
        {
            var rootDirectory = new Directory("/");
            _directories.Add(rootDirectory);

            var currentDirectory = rootDirectory;

            foreach (var input in fileInputs)
            {
                var match = cmdRgx.Match(input);

                if (match.Success)
                {
                    if (match.Groups[1].Value.Equals("cd"))
                    {
                        if (match.Groups[2].Value.Contains('.'))
                        {
                            for (int i = 1; i < match.Groups[2].Value.Length; i++)
                            {
                                currentDirectory = currentDirectory.Parent ?? throw new Exception("Filesystem broken!");
                            }
                        }
                        else if (match.Groups[2].Value.Any(x => char.IsLetter(x)))
                        {
                            var directory = new Directory(match.Groups[2].Value, currentDirectory);
                            currentDirectory.SubDirectories.Add(directory);
                            _directories.Add(directory);

                            currentDirectory = directory;
                        }
                    }
                }
                else if (!input.StartsWith("dir"))
                {
                    var commandSplitted = input.Split(' ');
                    var file = new File(commandSplitted[1], Convert.ToInt32(commandSplitted[0]));
                    currentDirectory.Files.Add(file);
                }
            }
        }

        protected override string RunPart1()
            => _directories.Where(x => x.Size < 100000).Sum(x => x.Size).ToString();

        protected override string RunPart2()
        {
            var unusedSpace = 70000000 - _directories[0].Size;
            var needToDelete = 30000000 - unusedSpace;
            return _directories.Where(x => x.Size >= needToDelete).Min(x => x.Size).ToString();
        }
    }
}