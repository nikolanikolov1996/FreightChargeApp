namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public class MaltaShipPackageDetailsValidator : IPackageDetailsValidator
    {
        public ValidationResult IsVolumeValid(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                >= 500 => ValidationResult.Success,
                _ => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 500 }
                })
            };

        public ValidationResult IsWeightValid(float weight)
            => weight switch
            {
                >= 10 => ValidationResult.Success,
                _ => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 10 }
                })
            };
    }
}
