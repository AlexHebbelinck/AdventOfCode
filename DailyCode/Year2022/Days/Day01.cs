﻿using DailyCode.Base;
using DailyCode.Year2022.Models;

namespace DailyCode.Year2022.Days
{
    internal class Day01 : BaseDay
    {
        private readonly List<Elf> _elfs = new() { new Elf() };

        public Day01(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            foreach (var input in fileInput)
            {
                if (!string.IsNullOrEmpty(input))
                    _elfs.Last().Calories.Add(Convert.ToInt32(input));
                else
                    _elfs.Add(new Elf());
            }
        }

        protected override string RunPart1()
            => _elfs.Max(x => x.Total).ToString();

        protected override string RunPart2()
              => _elfs.OrderByDescending(x => x.Total).Take(3).Sum(x => x.Total).ToString();
    }
}