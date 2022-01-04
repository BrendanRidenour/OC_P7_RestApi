using Poseidon.RestApi.Internal;
using System.Text.Json.Serialization;

namespace Poseidon.RestApi.Trades
{
    /// <summary>
    /// Represents a <see cref="BidEntity" /> model class
    /// </summary>
    public class TradeEntity : TradeEntityBase, IEntityBasePropertyCopy<TradeEntity>
    {
        [JsonPropertyName("TradeId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTimeOffset? TradeDate { get; set; }

        /// <inheritdoc />
        public void CopyProperties(TradeEntity entity)
        {
            base.CopyProperties(entity);

            BuyQuantity = entity.BuyQuantity;
            SellQuantity = entity.SellQuantity;
            BuyPrice = entity.BuyPrice;
            SellPrice = entity.SellPrice;
            TradeDate = entity.TradeDate;
        }
    }
}