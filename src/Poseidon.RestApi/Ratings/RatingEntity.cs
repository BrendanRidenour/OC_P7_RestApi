using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Ratings
{
    /// <summary>
    /// Represents a <see cref="RatingEntity" /> model class
    /// </summary>
    public class RatingEntity : EntityBase, IEntityBasePropertyCopy<RatingEntity>
    {
        public string? MoodysRating { get; set; }
        public string? SandPRating { get; set; }
        public string? FitchRating { get; set; }
        public int? OrderNumber { get; set; }

        /// <inheritdoc />
        public void CopyProperties(RatingEntity entity)
        {
            MoodysRating = entity.MoodysRating;
            SandPRating = entity.SandPRating;
            FitchRating = entity.FitchRating;
            OrderNumber = entity.OrderNumber;
        }
    }
}