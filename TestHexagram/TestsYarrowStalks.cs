using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using HexagramNS;

namespace Test
{


    [TestFixture]
    public class TestsYarrowStalks
    {
        [Test]
        public void GetHexagramLineValues_ReturnsExpectedValues()
        {
            // Arrange
            var countedPiles = new List<int> { 9, 9, 7, 6, 9, 9 }; // Example counted piles

            // Act
            var result = new YarrowStalksHelper().GetHexagramLineValues(new [] { 1, 2, 3, 4, 5, 6 });

            // Assert
            result.Should().BeEquivalentTo(countedPiles);

        }

        // TODO test getLine

        [Test]
        public void GetLine_ReturnsExpectedLine()
        {
            // Arrange
            var helper = new YarrowStalksHelper();

            // Act
            var result = helper.GetLine([1, 1, 1]);

            // Assert
            helper.RemainingStalkCount.Should().Be(36);
            result.Should().Be(9);

            result = helper.GetLine([2, 2, 2]);
            helper.RemainingStalkCount.Should().Be(36);
            result.Should().Be(9);

            result = helper.GetLine([3, 3, 3]);
            helper.RemainingStalkCount.Should().Be(28);
            result.Should().Be(7);
        }
    }
}
