namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public interface IPackageDetailsValidator
    {
        /// <summary>
        /// Validates the dimensions of the package.
        /// </summary>
        /// <param name="packageDimensions">The dimensions of the package</param>
        /// <returns>A <see cref="ValidationResult"/> object which describes the result of the validation and any errors, if there were any.</returns>
        ValidationResult IsVolumeValid(PackageDimensions packageDimensions);

        /// <summary>
        /// Validates the weight of the package.
        /// </summary>
        /// <param name="weight">The weight of the package.</param>
        /// <returns>A <see cref="ValidationResult"/> object which describes the result of the validation and any errors, if there were any.</returns>
        ValidationResult IsWeightValid(float weight);
    }
}
