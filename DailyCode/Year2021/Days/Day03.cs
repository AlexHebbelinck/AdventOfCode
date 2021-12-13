using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day03 : BaseDay
    {
        private int[][]? _fileInput;

        public Day03(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            _fileInput = fileInput.Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        }

        protected override string RunPart1()
        {
            if (_fileInput == null) throw new NullReferenceException("Kapoowie explosions!");

            (int[] gammaRate, int[] epsilonRate) = (new int[_fileInput[0].Length], new int[_fileInput[0].Length]);

            for (int y = 0; y < _fileInput[0].Length; y++)
            {
                var totalZeros = 0;
                for (int x = 0; x < _fileInput.Length; x++)
                {
                    totalZeros += Convert.ToInt32(_fileInput[x][y] == 0);
                }

                gammaRate[y] = (Convert.ToInt32(totalZeros < (_fileInput.Length / 2)));
            }

            epsilonRate = gammaRate.Select(x => x ^= 1).ToArray();

            return (Convert.ToInt32(string.Join("", gammaRate), 2) * Convert.ToInt32(string.Join("", epsilonRate), 2)).ToString();
        }

        protected override string RunPart2()
        {
            if (_fileInput == null) throw new NullReferenceException("Kapoowie explosions!");

            var oxygenGeneratorRating = CalculateRatings(_fileInput, 0, true);
            var scrubberRating = CalculateRatings(_fileInput, 0, false);

            return (Convert.ToInt32(string.Join("", oxygenGeneratorRating), 2) * Convert.ToInt32(string.Join("", scrubberRating), 2)).ToString();
        }

        private int[] CalculateRatings(int[][] validArrays, int currentColumn, bool keepMost)
        {
            if (validArrays.Length == 1) return validArrays[0];

            var totalZeros = 0;
            for (int row = 0; row < validArrays.Length; row++)
            {
                totalZeros += Convert.ToInt32(validArrays[row][currentColumn] == 0);
            }

            var isMostlyZeros = totalZeros <= (validArrays.Length / 2f);
            var newValidArrays = validArrays.Where(x => x[currentColumn] == (Convert.ToInt32(keepMost ? isMostlyZeros : !isMostlyZeros))).ToArray();

            return CalculateRatings(newValidArrays, ++currentColumn, keepMost);
        }
    }
}