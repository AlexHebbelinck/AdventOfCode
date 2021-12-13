using DailyCode.Base;

namespace DailyCode.Year2021.Days
{
    public class Day02 : BaseDay
    {
        private List<(string command, int units)> _fileInput = new();

        public Day02(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            _fileInput = fileInput.ConvertAll(x =>
                {
                    var splitted = x.Split(' ');
                    return (splitted[0].Trim(), int.Parse(splitted[1].Trim()));
                });
        }

        protected override string RunPart1()
        {
            var xPos = 0;
            var yPos = 0;

            foreach (var (command, units) in _fileInput)
            {
                switch (command)
                {
                    case "forward":
                        xPos += units;
                        break;
                    case "down":
                        yPos += units;
                        break;
                    case "up":
                        yPos -= units;
                        break;
                    default:
                        throw new Exception("Blub blub blub.....");
                }
            }

            return (yPos * xPos).ToString();
        }

        protected override string RunPart2()
        {
            (int xPos, int yPos, int aim) = (0, 0, 0);

            foreach (var (command, units) in _fileInput)
            {
                switch (command)
                {
                    case "forward":
                        xPos += units;
                        yPos += (aim * units);
                        break;
                    case "down":
                        aim += units;
                        break;
                    case "up":
                        aim -= units;
                        break;
                    default:
                        throw new Exception("Blub blub blub.....");
                }
            }

            return (yPos * xPos).ToString();
        }
    }
}