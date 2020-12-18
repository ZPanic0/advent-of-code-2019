using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class WireCalculator
    {
        public static int GetShortestIntersectDistance(string[] inputText)
        {
            var firstWireMappedPoints = new Dictionary<(int, int), int>();
            var secondWireIntersects = new Dictionary<(int, int), List<int>>();

            var wirePaths = inputText.Select(line => line.Split(','));

            var stepCount = 0;

            MapWires(wirePaths.First(), point =>
            {
                stepCount++;
                if (!firstWireMappedPoints.ContainsKey(point))
                {
                    firstWireMappedPoints.Add(point, 0);
                }
                firstWireMappedPoints[point] = stepCount;
            });

            stepCount = 0;

            MapWires(wirePaths.Last(), point =>
            {
                stepCount++;
                if (firstWireMappedPoints.ContainsKey(point))
                {
                    if (!secondWireIntersects.ContainsKey(point))
                    {
                        secondWireIntersects.Add(point, new List<int>());
                    }

                    secondWireIntersects[point].Add(stepCount);
                }
            });

            return secondWireIntersects
                .Select(intersect => firstWireMappedPoints[intersect.Key] + intersect.Value.OrderBy(distance => distance).First())
                .OrderBy(distance => distance)
                .First();
        }

        private static void MapWires(string[] wirePath, Action<(int, int)> callback)
        {
            var lastPoint = (x: 0, y: 0);

            foreach (var wireRun in wirePath)
            {
                var distance = int.Parse(wireRun[1..]);

                var thisPoint = lastPoint;

                switch (wireRun[0])
                {
                    case 'U':
                        for (int y = lastPoint.y + 1; y <= distance + lastPoint.y; y++)
                        {
                            callback((lastPoint.x, y));
                        }
                        thisPoint.y += distance;
                        break;
                    case 'R':
                        for (int x = lastPoint.x + 1; x <= distance + lastPoint.x; x++)
                        {
                            callback((x, lastPoint.y));
                        }
                        thisPoint.x += distance;
                        break;
                    case 'D':
                        for (int y = lastPoint.y - 1; y >= lastPoint.y - distance; y--)
                        {
                            callback((lastPoint.x, y));
                        }
                        thisPoint.y -= distance;
                        break;
                    case 'L':
                        for (int x = lastPoint.x - 1; x >= lastPoint.x - distance; x--)
                        {
                            callback((x, lastPoint.y));
                        }
                        thisPoint.x -= distance;
                        break;
                    default:
                        throw new ArgumentException();
                }

                lastPoint = thisPoint;
            }
        }
    }
}
