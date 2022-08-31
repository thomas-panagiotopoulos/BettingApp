using Autofac;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using BettingApp.Services.Betslips.Infrastructure.Idempotency;
using BettingApp.Services.Betslips.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // repositories
            builder.RegisterType<BetslipRepository>()
                .As<IBetslipRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WalletRepository>()
                .As<IWalletRepository>()
                .InstancePerLifetimeScope();

            // request manager
            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            // integration event handlers
            builder.RegisterAssemblyTypes(typeof(CreateBetslipCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
