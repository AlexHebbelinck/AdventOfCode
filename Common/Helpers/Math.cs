
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
    }
}
