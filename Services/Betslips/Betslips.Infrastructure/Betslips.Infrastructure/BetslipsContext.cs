using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using BettingApp.Services.Betslips.Domain.Seedwork;
using BettingApp.Services.Betslips.Infrastructure.EntityConfigurations;
using BettingApp.Services.Betslips.Infrastructure.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure
{
    public class BetslipsContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "betslips";
        private readonly IMediator _mediator;

        public DbSet<ClientRequest> ClientRequests { get; set; }
        public DbSet<Betslip> Betslips { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<Wallet> Wallets { get; set; }


        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;


        public BetslipsContext(DbContextOptions<BetslipsContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BetslipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SelectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MatchEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RequirementEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RequirementTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WalletEntityTypeConfiguration());
        }


        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
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
