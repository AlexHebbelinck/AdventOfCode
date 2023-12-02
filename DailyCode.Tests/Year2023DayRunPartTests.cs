﻿using DailyCode.Tests.Fixtures;
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
                new object[] { 1, 2, 54925 }
            };
        }
    }
}