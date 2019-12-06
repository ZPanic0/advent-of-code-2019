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
                        AddAndStore(ref i);
                        continue;
                    case 2:
                        MultiplyAndStore(ref i);
                        continue;
                    case 3:
                        ReadInputAndStore(ref i);
                        continue;
                    case 4:
                        ReadMemoryAndOutput(ref i);
                        continue;
                    case 5:
                        JumpIfTrue(ref i);
                        continue;
                    case 6:
                        JumpIfFalse(ref i);
                        continue;
                    case 7:
                        LessThan(ref i);
                        continue;
                    case 8:
                        Equals(ref i);
                        continue;
                    case 99:
                        return EndProcess();
                    default:
                        throw new ArgumentException($"opCode '{opCode}' is invalid.");
                }
            }

            throw new Exception("Unexpected end of intcode sequence");
        }

        public int[] DumpMemory() => (int[])memory.Clone();

        private int GetParameter(int operandNumber, int pointer)
        {
            var mode = memory[pointer] / (int)Math.Pow(10, operandNumber + 1) % 10;

            return mode switch
            {
                0 => memory[memory[pointer + operandNumber]],
                1 => memory[pointer + operandNumber],
                _ => throw new ArgumentException($"'{mode}' is not a registered mode."),
            };
        }

        private void AddAndStore(ref int pointer)
        {
            memory[memory[pointer + 3]] = GetParameter(1, pointer) + GetParameter(2, pointer);
            pointer += 4;
        }

        private void MultiplyAndStore(ref int pointer)
        {
            memory[memory[pointer + 3]] = GetParameter(1, pointer) * GetParameter(2, pointer);
            pointer += 4;
        }

        private void ReadInputAndStore(ref int pointer)
        {
            memory[memory[pointer + 1]] = readInput();
            pointer += 2;
        }

        private void ReadMemoryAndOutput(ref int pointer)
        {
            writeOutput(GetParameter(1, pointer));
            pointer += 2;
        }

        private void JumpIfTrue(ref int pointer)
        {
            pointer = GetParameter(1, pointer) != 0 ? GetParameter(2, pointer) : pointer + 3;
        }

        private void JumpIfFalse(ref int pointer)
        {
            pointer = GetParameter(1, pointer) == 0 ? GetParameter(2, pointer) : pointer + 3;
        }

        private void LessThan(ref int pointer)
        {
            memory[memory[pointer + 3]] = GetParameter(1, pointer) < GetParameter(2, pointer) ? 1 : 0;
            pointer += 4;
        }

        private void Equals(ref int pointer)
        {
            memory[memory[pointer + 3]] = GetParameter(1, pointer) == GetParameter(2, pointer) ? 1 : 0;
            pointer += 4;
        }

        private int EndProcess()
        {
            return memory[0];
        }
    }
}
