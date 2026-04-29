using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.DTOs
{
    public class BidListDTO
    {
        
        public int Id { get; set; }
        [Required]
        [String_NotWhiteSpaceAttribute]
        public string? Account { get; set; }
        [Required]
        [String_NotWhiteSpaceAttribute]
        public string? BidType { get; set; }
        [Double_Positive]
        [Double_MinValue(null, 0)]
        public double? BidQuantity { get; set; }

    }
}
