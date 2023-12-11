﻿using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2021.Days
{
    public class Day16 : BaseDay
    {
        private List<int> _fileInput = new();

        public Day16(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInput)
        {
            _fileInput = fileInput.ConvertAll(x => int.Parse(x));
        }

        protected override string RunPart1()
        {
            return "";
        }

        protected override string RunPart2()
        {
            return "";
        }
    }
}