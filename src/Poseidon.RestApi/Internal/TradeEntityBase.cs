#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Poseidon.RestApi.Internal
{
    public abstract class TradeEntityBase : EntityBase
    {
        public string Account { get; set; }
        public string Type { get; set; }
        public string Benchmark { get; set; }
        public string Security { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public string Book { get; set; }
        public string CreationName { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string RevisionName { get; set; }
        public DateTimeOffset RevisionDate { get; set; }
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.