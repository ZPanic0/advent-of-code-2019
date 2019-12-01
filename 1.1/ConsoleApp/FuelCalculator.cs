using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class FuelCalculator
    {
        public static int GetFuelRequirements(IEnumerable<int> masses) => masses.Sum(mass => (int)(Math.Truncate(mass / 3f) - 2));
    }
}
