using Autofac;
using BettingApp.BuildingBlocks.EventBus;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.BuildingBlocks.EventBusRabbitMQ;
using BettingApp.Services.Payments.API;
using BettingApp.Services.Payments.API.AutofacModules;
using BettingApp.Services.Payments.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Payments.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }  

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PaymentsSettings>(Configuration)
                    .AddCustomIntegrations(Configuration)
                    .AddEventBus(Configuration);

            // after this, ConfigureContainer will be invoked (see below)
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. 
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac
            builder.RegisterModule(new ApplicationModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/home/index", async context =>
                {
                    await context.Response.WriteAsync("Welcome to Payment!");
                });
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserRequestedTopUpIntegrationEvent, IIntegrationEventHandler<UserRequestedTopUpIntegrationEvent>>();
            eventBus.Subscribe<UserRequestedWithdrawIntegrationEvent, IIntegrationEventHandler<UserRequestedWithdrawIntegrationEvent>>();
            eventBus.Subscribe<ApplyTopUpSucceededIntegrationEvent, IIntegrationEventHandler<ApplyTopUpSucceededIntegrationEvent>>();
            eventBus.Subscribe<ApplyTopUpFailedIntegrationEvent, IIntegrationEventHandler<ApplyTopUpFailedIntegrationEvent>>();
            eventBus.Subscribe<ApplyWithdrawSucceededIntegrationEvent, IIntegrationEventHandler<ApplyWithdrawSucceededIntegrationEvent>>();
            eventBus.Subscribe<ApplyWithdrawFailedIntegrationEvent, IIntegrationEventHandler<ApplyWithdrawFailedIntegrationEvent>>();

        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            }
            );

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            }
            );

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
