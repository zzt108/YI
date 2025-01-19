using FluentAssertions;
using HexagramNS;

namespace Test;
[TestFixture]

public class TestHexagram
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test11_12()
    {
        int[,] data = new int[,]
        {
    {0, 0, 0}, // top i0
    {0, 0, 0},
    {0, 0, 0},
    {1, 1, 1},
    {1, 1, 1},
    {1, 1, 1} // bottom i5

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(11);
        hexagram.New.Should().Be(12);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
    }

    [Test]
    public void Test12_1()
    {
        int[,] data = new int[,]
        {
    {0, 0, 0}, // yin 2 yang - top (i0)
    {0, 0, 0}, // yin 2 yang
    {0, 0, 0}, // yin 2 yang
    {1, 0, 0}, // yang
    {1, 0, 0}, // yang
    {1, 0, 0}  // yang - bottom (i5)

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(11);
        hexagram.New.Should().Be(1);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] {  4, 5, 6 });
    }

    [Test]
    public void Test03_12()
    {
        int[,] data = new int[,]
        {
    {0, 0, 0}, // top 0 2 1
    {1, 1, 1}, // 1 2 0
    {0, 0, 0}, // 0 2 1
    {0, 0, 0}, // 0 2 1
    {0, 0, 0}, // 0 2 1
    {1, 1, 1}  // bottom 1 2 0

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(3);
        hexagram.New.Should().Be(50);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
    }
}