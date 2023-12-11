using Common.Extensions;
using Common.Models;
using DailyCode.Base;

namespace DailyCode.Year2023.Day10
{
    public class Day10(string sessionId) : BaseDay(sessionId)
    {
        private char[][] _diagram;
        private Coordinates _startCoordinates;

        protected override void SetupData(FileInputCollection fileInputs)
        {
            _diagram = fileInputs.TrimTrailingNewLine().ToJaggedArray<char>();

            var posY = fileInputs.TakeWhile(x => !x.Contains('S')).Count();
            var posX = fileInputs[posY].IndexOf('S');
            _startCoordinates = new Coordinates(posX, posY);
        }

        protected override string RunPart1()
        {
            var (Symbol, Coords) = GetValidStart(_diagram, _startCoordinates, false);
            var currentSymbol = Symbol;
            var steps = 1;

            (int PosX, int PosY) currentPosition = (Coords.PosX, Coords.PosY);
            (int PosX, int PosY) previousPosition = (_startCoordinates.PosX, _startCoordinates.PosY);

            do
            {
                var newPosition = SymbolHelper.CalulateNewPosition(previousPosition, currentPosition, currentSymbol);

                previousPosition = currentPosition;
                currentPosition = newPosition;
                currentSymbol = _diagram[currentPosition.PosY][currentPosition.PosX];

                steps++;
            }
            while (currentSymbol != 'S');
            return Math.Floor(steps / 2m).ToString();
        }

        private (char Symbol, Coordinates Coords) GetValidStart(char[][] _diagram, Coordinates _startCoordinates, bool skipFirst)
        {
            if (!skipFirst && _startCoordinates.PosY + 1 <= _diagram.Length)
            {
                var southVerticalSymbol = _diagram[_startCoordinates.PosY + 1][_startCoordinates.PosX];
                if (southVerticalSymbol == SymbolHelper.VerticalPipe.Symbol || southVerticalSymbol == SymbolHelper.BendNorthEast.Symbol || southVerticalSymbol == SymbolHelper.BendNorthWest.Symbol)
                    return (_diagram[_startCoordinates.PosY + 1][_startCoordinates.PosX], new Coordinates(_startCoordinates.PosX, _startCoordinates.PosY + 1));
            }

            if (_startCoordinates.PosY - 1 < _diagram.Length)
            {
                var northVerticalSymbol = _diagram[_startCoordinates.PosY - 1][_startCoordinates.PosX];
                if (northVerticalSymbol == SymbolHelper.VerticalPipe.Symbol || northVerticalSymbol == SymbolHelper.BendSouthEast.Symbol || northVerticalSymbol == SymbolHelper.BendSouthWest.Symbol)
                    return (_diagram[_startCoordinates.PosY - 1][_startCoordinates.PosX], new Coordinates(_startCoordinates.PosX, _startCoordinates.PosY - 1));
            }

            if (_startCoordinates.PosX + 1 <= _diagram[0].Length)
            {
                var westHorizontalSymbol = _diagram[_startCoordinates.PosY][_startCoordinates.PosX + 1];
                if (westHorizontalSymbol == SymbolHelper.HorizontalPipe.Symbol || westHorizontalSymbol == SymbolHelper.BendNorthWest.Symbol || westHorizontalSymbol == SymbolHelper.BendSouthWest.Symbol)
                    return (_diagram[_startCoordinates.PosY][_startCoordinates.PosX + 1], new Coordinates(_startCoordinates.PosX + 1, _startCoordinates.PosY));
            }

            if (_startCoordinates.PosX - 1 < _diagram[0].Length)
            {
                var eastHorizontalSymbol = _diagram[_startCoordinates.PosY][_startCoordinates.PosX - 1];
                if (eastHorizontalSymbol == SymbolHelper.HorizontalPipe.Symbol || eastHorizontalSymbol == SymbolHelper.BendNorthEast.Symbol || eastHorizontalSymbol == SymbolHelper.BendSouthEast.Symbol)
                    return (_diagram[_startCoordinates.PosY][_startCoordinates.PosX - 1], new Coordinates(_startCoordinates.PosX - 1, _startCoordinates.PosY));
            }

            throw new InvalidDataException();
        }

