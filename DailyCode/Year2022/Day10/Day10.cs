using DailyCode.Base;

namespace DailyCode.Year2022.Day10
{
    internal class Day10 : BaseDay
    {
        private readonly Cpu _cpu = new Cpu();

        public Day10(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInputs)
            => fileInputs.ForEach(_cpu.DoInstruction);

        protected override string RunPart1()
            => _cpu.GetSignalStrength(20, 60, 100, 140, 180, 220).ToString();

        protected override string RunPart2()
        {
            var crt = new Crt();
            crt.SetSpriteVisibility(_cpu.CycleValues);

            return crt.GetScreenOutput();
        }
    }
}