using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day10 : BaseDay
    {
        private List<int> _fileInput = new();

        public Day10(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            _fileInput = fileInput.ConvertAll(x => int.Parse(x));
        }

        protected override string RunPart1()
        {
            return "";
        }

        protected override string RunPart2()
        {
            return "";
        }
    }
}