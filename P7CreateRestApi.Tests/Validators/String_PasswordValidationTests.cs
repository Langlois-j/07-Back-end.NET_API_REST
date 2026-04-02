using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class String_PasswordValidationTests
    {
        private ValidationResult? Validate(string? value)
        {
            var attr = new String_PasswordValidationAttribute();
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenPasswordIsValid()
        {
            var result = Validate("Test@1234");
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenPasswordIsEmpty()
        {
            var result = Validate("");
            Assert.NotNull(result);
            Assert.Contains(String_PasswordValidationAttribute.ErrorCodeRequired, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenPasswordTooShort()
        {
            var result = Validate("T@1");
            Assert.NotNull(result);
            Assert.Contains(String_PasswordValidationAttribute.ErrorCodeMinLength, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenNoUpperCase()
        {
            var result = Validate("test@1234");
            Assert.NotNull(result);
            Assert.Contains(String_PasswordValidationAttribute.ErrorCodeUpperCase, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenNoDigit()
        {
            var result = Validate("Test@abcd");
            Assert.NotNull(result);
            Assert.Contains(String_PasswordValidationAttribute.ErrorCodeDigit, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenNoSymbol()
        {
            var result = Validate("Test1234");
            Assert.NotNull(result);
            Assert.Contains(String_PasswordValidationAttribute.ErrorCodeSymbol, result!.MemberNames);
        }
    }
}