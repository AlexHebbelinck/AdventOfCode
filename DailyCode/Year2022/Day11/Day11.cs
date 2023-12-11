using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2022.Day11
{
    internal class Day11 : BaseDay
    {
        private readonly List<Monkey> _monkeys = new();

        public Day11(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
        {
            List<(int monkeyId, int testTruthMonkeyId, int testFalseMonkeyId)> testMonkeys = new();

            var totalMonkeys = (fileInputs.Count + 1) / 7;
            for (int i = 1; i <= totalMonkeys; i++)
            {
                var monkeyId = i - 1;
                var monkey = new Monkey
                {
                    Id = monkeyId,
                    Items = new Queue<ulong>(fileInputs[1 + 7 * monkeyId].Split(':')[1].Split(',').Select(x => (ulong)Convert.ToInt64(x))),
                    Operation = fileInputs[2 + 7 * monkeyId].Split('=')[1].Trim(),
                    TestDivider = (uint)Convert.ToInt32(fileInputs[3 + 7 * monkeyId].Split(' ').Last())
                };
                var testTruthMonkeyId = Convert.ToInt32(fileInputs[4 + 7 * monkeyId].Split(' ').Last());
                var testFalsehMonkeyId = Convert.ToInt32(fileInputs[5 + 7 * monkeyId].Split(' ').Last());

                testMonkeys.Add((monkeyId, testTruthMonkeyId, testFalsehMonkeyId));
                _monkeys.Add(monkey);
            }

            foreach (var (monkeyId, testTruthMonkeyId, testFalseMonkeyId) in testMonkeys)
            {
                var monkey = _monkeys.First(x => x.Id == monkeyId);
                monkey.TestTruthMonkey = _monkeys.First(x => x.Id == testTruthMonkeyId);
                monkey.TestFalseMonkey = _monkeys.First(x => x.Id == testFalseMonkeyId);
            }
        }

        protected override string RunPart1()
            => CalculateMonkeyBusiness(20, true);

        protected override string RunPart2()
            => CalculateMonkeyBusiness(10000, false);

        private string CalculateMonkeyBusiness(int rounds, bool canGetBored)
        {
            uint itemWorryLevelRoof = 1;
            _monkeys.ForEach(x => itemWorryLevelRoof *= x.TestDivider);

            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in _monkeys)
                    monkey.InspectItems(itemWorryLevelRoof, canGetBored);
            }

            var topMonkeys = _monkeys.OrderByDescending(x => x.ItemsInspected).Take(2).ToArray();

            return (topMonkeys[0].ItemsInspected * topMonkeys[1].ItemsInspected).ToString();
        }
    }
}