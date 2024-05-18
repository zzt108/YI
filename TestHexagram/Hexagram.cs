using FluentAssertions;
using HexagramNS;

namespace Test
{
    public class Hexagram
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            bool[,] data = new bool[,]
            {
        {false, false, false},
        {false, false, false},
        {false, false, false},
        {true, true, true},
        {true, true, true},
        {true, true, true}

        };
            var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item));
            hexagram.Main.Should().Be(11);
            hexagram.Changed.Should().Be(12);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] {1 , 2, 3, 4, 5, 6});
        }
    }
}