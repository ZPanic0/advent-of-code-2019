using ConsoleApp;
using Xunit;

namespace Tests
{
    public class UnitTest
    {
        [Theory]
        [InlineData(12,2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void EmitsExpectedResults(int mass, int expectedResult)
        {
            Assert.Equal(FuelCalculator.GetFuelRequirements(new[] { mass }), expectedResult);
        }
    }
}
