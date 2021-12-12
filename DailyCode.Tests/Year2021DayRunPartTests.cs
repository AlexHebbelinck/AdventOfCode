using DailyCode.Tests.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace DailyCode.Tests
{
    public class Year2021DayRunPartTests : IClassFixture<DayRunPartFixture>
    {
        private readonly DayRunPartFixture _fixture;

        public Year2021DayRunPartTests(DayRunPartFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Day_RunPart_Expected_Result(uint day, uint part, long expectedResult)
        {
            var result = _fixture.RunPart(2021, day, part);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            return new List<object[]>
            {
                new object[] { 1, 1, 1266 },
                new object[] { 1, 2, 1217 },
                new object[] { 2, 1, 1480518 },
                new object[] { 2, 2, 1282809906 },
                new object[] { 3, 1, 3958484 },
                new object[] { 3, 2, 1613181 },
                new object[] { 4, 1, 44736 },
                new object[] { 4, 2, 1827 },
                new object[] { 5, 1, 4826 },
                new object[] { 5, 2, 16793 },
                new object[] { 6, 1, 351092 },
                new object[] { 6, 2, 1595330616005 },
                new object[] { 7, 1, 344605 },
                new object[] { 7, 2, 93699985 },
                new object[] { 8, 1, 284 },
                new object[] { 8, 2, 973499 },
                new object[] { 9, 1, 452 },
                new object[] { 9, 2, 1263735 },
                new object[] { 10, 1, 469755 },
                new object[] { 10, 2, 2762335572 },
                new object[] { 11, 1, 1625 },
                new object[] { 11, 2, 244 },
                new object[] { 12, 1, 4186 },
                new object[] { 12, 2, 92111 },
            };
        }
    }
}