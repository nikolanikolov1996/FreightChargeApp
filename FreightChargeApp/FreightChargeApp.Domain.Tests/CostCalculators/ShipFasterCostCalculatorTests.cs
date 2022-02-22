using System;
using FluentAssertions;
using FreightChargeApp.Domain.CostCalculators;
using Xunit;

namespace FreightChargeApp.Domain.Tests.CostCalculators
{
    public class ShipFasterCostCalculatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(314.5)]
        [InlineData(900)]
        [InlineData(1000)]
        public void WhenParcelVolumeIsSmallerThan1000_ShouldCost11990(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            ShipFasterCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(11990);
        }

        [Theory]
        [InlineData(1000.001)]
        [InlineData(1234.001)]
        [InlineData(1500)]
        [InlineData(1699)]
        [InlineData(1700)]
        public void WhenParcelVolumeIsBetween1000And1700_ShouldCost21990(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            ShipFasterCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(21990);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        [InlineData(2001)]
        [InlineData(13123123)]
        public void WhenParcelVolumeOutOfRange_ShouldThrowException(float volume)
        {
            PackageDimensions packageDimensions = new(volume);
            ShipFasterCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(10.1)]
        [InlineData(12)]
        [InlineData(14.9)]
        [InlineData(15)]
        public void WhenParcelWeightIsBetween10And15_ShouldCost16500(float weight)
        {
            ShipFasterCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(16500);
        }

        [Theory]
        [InlineData(15.1)]
        [InlineData(17.89)]
        [InlineData(19.9)]
        [InlineData(20)]
        [InlineData(25)]
        public void WhenParcelWeightIsBetween15And25_ShouldCost36500(float weight)
        {
            ShipFasterCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(36500);
        }

        [Theory]
        [InlineData(25.1)]
        [InlineData(30.89)]
        [InlineData(50.9)]
        [InlineData(123.12312313)]
        [InlineData(10000)]
        public void WhenParcelWeightIsGreaterThan25_ShouldCost40000PlusExtra(float weight)
        {
            ShipFasterCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            long extraCost = (int) Math.Ceiling((weight - 25) * 417);
            long finalCost = 40000 + extraCost;

            cost.Should().Be(finalCost);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.2131)]
        [InlineData(-123)]
        public void WhenParcelWeightOutOfRange_ShouldThrowException(float weight)
        {
            ShipFasterCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnWeight(weight);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
