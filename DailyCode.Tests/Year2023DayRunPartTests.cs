using DailyCode.Tests.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace DailyCode.Tests
{
    public class Year2023DayRunPartTests(DayRunPartFixture fixture) : IClassFixture<DayRunPartFixture>
    {
        private readonly DayRunPartFixture _fixture = fixture;

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Day_RunPart_Expected_Result(uint day, uint part, string expectedResult)
        {
            var result = _fixture.RunPart(2023, day, part);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            return new List<object[]>
            {
                new object[] { 1, 1, 55172 },
                new object[] { 1, 2, 54925 },
                new object[] { 2, 1, 2416 },
                new object[] { 2, 2, 63307 },
                new object[] { 3, 1, 527369 },
                new object[] { 3, 2, 73074886 },
                new object[] { 4, 1, 33950 },
                new object[] { 4, 2, 14814534 },
                new object[] { 5, 1, 579439039 },
                new object[] { 5, 2, 7873084 },
                new object[] { 6, 1, 2344708 },
                new object[] { 6, 2, 30125202 },
                new object[] { 7, 1, 246163188 },
                new object[] { 7, 2, 245794069 },
                new object[] { 8, 1, 12083 },
                new object[] { 8, 2, 13385272668829 }
            };
        }
    }
}