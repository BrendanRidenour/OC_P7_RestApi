using Microsoft.EntityFrameworkCore;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.CurvePoints;
using Poseidon.RestApi.Ratings;
using Poseidon.RestApi.Rules;
using Poseidon.RestApi.Trades;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// The class used to interact with the Entity Framework database
    /// </summary>
    public class PoseidonDbContext : DbContext
    {
        public DbSet<BidEntity> Bids { get; set; } = null!;
        public DbSet<TradeEntity> Trades { get; set; } = null!;
        public DbSet<CurvePointEntity> CurvePoints { get; set; } = null!;
        public DbSet<RatingEntity> Ratings { get; set; } = null!;
        public DbSet<RuleEntity> Rules { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="options">The options required for operation</param>
        public PoseidonDbContext(DbContextOptions<PoseidonDbContext> options) : base(options) { }
        
        /// <summary>
        /// Applies configuration to the given <paramref name="modelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The builder to configure</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BidEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TradeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurvePointEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RatingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RuleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}