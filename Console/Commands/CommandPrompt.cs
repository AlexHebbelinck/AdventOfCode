using Common.Models;
using System.Text.RegularExpressions;

namespace ConsoleApp.Commands
{
    public sealed class ActionCommandPrompt
    {
        public static ActionCommandPrompt Clear { get; } = new("cls", Console.Clear);

        public static ActionCommandPrompt Exit { get; } = new("exit", () => Environment.Exit(0));
        private ActionCommandPrompt(string command, Action action)
        {
            Command = command;
            Action = action;
        }

        public string Command { get; set; }
        public Action Action { get; set; }

        private static List<ActionCommandPrompt> CommandPrompts => new() { Clear, Exit };

        //public static void Execute(string command) => FindBy(command).

        public static ActionCommandPrompt FindBy(string command) => CommandPrompts.First(x => x.Command.Equals(command, StringComparison.InvariantCultureIgnoreCase));
    }

    public sealed class FuncCommandPrompt
    {
        //public static FuncCommandPrompt PartSelector { get; } = new("p", Console.Clear, typeof(int?));
        //public static FuncCommandPrompt DaySelector { get; } = new("d", Console.Clear, typeof(int?));
        //public static FuncCommandPrompt TestDataSelector { get; } = new("t", Console.Clear, typeof(int?));
        //public static FuncCommandPrompt YearSelector { get; } = new("y", cmd => Regex.IsMatch(cmd, @"y *?(\d+)") ? Regex.Match(cmd, @"-y *?(\d+)").Groups[1].Value : null, typeof(int?));
        public static FuncCommandPrompt PartSelector { get; } = new("p", (config, value) => config.Part = uint.Parse(value));
        public static FuncCommandPrompt DaySelector { get; } = new("d", (config, value) => config.Day = uint.Parse(value));
        public static FuncCommandPrompt TestDataSelector { get; } = new("t", (config, value) => config.UseTestData = Convert.ToBoolean(int.Parse(value)));
        public static FuncCommandPrompt YearSelector { get; } = new("y", (config, value) => config.Year = int.Parse(value));

        private FuncCommandPrompt(string command, Action<AdventConfig, string> action)
        {
            Command = command;
            Action = action;
          //  Func = func;
            //CastTo = castTo;
        }

        public string Command { get; set; }
        public Action<AdventConfig, string> Action { get; set; }
        // public Func<string, string?> Func { get; set; }
        //public Type CastTo { get; set; }

        private static List<FuncCommandPrompt> CommandPrompts => new() { PartSelector, DaySelector, TestDataSelector, YearSelector };

        public static FuncCommandPrompt FindBy(string command) => CommandPrompts.First(x => x.Command.Equals(command, StringComparison.InvariantCultureIgnoreCase));

        //public void Test()
        //{
        //    Convert.ChangeType(CommandPrompt.YearSelector.Func(""), CommandPrompt.YearSelector.CastTo);
        //  var c =  CommandPrompt.YearSelector.CastTo
        //        //;
        //}
    }

}