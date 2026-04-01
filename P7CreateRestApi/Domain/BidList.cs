using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class BidList : BaseEntity
    {

        [Required(ErrorMessage = "Le compte est obligatoire.")]
        public string? Account { get; set; }

        [Required(ErrorMessage = "Le type est obligatoire.")]
        public string? BidType { get; set; }

        [PositiveDouble]
        [MinValue(0)]
        public double? BidQuantity { get; set; }

        [PositiveDouble]
        public double? AskQuantity { get; set; }

        [PositiveDouble]
        public double? Bid { get; set; }

        [PositiveDouble]
        public double? Ask { get; set; }

        public string? Benchmark { get; set; }

        [ValidDate]
        public DateTime? BidListDate { get; set; }

        public string? Commentary { get; set; }
        public string? BidSecurity { get; set; }
        public string? BidStatus { get; set; }
        public string? Trader { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }

        [ValidDate]
        public DateTime? CreationDate { get; set; }

        public string? RevisionName { get; set; }

        [ValidDate]
        public DateTime? RevisionDate { get; set; }

        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }
}