using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var upperLimit = 99;
            var input = File
                .ReadAllText("input.txt")
                .Split(',')
                .Select(input => int.Parse(input))
                .ToArray();

            for (int noun = 0; noun < upperLimit; noun++)
            {
                for (int verb = 0; verb < upperLimit; verb++)
                {
                    var freshMemory = (int[])input.Clone();

                    freshMemory[1] = noun;
                    freshMemory[2] = verb;

                    var result = Intcode.Process(freshMemory);

                    if (result == 19690720)
                    {
                        Console.WriteLine(100 * noun + verb);
                    }
                }
            }
        }
    }
}
