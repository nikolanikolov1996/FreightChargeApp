using System;

namespace FreightChargeApp.Domain.CostCalculators
{
    public class MaltaShipCostCalculator : ICostCalculator
    {
        public long CalculateCostBasedOnDimensions(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                >= 500 and <= 1000 => 9500,
                > 1000 and <= 2000 => 19500,
                > 2000 and <= 5000 => 48500,
                > 5000 => 147500,
                _ => throw new ArgumentOutOfRangeException(nameof(packageDimensions), packageDimensions,
                    "Volume of the package must be greater than 500")
            };

        public long CalculateCostBasedOnWeight(float weight)
            => weight switch
            {
                >= 10 and <= 20 => 16990,
                > 20 and <= 30 => 33990,
                > 30 => 43990 + (int) Math.Ceiling(410 * (weight - 25)),
                _ => throw new ArgumentOutOfRangeException(nameof(weight), weight,
                    "Weight of the package must be greater than 10")
            };
    }
}
