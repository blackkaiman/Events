using FluentAssertions;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Infrastructure.Utilities.Extensions
{
    [TestFixture]
    public class PrimitiveExtensionsTests
    {
        [Test]
        public void WhenStringEmpty_ShouldReturnNull()
        {
            var text = "";

            text.NullIfEmpty().Should().Be(null);
        }

        [Test]
        public void WhenStringNull_ShouldReturnNull()
        {
            var text = (string)null;

            text.NullIfEmpty().Should().Be(null);
        }

        [Test]
        public void WhenStringNotEmpty_ShouldReturnString()
        {
            var text = "test";

            text.NullIfEmpty().Should().Be(text);
        }
    }
}