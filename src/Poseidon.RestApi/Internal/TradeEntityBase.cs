using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Internal
{
    public abstract class TradeEntityBase : EntityBase
    {
        [Required]
        public string Account { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        public string? Benchmark { get; set; }
        public string? Security { get; set; }
        public string? Status { get; set; }
        public string? Trader { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public string? RevisionName { get; set; }
        public DateTimeOffset? RevisionDate { get; set; }
        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }
}