using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class String_NotWhiteSpaceTests
    {
        private ValidationResult? Validate(string? value)
        {
            var attr = new String_NotWhiteSpaceAttribute();
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsValid()
        {
            var result = Validate("TestValue");
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsWhiteSpace()
        {
            var result = Validate("   ");
            Assert.NotNull(result);
            Assert.Contains(String_NotWhiteSpaceAttribute.ErrorCodeWhiteSpace, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenValueIsEmpty()
        {
            var result = Validate("");
            Assert.NotNull(result);
            Assert.Contains(String_NotWhiteSpaceAttribute.ErrorCodeEmpty, result!.MemberNames);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null);
            Assert.Equal(ValidationResult.Success, result);
        }
    }
}