namespace DailyCode.Year2023.Day02
{
    public class Game
    {
        public Game(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public List<Set> Sets { get; set; } = [];

        public List<(string Color, int Amount)> GetLowestPerColor()
            => Sets.SelectMany(x => x.GetTotal())
                .GroupBy(x => x.Color)
                .Select(x => (Color: x.Key, Amount: x.Max(s => s.Amount)))
                .ToList();

        public bool IsValid(List<(string Color, int MaxAmount)> maxPerColors)
        {
            var isValid = true;
            foreach (var (Color, MaxAmount) in maxPerColors)
            {
                if (Sets.Exists(x => x.GetTotal().Exists(x => x.Color.Equals(Color, StringComparison.InvariantCultureIgnoreCase) && x.Amount > MaxAmount)))
                    isValid = false;
            }

            return isValid;
        }
    }

    public class Set
    {
        public List<Cube> RevealedCubes { get; set; } = [];

        public List<(string Color, int Amount)> GetTotal()
            => RevealedCubes.GroupBy(x => x.Color)
                .Select(x => (Color: x.Key, Amount: x.Select(x => x.Amount).Sum()))
                .ToList();
    }

    public class Cube
    {
        public Cube(string color, int amount)
        {
            Color = color;
            Amount = amount;
        }

        public string Color { get; set; }

        public int Amount { get; set; }
    }
}