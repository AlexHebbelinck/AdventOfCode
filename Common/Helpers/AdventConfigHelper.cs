using Common.Models;
using System.Text.Json;

namespace Common.Helpers
{
    public sealed class AdventConfigHelper
    {
        public static AdventConfigHelper Instance { get; } = new();

        private readonly string _filePath = @"F:\Documents\AdventOfCode\config.cfg";
        private readonly int _year = YearHelper.Instance.GetAoCYear();

        static AdventConfigHelper()
        {
        }

        private AdventConfigHelper()
        {
        }

        public async Task<AdventConfig> GetAdventConfig(Class1 class1)
        {
            return !File.Exists(_filePath)
                ? await GetNewConfig(class1)
                : await GetExistingConfig(class1);
        }

        private AdventConfig SetupConfigModel(Class1 class1)
            => UpdateConfigModel(new() { Year =  _year, Day = 1, Part =  1, UseTestData = false }, class1);

        private async Task<AdventConfig> GetNewConfig(Class1 class1)
        {
            var config = SetupConfigModel(class1);
            using var fs = File.Create(_filePath);
            fs.Close();

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private async Task<AdventConfig> GetExistingConfig(Class1 class1)
        {
            using var fs = File.OpenRead(_filePath);
            var config = await JsonSerializer.DeserializeAsync<AdventConfig>(fs) ?? SetupConfigModel(class1);
            fs.Close();

            config = UpdateConfigModel(config, class1);

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private AdventConfig UpdateConfigModel(AdventConfig config, Class1 class1)
        {
            config.Year = _year;

            foreach(var action in class1.ActionList)
            {
                action.Action(config, action.Value);
            }

            return config;
        }
    }
}