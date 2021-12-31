using System.Text.Json.Serialization;

namespace Poseidon.RestApi.Trades
{
    public class TradeEntity : Internal.TradeEntityBase
    {
        [JsonPropertyName("TradeId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTimeOffset? TradeDate { get; set; }
    }
}