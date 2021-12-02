using DailyCode;
using Common.Helpers;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

var startupCfg = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddUserSecrets<Program>()
			.Build();

Console.WriteLine("Commands:");
var commands = Console.ReadLine();

int? year = null;
uint? day = null;
uint? part = null;
bool? useTestData = null;

if (!string.IsNullOrWhiteSpace(commands))
{
	year = Regex.IsMatch(commands, @"-y *?(\d+)") ? int.Parse(Regex.Match(commands, @"-y *?(\d+)").Groups[1].Value) : (int?)null;
	day = Regex.IsMatch(commands, @"-d *?(\d+)") ? uint.Parse(Regex.Match(commands, @"-d *?(\d+)").Groups[1].Value) : (uint?)null;
	part = Regex.IsMatch(commands, @"-p *?(\d+)") ? uint.Parse(Regex.Match(commands, @"-p *?(\d+)").Groups[1].Value) : (uint?)null;
	useTestData = Regex.IsMatch(commands, @"-t *?(\d+)") ? Convert.ToBoolean(int.Parse(Regex.Match(commands, @"-t *?(\d+)").Groups[1].Value)) : (bool?)null;
}

var config = await AdventConfigHelper.Instance.GetAdventConfig(year,day,part, useTestData);

DaySelector.Instance.Initialize(config, startupCfg.GetSection("sessionId").Value);

Console.WriteLine("Output:");
Console.WriteLine(await DaySelector.Instance.Run());

