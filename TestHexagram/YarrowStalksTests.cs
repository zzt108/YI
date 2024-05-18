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
    public class YarrowStalksTests
    {
        [Test]
        public void GetHexagramLineValues_ReturnsExpectedValues()
        {
            // Arrange
            var countedPiles = new List<int> { 1, 2, 3, 4, 5, 6 }; // Example counted piles

            // Act
            var result = new YarrowStalksHelper().GetHexagramLineValues();

            // Assert
            result.Should().BeEquivalentTo(countedPiles);

        }
    }
}
