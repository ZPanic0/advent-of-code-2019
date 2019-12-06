using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        public static int globalCount = 0;
        static void Main(string[] args)
        {
            var treeBuilder = new TreeBuilder(File.ReadAllLines("input.txt").ToList());
            _ = treeBuilder.Build();

            Console.WriteLine(treeBuilder.GlobalCount);
        }
    }
}
