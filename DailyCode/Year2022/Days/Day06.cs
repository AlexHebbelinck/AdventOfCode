using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2022.Days
{
    internal class Day06 : BaseDay
    {
        private string _fileInput = null!;

        public Day06(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
        {
            _fileInput = fileInputs[0];
        }

        protected override string RunPart1()
        {
            Regex rgx = new("(.)((?!\\1).)((?!\\1)(?!\\2).)((?!\\1)(?!\\2)(?!\\3).)");
            var match = rgx.Match(_fileInput);
            return (match.Index + match.Length).ToString();
        }

        protected override string RunPart2()
        {
            Regex rgx = new("(.)(.)((?!\\1)(?!\\2).)((?!\\1)(?!\\2)(?!\\3).)((?!\\1)(?!\\2)(?!\\3)(?!\\4).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8)(?!\\9).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8)(?!\\9)(?!\\10).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8)(?!\\9)(?!\\10)(?!\\11).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8)(?!\\9)(?!\\10)(?!\\11)(?!\\12).)((?!\\1)(?!\\2)(?!\\3)(?!\\4)(?!\\5)(?!\\6)(?!\\7)(?!\\8)(?!\\9)(?!\\10)(?!\\11)(?!\\12)(?!\\13).)");
            var match = rgx.Match(_fileInput);
            return (match.Index + match.Length).ToString();
        }
    }
}