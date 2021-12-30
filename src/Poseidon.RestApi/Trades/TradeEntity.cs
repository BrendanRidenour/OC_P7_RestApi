#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.Text.Json.Serialization;

namespace Poseidon.RestApi.Trades
{
    public class TradeEntity : Internal.TradeEntityBase
    {
        [JsonPropertyName("TradeId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public double BuyQuantity { get; set; }
        public double SellQuantity { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public DateTimeOffset TradeDate { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.