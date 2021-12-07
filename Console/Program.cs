using Common.Helpers;
using ConsoleApp.Commands;
using DailyCode;
using Microsoft.Extensions.Configuration;

var startupCfg = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();

//while (true)
//{
    Console.Write("Commands: ");
    var commandAdventConfigActions = CommandPromptHandler.Instance.GetCommands(Console.ReadLine());
    var config = await AdventConfigHelper.Instance.GetAdventConfig(commandAdventConfigActions);

    DaySelector.Instance.Initialize(config);

    Console.Write("Output: ");
    Console.WriteLine(await DaySelector.Instance.Run(startupCfg.GetSection("sessionId").Value));

    Console.Write("\n \n");

    //Loop is still broken.... I swear I'll get it fixed one day....
    Console.ReadLine();
//}