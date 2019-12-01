using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(input => int.Parse(input));
            Console.WriteLine(FuelCalculator.GetFuelRequirements(input));
        }
    }
}
