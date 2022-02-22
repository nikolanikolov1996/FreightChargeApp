using System;
using FluentAssertions;
using FreightChargeApp.Domain.CostCalculators;
using Xunit;

namespace FreightChargeApp.Domain.Tests.CostCalculators
{
    public class Cargo4YouCostCalculatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(314.5)]
        [InlineData(900)]
        [InlineData(1000)]
        public void WhenParcelVolumeIsSmallerThan1000_ShouldCost10000(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            Cargo4YouCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(10000);
        }

        [Theory]
        [InlineData(1000.001)]
        [InlineData(1234.001)]
        [InlineData(1500)]
        [InlineData(1999)]
        [InlineData(2000)]
        public void WhenParcelVolumeIsBetween1000And2000_ShouldCost20000(float volume)
        {
            PackageDimensions packageDimensions = new(volume);

            Cargo4YouCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            cost.Should().Be(20000);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        [InlineData(2001)]
        [InlineData(13123123)]
        public void WhenParcelVolumeOutOfRange_ShouldThrowException(float volume)
        {
            PackageDimensions packageDimensions = new(volume);
            Cargo4YouCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnDimensions(packageDimensions);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(0.1)]
        [InlineData(1)]
        [InlineData(1.9)]
        [InlineData(2)]
        public void WhenParcelWeightIsLessThen2_ShouldCost15000(float weight)
        {
            Cargo4YouCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(15000);
        }

        [Theory]
        [InlineData(2.1)]
        [InlineData(7)]
        [InlineData(9)]
        [InlineData(14.9)]
        [InlineData(15)]
        public void WhenParcelWeightIsBetween2And15_ShouldCost18000(float weight)
        {
            Cargo4YouCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(18000);
        }

        [Theory]
        [InlineData(15.1)]
        [InlineData(17.89)]
        [InlineData(19.9)]
        [InlineData(20)]
        public void WhenParcelWeightIsBetween15And20_ShouldCost35000(float weight)
        {
            Cargo4YouCostCalculator costCalculator = new();

            long cost = costCalculator.CalculateCostBasedOnWeight(weight);

            cost.Should().Be(35000);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        [InlineData(2001)]
        [InlineData(13123123)]
        public void WhenParcelWeightOutOfRange_ShouldThrowException(float weight)
        {
            Cargo4YouCostCalculator costCalculator = new();

            Action calculateCost = () => costCalculator.CalculateCostBasedOnWeight(weight);

            calculateCost.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
