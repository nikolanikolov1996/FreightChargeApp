namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public class ValidationResult
    {
        public ValidationError? ValidationError { get; }
        public bool IsValid { get; private set; }

        private ValidationResult(ValidationError? error)
            => ValidationError = error;

        public static ValidationResult Failed(ValidationError error)
            => new(error) { IsValid = false };

        public static ValidationResult Success => new(null) { IsValid = true };
    }
}
