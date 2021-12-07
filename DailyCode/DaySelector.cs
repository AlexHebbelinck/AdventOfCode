using Common.Models;
using DailyCode.Base;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DailyCode
{
    public sealed class DaySelector
    {
        private readonly Regex _numberRgx = new(@"\d+");
        private readonly Regex _namespaceRgx = new(@"DailyCode\.Year\d{4}\.Days");

        private readonly List<(Type type, int year, uint day)> _dayCollection = new();

        public static DaySelector Instance { get; } = new();

        static DaySelector()
        {
        }

        private DaySelector()
        {
        }

        public void Initialize()
        {
            if (_dayCollection.Count > 0) return;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && typeof(BaseDay).IsAssignableFrom(x) && _namespaceRgx.IsMatch(x.Namespace ?? string.Empty)))
            {
                var year = int.Parse(_numberRgx.Match(type.Namespace ?? string.Empty).Value);
                var day = uint.Parse(_numberRgx.Match(type.Name).Value);
                _dayCollection.Add((type, year, day));
            }
        }

        public async Task<string?> Run(string sessionId, AdventConfig config)
        {
            if (config != null)
            {
                var (type, year, day) = _dayCollection.First(x => x.year.Equals(config.Year) && x.day.Equals(config.Day));

                if (Activator.CreateInstance(type, sessionId) is BaseDay classInstance)
                {
                    if (type.GetMethod(nameof(BaseDay.Run))?.Invoke(classInstance, new object[] { config }) is Task<string> result) return await result;
                }
            }

            return null;
        }
    }
}