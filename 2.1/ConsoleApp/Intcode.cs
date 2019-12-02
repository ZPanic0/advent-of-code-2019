using System;
using System.Linq;

namespace ConsoleApp
{
    public static class Intcode
    {
        public static int Process(string input) => Process(
            input
                .Split(',')
                .Select(input => int.Parse(input))
                .ToArray());

        public static int Process(int[] input)
        {
            for (int i = 0; i < input.Length; i += 4)
            {
                int first, second;

                switch (input[i])
                {
                    case 1:
                        first = input[input[i + 1]];
                        second = input[input[i + 2]];
                        input[input[i + 3]] = first + second;
                        break;
                    case 2:
                        first = input[input[i + 1]];
                        second = input[input[i + 2]];
                        input[input[i + 3]] = first * second;
                        break;
                    case 99:
                        return input[0];
                    default:
                        throw new ArgumentException($"intcode '{input[i]}' is invalid.");
                }
            }

            throw new Exception("Unexpected end of intcode sequence");
        }
    }
}
