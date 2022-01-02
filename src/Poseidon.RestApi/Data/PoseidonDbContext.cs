﻿using Microsoft.EntityFrameworkCore;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.CurvePoints;
using Poseidon.RestApi.Ratings;
using Poseidon.RestApi.Rules;
using Poseidon.RestApi.Trades;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    public class PoseidonDbContext : DbContext
    {
        private readonly PoseidonDbContextConfiguration _config;

        public DbSet<BidEntity> Bids { get; set; } = null!;
        public DbSet<TradeEntity> Trades { get; set; } = null!;
        public DbSet<CurvePointEntity> CurvePoints { get; set; } = null!;
        public DbSet<RatingEntity> Ratings { get; set; } = null!;
        public DbSet<RuleEntity> Rules { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        public PoseidonDbContext(DbContextOptions<PoseidonDbContext> options,
            PoseidonDbContextConfiguration config)
            : base(options)
        {
            this._config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._config.ConnectionString);
        }

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