using Common.Model;
using ConsoleApp.Commands;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    //TODO: WIP
    public sealed class CommandPromptHandler
    {
        public static CommandPromptHandler Instance { get; } = new();

        private readonly Regex AllCommandsRgx = new(@"(- *?([a-zA-Z]+) *(\d*))");

        static CommandPromptHandler()
        {
        }

        private CommandPromptHandler()
        {
        }

        public Class1 DoSomething(string? commands)
        {
            var b = new Class1();
            if (!string.IsNullOrWhiteSpace(commands))
            {
                var matches = AllCommandsRgx.Matches(commands);
                foreach (Match match in matches)
                {
                    if (string.IsNullOrWhiteSpace(match.Groups[3].Value))
                    {
                        ActionCommandPrompt.FindBy(match.Groups[2].Value).Action();
                    }
                    else
                    {
                        var cmdPrompt = FuncCommandPrompt.FindBy(match.Groups[2].Value);
                        b.ActionList.Add(new Class2 { Action = cmdPrompt.Action, Value = match.Groups[3].Value });
                    }
                }
            }

            return b;
        }
    }
}