using System;
using FluentAssertions;
using FreightChargeApp.Domain.CostCalculators;
using Xunit;

namespace FreightChargeApp.Domain.Tests.CostCalculators
{
    public class MaltaShipCostCalculatorTests
    {
        [Theory]
        [InlineData(500)]
        [InlineData(714.5)]
        [InlineData(900)]
        [InlineData(1000)]
        public void WhenParcelVolumeIsBetween500And1000_ShouldCost9500(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(9500);
        }

        [Theory]
        [InlineData(1000.001)]
        [InlineData(1234.001)]
        [InlineData(1500)]
        [InlineData(1999)]
        [InlineData(2000)]
        public void WhenParcelVolumeIsBetween1000And2000_ShouldCost19500(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(19500);
        }

        [Theory]
        [InlineData(2000.001)]
        [InlineData(2234.001)]
        [InlineData(3500)]
        [InlineData(4999)]
        [InlineData(5000)]
        public void WhenParcelVolumeIsBetween2000And5000_ShouldCost48500(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(48500);
        }

        [Theory]
        [InlineData(5000.001)]
        [InlineData(5234.001)]
        [InlineData(7500)]
        [InlineData(9999)]
        [InlineData(50000)]
        public void WhenParcelVolumeIsGreaterThan5000_ShouldCost147500(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(147500);
        }

        [Theory]
        [InlineData(-123)]
        [InlineData(-1.123123)]
        [InlineData(0)]
        [InlineData(50.678)]
        [InlineData(300)]
        [InlineData(499)]
        public void WhenParcelVolumeOutOfRange_ShouldThrowException(float volume)
        {
            PackageDimensions packageDimensions = new(volume);
            MaltaShipCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(10.1)]
        [InlineData(12)]
        [InlineData(17.9)]
        [InlineData(20)]
        public void WhenParcelWeightIsBetween10And20_ShouldCost16990(float weight)
        {
            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(16990);
        }

        [Theory]
        [InlineData(22.1)]
        [InlineData(25)]
        [InlineData(27.89)]
        [InlineData(29.9)]
        public void WhenParcelWeightIsBetween20And30_ShouldCost33990(float weight)
        {
            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(33990);
        }

        [Theory]
        [InlineData(30.89)]
        [InlineData(35.1)]
        [InlineData(50.9)]
        [InlineData(123.12312313)]
        [InlineData(10000)]
        public void WhenParcelWeightIsGreaterThan30_ShouldCost43990PlusExtra(float weight)
        {
            MaltaShipCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            long extraCost = (int) Math.Ceiling((weight - 25) * 410);
            long finalCost = 43990 + extraCost;

            cost.Should().Be(finalCost);
        }

        [Theory]
        [InlineData(-123)]
        [InlineData(-1.123123)]
        [InlineData(9)]
        [InlineData(5.678)]
        [InlineData(0)]
        public void WhenParcelWeightOutOfRange_ShouldThrowException(float weight)
        {
            MaltaShipCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnWeight(weight);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
