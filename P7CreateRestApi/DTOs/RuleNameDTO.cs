using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.DTOs
{
    public class RuleNameDTO
    {
        public int Id { get; set; }
        [Required]
        [String_NotWhiteSpace]
        public string? Name { get; set; }
        [Required]
        [String_NotWhiteSpace]
        public string? Description { get; set; }
        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }
    }
}
