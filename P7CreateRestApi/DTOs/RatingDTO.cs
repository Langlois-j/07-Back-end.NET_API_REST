using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
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
