using Common.Model;
using DailyCode.Base;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DailyCode
{
    public sealed class DaySelector
    {
        private readonly Regex _numberRgx = new(@"\d+");
        private readonly List<(Type type, BaseDay classInstance, uint value)> _dayCollection = new();

        private AdventConfig? _config;

        public static DaySelector Instance { get; } = new();

        static DaySelector()
        {
        }

        private DaySelector()
        {
        }

        public void Initialize(AdventConfig config, string sessionId)
        {
            if (_config != null) throw new NotSupportedException("Already initialized");

            _config = config;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && typeof(BaseDay).IsAssignableFrom(x) && x.Namespace == $"DailyCode.Year{config.Year}.Days"))
            {
                if (Activator.CreateInstance(type, sessionId) is BaseDay classInstance)
                {
                    var numberValue = uint.Parse(_numberRgx.Match(type.Name).Value);
                    _dayCollection.Add((type, classInstance, numberValue));
                }
            }
        }

        public async Task<string?> Run()
        {
            if (_config != null)
            {
                var (type, classInstance, value) = _dayCollection.First(x => x.value.Equals(_config.Day));
                if (type.GetMethod(nameof(BaseDay.Run))?.Invoke(classInstance, new object[] { _config }) is Task<string> result) return await result;
            }

            return null;
        }
    }
}