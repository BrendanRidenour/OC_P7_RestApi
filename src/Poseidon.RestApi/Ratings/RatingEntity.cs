using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Ratings
{
    public class RatingEntity : EntityBase, IEntityBasePropertyCopy<RatingEntity>
    {
        public string? MoodysRating { get; set; }
        public string? SandPRating { get; set; }
        public string? FitchRating { get; set; }
        public int? OrderNumber { get; set; }

        public void CopyProperties(RatingEntity entity)
        {
            MoodysRating = entity.MoodysRating;
            SandPRating = entity.SandPRating;
            FitchRating = entity.FitchRating;
            OrderNumber = entity.OrderNumber;
        }
    }
}