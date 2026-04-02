using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class Double_MinValueTests
    {
        private ValidationResult? Validate(double? value, double min)
        {
            var attr = new Double_MinValueAttribute(null, min);
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsAboveMin()
        {
            var result = Validate(10, 0);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null, 0);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsEqualToMin()
        {
            var result = Validate(0, 0);
            Assert.NotNull(result);
            Assert.Contains(Double_MinValueAttribute.ErrorCodeBelowMin, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenValueIsBelowMin()
        {
            var result = Validate(-1, 0);
            Assert.NotNull(result);
            Assert.Contains(Double_MinValueAttribute.ErrorCodeBelowMin, result!.MemberNames);
        }
    }
}