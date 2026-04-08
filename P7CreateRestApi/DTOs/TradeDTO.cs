using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.DTOs
{
    public class TradeDTO
    {
        public int Id { get; set; }
        [String_NotWhiteSpace]
        public string? Account { get; set; }
        [String_NotWhiteSpace]
        public string? AccountType { get; set; }
        [Double_Positive]
        public double? BuyQuantity { get; set; }
        [Double_Positive]
        public double? SellQuantity { get; set; }
        [Double_Positive]
        public double? BuyPrice { get; set; }
        [Double_Positive]
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        public string? TradeSecurity { get; set; }
        public string? TradeStatus { get; set; }
        public string? Trader { get; set; }
        public string? Benchmark { get; set; }
        public string? Book { get; set; }
        public string? RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }
}
