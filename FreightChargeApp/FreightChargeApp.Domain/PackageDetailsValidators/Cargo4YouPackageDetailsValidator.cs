namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public class Cargo4YouPackageDetailsValidator : IPackageDetailsValidator
    {
        public ValidationResult IsVolumeValid(PackageDimensions packageDimensions)
            => packageDimensions.Volume switch
            {
                > 0 and <= 2000 => ValidationResult.Success,
                <= 0 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 0 }
                }),
                > 2000 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooHigh)
                {
                    ValidationLimits = new ValidationLimits { UpperLimit = 2000 }
                })
            };

        public ValidationResult IsWeightValid(float weight)
            => weight switch
            {
                > 0 and <= 20 => ValidationResult.Success,
                <= 0 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooLow)
                {
                    ValidationLimits = new ValidationLimits { LowerLimit = 0 }
                }),
                > 20 => ValidationResult.Failed(new ValidationError(ValidationErrorType.ValueIsTooHigh)
                {
                    ValidationLimits = new ValidationLimits { UpperLimit = 20 }
                })
            };
    }
}
