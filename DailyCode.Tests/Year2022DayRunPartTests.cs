using DailyCode.Tests.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace DailyCode.Tests
{
    public class Year2022DayRunPartTests : IClassFixture<DayRunPartFixture>
    {
        private readonly DayRunPartFixture _fixture;

        public Year2022DayRunPartTests(DayRunPartFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Day_RunPart_Expected_Result(uint day, uint part, string expectedResult)
        {
            var result = _fixture.RunPart(2022, day, part);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            return new List<object[]>
            {
                new object[] { 1, 1, 69289 },
                new object[] { 1, 2, 205615 },
                new object[] { 2, 1, 13268 },
                new object[] { 2, 2, 15508 },
                new object[] { 3, 1, 8202 },
                new object[] { 3, 2, 2864 }
            };
        }
    }
}