namespace Common.Helpers
{
    public static class MathHelper
    {
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Calculate Least Common Multiple of two numbers
        /// </summary>
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        public static long LCM(params long[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentException("At least two numbers are required to find LCM.");
            }

            long lcm = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                lcm = LCM(lcm, numbers[i]);
            }
            return lcm;
        }

        public static long Extrapolate(long[] sequence)
        {
            List<List<long>> sequences = [[.. sequence]];

            do
            {
                var sequenceToCheck = sequences[^1];
                var differences = new long[sequenceToCheck.Count - 1];
                for (int i = 1; i < sequenceToCheck.Count; i++)
                {
                    differences[i - 1] = sequenceToCheck[i] - sequenceToCheck[i - 1];
                }

                sequences.Add([.. differences]);
            }
            while (!sequences[^1].TrueForAll(x => x == 0));


            for(int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i][^1] + sequences[i + 1][^1]);
            }

            return sequences[0][^1];
        }
    }
}