using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day01 : BaseDay
    {
        private List<int> _fileInput = new();

        public Day01(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
        {
            _fileInput = fileInput.ConvertAll(x => int.Parse(x));
        }

        protected override string RunPart1()
        {
            var counter = 0;
            for (int i = 1; i < _fileInput.Count; i++)
            {
                if (_fileInput[i] > _fileInput[i - 1])
                    counter++;
            }
            return counter.ToString();
        }

        protected override string RunPart2()
        {
            var counter = 0;
            var previousSum = 0;

            for (int i = 0; i < _fileInput.Count; i++)
            {
                var sum = _fileInput[i];
                if (_fileInput.Count > i + 1) sum += _fileInput[i + 1];
                if (_fileInput.Count > i + 2) sum += _fileInput[i + 2];

                if (i > 0 && sum > previousSum)
                    counter++;

                previousSum = sum;
            }

            return counter.ToString();
        }
    }
}