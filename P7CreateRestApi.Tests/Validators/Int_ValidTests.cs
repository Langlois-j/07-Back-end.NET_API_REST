using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class Int_ValidTests
    {
        private ValidationResult? Validate(int? value, int min = int.MinValue, int max = int.MaxValue)
        {
            var attr = new Int_ValidAttribute(null, min, max);
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsInRange()
        {
            var result = Validate(50, 0, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsMin()
        {
            var result = Validate(0, 0, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsMax()
        {
            var result = Validate(100, 0, 100);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsOutOfRange()
        {
            var result = Validate(150, 0, 100);
            Assert.NotNull(result);
            Assert.Contains(Int_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }
    }
}