using FluentAssertions;
using Xunit;

namespace FreightChargeApp.Domain.Tests
{
    public class PackageDimensionTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(123, 134, 245122)]
        [InlineData(0.123246, 1, 0.016999878)]
        [InlineData(312, 0.123, 123.448282)]
        public void WhenInitialisingObject_ShouldSetVolume(float length, float width, float height)
        {
            float volume = length * width * height;

            PackageDimensions packageDimensions = new(length, width, height);

            packageDimensions.Volume.Should().Be(volume);
        }
    }
}
