using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File
                .ReadAllText("input.txt")
                .Split(',')
                .Select(input => int.Parse(input))
                .ToArray();

            input[1] = 12;
            input[2] = 2;

            Console.WriteLine(Intcode.Process(input));
        }
    }
}
