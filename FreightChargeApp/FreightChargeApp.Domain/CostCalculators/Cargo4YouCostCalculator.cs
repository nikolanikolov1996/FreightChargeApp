using System;

namespace FreightChargeApp.Domain.CostCalculators
{
    public class Cargo4YouCostCalculator : ICostCalculator
    {
        public long CalculateCostBasedOnDimensions(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                > 0 and <= 1000 => 10000,
                > 1000 and <= 2000 => 20000,
                _ => throw new ArgumentOutOfRangeException(nameof(packageDimensions), packageDimensions,
                    "Volume of the package must be greater than 0, and less than or equal to 2000")
            };

        public long CalculateCostBasedOnWeight(float weight)
            => weight switch
            {
                > 0 and <= 2 => 15000,
                > 2 and <= 15 => 18000,
                > 15 and <= 20 => 35000,
                _ => throw new ArgumentOutOfRangeException(nameof(weight), weight,
                    "Weight of the package must be greater than 0, and less than or equal to 20")
            };
    }
}
