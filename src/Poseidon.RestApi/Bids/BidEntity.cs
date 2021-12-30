#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.Text.Json.Serialization;

namespace Poseidon.RestApi.Bids
{
    public class BidEntity : Internal.TradeEntityBase
    {
        [JsonPropertyName("BidListId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public double BidQuantity { get; set; }
        public double AskQuantity { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public DateTimeOffset BidListDate { get; set; }
        public string Commentary { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.