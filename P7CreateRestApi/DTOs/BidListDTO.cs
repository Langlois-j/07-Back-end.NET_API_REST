using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.DTOs
{
    public class BidListDTO
    {
        
        public int Id { get; set; }
        [String_NotWhiteSpaceAttribute]
        public string? Account { get; set; }
        [String_NotWhiteSpaceAttribute]
        public string? BidType { get; set; }
        [Double_Positive]
        [Double_MinValue(null, 0)]
        public double? BidQuantity { get; set; }
        //public double? AskQuantity { get; set; }
        //public double? Bid { get; set; }
        //public double? Ask { get; set; }
        //public string? Benchmark { get; set; }
        //public DateTime? BidListDate { get; set; }
        //public string? Commentary { get; set; }
        //public string? BidSecurity { get; set; }
        //public string? BidStatus { get; set; }
        //public string? Trader { get; set; }
        //public string? Book { get; set; }   
        //public string? DealName { get; set; }
        //public string? DealType { get; set; }
        //public string? Side { get; set; }
    }
}
