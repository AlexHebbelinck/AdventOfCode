using Common.Models;
using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2023.Day01
{
    public class Day01(string sessionId) : BaseDay(sessionId)
    {
        private List<string> _calibrationInputs = [];

        protected override void SetupData(FileInputCollection fileInputs)
            => _calibrationInputs = fileInputs.ConvertAll(x => x.Trim('\r'));

        protected override string RunPart1()
        {
            Regex rgx = new("[^0-9.]");
            return _calibrationInputs.Select(x =>
            {
                var numbers = rgx.Replace(x, "");
                return int.Parse(new string(new char[] { numbers[0], numbers.Last() }));
            }).Sum().ToString();
        }

        protected override string RunPart2()
        {
            return _calibrationInputs.ConvertAll(x => int.Parse(FindFirstAndLastDigit(x)))
                .Sum()
                .ToString();

            static string FindFirstAndLastDigit(string text)
            {
                var aggregatedDigits = DigitsHelper.DigitList.Select(x => x.Key).Aggregate((curr, next) => $"{curr}|{next}");
                var rgx = new Regex($"(?:^.*?(?<FirstValue>{aggregatedDigits}|\\d).*(?<LastValue>{aggregatedDigits}|\\d)(?!({aggregatedDigits}|\\d)).*?$)|(?:^.*?(?<OnlyValue>{aggregatedDigits}|\\d)(?!({aggregatedDigits}|\\d)).*?$)", RegexOptions.IgnoreCase);

                GroupCollection groups = rgx.Match(text).Groups;

                return groups["FirstValue"].Success
                    ? DigitsHelper.ConvertDigitToInt(groups["FirstValue"].Value + groups["LastValue"].Value)
                    : DigitsHelper.ConvertDigitToInt(groups["OnlyValue"].Value + groups["OnlyValue"].Value);
            }
        }
    }
}