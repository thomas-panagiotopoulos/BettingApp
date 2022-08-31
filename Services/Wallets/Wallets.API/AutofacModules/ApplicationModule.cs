using Autofac;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Wallets.API.Infrastructure.Repositories;
using BettingApp.Services.Wallets.API.IntegrationEvents.Events;
using BettingApp.Services.Wallets.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // repositories
            builder.RegisterType<WalletsRepository>()
                .As<IWalletsRepository>()
                .InstancePerLifetimeScope();

            // integration event handlers
            builder.RegisterAssemblyTypes(typeof(BetStartedIntegrationEvent).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
