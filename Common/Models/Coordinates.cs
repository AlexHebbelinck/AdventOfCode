namespace DailyCode.Common.Models
{
    public class Coordinates : IEquatable<Coordinates>
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Coordinates(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
        }

        public bool Equals(Coordinates? other)
            => PosX == other?.PosX && PosY == other?.PosY;

        public override int GetHashCode()
            => Tuple.Create(PosX, PosY).GetHashCode();

        public bool IsAdjacent(Coordinates coordinates, bool allowDiagonal)
        {
            if(coordinates.Equals(this)) return false;

            return ((IsAdjacent(PosX, coordinates.PosX))
                && (IsAdjacent(PosY, coordinates.PosY)))
                && (allowDiagonal || !(coordinates.PosX != PosX && coordinates.PosY != PosY));
        }

        private bool IsAdjacent(int currentPos, int comparePos)
            => comparePos + 1 == currentPos || comparePos - 1 == currentPos || comparePos == currentPos;
    }
}