        protected override string RunPart2()
        {
            var diagramDuplicate = new int[_diagram.Length][];
            for (var y = 0; y < diagramDuplicate.Length; y++)
            {
                diagramDuplicate[y] = new int[_diagram[0].Length];

                for (var x = 0; x < diagramDuplicate[y].Length; x++)
                {
                    diagramDuplicate[y][x] = 0;
                }
            }

            diagramDuplicate = NewMethod(diagramDuplicate);
            diagramDuplicate = NewMethod2(diagramDuplicate);

            for (var y = 0; y < diagramDuplicate.Length; y++)
            {
                for (var x = 0; x < diagramDuplicate[y].Length; x++)
                {
                    Console.Write(diagramDuplicate[y][x]);
                }
                Console.Write("\n");
            }
            //        var total = 0;
            //do
            //{
            //    Console.Clear();

            //    for (var y = 0; y < diagramDuplicate.Length; y++)
            //    {
            //        for (var x = 0; x < diagramDuplicate[y].Length; x++)
            //        {
            //            var currentTile = diagramDuplicate[y][x];
            //            if (currentTile != 9)
            //            {
            //                if (x == 0 || y == 0 || x == diagramDuplicate[y].Length || y == diagramDuplicate.Length)
            //                {
            //                    diagramDuplicate[y][x] = -1;
            //                }
            //                else
            //                {
            //                    if (currentTile >= 0)
            //                    {
            //                        if (!diagramDuplicate[y].Skip(x).Any(x => x == 9))
            //                        {
            //                            diagramDuplicate[y][x] = -1;
            //                        }
            //                        else
            //                        {
            //                            var adjacent = diagramDuplicate.GetAdjacent((y, x), false);
            //                            if (adjacent.Any(x => x == -1)) diagramDuplicate[y][x] = -1;
            //                            else if (currentTile == 4)
            //                            {
            //                                var adjactenWithDiagonals = diagramDuplicate.GetAdjacent((y, x), true);
            //                                if (adjactenWithDiagonals.All(x => x == 9)) diagramDuplicate[y][x] = 8;
            //                                else diagramDuplicate[y][x] = -1;
            //                            }
            //                            else if (currentTile == 3) diagramDuplicate[y][x] = 8;
            //                            else if (adjacent.Any(x => x >= 3 && x < 9)) diagramDuplicate[y][x] = 8;
            //                        }


            //                    }
            //                }
            //            }
            //            Console.Write(diagramDuplicate[y][x]);
            //        }
            //        Console.Write("\n");
            //    }
            //}
            //while (diagramDuplicate.Any(x => x.Any(y => y >= 0 && y < 3)));

            return diagramDuplicate.Select(x => x.Where(y => y == 2).Count()).Sum().ToString();

        }

        private int[][] NewMethod(int[][] diagramDuplicate)
        {
            var (Symbol, Coords) = GetValidStart(_diagram, _startCoordinates, false);
            var currentSymbol = Symbol;
            var steps = 1;

            (int PosX, int PosY) currentPosition = (Coords.PosX, Coords.PosY);
            (int PosX, int PosY) previousPosition = (_startCoordinates.PosX, _startCoordinates.PosY);
            var isLoopCompleted = false;
            do
            {
                if (currentSymbol == 'S')
                {
                    isLoopCompleted = true;
                    DoSomething(diagramDuplicate, previousPosition, currentPosition, true);
                }
                else
                {
                    var newPosition = SymbolHelper.CalulateNewPosition(previousPosition, currentPosition, currentSymbol);

                    diagramDuplicate[currentPosition.PosY][currentPosition.PosX] = 9;
                    DoSomething(diagramDuplicate, previousPosition, currentPosition, true);

                    previousPosition = currentPosition;
                    currentPosition = newPosition;
                    currentSymbol = _diagram[currentPosition.PosY][currentPosition.PosX];

                    steps++;
                }
            }
            while (!isLoopCompleted);

            return diagramDuplicate;
        }

        private int[][] NewMethod2(int[][] diagramDuplicate)
        {
            var (Symbol, Coords) = GetValidStart(_diagram, _startCoordinates, true);
            var currentSymbol = Symbol;
            var steps = 1;

            (int PosX, int PosY) currentPosition = (Coords.PosX, Coords.PosY);
            (int PosX, int PosY) previousPosition = (_startCoordinates.PosX, _startCoordinates.PosY);
            var isLoopCompleted = false;
            do
            {
                if (currentSymbol == 'S')
                {
                    isLoopCompleted = true;
                    DoSomething(diagramDuplicate, previousPosition, currentPosition, false);
                }
                else
                {
                    var newPosition = SymbolHelper.CalulateNewPosition(previousPosition, currentPosition, currentSymbol);

                    diagramDuplicate[currentPosition.PosY][currentPosition.PosX] = 9;
                    DoSomething(diagramDuplicate, previousPosition, currentPosition, false);

                    previousPosition = currentPosition;
                    currentPosition = newPosition;
                    currentSymbol = _diagram[currentPosition.PosY][currentPosition.PosX];

                    steps++;
                }
            }
            while (!isLoopCompleted);

            return diagramDuplicate;
        }

