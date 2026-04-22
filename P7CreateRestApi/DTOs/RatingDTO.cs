using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        [Required]
        [String_NotWhiteSpace]
        public string? MoodysRating { get; set; }
        [Required]
        [String_NotWhiteSpace]
        public string? SandPRating { get; set; }
        [Required]
        [String_NotWhiteSpace]
        public string? FitchRating { get; set; }
        [Byte_Valid]
        public byte? OrderNumber { get; set; }
    }
}
