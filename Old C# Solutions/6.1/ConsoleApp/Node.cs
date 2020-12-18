using System.Collections.Generic;

namespace ConsoleApp
{
    public class Node
    {
        public string Identity { get; set; }
        public int Depth { get; set; }
        public List<Node> Children { get; set; }
    }
}
