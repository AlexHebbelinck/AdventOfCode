using Common.Helpers;
using ConsoleApp.Commands;
using DailyCode;
using Microsoft.Extensions.Configuration;

var startupCfg = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();

while (true)
{
    Console.Write("Commands: ");
    var commandAdventConfigActions = CommandPromptHandler.Instance.GetCommands(Console.ReadLine());
    var config = await AdventConfigHelper.Instance.GetAdventConfig(commandAdventConfigActions);

    DaySelector.Instance.Initialize(config, startupCfg.GetSection("sessionId").Value);

    Console.Write("Output: ");
    Console.WriteLine(await DaySelector.Instance.Run());

    Console.Write("\n \n");
}