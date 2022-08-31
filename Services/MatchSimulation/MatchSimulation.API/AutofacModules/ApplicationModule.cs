using Autofac;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using BettingApp.Services.MatchSimulation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // repositories
            builder.RegisterType<SimulationRepository>()
                .As<ISimulationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClubRepository>()
                .As<IClubRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MatchRepository>()
                .As<IMatchRepository>()
                .InstancePerLifetimeScope();

            // integration event handlers
            //builder.RegisterAssemblyTypes(typeof(BetStartedIntegrationEvent).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
