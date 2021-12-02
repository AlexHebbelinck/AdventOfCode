using System.Text.RegularExpressions;

namespace ConsoleApp.Commands
{
    public sealed class CommandPrompt
    {
        public static CommandPrompt Clear { get; } = new("-cls", Console.Clear);
        public static CommandPrompt PartSelector { get; } = new("-p", Console.Clear);
        public static CommandPrompt DaySelector { get; } = new("-d", Console.Clear);
        public static CommandPrompt TestDataSelector { get; } = new("-t", Console.Clear);
      //  public static CommandPrompt YearSelector { get; } = new("-y", cmd => Regex.IsMatch(cmd, @"-y *?(\d+)") ? int.Parse(Regex.Match(cmd, @"-y *?(\d+)").Groups[1].Value) : (int?)null);

        private CommandPrompt(string command, Action action)
        {
            Command = command;
            Action = action;
        }

        private CommandPrompt(string command, Func<string, string> func)
        {
            Command = command;
            Func = func;
        }

        private string Command { get; set; }
        public Action? Action { get; set; }
        public Func<string, string>? Func { get; set; }
    }
}