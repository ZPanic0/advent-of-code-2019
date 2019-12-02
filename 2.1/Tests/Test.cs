using ConsoleApp;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Theory]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("2,3,0,3,99", 2)]
        [InlineData("2,4,4,5,99,0", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void EmitsExpectedResult(string inputString, int expected)
        {
            Assert.Equal(expected, Intcode.Process(inputString));
        }
    }
}
