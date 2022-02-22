namespace FreightChargeApp.Domain.PackageDetailsValidators
{
    public record ValidationLimits
    {
        public float? LowerLimit { get; init; }
        public float? UpperLimit { get; init; }
    }
}
