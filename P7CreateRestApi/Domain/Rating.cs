using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class Rating : BaseEntity
    {
        [String_NotWhiteSpace]
        public string? MoodysRating { get; set; }

        [String_NotWhiteSpace]
        public string? SandPRating { get; set; }

        [String_NotWhiteSpace]
        public string? FitchRating { get; set; }

        [Byte_Valid]
        public byte? OrderNumber { get; set; }
    }
}