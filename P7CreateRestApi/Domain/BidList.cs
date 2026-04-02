using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class BidList : BaseEntity
    {

        [String_NotWhiteSpaceAttribute]
        public string? Account { get; set; }

        [String_NotWhiteSpaceAttribute]
        public string? BidType { get; set; }

        [Double_Positive]
        [Double_MinValue(null,0)]
        public double? BidQuantity { get; set; }

        [Double_Positive]
        public double? AskQuantity { get; set; }

        [Double_Positive]
        public double? Bid { get; set; }

        [Double_Positive]
        public double? Ask { get; set; }

        public string? Benchmark { get; set; }

        [DateTime_FutureAttribute]
        public DateTime? BidListDate { get; set; }

        public string? Commentary { get; set; }
        public string? BidSecurity { get; set; }
        public string? BidStatus { get; set; }
        public string? Trader { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }

        [DateTime_Past]
        public DateTime? CreationDate { get; set; }

        public string? RevisionName { get; set; }

        [DateTime_FutureAttribute]
        public DateTime? RevisionDate { get; set; }

        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }
}