        private void DoSomething(int[][] diagram, (int PosX, int PosY) prevPosition, (int PosX, int PosY) currentPosition, bool isRightSide)
        {
            if(prevPosition.PosX > currentPosition.PosX)
            {
                if (isRightSide && diagram.Length > currentPosition.PosY + 1 && diagram[currentPosition.PosY + 1][currentPosition.PosX] != 9)
                    diagram[currentPosition.PosY + 1][currentPosition.PosX]++;

                else if(!isRightSide && currentPosition.PosY - 1 >= 0 && diagram[currentPosition.PosY - 1][currentPosition.PosX] !=9)
                     diagram[currentPosition.PosY - 1][currentPosition.PosX]++;
            }

            if (prevPosition.PosX < currentPosition.PosX)
            {
                if (!isRightSide && diagram.Length > currentPosition.PosY + 1 && diagram[currentPosition.PosY + 1][currentPosition.PosX] !=9)
                    diagram[currentPosition.PosY + 1][currentPosition.PosX]++;

                else if (isRightSide && currentPosition.PosY - 1 >= 0 && diagram[currentPosition.PosY - 1][currentPosition.PosX] != 9)
                    diagram[currentPosition.PosY - 1][currentPosition.PosX]++;
            }

            if (prevPosition.PosY > currentPosition.PosY)
            {
                if (isRightSide && diagram[0].Length > currentPosition.PosX + 1 && diagram[currentPosition.PosY][currentPosition.PosX + 1] != 9)
                    diagram[currentPosition.PosY][currentPosition.PosX + 1]++;

                else if (!isRightSide && currentPosition.PosX - 1 >= 0 && diagram[currentPosition.PosY][currentPosition.PosX - 1] != 9)
                    diagram[currentPosition.PosY][currentPosition.PosX - 1]++;
            }

            if (prevPosition.PosY < currentPosition.PosY)
            {
                if (!isRightSide && diagram[0].Length > currentPosition.PosX + 1 && diagram[currentPosition.PosY][currentPosition.PosX + 1] != 9)
                    diagram[currentPosition.PosY][currentPosition.PosX + 1]++;

                else if (isRightSide && currentPosition.PosX - 1 >= 0 && diagram[currentPosition.PosY][currentPosition.PosX - 1] != 9)
                    diagram[currentPosition.PosY][currentPosition.PosX - 1]++;
            }
            //if (diagram.Length > currentPosition.PosY + 1 && diagram[currentPosition.PosY + 1][currentPosition.PosX] != 9)
            //{
            //    diagram[currentPosition.PosY + 1][currentPosition.PosX]++;
            //}

            //if (currentPosition.PosY - 1 >= 0 && diagram[currentPosition.PosY + -1][currentPosition.PosX] != 9)
            //{
            //    diagram[currentPosition.PosY - 1][currentPosition.PosX]++;
            //}

            //if (diagram[0].Length > currentPosition.PosX + 1 && diagram[currentPosition.PosY][currentPosition.PosX + 1] != 9)
            //{
            //    diagram[currentPosition.PosY][currentPosition.PosX + 1]++;
            //}

            //if (currentPosition.PosX - 1 >= 0 && diagram[currentPosition.PosY][currentPosition.PosX - 1] != 9)
            //{
            //    diagram[currentPosition.PosY][currentPosition.PosX - 1]++;
            //}
        }
    }

    public sealed class SymbolHelper
    {
        public static readonly SymbolHelper VerticalPipe = new('|', (prevPos, currentPos) => prevPos.PosY > currentPos.PosY
                                                                    ? (currentPos.PosX, currentPos.PosY - 1)
                                                                    : (currentPos.PosX, currentPos.PosY + 1));

        public static readonly SymbolHelper HorizontalPipe = new('-', (prevPos, currentPos) => prevPos.PosX > currentPos.PosX
                                                            ? (currentPos.PosX - 1, currentPos.PosY)
                                                            : (currentPos.PosX + 1, currentPos.PosY));

        public static readonly SymbolHelper BendNorthEast = new('L', (prevPos, currentPos) => prevPos.PosY < currentPos.PosY
                                                           ? (currentPos.PosX + 1, currentPos.PosY)
                                                           : (currentPos.PosX, currentPos.PosY - 1));

        public static readonly SymbolHelper BendNorthWest = new('J', (prevPos, currentPos) => prevPos.PosY < currentPos.PosY
                                                           ? (currentPos.PosX - 1, currentPos.PosY)
                                                           : (currentPos.PosX, currentPos.PosY - 1));

        public static readonly SymbolHelper BendSouthWest = new('7', (prevPos, currentPos) => prevPos.PosY > currentPos.PosY
                                                           ? (currentPos.PosX - 1, currentPos.PosY)
                                                           : (currentPos.PosX, currentPos.PosY + 1));

        public static readonly SymbolHelper BendSouthEast = new('F', (prevPos, currentPos) => prevPos.PosY > currentPos.PosY
                                                           ? (currentPos.PosX + 1, currentPos.PosY)
                                                           : (currentPos.PosX, currentPos.PosY + 1));

        public char Symbol { get; set; }
        public Func<(int PosX, int PosY), (int PosX, int PosY), (int PosX, int PosY)> CalculateNewPos { get; set; }

        private SymbolHelper(char symbol, Func<(int PosX, int PosY), (int PosX, int PosY), (int PosX, int PosY)> calculateNewPos)
        {
            Symbol = symbol;
            CalculateNewPos = calculateNewPos;
        }

        public static (int PosX, int PosY) CalulateNewPosition((int PosX, int PosY) previousPos, (int PosX, int PosY) currentPos, char symbol)
            => GetList().First(x => x.Symbol == symbol).CalculateNewPos(previousPos, currentPos);

        private static List<SymbolHelper> GetList()
            => new()
            {
                VerticalPipe,
                HorizontalPipe,
                BendNorthEast,
                BendSouthWest,
                BendNorthWest,
                BendSouthEast
            };
    }
}