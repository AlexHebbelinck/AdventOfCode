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
        List<(List<int> l1, List<int> l2)> _l3 = new();

        public Day03(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
        {
            foreach(var input in fileInputs)
            {
                _l3.Add((input.Substring(0, input.Length / 2).Select(x => char.IsUpper(x) ? x - 38 : x - 96).ToList(),
                 input.Substring(input.Length / 2, input.Length / 2).Select(x => char.IsUpper(x) ? x - 38 : x - 96).ToList()));
            }
        }

        protected override string RunPart1()
        {
            var result = 0;
            foreach(var input in _l3)
            {
                result+= input.l1.Intersect(input.l2).Sum();
            }

            return result.ToString();
        }

        protected override string RunPart2()
        {
            var total = _l3.Count / 3;
            var result = 0;
            for (int i = 0; i < total; i++)
            {
                var l4 = _l3.Skip(i * 3).Take(3).Select(x => x.l1.Concat(x.l2)).ToList();
                result += l4[0].Intersect(l4[1]).Intersect(l4[2]).Sum();
            }
            return result.ToString();
        }
    }
}
