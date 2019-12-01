using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class FuelCalculator
    {
        public static int GetFuelRequirements(IEnumerable<int> masses) =>
            masses.Sum(mass =>
            {
                var sum = 0;
                var fuelMass = mass;

                while (fuelMass > 0)
                {
                    var delta = GetModuleRequirement(fuelMass);
                    sum += delta;
                    fuelMass = delta;
                }

                return sum;
            });

        private static int GetModuleRequirement(int mass)
        {
            var result = (int)(Math.Truncate(mass / 3f) - 2);
            return result >= 0 ? result : 0;
        }
    }
}
