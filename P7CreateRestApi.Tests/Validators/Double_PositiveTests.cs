using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class Double_PositiveTests
    {
        private ValidationResult? Validate(double? value)
        {
            var attr = new Double_PositiveAttribute();
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsPositive()
        {
            var result = Validate(10.5);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsZero()
        {
            var result = Validate(0);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsNegative()
        {
            var result = Validate(-1.5);
            Assert.NotNull(result);
            Assert.Contains(Double_PositiveAttribute.ErrorCodeNegative, result!.MemberNames);
        }
    }
}