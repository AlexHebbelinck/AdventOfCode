using Common.Helpers;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class AdventConfig
    {
        public uint Day { get; set; }
        public uint Part { get; set; }

        public bool UseTestData { get; set; }

        [JsonIgnore]
        public int Year { get; set; } = YearHelper.Instance.GetAoCYear();
    }
}