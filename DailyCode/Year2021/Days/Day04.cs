using Common.Helpers.Extensions;
using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2021.Days
{
    public class Day04 : BaseDay
    {
        private List<int> DrawNumbers { get; set; } = new List<int>();
        private List<int[][]> Boards { get; } = new List<int[][]>();

        private readonly Regex NumberRgx = new(@"\d+");

        public Day04(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            DrawNumbers = fileInput[0].Split(',').Select(x => int.Parse(x)).ToList();
            fileInput.RemoveAt(0);

            int[][]? newCreatedBoard = null;
            fileInput.ForEach(line =>
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var numbers = NumberRgx.Matches(line).Select(x => int.Parse(x.Value)).ToArray();

                    newCreatedBoard = newCreatedBoard == null
                        ? new int[][] { numbers }
                        : newCreatedBoard.Append(numbers).ToArray();
                }
                else
                {
                    if (newCreatedBoard != null) Boards.Add(newCreatedBoard);
                    newCreatedBoard = null;
                }
            });

            if (newCreatedBoard != null) Boards.Add(newCreatedBoard);
        }

        protected override string RunPart1()
        {
            var pivotBoards = Boards.ConvertAll(board => board.Pivot());
            var numbersPerRow = Boards[0][0].Length;
            var drawnNumbers = new List<int>();
            int[][]? winningBoard = null;

            do
            {
                var totalNumbersDrawn = drawnNumbers.Count;
                drawnNumbers.Add(DrawNumbers[totalNumbersDrawn]);

                if (totalNumbersDrawn >= numbersPerRow)
                    winningBoard = GetWinningBoard(Boards, drawnNumbers) ?? GetWinningBoard(pivotBoards, drawnNumbers);
            }
            while (winningBoard == null);

            var sumUnmarkedNumbers = winningBoard.SelectMany(line => line.Where(number => !drawnNumbers.Contains(number))).Sum();

            return (sumUnmarkedNumbers * drawnNumbers.Last()).ToString();
        }

        protected override string RunPart2()
        {
            var pivotBoards = Boards.ConvertAll(board => board.Pivot());
            var numbersPerRow = Boards[0][0].Length;
            var drawnNumbers = new List<int>();
            int[][]? losingBoard = null;

            do
            {
                var totalNumbersDrawn = drawnNumbers.Count;
                drawnNumbers.Add(DrawNumbers[totalNumbersDrawn]);

                if (totalNumbersDrawn >= numbersPerRow)
                {
                    if (Boards.Count > 1)
                    {
                        RemoveWinningBoards(Boards, pivotBoards, drawnNumbers);
                    }
                    else
                    {
                        losingBoard = GetWinningBoard(Boards, drawnNumbers) ?? GetWinningBoard(pivotBoards, drawnNumbers);
                    }
                }
            }
            while (losingBoard == null);

            var sumUnmarkedNumbers = losingBoard.SelectMany(line => line.Where(number => !drawnNumbers.Contains(number))).Sum();

            return (sumUnmarkedNumbers * drawnNumbers.Last()).ToString();
        }

        private int[][]? GetWinningBoard(List<int[][]> boards, List<int> drawnNumbers)
            => boards.Find(board => board.Any(line => line.All(number => drawnNumbers.Contains(number))));

        private List<int[][]> GetWinningBoards(List<int[][]> boards, List<int> drawnNumbers)
            => boards.Where(board => board.Any(line => line.All(number => drawnNumbers.Contains(number)))).ToList();

        private void RemoveWinningBoards(List<int[][]> boards, List<int[][]> pivotBoards, List<int> drawnNumbers)
        {
            var winningBoards = GetWinningBoards(boards, drawnNumbers);
            winningBoards.ForEach(winningBoard =>
            {
                var index = Boards.IndexOf(winningBoard);
                Boards.Remove(winningBoard);
                pivotBoards.RemoveAt(index);
            });

            var winningPivotBoards = GetWinningBoards(pivotBoards, drawnNumbers);
            winningPivotBoards.ForEach(winningBoard =>
            {
                var index = pivotBoards.IndexOf(winningBoard);
                pivotBoards.Remove(winningBoard);
                Boards.RemoveAt(index);
            });
        }
    }
}