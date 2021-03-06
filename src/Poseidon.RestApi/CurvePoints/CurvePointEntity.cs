using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.CurvePoints
{
    /// <summary>
    /// Represents a <see cref="CurvePointEntity" /> model class
    /// </summary>
    public class CurvePointEntity : EntityBase, IEntityBasePropertyCopy<CurvePointEntity>
    {
        public int? CurveId { get; set; }
        public DateTimeOffset? AsOfDate { get; set; }
        public double? Term { get; set; }
        public double? Value { get; set; }
        public DateTimeOffset? CreationDate { get; set; }

        /// <inheritdoc />
        public void CopyProperties(CurvePointEntity entity)
        {
            CurveId = entity.CurveId;
            AsOfDate = entity.AsOfDate;
            Term = entity.Term;
            Value = entity.Value;
            CreationDate = entity.CreationDate;
        }
    }
}