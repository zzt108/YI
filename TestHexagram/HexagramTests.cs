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
            _hexagram.Main.Should().Be(1);
            _hexagram.Changed.Should().Be(2);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new [] {1,2,3,4,5,6});

        }


        [Test]
        public void ChangingLines_ShouldBeEmpty()
        {
            // 111 001 = #34
            // Arrange
            _values.SetValue(0, 0, true); // 1
            _values.SetValue(0, 1, true); // 1
            _values.SetValue(0, 2, true); // 1
            _values.SetValue(1, 0, false); // 0
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
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int [] { });
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
            _hexagram.Main.Should().Be(30);
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
            _hexagram.Changed.Should().Be(29);
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
            _values.SetValue(5, 0, true);
            _values.SetValue(5, 1, true);
            _values.SetValue(5, 2, true);

            // Act

            // Assert
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 6 });
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
            _values.SetValue(2, 0, true);
            _values.SetValue(2, 1, true);
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
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 3, 6 });
        }
    }

    [TestFixture]
    public class ValuesTests
    {
        private Values _values;

        [SetUp]
        public void Setup()
        {
            _values = new Values();
        }

        [Test]
        public void GetValue_ShouldReturnCorrectValue()
        {
            // Arrange
            _values.SetValue(0, 0, true);

            // Act
            var result = _values.GetValue(0, 0);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void SetValue_ShouldUpdateValue()
        {
            // Act
            _values.SetValue(0, 0, false);

            // Assert
            _values.GetValue(0, 0).Should().BeFalse();
        }

        [Test]
        public void SetHexagramRow_ShouldSetCorrectValues()
        {
            // Act
            _values.SetHexagramRow(0, 6);

            // Assert
            _values.GetValue(0, 0).Should().BeFalse();
            _values.GetValue(0, 1).Should().BeFalse();
            _values.GetValue(0, 2).Should().BeFalse();
        }

        [Test]
        public void InitValues_ShouldInitializeCorrectly()
        {
            // Arrange
            bool[,] array = new bool[,] { { true, false }, { false, true } };

            // Act
            _values.InitValues(array, (value, row, col) => value);

            // Assert
            _values.GetValue(0, 0).Should().BeTrue();
            _values.GetValue(1, 1).Should().BeTrue();
        }
    }
}
