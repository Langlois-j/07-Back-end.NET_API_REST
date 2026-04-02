using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class Double_MaxValueTests
    {
        private ValidationResult? Validate(double? value, double max)
        {
            var attr = new Double_MaxValueAttribute(null, max);
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsBelowMax()
        {
            var result = Validate(50, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsEqualToMax()
        {
            var result = Validate(100, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsAboveMax()
        {
            var result = Validate(150, 100);
            Assert.NotNull(result);
            Assert.Contains(Double_MaxValueAttribute.ErrorCodeAboveMax, result!.MemberNames);
        }
    }
}