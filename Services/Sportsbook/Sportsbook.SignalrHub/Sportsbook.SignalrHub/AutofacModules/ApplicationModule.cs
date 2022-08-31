using Autofac;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Sportsbook.SignalrHub.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportsbook.SignalrHub.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // integration event handlers
            builder.RegisterAssemblyTypes(typeof(SportsbookMatchCurrentMinuteChangedIntegrationEvent).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
