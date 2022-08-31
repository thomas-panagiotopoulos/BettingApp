using BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations;
using BettingApp.Services.Sportbook.API.Model;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure
{
    public class SportsbookContext : DbContext, IUnitOfWork
    {
        public SportsbookContext(DbContextOptions<SportsbookContext> options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<PossiblePick> PossiblePicks { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MatchEntityTypeConfiguration());
            builder.ApplyConfiguration(new PossiblePickEntityTypeConfiguration());
            builder.ApplyConfiguration(new MatchResultEntityTypeConfiguration());
            builder.ApplyConfiguration(new RequirementTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new LeagueEntityTypeConfiguration());
        }
    }
}
