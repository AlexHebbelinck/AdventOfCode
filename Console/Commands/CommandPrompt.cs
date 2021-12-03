using System.Text.RegularExpressions;

namespace ConsoleApp.Commands
{
    public sealed class ActionCommandPrompt
    {
        public static ActionCommandPrompt Clear { get; } = new("cls", Console.Clear);
        private ActionCommandPrompt(string command, Action action)
        {
            Command = command;
            Action = action;
        }

        public string Command { get; set; }
        public Action Action { get; set; }

        private static List<ActionCommandPrompt> CommandPrompts => new() { Clear };

        //public static void Execute(string command) => FindBy(command).

        public static ActionCommandPrompt FindBy(string command) => CommandPrompts.First(x => x.Command.Equals(command, StringComparison.InvariantCultureIgnoreCase));
    }

    public sealed class FuncCommandPrompt
    {
        //public static FuncCommandPrompt PartSelector { get; } = new("p", Console.Clear, typeof(int?));
        //public static FuncCommandPrompt DaySelector { get; } = new("d", Console.Clear, typeof(int?));
        //public static FuncCommandPrompt TestDataSelector { get; } = new("t", Console.Clear, typeof(int?));
        public static FuncCommandPrompt YearSelector { get; } = new("y", cmd => Regex.IsMatch(cmd, @"y *?(\d+)") ? Regex.Match(cmd, @"-y *?(\d+)").Groups[1].Value : null, typeof(int?));

        private FuncCommandPrompt(string command, Func<string, string?> func, Type castTo)
        {
            Command = command;
            Func = func;
            CastTo = castTo;
        }

        public string Command { get; set; }
        public Func<string, string?> Func { get; set; }
        public Type CastTo { get; set; }

        private static List<FuncCommandPrompt> CommandPrompts => new() { YearSelector };

        public static FuncCommandPrompt FindBy(string command) => CommandPrompts.First(x => x.Command.Equals(command, StringComparison.InvariantCultureIgnoreCase));

        //public void Test()
        //{
        //    Convert.ChangeType(CommandPrompt.YearSelector.Func(""), CommandPrompt.YearSelector.CastTo);
        //  var c =  CommandPrompt.YearSelector.CastTo
        //        //;
        //}
    }

}