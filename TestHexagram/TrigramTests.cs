using FluentAssertions;
using NUnit.Framework;
using HexagramNS;

namespace TestHexagram
{
    [TestFixture]
    public class TrigramSetTests
    {
        [Test]
        public void Constructor_SetsDefaultValues()
        {
            // Arrange & Act
            var trigramSet = new TrigramSet();

            // Assert
            trigramSet.TopBinaryString.Should().Be("xxx");
            trigramSet.BottomBinaryString.Should().Be("xxx");
            // this should throw format exception
            // trigramSet.Top.Should().Be(0); // Assuming Top parses "xxx" as 0
            // trigramSet.Bottom.Should().Be(0); // Assuming Bottom parses "xxx" as 0
            Action action = () => trigramSet.Top.Should().Be(0);
            action.Should().Throw<FormatException>();

            action = () => trigramSet.Bottom.Should().Be(0);
            action.Should().Throw<FormatException>();

        }

        [Test]
        public void Constructor_WithTopAndBottom_SetsValues()
        {
            // Arrange
            int top = 110;
            int bottom = 011;

            // Act
            var trigramSet = new TrigramSet(top, bottom);

            // Assert
            trigramSet.Top.Should().Be(top);
            trigramSet.Bottom.Should().Be(bottom);
            trigramSet.TopBinaryString.Should().Be("110");
            trigramSet.BottomBinaryString.Should().Be("011");
        }

        [Test]
        public void Top_Setter_UpdatesTopBinaryString()
        {
            // Arrange
            var trigramSet = new TrigramSet();

            // Act
            trigramSet.Top = 101;

            // Assert
            trigramSet.TopBinaryString.Should().Be("101");
        }

        [Test]
        public void Bottom_Setter_UpdatesBottomBinaryString()
        {
            // Arrange
            var trigramSet = new TrigramSet();

            // Act
            trigramSet.Bottom = 10;

            // Assert
            trigramSet.BottomBinaryString.Should().Be("010");
        }
        
        [Test]
        public void Top_Getter_ReturnsCorrectIntValue()
        {
            // Arrange
            var trigramSet = new TrigramSet
            {
                TopBinaryString = "110"
            };

            // Act
            int topValue = trigramSet.Top;

            // Assert
            topValue.Should().Be(110);
        }

        [Test]
        public void Bottom_Getter_ReturnsCorrectIntValue()
        {
            // Arrange
            var trigramSet = new TrigramSet
            {
                BottomBinaryString = "011"
            };

            // Act
            int bottomValue = trigramSet.Bottom;

            // Assert
            bottomValue.Should().Be(11);
        }

        [Test]
        public void ToString_ReturnsConcatenatedBinaryStrings()
        {
            // Arrange
            var trigramSet = new TrigramSet(100, 11);

            // Act
            string result = trigramSet.ToString();

            // Assert
            result.Should().Be("100011");
        }

        [TestCase(0, false, "000010")]
        [TestCase(1, false, "100010")]
        [TestCase(2, true, "101010")]
        [TestCase(3, true, "100110")]
        [TestCase(4, false, "100000")]
        [TestCase(5, true, "100011")]
        public void SetLine_UpdatesCorrectBinaryString(int index, bool isYang, string expected)
        {
            // Arrange
            var trigramSet = new TrigramSet(100, 10); 

            // Act
            trigramSet.SetLine(index, isYang);

            // Assert
            trigramSet.ToString().Should().Be(expected);
        }

        [TestCase(0, true, "1xx010")]
        [TestCase(1, false, "0xx010")]
        [TestCase(2, true, "1xx010")]
        [TestCase(3, false, "100000")]
        [TestCase(4, true, "100010")]
        [TestCase(5, false, "100000")]
        public void SetValue_UpdatesCorrectBitInBinaryString(int trigramPos, bool isYang, string expected)
        {
            // Arrange
            var trigramSet = new TrigramSet(100, 10);

            // Act
            trigramSet.SetLine(trigramPos, isYang);

            // Assert
            trigramSet.ToString().Should().Be(expected);
        }
    }
}