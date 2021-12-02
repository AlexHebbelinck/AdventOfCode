using System.Text.Json.Serialization;

namespace Common.Model
{
    public class AdventConfig
    {
        public uint Day { get; set; }
        public uint Part { get; set; }

        public bool UseTestData { get; set; }

        [JsonIgnore]
        public int Year { get; set; }
    }
}