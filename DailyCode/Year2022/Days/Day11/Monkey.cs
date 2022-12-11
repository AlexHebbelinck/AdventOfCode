using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCode.Year2022.Days.Day11
{
    public class Monkey
    {
        public int Id { get; init; }

        public Queue<ulong> Items { get; init; } = new();

        public string Operation { get; init; } = null!;

        public uint TestDivider { get; init; }

        public Monkey TestTruthMonkey { get; set; } = null!;

        public Monkey TestFalseMonkey { get; set; } = null!;

        public long ItemsInspected { get; private set; } = 0;

        public void InspectItems(uint itemWorryLevelRoof, bool willGetBored)
        {
            var loops = Items.Count;
            for (int i = 0; i < loops; i++)
            {
                var itemWorryLevel = Items.Dequeue();
                if (itemWorryLevel > itemWorryLevelRoof)
                    itemWorryLevel -= (itemWorryLevel / itemWorryLevelRoof) * itemWorryLevelRoof;

                itemWorryLevel = DoOperation(itemWorryLevel);
                if (willGetBored)
                    itemWorryLevel = GetsBored(itemWorryLevel);

                var passToMonkey = (itemWorryLevel % TestDivider) == 0 ? TestTruthMonkey : TestFalseMonkey;

                passToMonkey.Items.Enqueue(itemWorryLevel);

                ++ItemsInspected;
            }
        }

        private static ulong GetsBored(ulong itemWorryLevel)
            => itemWorryLevel / 3;

        private ulong DoOperation(ulong itemWorryLevel)
        {
            var splitOperation = Operation.Split(' ');
            return splitOperation[1] switch
            {
                "+" => itemWorryLevel + extractSecondValue(itemWorryLevel, splitOperation[2]),
                "-" => itemWorryLevel - extractSecondValue(itemWorryLevel, splitOperation[2]),
                "/" => itemWorryLevel / extractSecondValue(itemWorryLevel, splitOperation[2]),
                "*" => itemWorryLevel * extractSecondValue(itemWorryLevel, splitOperation[2]),
                _ => 0,
            };

            static ulong extractSecondValue(ulong itemWorryLevel, string secondValue)
                => secondValue.Trim().Equals("old")
                    ? itemWorryLevel
                    : (ulong)Convert.ToInt64(secondValue);
        }
    }
}
