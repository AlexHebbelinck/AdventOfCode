namespace DailyCode.Year2023.Day01
{
    public enum Digits
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine
    }

    public static class DigitsHelper
    {
        public static string ConvertDigitToInt(string text)
        {
            foreach (var digit in GetList())
            {
                text = text.Replace(digit.Key, digit.Value.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }
            return text;
        }

        public static Dictionary<string, int> GetList()
            => new()
            {
                { nameof(Digits.Zero), (int)Digits.Zero },
                { nameof(Digits.One), (int)Digits.One },
                { nameof(Digits.Two), (int)Digits.Two },
                { nameof(Digits.Three), (int)Digits.Three },
                { nameof(Digits.Four), (int)Digits.Four },
                { nameof(Digits.Five), (int)Digits.Five },
                { nameof(Digits.Six), (int)Digits.Six },
                { nameof(Digits.Seven), (int)Digits.Seven },
                { nameof(Digits.Eight), (int)Digits.Eight },
                { nameof(Digits.Nine), (int)Digits.Nine },
            };
    }
}