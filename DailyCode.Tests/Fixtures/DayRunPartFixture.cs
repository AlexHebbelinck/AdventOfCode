using Common.Helpers;
using Common.Models;
using DailyCode.Base;
using DailyCode.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DailyCode.Tests.Fixtures
{
    public class DayRunPartFixture
    {
        private readonly Regex _numberRgx = new(@"\d+");
        private readonly Regex _namespaceRgx = new(@"DailyCode\.Year\d{4}\.Days");

        public List<DailyCodeMethods> DailyCodeMethods { get; set; } = new();

        public DayRunPartFixture()
        {
            foreach (var type in Assembly.Load("DailyCode").GetTypes().Where(x => x.IsClass && typeof(BaseDay).IsAssignableFrom(x) && _namespaceRgx.IsMatch(x.Namespace ?? string.Empty)))
            {
                if (Activator.CreateInstance(type, string.Empty) is BaseDay classInstance)
                {
                    var runPart1 = type.GetMethod("RunPart1", BindingFlags.Instance | BindingFlags.NonPublic);
                    var runPart2 = type.GetMethod("RunPart2", BindingFlags.Instance | BindingFlags.NonPublic);

                    DailyCodeMethods.Add(new DailyCodeMethods
                    {
                        Year =  int.Parse(_numberRgx.Match(type.Namespace ?? string.Empty).Value),
                        Day = uint.Parse(_numberRgx.Match(type.Name).Value),
                        Type = type,
                        RunPart1 = runPart1,
                        RunPart2 = runPart2
                    });
                }
            }
        }

        public long RunPart(int year, uint day, uint part)
        {
            var model = DailyCodeMethods.Find(x => x.Year == year && x.Day == day);
            if (model == null || model.Type == null) throw new ArgumentException("Day doesn't exist for chosen year.");

            if (Activator.CreateInstance(model.Type, string.Empty) is BaseDay classInstance)
            {
                var input = InputHelper.Instance.GetInputData(model.Type.Name, string.Empty, new AdventConfig()).Result;

                var extractDataMethod = model.Type.GetMethod("ExtractData", BindingFlags.Instance | BindingFlags.NonPublic);
                extractDataMethod?.Invoke(classInstance, new object[] { input });

                return part switch
                {
                    1 => model.RunPart1?.Invoke(classInstance, null) as long? ?? 0,
                    2 => model.RunPart2?.Invoke(classInstance, null) as long? ?? 0,
                    _ => throw new Exception("Part doesn't exist"),
                };
            }

            return 0;
        }
    }
}