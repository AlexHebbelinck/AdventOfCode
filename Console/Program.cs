using Common.Helpers;
using ConsoleApp;
using DailyCode;
using Microsoft.Extensions.Configuration;

var startupCfg = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();

while (true)
{
    Console.Write("Commands: ");
    var class1 = CommandPromptHandler.Instance.DoSomething(Console.ReadLine());
    var config = await AdventConfigHelper.Instance.GetAdventConfig(class1);

    DaySelector.Instance.Initialize(config, startupCfg.GetSection("sessionId").Value);

    Console.Write("Output: ");
    Console.WriteLine(await DaySelector.Instance.Run());

    Console.Write("\n \n");
}

