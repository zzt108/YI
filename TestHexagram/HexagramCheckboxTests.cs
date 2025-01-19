using FluentAssertions;
using HexagramNS;

namespace Test
{
    [TestFixture]
    public class HexagramCheckboxTests
    {
        private Values _values;

        [SetUp]
        public void Setup()
        {
            // Initialize Values object instead of actual UI controls
            _values = new Values(Hexagram.RowCount, Hexagram.ColCount);
        }


        /// <summary>
        /// Tests all 64 possible hexagrams by iterating through each combination of 
        /// current and new hexagram numbers. Ensures that the hexagram created from 
        /// values matches the expected current and new hexagram numbers.
        /// </summary>

        [Test]
        public void CheckEquivalenceOfHexagramConstructors()
        {
            // Test all 64 possible hexagrams
            for (int currentHexagramNum = 1; currentHexagramNum <= 64; currentHexagramNum++)
            {
                for (int newHexagramNum = 1; newHexagramNum <= 64; newHexagramNum++)
                {
                    // Arrange
                    var hexagram = new Hexagram(currentHexagramNum, newHexagramNum);

                    // Create hexagram from values
                    var hexagram2 = new Hexagram(hexagram.Values);

                    // Act
                    int actualCurrent = hexagram2.Current;
                    int actualNew = hexagram2.New;

                    // Assert
                    actualCurrent.Should().Be(currentHexagramNum, $"Current hexagram mismatch for input {currentHexagramNum}");
                    actualNew.Should().Be(newHexagramNum, $"New hexagram mismatch for input {newHexagramNum}");
                }
            }
        }

        // [Test]
        public void FillCheckBoxes_SpecificHexagram_ShouldMatchExpected()
        {
            // Arrange
            int currentHexagramNum = 30; // Example: Li ☲ (101101)
            int newHexagramNum = 56;    // Example: Lü ☶ (100101)

            // Expected checkbox states based on the binary representation of hexagram 30 (current) and 56 (new)
            bool[,] expectedCheckBoxStates = new bool[,] {
                { true, true, true },     // Row 0 (bottom): Yang, not changing (1)
                { false, false, false }, // Row 1: Yin, not changing (0)
                { true, true, true },     // Row 2: Yang, not changing (1)
                { false, false, false }, // Row 3: Yang, changing (1 -> 0)
                { false, false, false }, // Row 4: Yin, not changing (0)
                { true, true, true }      // Row 5 (top): Yang, not changing (1)
            };

            // Act
            // FillCheckBoxesFromHexagramNumbers(currentHexagramNum, newHexagramNum);

            // Assert
            for (int row = 0; row < Hexagram.RowCount; row++)
            {
                for (int col = 0; col < Hexagram.ColCount; col++)
                {
                    _values.GetValue(row, col).Should().Be(expectedCheckBoxStates[row, col],
                        $"Checkbox state mismatch at row {row}, col {col} for hexagrams {currentHexagramNum} and {newHexagramNum}");
                }
            }
        }
    }
}