using Common.Models;

namespace ConsoleApp.Commands
{
    public sealed class CommandPrompt
    {
        public static CommandPrompt Clear { get; } = new("cls", Console.Clear);
        public static CommandPrompt Exit { get; } = new("exit", () => Environment.Exit(0));
        public static CommandPrompt PartSelector { get; } = new("p", (config, value) => config.Part = uint.Parse(value));
        public static CommandPrompt DaySelector { get; } = new("d", (config, value) => config.Day = uint.Parse(value));
        public static CommandPrompt TestDataSelector { get; } = new("t", (config, value) => config.UseTestData = Convert.ToBoolean(int.Parse(value)));
        public static CommandPrompt YearSelector { get; } = new("y", (config, value) => config.Year = int.Parse(value));

        private CommandPrompt(string command, Action action)
        {
            Command = command;
            ExecuteAction = action;
            IsExecuteAction = true;
        }

        private CommandPrompt(string command, Action<AdventConfig, string> action)
        {
            Command = command;
            AdventConfigAction = action;
        }

        public string Command { get; set; }
        public Action ExecuteAction { get; set; } = () => { };
        public Action<AdventConfig, string> AdventConfigAction { get; set; } = (_, __) => { };
        public bool IsExecuteAction { get; set; }

        private static List<CommandPrompt> CommandPrompts => new() { Clear, Exit, PartSelector, DaySelector, TestDataSelector, YearSelector };

        public static CommandPrompt FindBy(string command) => CommandPrompts.First(x => x.Command.Equals(command, StringComparison.InvariantCultureIgnoreCase));
    }
}