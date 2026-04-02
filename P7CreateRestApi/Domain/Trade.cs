using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class Trade : BaseEntity
    {
      
        [Required(ErrorMessage = "Le compte est obligatoire.")]
        public string? Account { get; set; }

        [Required(ErrorMessage = "Le type de compte est obligatoire.")]
        public string? AccountType { get; set; }

        [Double_Positive]
        public double? BuyQuantity { get; set; }

        [Double_Positive]
        public double? SellQuantity { get; set; }

        [Double_Positive]
        public double? BuyPrice { get; set; }

        [Double_Positive]
        public double? SellPrice { get; set; }

        [DateTime_FutureAttribute]
        public DateTime? TradeDate { get; set; }

        public string? TradeSecurity { get; set; }
        public string? TradeStatus { get; set; }
        public string? Trader { get; set; }
        public string? Benchmark { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }

        [DateTime_PastAttribute]
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