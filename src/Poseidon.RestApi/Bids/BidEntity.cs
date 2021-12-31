using System.Text.Json.Serialization;

namespace Poseidon.RestApi.Bids
{
    public class BidEntity : Internal.TradeEntityBase
    {
        [JsonPropertyName("BidListId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public DateTimeOffset? BidListDate { get; set; }
        public string? Commentary { get; set; }
    }
}