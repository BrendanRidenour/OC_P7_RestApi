namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointEntity : Internal.EntityBase
    {
        public int CurveId { get; set; }
        public DateTimeOffset AsOfDate { get; set; }
        public double Term { get; set; }
        public double Value { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}