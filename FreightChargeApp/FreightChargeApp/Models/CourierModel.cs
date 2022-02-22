using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using FreightChargeApp.Domain;
using FreightChargeApp.Domain.PackageDetailsValidators;

namespace FreightChargeApp.Models
{
    public class CourierModel : ReactiveObject
    {
        public readonly CourierType CourierType;

        public CourierModel(CourierType courierType, IObservable<PackageDimensions> packageDimensionsObservable,
            IObservable<float> weightObservable)
        {
            CourierType = courierType;

            Courier courier = Courier.Create(courierType);
            courier.InitializeObservables(packageDimensionsObservable, weightObservable);

            courier.PackageCost
                .ToPropertyEx(this, x => x.Cost);

            courier.PackageCost
                .Select(cost
                    => cost is not null
                        ? (float) cost / 1000f
                        : (float?) null)
                .Select(cost => cost is not null ? $"€{cost.Value:F3}" : null)
                .ToPropertyEx(this, x => x.CostAsString);

            IObservable<string?> weightMessageObservable = courier
                .WeightValidation
                .Select(validationResult => BuildValidationMessage(validationResult, "weight"));

            IObservable<string?> volumeMessageObservable = courier
                .VolumeValidation
                .Select(validationResult => BuildValidationMessage(validationResult, "volume"));

            volumeMessageObservable
                .CombineLatest(weightMessageObservable, (volumeMessage, weightMessage)
                    => volumeMessage is not null && weightMessage is not null
                        ? $"For this courier the {weightMessage} and the {volumeMessage}."
                        : volumeMessage is not null
                            ? $"For this courier the {volumeMessage}."
                            : weightMessage is not null
                                ? $"For this courier the {weightMessage}."
                                : null)
                .ToPropertyEx(this, x => x.ValidationMessage);
        }

        private static string? BuildValidationMessage(ValidationResult validationResult, string validationType)
        {
            if (validationResult.IsValid || validationResult.ValidationError is null)
            {
                return null;
            }

            ValidationLimits? limits = validationResult.ValidationError.ValidationLimits;

            return validationResult.ValidationError.ErrorType switch
            {
                ValidationErrorType.ValueIsTooHigh => limits?.UpperLimit is not null
                    ? $"{validationType} must be less than {limits.UpperLimit}"
                    : $"{validationType} is too high",
                ValidationErrorType.ValueIsTooLow => limits?.LowerLimit is not null
                    ? $"{validationType} must be greater than {limits.LowerLimit}"
                    : $"{validationType} is too low",
                _ => limits?.UpperLimit is not null && limits.LowerLimit is not null
                    ? $"{validationType} must be between {limits.LowerLimit} and {limits.UpperLimit}"
                    : $"{validationType} is not within range"
            };
        }

        [ObservableAsProperty]
        public long? Cost { get; }

        [ObservableAsProperty]
        public string? CostAsString { get; }

        [ObservableAsProperty]
        public string? ValidationMessage { get; }
    }
}
