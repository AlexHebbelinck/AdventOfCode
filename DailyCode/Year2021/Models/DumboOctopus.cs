namespace DailyCode.Year2021.Models
{
    public class DumboOctopus
    {
        public DumboOctopus(int energyLevel)
        {
            EnergyLevel = energyLevel;
        }

        public int EnergyLevel { get; set; }

        public List<DumboOctopus> AdjacentDumboOctopi { get; set; } = new();

        public int TotalFlashes { get; private set; }

        public int FlashedOn { get; private set; }

        public void IncreaseEnergyLevel(int step)
        {
            if(FlashedOn != step)
            {
                ++EnergyLevel;

                if (EnergyLevel > 9)
                    Flash(step);
            }
        }

        private void Flash(int step)
        {
            FlashedOn = step;
            ++TotalFlashes;
            EnergyLevel = 0;
            AdjacentDumboOctopi.ForEach(x => x.IncreaseEnergyLevel(step));
        }
    }
}