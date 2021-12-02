using Common.Model;
using System.Net;

namespace Common.Helpers
{
    public sealed class InputHelper
    {
        public static InputHelper Instance { get; } = new();

        static InputHelper()
        {
        }

        private InputHelper()
        {
        }

        public async Task<List<string>> GetTestData()
        {
            const string? fileLocation = @"F:\Documents\AdventOfCode\TestData.txt";
            return (await File.ReadAllTextAsync(fileLocation))
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToList();
        }

        public async Task<List<string>> GetInputData(string filename, string sessionId, AdventConfig config)
        {
            var fileLocation = @$"F:\Documents\AdventOfCode\{config.Year}\{filename}.txt";

            if (!File.Exists(fileLocation))
                await DownloadInput(fileLocation, sessionId, config);

            return (await File.ReadAllTextAsync(fileLocation))
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToList();
        }

        private async Task DownloadInput(string fileLocation, string sessionId, AdventConfig config)
        {
            var baseAddress = new Uri($"https://adventofcode.com/{config.Year}/day/{config.Day}/input");
            var fileInfo = new FileInfo(fileLocation);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("session", sessionId));
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler);

            var resp = await client.GetAsync(baseAddress);
            using var stream = await resp.Content.ReadAsStreamAsync();

            using var fileStream = fileInfo.OpenWrite();
            await stream.CopyToAsync(fileStream);
        }
    }
}