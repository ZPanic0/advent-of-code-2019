using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class WireCalculator
    {
        public static int GetNearestIntersectDistance(string[] inputText)
        {
            var mappedPoints = new HashSet<(int, int)>();
            var intersects = new List<(int, int)>();

            var wirePaths = inputText.Select(line => line.Split(','));

            MapWires(wirePaths.First(), point => mappedPoints.Add(point));

            MapWires(wirePaths.Last(), point =>
            {
                if (mappedPoints.Contains(point))
                {
                    intersects.Add(point);
                }
            });

            return intersects
                .Select(point => Math.Abs(point.Item1) + Math.Abs(point.Item2))
                .OrderBy(distance => distance)
                .Skip(1)
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
                        for (int y = lastPoint.y; y <= distance + lastPoint.y; y++)
                        {
                            callback((lastPoint.x, y));
                        }
                        thisPoint.y += distance;
                        break;
                    case 'R':
                        for (int x = lastPoint.x; x <= distance + lastPoint.x; x++)
                        {
                            callback((x, lastPoint.y));
                        }
                        thisPoint.x += distance;
                        break;
                    case 'D':
                        for (int y = lastPoint.y; y >= lastPoint.y - distance; y--)
                        {
                            callback((lastPoint.x, y));
                        }
                        thisPoint.y -= distance;
                        break;
                    case 'L':
                        for (int x = lastPoint.x; x >= lastPoint.x - distance; x--)
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
