using Common.Models;
using System.Text.RegularExpressions;

namespace ConsoleApp.Commands
{
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

        public List<CommandAdventConfigAction> DoSomething(string? commands)
        {
            var commandAdventConfigActions = new List<CommandAdventConfigAction>();
            if (!string.IsNullOrWhiteSpace(commands))
            {
                foreach (Match match in AllCommandsRgx.Matches(commands))
                {
                    var commandPrompt = CommandPrompt.FindBy(match.Groups[2].Value);
                    if (commandPrompt.IsExecuteAction)
                    {
                        commandPrompt.ExecuteAction();
                    }
                    else if (match.Groups[3] != null)
                    {
                        commandAdventConfigActions.Add(new CommandAdventConfigAction { Action = commandPrompt.AdventConfigAction, Value = match.Groups[3].Value });
                    }
                }
            }

            return commandAdventConfigActions;
        }
    }
}