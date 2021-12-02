using Common.Model;
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

        public async Task<AdventConfig> GetAdventConfig(int? overrideYear, uint? overrideDay, uint? overridePart, bool? useTestData)
        {
            return !File.Exists(_filePath)
                ? await GetNewConfig(overrideYear, overrideDay, overridePart, useTestData)
                : await GetExistingConfig(overrideYear, overrideDay, overridePart, useTestData);
        }

        private AdventConfig SetupConfigModel(int? overrideYear, uint? overrideDay, uint? overridePart, bool? useTestData)
            => new() { Year = overrideYear ?? _year, Day = overrideDay ?? 1, Part = overridePart ?? 1, UseTestData = useTestData ?? false };

        private async Task<AdventConfig> GetNewConfig(int? overrideYear, uint? overrideDay, uint? overridePart, bool? useTestData)
        {
            var config = SetupConfigModel(overrideYear, overrideDay, overridePart, useTestData);
            using var fs = File.Create(_filePath);
            fs.Close();

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private async Task<AdventConfig> GetExistingConfig(int? overrideYear, uint? overrideDay, uint? overridePart, bool? useTestData)
        {
            using var fs = File.OpenRead(_filePath);
            var config = await JsonSerializer.DeserializeAsync<AdventConfig>(fs) ?? SetupConfigModel(overrideYear, overrideDay, overridePart, useTestData);
            fs.Close();

            config = UpdateConfigModel(config, overrideYear, overrideDay, overridePart, useTestData);

            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(config));

            return config;
        }

        private AdventConfig UpdateConfigModel(AdventConfig config, int? overrideYear, uint? overrideDay, uint? overridePart, bool? useTestData)
        {
            config.Day = overrideDay ?? config.Day;
            config.Part = overridePart ?? config.Part;
            config.Year = overrideYear ?? _year;
            config.UseTestData = useTestData ?? config.UseTestData;

            return config;
        }
    }
}