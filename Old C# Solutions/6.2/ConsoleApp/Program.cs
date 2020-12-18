using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var treeBuilder = new TreeBuilder(input.ToList());

            Console.WriteLine(Node.GetDistanceBetweenNodes(treeBuilder.Build(), "YOU", "SAN"));
        }
    }
}
