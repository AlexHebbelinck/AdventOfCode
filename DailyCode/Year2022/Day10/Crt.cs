namespace DailyCode.Year2022.Day10
{
    public class Crt
    {
        private const int MAX_Y = 6;
        private const int MAX_X = 40;
        public char[,] Screen { get; } = new char[MAX_Y, MAX_X];

        public void SetSpriteVisibility(List<CycleValue> cycleValues)
        {
            var crtPosition = 0;
            foreach (var mo in cycleValues)
            {
                if (mo.Cycle == 241)
                    break;

                if (crtPosition > 39)
                    crtPosition = 0;

                Screen[(mo.Cycle - 1) / 40, crtPosition] = mo.Value + 1 >= crtPosition && mo.Value - 1 <= crtPosition ? '#' : '.';

                crtPosition++;
            }
        }

        public string GetScreenOutput()
        {
            var output = string.Empty;
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    output += Screen[y, x];
                }

                output += Environment.NewLine;
            }

            return output;
        }
    }
}