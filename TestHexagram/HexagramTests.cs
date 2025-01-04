using FluentAssertions;
using NUnit.Framework;
using HexagramNS;

namespace TestHexagram
{
    [TestFixture]
    public class HexagramTests
    {
        private Values _values;
        private Hexagram _hexagram;

        [SetUp]
        public void Setup()
        {
            _values = new Values();
            _hexagram = new Hexagram(_values);
        }

        [Test]
        public void Hexagram1_2()
        {
            // Arrange
            _values.SetValue(0, 0, true); // 1
            _values.SetValue(0, 1, true); // 1
            _values.SetValue(0, 2, true); // 1
            _values.SetValue(1, 0, true); // 1
            _values.SetValue(1, 1, true); // 1
            _values.SetValue(1, 2, true); // 1
            _values.SetValue(2, 0, true); // 1
            _values.SetValue(2, 1, true); // 1
            _values.SetValue(2, 2, true); // 1
            _values.SetValue(3, 0, true); // 1
            _values.SetValue(3, 1, true); // 1
            _values.SetValue(3, 2, true); // 1
            _values.SetValue(4, 0, true); // 1
            _values.SetValue(4, 1, true); // 1
            _values.SetValue(4, 2, true); // 1
            _values.SetValue(5, 0, true); // 1
            _values.SetValue(5, 1, true); // 1
            _values.SetValue(5, 2, true); // 1

            // Act

            // Assert
            _hexagram.Current.Should().Be(1);
            _hexagram.New.Should().Be(2);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });

        }


        [Test]
        public void ChangingLines_ShouldBeEmpty()
        {
            // 111 001 = #34
            // Arrange
            _values.SetValue(0, 0, true); // 1
            _values.SetValue(0, 1, true); // 1
            _values.SetValue(0, 2, true); // 1

            _values.SetValue(1, 0, false); // 0 // line 5
            _values.SetValue(1, 1, false); // 0
            _values.SetValue(1, 2, true); // 1

            _values.SetValue(2, 0, true); // 1
            _values.SetValue(2, 1, true); // 1
            _values.SetValue(2, 2, true); // 1

            _values.SetValue(3, 0, true); // 1
            _values.SetValue(3, 1, true); // 1
            _values.SetValue(3, 2, true); // 1

            _values.SetValue(4, 0, true); // 1
            _values.SetValue(4, 1, true); // 1
            _values.SetValue(4, 2, true); // 1

            _values.SetValue(5, 0, true); // 1
            _values.SetValue(5, 1, true); // 1
            _values.SetValue(5, 2, true); // 1

            // Act

            // Assert
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int[] { 1, 2, 3, 4, 6 });
        }

        [Test]
        public void Main_ShouldReturnCorrectHexagramNumber_WhenNoChangingLines()
        {
            // Arrange
            _values.SetValue(0, 0, true);
            _values.SetValue(0, 1, false);
            _values.SetValue(0, 2, true);
            _values.SetValue(1, 0, false);
            _values.SetValue(1, 1, true);
            _values.SetValue(1, 2, false);
            _values.SetValue(2, 0, true);
            _values.SetValue(2, 1, false);
            _values.SetValue(2, 2, true);
            _values.SetValue(3, 0, true);
            _values.SetValue(3, 1, false);
            _values.SetValue(3, 2, true);
            _values.SetValue(4, 0, false);
            _values.SetValue(4, 1, true);
            _values.SetValue(4, 2, false);
            _values.SetValue(5, 0, true);
            _values.SetValue(5, 1, false);
            _values.SetValue(5, 2, true);

            // Act

            // Assert
            _hexagram.Current.Should().Be(30);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int[] { });
        }

        [Test]
        public void Changed_ShouldReturnCorrectHexagramNumber_WhenChangingLines()
        {
            // Arrange
            _values.SetValue(0, 0, true);
            _values.SetValue(0, 1, false);
            _values.SetValue(0, 2, true);
            _values.SetValue(1, 0, false);
            _values.SetValue(1, 1, true);
            _values.SetValue(1, 2, false);
            _values.SetValue(2, 0, true);
            _values.SetValue(2, 1, false);
            _values.SetValue(2, 2, true);
            _values.SetValue(3, 0, true);
            _values.SetValue(3, 1, false);
            _values.SetValue(3, 2, true);
            _values.SetValue(4, 0, false);
            _values.SetValue(4, 1, true);
            _values.SetValue(4, 2, false);
            _values.SetValue(5, 0, true);
            _values.SetValue(5, 1, true);
            _values.SetValue(5, 2, true);

            // Act

            // Assert
            _hexagram.New.Should().Be(56);
        }

        [Test]
        public void ChangingLines_ShouldReturnCorrectLines_WhenOneLineChanges()
        {
            // Arrange
            _values.SetValue(0, 0, true);
            _values.SetValue(0, 1, false);
            _values.SetValue(0, 2, true);

            _values.SetValue(1, 0, false);
            _values.SetValue(1, 1, true);
            _values.SetValue(1, 2, false);

            _values.SetValue(2, 0, true);
            _values.SetValue(2, 1, false);
            _values.SetValue(2, 2, true);

            _values.SetValue(3, 0, true);
            _values.SetValue(3, 1, false);
            _values.SetValue(3, 2, true);

            _values.SetValue(4, 0, false);
            _values.SetValue(4, 1, true);
            _values.SetValue(4, 2, false);

            _values.SetValue(5, 0, true); // line 1
            _values.SetValue(5, 1, true);
            _values.SetValue(5, 2, true);

            // Act

            // Assert
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1 });
        }

        [Test]
        public void ChangingLines_ShouldReturnCorrectLines_WhenMultipleLinesChange()
        {
            // Arrange
            _values.SetValue(0, 0, true);
            _values.SetValue(0, 1, false);
            _values.SetValue(0, 2, true);

            _values.SetValue(1, 0, false);
            _values.SetValue(1, 1, true);
            _values.SetValue(1, 2, false);

            _values.SetValue(2, 0, true); // line 4
            _values.SetValue(2, 1, true);
            _values.SetValue(2, 2, true);

            _values.SetValue(3, 0, true);
            _values.SetValue(3, 1, false);
            _values.SetValue(3, 2, true);

            _values.SetValue(4, 0, false);
            _values.SetValue(4, 1, true);
            _values.SetValue(4, 2, false);

            _values.SetValue(5, 0, true); // line 1
            _values.SetValue(5, 1, true);
            _values.SetValue(5, 2, true);

            // Act

            // Assert
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 4 });
        }

        [Test]
        public void TrigramsToBinaryString_ShouldReturnCorrectBinaryRepresentation()
        {
            // Arrange
            var hexagram = new Hexagram(_values);

            // Act & Assert
            hexagram.TrigramsToBinaryString((0, 0)).Should().Be("000000");
            hexagram.TrigramsToBinaryString((111, 111)).Should().Be("111111");
            hexagram.TrigramsToBinaryString((10, 101)).Should().Be("010101");
            hexagram.TrigramsToBinaryString((101, 1)).Should().Be("101001");
        }

        [TestCase(1, "111111")]
        [TestCase(2, "000000")]
        [TestCase(3, "010001")]
        [TestCase(4, "100010")]
        [TestCase(63, "010101")]
        public void HexagramToBinary_ConvertsCorrectly(int hexagramNumber, string expectedBinary)
        {
            // Arrange
            var hexagram = new Hexagram(_values);

            // Act
            var result = hexagram.HexagramToString(hexagramNumber);

            // Assert
            result.Should().Be(expectedBinary);
        }
    }
}
