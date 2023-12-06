namespace DailyCode.Year2023.Day06
{
    public static class Race
    {
        public static int GetTotalOptionsToBeatRecord(ToyBoat boat, RaceData raceData)
            => CalculateTotalDistances(boat, raceData).Count(x => x > raceData.RecordDistance);

        public static List<long> CalculateTotalDistances(ToyBoat boat, RaceData raceData)
        {
            var results = new List<long>();
            for (int i = 0; i < raceData.Time; i++)
            {
                results.Add((boat.StartSpeed + (boat.SpeedIncrease * (i / boat.ChargeTick))) * (raceData.Time - i));
            }

            return results;
        }
    }
}