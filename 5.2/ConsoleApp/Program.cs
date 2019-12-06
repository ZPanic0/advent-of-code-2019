using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var intCode = new Intcode(
                () =>
                {
                    Console.Write("Input: ");
                    var input = int.Parse(Console.ReadLine());
                    Console.Clear();
                    return input;
                },
                (output) => { Console.WriteLine(output); });

            foreach (var inputLine in File.ReadAllLines("input.txt"))
            {
                intCode.Process(inputLine);
            }
        }
    }
}
