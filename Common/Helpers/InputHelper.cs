using Common.Models;
using System.Net;
using System.Net.Http.Headers;

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
            //Move to user secrets
            const string? fileLocation = @"F:\Documents\AdventOfCode\TestData.txt";
            return (await File.ReadAllTextAsync(fileLocation))
                .Split(new[] { '\n' })
                .ToList();
        }

        public async Task<List<string>> GetInputData(string filename, string sessionId, AdventConfig config)
        {
            //Move to user secrets
            var fileLocation = @$"F:\Documents\AdventOfCode\{config.Year}\{filename}.txt";

            if (!File.Exists(fileLocation))
                await DownloadInput(fileLocation, sessionId, config);

            var input = (await File.ReadAllTextAsync(fileLocation))
                .Split(new[] { '\n' })
                .ToList();

            //Last entry is always empty...
            if(string.IsNullOrWhiteSpace(input.Last()))
                input.RemoveAt(input.Count - 1);

            return input;
        }

        private async Task DownloadInput(string fileLocation, string sessionId, AdventConfig config)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{config.Year}/day/{config.Day}/input");

            var fileInfo = new FileInfo(fileLocation);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(request.RequestUri!, new Cookie("session", sessionId));
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(".NET 7.0 (IceCow@gmail.com)");

            var resp = await client.SendAsync(request);
            using var stream = await resp.Content.ReadAsStreamAsync();

            using var fileStream = fileInfo.OpenWrite();
            await stream.CopyToAsync(fileStream);
        }
    }
}