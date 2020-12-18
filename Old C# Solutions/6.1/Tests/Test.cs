using ConsoleApp;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Theory]
        [InlineData(new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" }, 42)]
        public void HasExpectedNodeCount(IEnumerable<string> input, int expected)
        {
            var treeBuilder = new TreeBuilder(input.ToList());
            treeBuilder.Build();

            Assert.Equal(expected, treeBuilder.GlobalCount);
        }
    }
}
