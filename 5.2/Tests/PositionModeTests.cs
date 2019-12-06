using ConsoleApp;
using System;
using Xunit;

namespace Tests
{
    public class PositionModeTests
    {
        [Theory]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void AdditionEmitsExpected(string inputString, int expected)
        {
            Assert.Equal(expected, new Intcode().Process(inputString));
        }

        [Theory]
        [InlineData("2,3,0,3,99", 2)]
        [InlineData("2,4,4,5,99,0", 2)]
        public void MultiplicationEmitsExpected(string inputString, int expected)
        {
            Assert.Equal(expected, new Intcode().Process(inputString));
        }

        [Theory]
        [InlineData("3,0,4,0,99")]
        public void InputAndOutputEmitsExpected(string inputString)
        {
            var randomGenerator = new Random();

            for (int i = 0; i < 100; i++)
            {
                var randomNumber = randomGenerator.Next();

                var intCode = new Intcode(() => { return randomNumber; }, (output) =>
                {
                    Assert.Equal(randomNumber, output);
                });

                intCode.Process(inputString);
            }
        }

        [Theory]
        //Equals
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 9, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 1, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 0, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 100, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", -1, 0)]
        //Less than
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 9, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 100, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 1, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 0, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", -1, 1)]
        //Zero if zero
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        //One if one
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1, 1)]
        public void EmitsExpected(string inputString, int inputValue, int expected)
        {
            var intCode = new Intcode(() => { return inputValue; }, (output) =>
            {
                Assert.Equal(expected, output);
            });

            intCode.Process(inputString);
        }
    }
}
