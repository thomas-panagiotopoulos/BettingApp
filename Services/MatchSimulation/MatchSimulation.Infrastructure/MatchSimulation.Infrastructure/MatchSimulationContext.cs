using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using BettingApp.Services.MatchSimulation.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure
{
    public class MatchSimulationContext : DbContext, IUnitOfWork
    {

        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "matchsimulation";

        // DbSets go here
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<PossiblePick> PossiblePicks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<League> Leagues { get; set; }


        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public MatchSimulationContext(DbContextOptions<MatchSimulationContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SimulationEntityTypeConfiguration());
            builder.ApplyConfiguration(new MatchEntityTypeConfiguration());
            builder.ApplyConfiguration(new ClubEntityTypeConfiguration());
            builder.ApplyConfiguration(new PossiblePickEntityTypeConfiguration());
            builder.ApplyConfiguration(new StatusEntityTypeConfiguration());
            builder.ApplyConfiguration(new MatchResultEntityTypeConfiguration());
            builder.ApplyConfiguration(new RequirementTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new LeagueEntityTypeConfiguration());

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);
            //var result = base.SaveChanges();

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
