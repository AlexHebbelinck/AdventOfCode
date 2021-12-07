using Common.Helpers;
using ConsoleApp.Commands;
using DailyCode;
using Microsoft.Extensions.Configuration;

var startupCfg = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();

DaySelector.Instance.Initialize();

while (true)
{
    Console.Write("Commands: ");
    var commandAdventConfigActions = CommandPromptHandler.Instance.GetCommands(Console.ReadLine());
    var config = await AdventConfigHelper.Instance.GetAdventConfig(commandAdventConfigActions);

    Console.Write("Output: ");
    Console.WriteLine(await DaySelector.Instance.Run(startupCfg.GetSection("sessionId").Value, config));

    Console.Write("\n \n");
}