using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class Byte_ValidTests
    {
        private ValidationResult? Validate(byte? value, byte min = 0, byte max = 255)
        {
            var attr = new Byte_ValidAttribute(null,min, max);
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsInRange()
        {
            var result = Validate(128);
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
            var result = Validate(0);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsMax()
        {
            var result = Validate(255);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsOutOfRange()
        {
            var result = Validate(200, 0, 100);
            Assert.NotNull(result);
            Assert.Contains(Byte_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }
    }
}