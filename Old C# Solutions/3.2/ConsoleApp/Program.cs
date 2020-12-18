using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = File.ReadAllLines("input.txt");

            Console.WriteLine(WireCalculator.GetShortestIntersectDistance(inputText));
        }
    }
}
