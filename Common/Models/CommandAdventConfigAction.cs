namespace Common.Models
{
    public class CommandAdventConfigAction
    {
        public Action<AdventConfig, string> Action { get; set; } = (_, __) => { };

        public string Value { get; set; } = string.Empty;
    }
}