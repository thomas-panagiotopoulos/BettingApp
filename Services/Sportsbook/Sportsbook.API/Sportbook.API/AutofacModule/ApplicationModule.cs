using Autofac;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Sportbook.API.Infrastructure.Repositories;
using BettingApp.Services.Sportbook.API.IntegrationEvents.Events;
using BettingApp.Services.Sportbook.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.AutofacModule
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // repositories
            builder.RegisterType<SportsbookRepository>()
                .As<ISportsbookRepository>()
                .InstancePerLifetimeScope();

            // integration event handlers
            builder.RegisterAssemblyTypes(typeof(MatchCreatedIntegrationEvent).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
