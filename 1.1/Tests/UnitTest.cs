using ConsoleApp;
using Xunit;

namespace Tests
{
    public class UnitTest
    {
        [Theory]
        [InlineData(12,2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void EmitsExpectedResults(int mass, int expectedResult)
        {
            Assert.Equal(FuelCalculator.GetFuelRequirements(new[] { mass }), expectedResult);
        }
    }
}
