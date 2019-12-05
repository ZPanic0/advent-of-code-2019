using ConsoleApp;
using System;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Theory]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void AdditionOperation(string inputString, int expected)
        {
            Assert.Equal(expected, new Intcode().Process(inputString));
        }

        [Theory]
        [InlineData("2,3,0,3,99", 2)]
        [InlineData("2,4,4,5,99,0", 2)]
        public void MultiplicationOperation(string inputString, int expected)
        {
            Assert.Equal(expected, new Intcode().Process(inputString));
        }

        [Theory]
        [InlineData("3,0,4,0,99")]
        public void InputAndOutputEmitExpected(string inputString)
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
        [InlineData("1002,4,3,4,33", 99)]
        public void ImmediateModeMultiplication(string inputString, int expected)
        {
            var intCode = new Intcode();
            intCode.Process(inputString);
            
            Assert.Equal(expected, intCode.DumpMemory()[4]);
        }

        [Theory]
        [InlineData("1101,100,-1,4,0", 99)]
        public void ImmediateModeAddition(string inputString, int expected)
        {
            var intCode = new Intcode();
            intCode.Process(inputString);

            Assert.Equal(expected, intCode.DumpMemory()[4]);
        }
    }
}
