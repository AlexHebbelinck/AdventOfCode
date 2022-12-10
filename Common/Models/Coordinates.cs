namespace DailyCode.Year2021.Models
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
    }
}