namespace DailyCode.Year2022.Days.Day10
{
    public class Cpu
    {
        public int Cycle { get; private set; } = 1;

        public int Register { get; private set; } = 1;

        public List<CycleValue> CycleValues = new() { new CycleValue(1, 1) };

        public void DoInstruction(string command)
        {
            CycleValues.Add(new CycleValue(++Cycle, Register));

            if (command.StartsWith("addx"))
                CycleValues.Add(new CycleValue(++Cycle, Register += Convert.ToInt32(command.Split(' ')[1])));
        }

        public int GetSignalStrength(params int[] cycles)
        {
            var result = 0;
            foreach (var cycle in cycles)
                result += GetSignalStrength(cycle);

            return result;
        }

        public int GetSignalStrength(int cycle)
            => CycleValues.First(x => x.Cycle == cycle).SignalStrength;
    }
}