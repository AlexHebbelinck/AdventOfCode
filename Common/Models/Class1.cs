using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Class1
    {
        public List<Class2> ActionList { get; set; } = new();
    }

    public class Class2
    {
        public Action<AdventConfig, string> Action { get; set; } = (_, __) => { };

        public string Value { get; set; } = string.Empty;
    }
}
