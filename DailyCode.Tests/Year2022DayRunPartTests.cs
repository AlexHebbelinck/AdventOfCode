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
                new object[] { 3, 2, 2864 },
                new object[] { 4, 1, 305 },
                new object[] { 4, 2, 811 },
                new object[] { 5, 1, "TWSGQHNHL" },
                new object[] { 5, 2, "JNRSCDWPP" },
                new object[] { 6, 1, 1361 },
                new object[] { 6, 2, 3263 },
                new object[] { 7, 1, 1182909 },
                new object[] { 7, 2, 2832508 },
                new object[] { 8, 1, 1733 },
                new object[] { 8, 2, 284648 },
                new object[] { 9, 1, 6494 },
                new object[] { 9, 2, 2691 }
            };
        }
    }
}