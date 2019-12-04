using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var matchCount = 0;

            for (int testNumber = 236491; testNumber <= 713787; testNumber++)
            {
                matchCount += MatchesRules(testNumber) ? 1 : 0;
            }

            Console.WriteLine(matchCount);
        }

        private static bool MatchesRules(int testNumber)
        {
            var partialNumber = testNumber;
            var foundConsecutiveMatch = false;
            for (int counter = 0; counter < 5; counter++)
            {
                var currentDigit = partialNumber % 10;
                partialNumber /= 10;
                var nextDigit = partialNumber % 10;

                if (currentDigit < nextDigit)
                {
                    return false;
                }

                if (currentDigit == nextDigit)
                {
                    foundConsecutiveMatch = true;
                }
            }

            return foundConsecutiveMatch;
        }
    }
}
