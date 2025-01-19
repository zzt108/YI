using FluentAssertions;
using HexagramNS;

namespace TestHexagram
{
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
            _values.SetIndexRow(0, 6);

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
