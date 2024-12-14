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
    {0, 0, 0},
    {0, 0, 0},
    {0, 0, 0},
    {1, 1, 1},
    {1, 1, 1},
    {1, 1, 1}

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(11);
        hexagram.New.Should().Be(12);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
    }

    [Test]
    public void Test2_1()
    {
        int[,] data = new int[,]
        {
    {0, 0, 0},
    {0, 0, 0},
    {0, 0, 0},
    {1, 0, 0},
    {1, 0, 0},
    {1, 0, 0}

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(2);
        hexagram.New.Should().Be(1);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] {1, 2, 3 });
    }

    [Test]
    public void Test03_12()
    {
        int[,] data = new int[,]
        {
    {0, 0, 0},
    {1, 1, 1},
    {0, 0, 0},
    {0, 0, 0},
    {0, 0, 0},
    {1, 1, 1}

    };
        var hexagram = new HexagramNS.Hexagram(new Values().InitValues(data, (item, row, col) => item > 0));
        hexagram.Current.Should().Be(3);
        hexagram.New.Should().Be(50);
        hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
    }
}