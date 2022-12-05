using DailyCode.Base;
using System.Text;
using System.Text.RegularExpressions;

namespace DailyCode.Year2022.Days
{
    internal class Day05 : BaseDay
    {
        private readonly Regex extractCrateRgx = new("(?:   |\\[[A-Z]\\])(?: |$|\\r)");
        private readonly Regex instructionsRgx = new("move (\\d+) from (\\d+) to (\\d+)");

        private List<Stack<char>> _stackedCrates = null!;
        private List<(int move, int from, int to)> _instructions = null!;

        public Day05(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
        {
            ExtractCrates(fileInputs);
            ExtractInstructions(fileInputs);

            void ExtractCrates(List<string> fileInputs)
            {
                List<List<char>> extractedCrates = new();
                foreach (var fileInput in fileInputs)
                {
                    var matches = extractCrateRgx.Matches(fileInput);
                    if (matches?.Any() == true)
                    {
                        for (int counter = 0; counter < matches.Count; counter++)
                        {
                            if (extractedCrates.Count <= counter)
                                extractedCrates.Add(new List<char>());

                            if (!string.IsNullOrWhiteSpace(matches[counter].Value))
                                extractedCrates[counter].Add(char.Parse(matches[counter].Value.Substring(1, 1)));
                        }
                    }
                }
                _stackedCrates = extractedCrates.ConvertAll(charList => { charList.Reverse(); return new Stack<char>(charList); });
            }

            void ExtractInstructions(List<string> fileInputs)
            {
                List<(int move, int from, int to)> extractedInstructions = new();
                foreach (var fileInput in fileInputs)
                {
                    var match = instructionsRgx.Match(fileInput);
                    if (match.Success)
                        extractedInstructions.Add((Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value)));
                }
                _instructions = extractedInstructions;
            }
        }

        protected override string RunPart1()
        {
            foreach (var (move, from, to) in _instructions)
            {
                for (int movements = 1; movements <= move; movements++)
                {
                    _stackedCrates[to - 1].Push(_stackedCrates[from - 1].Pop());
                }
            }

            return GetTopCratesForEachStack();
        }

        protected override string RunPart2()
        {
            foreach (var (move, from, to) in _instructions)
            {
                var crates = new List<char>();
                for (int movements = 1; movements <= move; movements++)
                {
                    crates.Add(_stackedCrates[from - 1].Pop());
                }

                crates.Reverse();
                foreach (var crate in crates)
                {
                    _stackedCrates[to - 1].Push(crate);
                }
            }

            return GetTopCratesForEachStack();
        }

        private string GetTopCratesForEachStack()
        {
            StringBuilder result = new();
            foreach (var crateStack in _stackedCrates)
            {
                result.Append(crateStack.Peek());
            }

            return result.ToString();
        }
    }
}