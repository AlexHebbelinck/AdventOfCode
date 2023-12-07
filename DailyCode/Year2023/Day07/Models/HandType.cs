namespace DailyCode.Year2023.Day07.Models
{
    public sealed class HandType
    {
        public static readonly HandType FiveKind = new(nameof(FiveKind), 1, "5");
        public static readonly HandType FourKind = new(nameof(FourKind), 2, "41");
        public static readonly HandType FullHouse = new(nameof(FullHouse), 3, "32");
        public static readonly HandType ThreeKind = new(nameof(ThreeKind), 4, "311");
        public static readonly HandType TwoPair = new(nameof(TwoPair), 5, "221");
        public static readonly HandType OnePair = new(nameof(OnePair), 6, "2111");
        public static readonly HandType HighCard = new(nameof(HighCard), 7, "11111");

        public string Name { get; set; }
        public int Rank { get; set; }
        public string Pattern { get; set; }

        private HandType(string name, int rank, string pattern)
        {
            Name = name;
            Rank = rank;
            Pattern = pattern;
        }

        public static HandType Get(string hand)
        {
            var newHand = HandleJoker(hand);
            var handPattern = newHand
               .GroupBy(c => c)
               .Select(x => x.Count())
               .OrderByDescending(x => x)
               .Aggregate(string.Empty, (curr, next) => $"{curr}{next}");

            return GetAll().First(x => x.Pattern.Equals(handPattern));
        }

        //They had do add a joker.... for real... lovely...
        private static string HandleJoker(string hand)
        {
            if (hand.Contains('1'))
            {
                var keyCount = hand.GroupBy(c => c)
                    .Select(x => new { x.Key, Count = x.Count() });

                var jokerKey = keyCount.First(x => x.Key.Equals('1'));
                if (jokerKey.Count == 5)
                    return "AAAAA";

                return hand.Replace('1', keyCount.Where(x => x.Key != '1')
                    .OrderByDescending(x => x.Count)
                    .First()
                    .Key);

            }

            return hand;

        }

        private static List<HandType> GetAll()
            => new()
            {
                FiveKind,
                FourKind,
                FullHouse,
                ThreeKind,
                TwoPair,
                OnePair,
                HighCard
            };
    }
}
