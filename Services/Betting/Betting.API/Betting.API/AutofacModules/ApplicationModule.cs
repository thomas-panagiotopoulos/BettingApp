using Autofac;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.Infrastructure.Idempotency;
using BettingApp.Services.Betting.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // repositories
            builder.RegisterType<BetRepository>()
                .As<IBetRepository>()
                .InstancePerLifetimeScope();

            // request manager
            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            // integration event handlers
            builder.RegisterAssemblyTypes(typeof(CreateBetCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
