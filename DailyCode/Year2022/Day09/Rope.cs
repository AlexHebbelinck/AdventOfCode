using DailyCode.Common.Models;

namespace DailyCode.Year2022.Day09
{
    public class Rope
    {
        public Rope(int length = 1)
        {
            HeadKnot = new Coordinates(0, 0);
            Knots = Enumerable.Range(0, length - 1).Select(_ => new Coordinates(0, 0)).ToList();
            TailKnot = new Coordinates(0, 0);
            TailKnotCoordHistory = new List<Coordinates> { new Coordinates(0, 0) };
        }

        public Coordinates HeadKnot { get; }

        public List<Coordinates> Knots { get; set; }

        public Coordinates TailKnot { get; }

        public List<Coordinates> TailKnotCoordHistory { get; }

        public void DoMotion(string direction, int steps)
        {
            switch (direction)
            {
                case "U":
                    MoveHead(() => HeadKnot.PosY++, steps);
                    break;

                case "D":
                    MoveHead(() => HeadKnot.PosY--, steps);
                    break;

                case "L":
                    MoveHead(() => HeadKnot.PosX--, steps);
                    break;

                case "R":
                    MoveHead(() => HeadKnot.PosX++, steps);
                    break;
            }
        }

        private void MoveHead(Action movememtAction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                movememtAction();
                CheckTrailingKnotsMovement();
            }
        }

        private void CheckTrailingKnotsMovement()
        {
            var leadingKnot = HeadKnot;
            foreach (var knot in Knots)
            {
                CheckKnotMovement(leadingKnot, knot);
                leadingKnot = knot;
            }

            CheckTailMovement(leadingKnot);
        }

        private void CheckTailMovement(Coordinates leadingKnot)
        {
            CheckKnotMovement(leadingKnot, TailKnot);
            if (!TailKnotCoordHistory.Any(coord => coord.PosX == TailKnot.PosX && coord.PosY == TailKnot.PosY))
                TailKnotCoordHistory.Add(new Coordinates(TailKnot.PosX, TailKnot.PosY));
        }

        private void CheckKnotMovement(Coordinates leadingKnot, Coordinates currentKnot)
        {
            var xPosDiff = Math.Abs(currentKnot.PosX - leadingKnot.PosX);
            var yPosDiff = Math.Abs(currentKnot.PosY - leadingKnot.PosY);

            if (xPosDiff == 2)
            {
                if (yPosDiff >= 1)
                {
                    if (leadingKnot.PosY > currentKnot.PosY)
                        currentKnot.PosY++;
                    else
                        currentKnot.PosY--;
                }

                if (leadingKnot.PosX > currentKnot.PosX)
                    currentKnot.PosX++;
                else
                    currentKnot.PosX--;
            }
            else if (yPosDiff == 2)
            {
                if (xPosDiff >= 1)
                {
                    if (leadingKnot.PosX > currentKnot.PosX)
                        currentKnot.PosX++;
                    else
                        currentKnot.PosX--;
                }

                if (leadingKnot.PosY > currentKnot.PosY)
                    currentKnot.PosY++;
                else
                    currentKnot.PosY--;
            }
        }
    }
}