using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class TreeBuilder
    {
        private readonly List<string> input;

        public int GlobalCount { get; set; }

        public TreeBuilder(List<string> input)
        {
            this.input = input;
        }

        public Node Build()
        {
            GlobalCount = 0;
            var baseNode = new Node
            {
                Identity = "COM",
                Depth = 0
            };

            baseNode.Children = GetChildren(baseNode).ToList();

            return baseNode;
        }

        public IEnumerable<Node> GetChildren(Node node)
        {
            var matches = input.Where(item => item.StartsWith(node.Identity)).ToArray();

            foreach (var match in matches)
            {
                input.Remove(match);

                var childNode = new Node
                {
                    Identity = match.Split(')').Last(),
                    Depth = node.Depth + 1
                };

                childNode.Children = GetChildren(childNode).ToList();

                yield return childNode;
                GlobalCount += childNode.Depth;
            }
        }
    }
}
