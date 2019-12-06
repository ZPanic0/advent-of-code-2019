using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ConsoleApp
{
    public class Node : IEquatable<Node>
    {
        public string Identity { get; set; }
        public int Depth { get; set; }
        public List<Node> Children { get; set; }

        public bool Equals([AllowNull] Node other)
        {
            return other != null && other.Identity == Identity;
        }

        public static int GetDistanceBetweenNodes(Node topNode, string targetIdentity, string destinationIdentity)
        {
            var first = GetPathToIdentity(targetIdentity, topNode);
            var second = GetPathToIdentity(destinationIdentity, topNode);

            return first.Except(second).Count() + second.Except(first).Count() - 2;
        }

        public static List<Node> GetPathToIdentity(string targetIdentity, Node startingNode)
        {
            var path = new List<Node>();

            BuildPath(targetIdentity, startingNode, path);

            return path;
        }

        private static bool BuildPath(string targetIdentity, Node currentNode, List<Node> nodesOnPath)
        {
            if (currentNode.Identity == targetIdentity)
            {
                nodesOnPath.Add(currentNode);
                return true;
            }
            else
            {
                foreach (var childNode in currentNode.Children)
                {
                    if (BuildPath(targetIdentity, childNode, nodesOnPath))
                    {
                        nodesOnPath.Add(currentNode);
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
