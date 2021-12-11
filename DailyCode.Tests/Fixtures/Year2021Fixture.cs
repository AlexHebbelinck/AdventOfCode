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
    public class Year2021Fixture
    {
        private readonly Regex numberRgx = new(@"\d+");
        public List<FixtureModel> Models { get; set; } = new();

        public Year2021Fixture()
        {
            foreach (var type in Assembly.Load("DailyCode").GetTypes().Where(x => x.IsClass && typeof(BaseDay).IsAssignableFrom(x) && x.Namespace?.Equals("DailyCode.Year2021.Days", StringComparison.InvariantCultureIgnoreCase) == true))
            {
                if (Activator.CreateInstance(type, string.Empty) is BaseDay classInstance)
                {
                    var runPart1 = type.GetMethod("RunPart1", BindingFlags.Instance | BindingFlags.NonPublic);
                    var runPart2 = type.GetMethod("RunPart2", BindingFlags.Instance | BindingFlags.NonPublic);

                    Models.Add(new FixtureModel
                    {
                        Day = uint.Parse(numberRgx.Match(type.Name).Value),
                        Type = type,
                        RunPart1 = runPart1,
                        RunPart2 = runPart2
                    });
                }
            }
        }

        public long RunPart(uint day, uint part)
        {
            var model = Models.Find(x => x.Day == day);
            if (model == null || model.Type == null) throw new ArgumentException("Day doesn't exist");

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