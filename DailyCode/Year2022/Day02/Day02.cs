using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2022.Day02
{
    internal class Day02 : BaseDay
    {
        private IEnumerable<(char column1, char column2)> _fileInputs = null!;

        public Day02(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
            => _fileInputs = fileInputs.Select(x => { string[] lineInputs = x.Split(' '); return (char.Parse(lineInputs[0]), char.Parse(lineInputs[1])); });

        protected override string RunPart1()
        {
            var score = 0;
            foreach (var (column1, column2) in _fileInputs)
            {
                char myChoiceCorrect = (char)(column2 - 23);

                if (column1 == myChoiceCorrect)
                    score += 3 + CalculateScore(myChoiceCorrect);
                else if (column1 == 'A' && myChoiceCorrect == 'C')
                    score += 0 + CalculateScore(myChoiceCorrect);
                else if (column1 == 'C' && myChoiceCorrect == 'A')
                    score += 6 + CalculateScore(myChoiceCorrect);
                else if (column1 > myChoiceCorrect)
                    score += 0 + CalculateScore(myChoiceCorrect);
                else if (column1 < myChoiceCorrect)
                    score += 6 + CalculateScore(myChoiceCorrect);
            }

            return score.ToString();

            static int CalculateScore(char myChoice) => myChoice - 64;
        }

        protected override string RunPart2()
        {
            var score = 0;
            foreach (var (column1, column2) in _fileInputs)
            {
                score += column2 switch
                {
                    'X' => 0 + (column1 == 'A' ? column1 - 62 : column1 - 65),
                    'Y' => 3 + (column1 - 64),
                    'Z' => 6 + (column1 == 'C' ? column1 - 66 : column1 - 63),
                    _ => 0,
                };
            }

            return score.ToString();
        }
    }
}