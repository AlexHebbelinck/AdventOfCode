using DailyCode.Base;
using DailyCode.Year2021.Models;

namespace DailyCode.Year2021.Days
{
    public class Day08 : BaseDay
    {
        private readonly List<(string[] patterns, string[] output)> _fileInput = new();

        public Day08(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            fileInput.ForEach(input =>
                {
                    var inputOutput = input.Split('|');
                    _fileInput.Add((inputOutput[0].Trim().Split(' ').Select(x => SortString(x)).ToArray(), inputOutput[1].Trim().Split(' ').Select(x => SortString(x)).ToArray()));
                });
        }

        protected override long RunPart1()
            => _fileInput.SelectMany(x => x.output).Count(x => x.Length <= 4 || x.Length == 7);

        protected override long RunPart2()
        {
            var total = 0;
            foreach (var input in _fileInput)
            {
                var segmentNumbers = new string[10];
                var one = input.patterns.Single(x => x.Length == DigitDisplay.One.SegmentLength);
                var four = input.patterns.Single(x => x.Length == DigitDisplay.Four.SegmentLength);
                var seven = input.patterns.Single(x => x.Length == DigitDisplay.Seven.SegmentLength);
                var eight = input.patterns.Single(x => x.Length == DigitDisplay.Eight.SegmentLength);
                var three = input.patterns.Single(x => x.Length == DigitDisplay.Three.SegmentLength && CompareString(one, x));
                var six = input.patterns.Single(x => x.Length == DigitDisplay.Six.SegmentLength && !CompareString(one, x));
                var five = input.patterns.Single(x => x.Length == DigitDisplay.Five.SegmentLength && GetDifference(six, x) == 1);
                var nine = input.patterns.Single(x => x.Length == DigitDisplay.Nine.SegmentLength && GetDifference(x, three) == 1);
                var two = input.patterns.Single(x => x.Length == DigitDisplay.Two.SegmentLength && GetDifference(eight, x) == 2 && x != three && x != five);
                var zero = input.patterns.Single(x => x.Length == DigitDisplay.Zero.SegmentLength && GetDifference(eight, x) == 1 && x != nine && x != six);

                var outputNumber = "";
                foreach (var output in input.output)
                {
                    if (output == zero) outputNumber += "0";
                    else if (output == one) outputNumber += "1";
                    else if (output == two) outputNumber += "2";
                    else if (output == three) outputNumber += "3";
                    else if (output == four) outputNumber += "4";
                    else if (output == five) outputNumber += "5";
                    else if (output == six) outputNumber += "6";
                    else if (output == seven) outputNumber += "7";
                    else if (output == eight) outputNumber += "8";
                    else if (output == nine) outputNumber += "9";
                }

                total += int.Parse(outputNumber);
            }

            return total;
        }

        private string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private bool CompareString(string val, string val2)
        {
            return val.ToArray().All(x => val2.Contains(x));
        }

        private int GetDifference(string val, string val2)
        {
            return val.ToArray().Count(x => !val2.Contains(x));
        }
    }
}