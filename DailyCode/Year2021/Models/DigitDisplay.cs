using System.Linq;

namespace DailyCode.Year2021.Models
{
	public sealed class DigitDisplay
	{
		public static DigitDisplay Zero { get; } = new("abcefg");
		public static DigitDisplay One { get; } = new("cf");
		public static DigitDisplay Two { get; } = new("acdeg");
		public static DigitDisplay Three { get; } = new("acdfg");
		public static DigitDisplay Four { get; } = new("bcdf");
		public static DigitDisplay Five { get; } = new("abdfg");
		public static DigitDisplay Six { get; } = new("abdefg");
		public static DigitDisplay Seven { get; } = new("acf");
		public static DigitDisplay Eight { get; } = new("abcdefg");
		public static DigitDisplay Nine { get; } = new("abcdfg");

		private DigitDisplay(string segmentPattern)
		{
			SegmentPattern = segmentPattern;
		}

		public string SegmentPattern { get; }

		public int SegmentLength => SegmentPattern.Length;
	}
}