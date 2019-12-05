using System;
using System.Linq;

namespace ConsoleApp
{
    public class Intcode
    {
        private readonly Func<int> readInput;
        private readonly Action<int> writeOutput;
        private int[] memory;

        public Intcode() { }

        public Intcode(Func<int> readInput, Action<int> writeOutput)
        {
            this.readInput = readInput;
            this.writeOutput = writeOutput;
        }

        public int Process(string input) => Process(
            input
                .Split(',')
                .Select(input => int.Parse(input))
                .ToArray());

        public int Process(int[] input)
        {
            memory = input;

            int i = 0;
            while (i < input.Length)
            {
                var opCode = input[i] % 100;

                switch (opCode)
                {
                    case 1:
                        AddAndStore(i, input);
                        i += 4;
                        continue;
                    case 2:
                        MultiplyAndStore(i, input);
                        i += 4;
                        continue;
                    case 3:
                        ReadInputAndStore(i, input);
                        i += 2;
                        continue;
                    case 4:
                        ReadMemoryAndOutput(i, input);
                        i += 2;
                        continue;
                    case 99:
                        return EndProcess(input);
                    default:
                        throw new ArgumentException($"opCode '{opCode}' is invalid.");
                }
            }

            throw new Exception("Unexpected end of intcode sequence");
        }

        public int[] DumpMemory() => (int[])memory.Clone();

        private int GetMode(int opCode, int operandNumber)
        {
            var mode = opCode / (int)Math.Pow(10, operandNumber + 1) % 10;

            return mode < 2
                ? mode
                : throw new ArgumentException($"'{mode}' is not a registered mode.");
        }

        private void AddAndStore(int pointer, int[] memory)
        {
            var first = GetMode(memory[pointer], 1) == 1 ? memory[pointer + 1] : memory[memory[pointer + 1]];
            var second = GetMode(memory[pointer], 2) == 1 ? memory[pointer + 2] : memory[memory[pointer + 2]];

            memory[memory[pointer + 3]] = first + second;
        }

        private void MultiplyAndStore(int pointer, int[] memory)
        {
            var first = GetMode(memory[pointer], 1) == 1 ? memory[pointer + 1] : memory[memory[pointer + 1]];
            var second = GetMode(memory[pointer], 2) == 1 ? memory[pointer + 2] : memory[memory[pointer + 2]];

            memory[memory[pointer + 3]] = first * second;
        }

        private void ReadInputAndStore(int pointer, int[] memory)
        {
            memory[memory[pointer + 1]] = readInput();
        }

        private void ReadMemoryAndOutput(int pointer, int[] memory)
        {
            writeOutput(GetMode(memory[pointer], 1) == 1 ? memory[pointer + 1] : memory[memory[pointer + 1]]);
        }

        private int EndProcess(int[] memory)
        {
            return memory[0];
        }
    }
}
