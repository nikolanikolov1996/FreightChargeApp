namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public record ValidationError
    {
        public ValidationError(ValidationErrorType errorType)
            => ErrorType = errorType;

        public ValidationErrorType ErrorType { get; }
        public ValidationLimits? ValidationLimits { get; init; }
    }
}
