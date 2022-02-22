using FluentAssertions;
using FreightChargeApp.Domain.PackageDetailsValidators;
using Xunit;

namespace FreightChargeApp.Domain.Tests.PackageDetailsValidators
{
    public class ShipFasterPackageDetailsValidatorTests
    {
        [Theory]
        [InlineData(-123123.333321)]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(8)]
        [InlineData(10)]
        public void WhenWeightValueIsTooLow_ShouldReturnFalse(float weight)
        {
            ShipFasterPackageDetailsValidator validator = new();

            ValidationResult isWeightValid = validator.IsWeightValid(weight);

            isWeightValid.IsValid.Should().BeFalse("weight value is too low for this courier");
            isWeightValid.ValidationError
                .Should()
                .NotBeNull();
            isWeightValid.ValidationError!.ErrorType
                .Should()
                .Be(ValidationErrorType.ValueIsTooLow);
            isWeightValid.ValidationError!.ValidationLimits?.LowerLimit
                .Should()
                .Be(10);
        }

        [Theory]
        [InlineData(30.00001)]
        [InlineData(50)]
        [InlineData(123456789)]
        public void WhenWeightValueIsTooHigh_ShouldReturnFalse(float weight)
        {
            ShipFasterPackageDetailsValidator validator = new();

            ValidationResult isWeightValid = validator.IsWeightValid(weight);

            isWeightValid.IsValid.Should().BeFalse("weight value is too high for this courier");
            isWeightValid.ValidationError
                .Should()
                .NotBeNull();
            isWeightValid.ValidationError!.ErrorType
                .Should()
                .Be(ValidationErrorType.ValueIsTooHigh);
            isWeightValid.ValidationError!.ValidationLimits?.UpperLimit
                .Should()
                .Be(30);
        }

        [Theory]
        [InlineData(19.9999999)]
        [InlineData(15)]
        [InlineData(25)]
        [InlineData(29.999)]
        public void WhenWeightValueIsWithinTheLimits_ShouldReturnTrue(float weight)
        {
            ShipFasterPackageDetailsValidator validator = new();

            ValidationResult isWeightValid = validator.IsWeightValid(weight);

            isWeightValid.IsValid.Should().BeTrue("weight value is within the limit for this courier");
            isWeightValid.ValidationError.Should().BeNull();
        }

        [Theory]
        [InlineData(-123123.333321)]
        [InlineData(-10)]
        [InlineData(0)]
        public void WhenVolumeValueIsTooLow_ShouldReturnFalse(float volume)
        {
            ShipFasterPackageDetailsValidator validator = new();

            PackageDimensions packageDimensions = new(volume);
            ValidationResult isVolumeValid = validator.IsVolumeValid(packageDimensions);

            isVolumeValid.IsValid.Should().BeFalse("volume value is too low for this courier");
            isVolumeValid.ValidationError
                .Should()
                .NotBeNull();
            isVolumeValid.ValidationError!.ErrorType
                .Should()
                .Be(ValidationErrorType.ValueIsTooLow);
            isVolumeValid.ValidationError!.ValidationLimits?.LowerLimit
                .Should()
                .Be(0);
        }

        [Theory]
        [InlineData(1700.0001)]
        [InlineData(2123.1233)]
        [InlineData(3000)]
        [InlineData(9876)]
        public void WhenVolumeValueIsTooHigh_ShouldReturnFalse(float volume)
        {
            ShipFasterPackageDetailsValidator validator = new();

            PackageDimensions packageDimensions = new(volume);
            ValidationResult isVolumeValid = validator.IsVolumeValid(packageDimensions);

            isVolumeValid.IsValid.Should().BeFalse("volume value is too high for this courier");
            isVolumeValid.ValidationError
                .Should()
                .NotBeNull();
            isVolumeValid.ValidationError!.ErrorType
                .Should()
                .Be(ValidationErrorType.ValueIsTooHigh);
            isVolumeValid.ValidationError!.ValidationLimits?.UpperLimit
                .Should()
                .Be(1700);
        }

        [Theory]
        [InlineData(1699.999999)]
        [InlineData(100)]
        [InlineData(1)]
        [InlineData(123)]
        public void WhenVolumeValueIsWithinTheLimit_ShouldReturnTrue(float volume)
        {
            ShipFasterPackageDetailsValidator validator = new();

            PackageDimensions packageDimensions = new(volume);
            ValidationResult isVolumeValid = validator.IsVolumeValid(packageDimensions);

            isVolumeValid.IsValid.Should().BeTrue("volume value is within the limit for this courier");
            isVolumeValid.ValidationError.Should().BeNull();
        }
    }
}
