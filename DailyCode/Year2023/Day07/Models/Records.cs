namespace DailyCode.Year2023.Day07.Models
{
    public record MappedCamelCardsMatch(string Hand, int Bid, HandType HandType);
    
    public record CamelCardsMatch(string Hand, int Bid);
}
