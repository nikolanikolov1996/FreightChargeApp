namespace FreightChargeApp.Domain.CostCalculators
{
    public interface ICostCalculator
    {
        /// <summary>
        /// Calculates the cost of the shipping based on the dimensions of the package.
        /// </summary>
        /// <param name="packageDimensions">The dimensions of the package.</param>
        /// <returns>The cost to ship the package.</returns>
        long CalculateCostBasedOnDimensions(PackageDimensions packageDimensions);

        /// <summary>
        /// Calculates the cost of the shipping based on the weight of the package.
        /// </summary>
        /// <param name="weight">The weight of the package.</param>
        /// <returns>The cost to ship the package.</returns>
        long CalculateCostBasedOnWeight(float weight);
    }
}
