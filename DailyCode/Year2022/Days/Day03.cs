using DailyCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCode.Year2022.Days
{
    internal class Day03 : BaseDay
    {
        private readonly List<(List<int> compartment1, List<int> compartment2)> _rucksacks = new();

        public Day03(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
        {
            foreach(var input in fileInputs)
            {
                _rucksacks.Add((input.Substring(0, input.Length / 2).Select(x => char.IsUpper(x) ? x - 38 : x - 96).ToList(),
                 input.Substring(input.Length / 2, input.Length / 2).Select(x => char.IsUpper(x) ? x - 38 : x - 96).ToList()));
            }
        }

        protected override string RunPart1()
        {
            var result = 0;
            _rucksacks.ForEach(rucksack => result += rucksack.compartment1.Intersect(rucksack.compartment2).Sum());
            return result.ToString();
        }

        protected override string RunPart2()
        {
            var totalGroups = _rucksacks.Count / 3;
            var result = 0;
            for (int i = 0; i < totalGroups; i++)
            {
                var groupRucksacks = _rucksacks.Skip(i * 3).Take(3).Select(x => x.compartment1.Concat(x.compartment2)).ToList();
                result += groupRucksacks[0].Intersect(groupRucksacks[1]).Intersect(groupRucksacks[2]).Sum();
            }
            return result.ToString();
        }
    }
}
