using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.DTOs
{
    public class RuleNameDTO
    {
        public int Id { get; set; }
        [String_NotWhiteSpace]
        public string? Name { get; set; }
        [String_NotWhiteSpace]
        public string? Description { get; set; }
        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }
    }
}
