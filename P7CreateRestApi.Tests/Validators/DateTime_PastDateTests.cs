using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace P7CreateRestApi.Tests.Validators
{
    public class DateTime_PastDateTests
    {
        private ValidationResult? Validate(DateTime? value, bool allowToday = true)
        {
            var attr = new DateTime_PastAttribute(null, allowToday);
            return attr.GetValidationResult(value, new ValidationContext(new { }));
        }

        [Fact]
        public void ShouldPass_WhenValueIsInPast()
        {
            var result = Validate(DateTime.UtcNow.AddDays(-1));
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsNull()
        {
            var result = Validate(null);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldPass_WhenValueIsToday_AndAllowTodayIsTrue()
        {
            var result = Validate(DateTime.UtcNow, allowToday: true);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void ShouldFail_WhenValueIsToday_AndAllowTodayIsFalse()
        {
            var result = Validate(DateTime.UtcNow, allowToday: false);
            Assert.NotNull(result);
            Assert.Contains(DateTime_PastAttribute.ErrorCodeFutureDate, result!.MemberNames);
        }

        [Fact]
        public void ShouldFail_WhenValueIsInFuture()
        {
            var result = Validate(DateTime.UtcNow.AddDays(1));
            Assert.NotNull(result);
            Assert.Contains(DateTime_PastAttribute.ErrorCodeFutureDate, result!.MemberNames);
        }
    }
}