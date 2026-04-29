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
        [Fact]
        public void ShouldFail_WhenValueIsBelowMin()
        {
            var result = Validate(-1, 0, 100);
            Assert.NotNull(result);
            Assert.Contains(Int_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenValueIsAboveMax()
        {
            var result = Validate(101, 0, 100);
            Assert.NotNull(result);
            Assert.Contains(Int_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }

        [Fact]
        public void ShouldPass_WhenMinEqualsMax()
        {
            var result = Validate(5, 5, 5);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenMinEqualsMaxAndValueDiffers()
        {
            var result = Validate(6, 5, 5);
            Assert.NotNull(result);
            Assert.Contains(Int_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenValueIsZeroAndMinIsOne()
        {
            var result = Validate(0, 1, 100);
            Assert.NotNull(result);
            Assert.Contains(Int_ValidAttribute.ErrorCodeOutOfRange, result!.MemberNames);
        }

        [Fact]
        public void ShouldPass_WhenValueIsIntMinValue()
        {
            var result = Validate(int.MinValue);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsIntMaxValue()
        {
            var result = Validate(int.MaxValue);
            Assert.Equal(ValidationResult.Success, result);
        }
    }
}