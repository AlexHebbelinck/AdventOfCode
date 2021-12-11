using DailyCode.Base;
using System;
using System.Reflection;

namespace DailyCode.Tests.Models
{
	public class DailyCodeMethods
	{
		public int Year { get; set; }
		public uint Day { get; set; }
		public MethodInfo? RunPart1 { get; set; }
		public MethodInfo? RunPart2 { get; set; }
		public Type? Type { get; set; }
	}
}