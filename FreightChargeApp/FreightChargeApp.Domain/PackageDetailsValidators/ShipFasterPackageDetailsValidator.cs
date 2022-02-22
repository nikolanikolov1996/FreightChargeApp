namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public class ShipFasterPackageDetailsValidator : IPackageDetailsValidator
    {
        public ValidationResult IsVolumeValid(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                > 0 and <= 1700 => ValidationResult.Success,
                <= 0 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 0 }
                }),
                > 1700 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooHigh)
                {
                    ValidationLimits = new ValidationLimits { UpperLimit = 1700 }
                })
            };

        public ValidationResult IsWeightValid(float weight)
            => weight switch
            {
                > 10 and <= 30 => ValidationResult.Success,
                <= 10 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 10 }
                }),
                > 30 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooHigh)
                {
                    ValidationLimits = new ValidationLimits { UpperLimit = 30 }
                })
            };
    }
}
