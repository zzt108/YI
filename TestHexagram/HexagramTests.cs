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
        public void Hexagram1_8()
        {
            // 111 001 = #34
            // Arrange
            _values.SetValue(0, 0, true); // top 6
            _values.SetValue(0, 1, true);
            _values.SetValue(0, 2, true); // 1 - 0

            _values.SetValue(1, 0, false); // line 5
            _values.SetValue(1, 1, false);
            _values.SetValue(1, 2, true); // 1 - 1

            _values.SetValue(2, 0, true); // line 4
            _values.SetValue(2, 1, true);
            _values.SetValue(2, 2, true); // 1 - 0

            _values.SetValue(3, 0, true); // 
            _values.SetValue(3, 1, true); // 
            _values.SetValue(3, 2, true); // 1 - 0

            _values.SetValue(4, 0, true); // 
            _values.SetValue(4, 1, true); // 
            _values.SetValue(4, 2, true); // 1 - 0

            _values.SetValue(5, 0, true); // 
            _values.SetValue(5, 1, true); // 
            _values.SetValue(5, 2, true); // 1 - 0 bottom 1

            // Act

            // Assert
            _hexagram.Current.Should().Be(1);
            _hexagram.New.Should().Be(8);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int[] { 1, 2, 3, 4, 6 });
        }

        [Test]
        public void Main_ShouldReturnCorrectHexagramNumber_WhenNoChangingLines()
        {
            // Arrange
            _values.SetValue(0, 0, true);
            _values.SetValue(0, 1, false);
            _values.SetValue(0, 2, true); // line 1 yin 0
            _values.SetValue(1, 0, false);
            _values.SetValue(1, 1, true);
            _values.SetValue(1, 2, false); // line 2 yang 1
            _values.SetValue(2, 0, true);
            _values.SetValue(2, 1, false);
            _values.SetValue(2, 2, true); // line 3 yin 0
            _values.SetValue(3, 0, true);
            _values.SetValue(3, 1, false);
            _values.SetValue(3, 2, true); // line 4 yin 0
            _values.SetValue(4, 0, false);
            _values.SetValue(4, 1, true);
            _values.SetValue(4, 2, false); // line 5 yang 1
            _values.SetValue(5, 0, true);
            _values.SetValue(5, 1, false);
            _values.SetValue(5, 2, true); // line 6 yin 0

            // Act

            // Assert
            _hexagram.Current.Should().Be(29);
            _hexagram.New.Should().Be(29);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int[] { });
        }

        [Test]
        public void Changed_ShouldReturnCorrectHexagramNumber_WhenChangingLines()
        {
            // Arrange
            int row = 0;
            _values.SetValue(row, 0, true); // top 6
            _values.SetValue(row, 1, false);
            _values.SetValue(row, 2, true); // 0
            row++;
            _values.SetValue(row, 0, false);
            _values.SetValue(row, 1, true);
            _values.SetValue(row, 2, false); // 1
            row++;
            _values.SetValue(row, 0, true);
            _values.SetValue(row, 1, false);
            _values.SetValue(row, 2, true); // 0
            row++;
            _values.SetValue(row, 0, true);
            _values.SetValue(row, 1, false);
            _values.SetValue(row, 2, true); // 0
            row++;
            _values.SetValue(row, 0, false);
            _values.SetValue(row, 1, true);
            _values.SetValue(row, 2, false); // 1
            row++;
            _values.SetValue(row, 0, true);
            _values.SetValue(row, 1, true);
            _values.SetValue(row, 2, true); // 1 - 0 bottom 1

            // Act

            // Assert
            _hexagram.Current.Should().Be(60);
            _hexagram.New.Should().Be(29);
            _hexagram.ChangingLines.Should().BeEquivalentTo(new int[] { 1 });
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
            var result = new TrigramSet(upperTrigram, lowerTrigram);

            // Act & Assert
            result.ToString().Should().Be(expectedBinary);
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
            var result = hexagram.TrigramString(hexagramNumber);

            // Assert
            result.Should().Be(expectedBinary);
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram1To2()
        {
            // Arrange
            var hexagram = new Hexagram(1, 2);
            var trigrams = hexagram.Trigrams;

            // Act & Assert
            trigrams.Current.Top.Should().Be(111);
            trigrams.Current.Bottom.Should().Be(111);
            trigrams.New.Top.Should().Be(000);
            trigrams.New.Bottom.Should().Be(000);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram34To56()
        {
            // Arrange
            var hexagram = new Hexagram(34, 56); // 001 111, 101 100
            var trigrams = hexagram.Trigrams;

            // Act & Assert
            trigrams.Current.Top.Should().Be(001);
            trigrams.New.Top.Should().Be(101);
            trigrams.Current.Bottom.Should().Be(111);
            trigrams.New.Bottom.Should().Be(100);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram34To35()
        {
            // Arrange
            var hexagram = new Hexagram(34, 35); // 001 111, 101 000
            var trigrams = hexagram.Trigrams;

            // Act & Assert
            trigrams.Current.Top.Should().Be(001);
            trigrams.New.Top.Should().Be(101);
            trigrams.Current.Bottom.Should().Be(111);
            trigrams.New.Bottom.Should().Be(000);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForHexagram63To64()
        {
            // Arrange
            var hexagram = new Hexagram(63, 64); // 010 101, 101 010
            var trigrams = hexagram.Trigrams;

            // Act & Assert
            trigrams.Current.Top.Should().Be(010);
            trigrams.Current.Bottom.Should().Be(101);
            trigrams.New.Top.Should().Be(101);
            trigrams.New.Bottom.Should().Be(010);
            hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6 });
        }

        [Test]
        public void FillValuesFromHexagramNumbers_ShouldSetCorrectValues_ForSameHexagram()
        {
            // Arrange
            var hexagram = new Hexagram(30, 30);
            var trigrams = hexagram.Trigrams;

            // Act & Assert
            trigrams.Current.Top.Should().Be(101);
            trigrams.Current.Bottom.Should().Be(101);
            trigrams.New.Top.Should().Be(101);
            trigrams.New.Bottom.Should().Be(101);
            hexagram.ChangingLines.Should().BeEmpty();
        }

        [Test]
        public void GetTrigrams_WhenValuesChanged_CalculatesNewTrigrams()
        {
            var yang = true;
            // Arrange - Set changing values
            _values.SetIndexRow(0, yang, true);   // 9 1 -0
            _values.SetIndexRow(1, !yang, true);  // 6 0 -1
            _values.SetIndexRow(2, yang, false);  // 7 1 -1
            _values.SetIndexRow(3, !yang, false); // 8 0 -0
            _values.SetIndexRow(4, yang, true);   // 9 1 -0
            _values.SetIndexRow(5, !yang, true);  // 6 0 -1

            // Act
            var result = _hexagram.Trigrams;

            // Assert
            _hexagram.Current.Should().Be(64);
            _hexagram.New.Should().Be(17);
            result.Current.Top.Should().Be(101); // Upper trigram
            result.Current.Bottom.Should().Be(010); // Lower trigram
            result.New.Top.Should().Be(011); // Changed upper trigram
            result.New.Bottom.Should().Be(001); // Changed lower trigram
        }

        [Test]
        public void GetTrigrams_WithDifferentRowCounts_ReturnsCorrectTrigrams()
        {
            var yang = true;

            // Arrange - Set values with different configurations
            _values.SetIndexRow(0, yang, false);  // 7 1 - 1
            _values.SetIndexRow(1, !yang, true);  // 6 0 - 1
            _values.SetIndexRow(2, yang, true);   // 9 1 - 0
            _values.SetIndexRow(3, !yang, false); // 8 0 - 0
            _values.SetIndexRow(4, yang, false);  // 7 1 - 1
            _values.SetIndexRow(5, !yang, true);  // 6 0 - 1

            // Act
            var result = _hexagram.Trigrams;

            // Assert
            _hexagram.ChangingLines.Should().BeEquivalentTo(new[] { 1, 4, 5 });
        }
    }
}
