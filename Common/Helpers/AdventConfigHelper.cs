using Common.Models;
using System.Text.Json;

namespace Common.Helpers
{
    public sealed class AdventConfigHelper
    {
        public static AdventConfigHelper Instance { get; } = new();

        private readonly string _filePath = @"..\..\..\..\Assets\config.cfg";
        private readonly int _year = YearHelper.Instance.GetAoCYear();

        static AdventConfigHelper()
        {
        }

        private AdventConfigHelper()
        {
        }

        public async Task<AdventConfig> GetAdventConfig(List<CommandAdventConfigAction> CommandAdventConfigAction)
        {
            return !File.Exists(_filePath)
                ? await GetNewConfig(CommandAdventConfigAction)
                : await GetExistingConfig(CommandAdventConfigAction);
        }

        private AdventConfig SetupConfigModel(List<CommandAdventConfigAction> CommandAdventConfigAction)
            => UpdateConfigModel(new() { Year =  _year, Day = 1, Part =  1, UseTestData = false }, CommandAdventConfigAction);

        private async Task<AdventConfig> GetNewConfig(List<CommandAdventConfigAction> CommandAdventConfigAction)
        {
            var config = SetupConfigModel(CommandAdventConfigAction);
            using var fs = File.Create(_filePath);
            fs.Close();

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private async Task<AdventConfig> GetExistingConfig(List<CommandAdventConfigAction> CommandAdventConfigAction)
        {
            using var fs = File.OpenRead(_filePath);
            var config = await JsonSerializer.DeserializeAsync<AdventConfig>(fs) ?? SetupConfigModel(CommandAdventConfigAction);
            fs.Close();

            config = UpdateConfigModel(config, CommandAdventConfigAction);

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private AdventConfig UpdateConfigModel(AdventConfig config, List<CommandAdventConfigAction> commandAdventConfigAction)
        {
            commandAdventConfigAction.ForEach(x => x.Action(config, x.Value));
            return config;
        }
    }
}