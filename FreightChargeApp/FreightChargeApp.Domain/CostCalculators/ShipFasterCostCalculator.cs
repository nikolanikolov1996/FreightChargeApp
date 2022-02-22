using System;

namespace FreightChargeApp.Domain.CostCalculators
{
    public class ShipFasterCostCalculator : ICostCalculator
    {
        public long CalculateCostBasedOnDimensions(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                > 0 and <= 1000 => 11990,
                > 1000 and <= 1700 => 21990,
                _ => throw new ArgumentOutOfRangeException(nameof(packageDimensions), packageDimensions,
                    "Volume of the package must be greater than 0, and less than or equal to 1700")
            };

        public long CalculateCostBasedOnWeight(float weight)
            => weight switch
            {
                > 10 and <= 15 => 16500,
                > 15 and <= 25 => 36500,
                > 25 => 40000 + (int) Math.Ceiling(417 * (weight - 25)),
                _ => throw new ArgumentOutOfRangeException(nameof(weight), weight,
                    "Weight of the package must be greater than 10")
            };
    }
}
