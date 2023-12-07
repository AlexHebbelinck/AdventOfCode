namespace DailyCode.Year2023.Day07.Models
{
    public static class CardMapping
    {
        private static readonly Dictionary<char, char> _mappingPairs = new()
        {
            { 'A', 'A'},
            { 'K', 'B'},
            { 'Q', 'C'},
            { 'J', 'D'},
            { 'T', 'E'},
            { '9', 'F'},
            { '8', 'G'},
            { '7', 'H'},
            { '6', 'I'},
            { '5', 'J'},
            { '4', 'K'},
            { '3', 'L'},
            { '2', 'M'},
            { '1', 'N'},
        };

        public static char Map(char card)
            => _mappingPairs.First(x => x.Key == card).Value;

        public static string Map(string cards)
        {
            var mappedCards = string.Empty;
            foreach (char card in cards)
            {
                mappedCards += Map(card);
            }
            return mappedCards;
        }
    }
}