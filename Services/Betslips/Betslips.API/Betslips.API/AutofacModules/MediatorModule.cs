using Autofac;
using BettingApp.Services.Betslips.API.Application.Behaviors;
using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.API.Application.DomainEventHandlers;
using BettingApp.Services.Betslips.API.Application.Validations;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateBetslipCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            
            // register the IdentifiedCommandHandler class individually
            builder.RegisterGeneric(typeof(IdentifiedCommandHandler<,>)).As(typeof(IRequestHandler<,>));
             
            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(MatchBetableStatusChangedDomainEventHandler).GetTypeInfo().Assembly)
                 .AsClosedTypesOf(typeof(INotificationHandler<>));
             
            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(CreateBetslipCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
            
            
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
            
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
