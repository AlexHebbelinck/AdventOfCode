using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCode.Year2022.Models
{
    internal class Elf
    {
        public List<int> Calories { get; init; } = new();

        public int Total => Calories.Sum();
    }
}
