using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day10 : BaseDay
    {
        private readonly (char opening, char closing)[] _symbols = new (char opening, char closing)[] { ('(', ')'), ('[', ']'), ('{', '}'), ('<', '>') };
        private readonly (char symbol, int value)[] _syntaxErrorScores = new (char symbol, int value)[] { (')', 3), (']', 57), ('}', 1197), ('>', 25137) };
        private readonly (char symbol, int value)[] _completionScores = new (char symbol, int value)[] { (')', 1), (']', 2), ('}', 3), ('>', 4) };

        private List<Queue<char>> _fileInput = new();

        public Day10(string sessionId) : base(sessionId)
        {
        }

        protected override void ExtractData(List<string> fileInput)
            => _fileInput = fileInput.ConvertAll(x => new Queue<char>(x));

        protected override long RunPart1()
        {
            var corruptSymbols = new List<char>();

            foreach (var line in _fileInput)
            {
                var (openedSymbols, corruptSymbol) = HandleLineSymbols(line);
                if (corruptSymbol != null) corruptSymbols.Add(corruptSymbol.Value);
            }

            return corruptSymbols.Sum(symbol => _syntaxErrorScores.Single(es => es.symbol == symbol).value);
        }

        protected override long RunPart2()
        {
            var totalCompletionScores = new List<long>();

            foreach (var line in _fileInput)
            {
                var (openedSymbols, corruptSymbol) = HandleLineSymbols(line);

                if (corruptSymbol == null && openedSymbols?.Any() == true)
                {
                    long symbolsCompletionScore = 0;
                    foreach (var openedSymbol in openedSymbols)
                    {
                        symbolsCompletionScore *= 5;
                        symbolsCompletionScore += _completionScores.Single(cs => cs.symbol == _symbols.Single(s => s.opening == openedSymbol).closing).value;
                    }
                    totalCompletionScores.Add(symbolsCompletionScore);
                }
            }

            return totalCompletionScores.OrderBy(x => x).ToArray()[totalCompletionScores.Count / 2];
        }

        private (Stack<char> openedSymbols, char? corruptSymbol) HandleLineSymbols(Queue<char> line)
        {
            var openedSymbols = new Stack<char>();

            foreach (var symbol in line)
            {
                if (_symbols.Any(x => x.opening == symbol))
                {
                    openedSymbols.Push(symbol);
                }
                else
                {
                    var matchingOpeningSymbol = _symbols.Single(x => x.closing == symbol).opening;

                    if (openedSymbols.Pop() != matchingOpeningSymbol)
                        return (openedSymbols, symbol); //CORRUPTION!!! BEEP BOOP
                }
            }

            return (openedSymbols, null);
        }
    }
}