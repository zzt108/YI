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

        [TestCase(0, 0, "000000")]
        [TestCase(111, 111, "111111")]
        [TestCase(10, 101, "010101")]
        [TestCase(101, 1, "101001")]
        public void TrigramsToBinaryString_ShouldReturnCorrectBinaryRepresentation(int upperTrigram, int lowerTrigram, string expectedBinary)
        {

            var hexagram = new Hexagram(_values);
            // Act & Assert
            hexagram.TrigramsToBinaryString((upperTrigram, lowerTrigram)).Should().Be(expectedBinary);
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

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram1To2()
        {
            // Arrange
            var hexagram = new Hexagram(1, 2);
            var trigrams = hexagram.GetTrigrams();

            // Act & Assert
            trigrams[0].Should().Be(111);
            trigrams[1].Should().Be(111);
            trigrams[2].Should().Be(000);
            trigrams[3].Should().Be(000);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram34To56()
        {
            // Arrange
            var hexagram = new Hexagram(34, 56); // 001 111, 101 100
            var trigrams = hexagram.GetTrigrams();

            // Act & Assert
            trigrams[0].Should().Be(001);
            trigrams[2].Should().Be(101);
            trigrams[1].Should().Be(111);
            trigrams[3].Should().Be(100);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram34To35()
        {
            // Arrange
            var hexagram = new Hexagram(34, 35); // 001 111, 101 000
            var trigrams = hexagram.GetTrigrams();

            // Act & Assert
            trigrams[0].Should().Be(001);
            trigrams[2].Should().Be(101);
            trigrams[1].Should().Be(111);
            trigrams[3].Should().Be(000);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 4, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram63To64()
        {
            // Arrange
            var hexagram = new Hexagram(63, 64); // 010 101, 101 010
            var trigrams = hexagram.GetTrigrams();

            // Act & Assert
            trigrams[0].Should().Be(010);
            trigrams[1].Should().Be(101);
            trigrams[2].Should().Be(101);
            trigrams[3].Should().Be(010);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForSameHexagram()
        {
            // Arrange
            var hexagram = new Hexagram(30, 30);
            var trigrams = hexagram.GetTrigrams();

            // Act & Assert
            trigrams[0].Should().Be(101);
            trigrams[1].Should().Be(101);
            trigrams[2].Should().Be(101);
            trigrams[3].Should().Be(101);
            hexagram.ChangingLines.Should().BeEmpty();
        }

        [Test]
        public void GetTrigrams_WhenValuesNotChanged_ReturnsLastTrigrams()
        {
            // Arrange - Set initial values
            _values.SetRow(0, true, false);  // 7
            _values.SetRow(1, true, false);  // 7
            _values.SetRow(2, true, false);  // 7
            _values.SetRow(3, true, false);  // 7
            _values.SetRow(4, true, false);  // 7
            _values.SetRow(5, true, false);  // 7

            // Get initial trigrams to set lastTrigrams
            var expected = _hexagram.GetTrigrams();

            // Act - Get trigrams again without changing values
            var result = _hexagram.GetTrigrams();

            // Assert
            result.Should().Equal(expected);
        }

        [Test]
        public void GetTrigrams_WhenValuesChanged_CalculatesNewTrigrams()
        {
            // Arrange - Set changing values
            _values.SetRow(0, true, true);   // 9
            _values.SetRow(1, false, true);  // 6
            _values.SetRow(2, true, false);  // 7
            _values.SetRow(3, false, false); // 8
            _values.SetRow(4, true, true);   // 9
            _values.SetRow(5, false, true);  // 6

            // Act
            var result = _hexagram.GetTrigrams();

            // Assert
            result.Should().HaveCount(4);
            result.Should().OnlyContain(x => x >= 1 && x <= 8);
            result[0].Should().Be(111); // Upper trigram
            result[1].Should().Be(111); // Lower trigram
            result[2].Should().Be(000); // Changed upper trigram
            result[3].Should().Be(000); // Changed lower trigram
        }

        [Test]
        public void GetTrigrams_WithDifferentRowCounts_ReturnsCorrectTrigrams()
        {
            // Arrange - Set values with different configurations
            _values.SetRow(0, true, false);  // 7
            _values.SetRow(1, false, true);  // 6
            _values.SetRow(2, true, true);   // 9
            _values.SetRow(3, false, false); // 8
            _values.SetRow(4, true, false);  // 7
            _values.SetRow(5, false, true);  // 6

            // Act
            var result = _hexagram.GetTrigrams();

            // Assert
            result.Should().HaveCount(4);
            result.Should().OnlyContain(x => x >= 0 && x <= 111);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 2, 3, 6 });
        }
    }
}
