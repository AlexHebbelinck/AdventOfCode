using Common.Models;
using DailyCode.Base;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DailyCode
{
    public sealed class DaySelector
    {
        private readonly Regex _numberRgx = new(@"\d+");
        private readonly List<(Type type, uint day)> _dayCollection = new();

        private AdventConfig? _config;

        public static DaySelector Instance { get; } = new();

        static DaySelector()
        {
        }

        private DaySelector()
        {
        }

        public void Initialize(AdventConfig config)
        {
            if (_config != null) return;

            _config = config;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && typeof(BaseDay).IsAssignableFrom(x) && x.Namespace == $"DailyCode.Year{config.Year}.Days"))
            {
                var day = uint.Parse(_numberRgx.Match(type.Name).Value);
                _dayCollection.Add((type, day));
            }
        }

        public async Task<string?> Run(string sessionId)
        {
            if (_config != null)
            {
                var (type, day) = _dayCollection.First(x => x.day.Equals(_config.Day));

                if (Activator.CreateInstance(type, sessionId) is BaseDay classInstance)
                {
                    if (type.GetMethod(nameof(BaseDay.Run))?.Invoke(classInstance, new object[] { _config }) is Task<string> result) return await result;
                }
            }

            return null;
        }
    }
}