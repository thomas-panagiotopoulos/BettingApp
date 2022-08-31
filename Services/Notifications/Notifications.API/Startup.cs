using Autofac;
using BettingApp.BuildingBlocks.EventBus;
using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.BuildingBlocks.EventBusRabbitMQ;
using BettingApp.BuildingBlocks.IntegrationEventLogEF;
using BettingApp.BuildingBlocks.IntegrationEventLogEF.Services;
using BettingApp.Services.Notifications.API.AutofacModules;
using BettingApp.Services.Notifications.API.Infrastructure;
using BettingApp.Services.Notifications.API.Infrastructure.Services;
using BettingApp.Services.Notifications.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
                    .AddCustomDbContext(Configuration)
                    .AddCustomIntegrations(Configuration)
                    .AddEventBus(Configuration)
                    .AddCustomAuthentication(Configuration);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<TopUpRequestSucceededIntegrationEvent, IIntegrationEventHandler<TopUpRequestSucceededIntegrationEvent>>();
            eventBus.Subscribe<TopUpRequestFailedIntegrationEvent, IIntegrationEventHandler<TopUpRequestFailedIntegrationEvent>>();
            eventBus.Subscribe<WithdrawRequestSucceededIntegrationEvent, IIntegrationEventHandler<WithdrawRequestSucceededIntegrationEvent>>();
            eventBus.Subscribe<WithdrawRequestFailedIntegrationEvent, IIntegrationEventHandler<WithdrawRequestFailedIntegrationEvent>>();
            eventBus.Subscribe<WelcomeBonusCreditedIntegrationEvent, IIntegrationEventHandler<WelcomeBonusCreditedIntegrationEvent>>();
            //eventBus.Subscribe<BetWinningsCreditedIntegrationEvent, IIntegrationEventHandler<BetWinningsCreditedIntegrationEvent>>();
            //eventBus.Subscribe<BetWageredAmountRefundedIntegrationEvent, IIntegrationEventHandler<BetWageredAmountRefundedIntegrationEvent>>();
            eventBus.Subscribe<BetDetailsChangedIntegrationEvent, IIntegrationEventHandler<BetDetailsChangedIntegrationEvent>>();
            //eventBus.Subscribe<BetRemovedDueToUnpaidCompletionIntegrationEvent, IIntegrationEventHandler<BetRemovedDueToUnpaidCompletionIntegrationEvent>>();
            //eventBus.Subscribe<BetRemovedDueToUnpaidCancelationIntegrationEvent, IIntegrationEventHandler<BetRemovedDueToUnpaidCancelationIntegrationEvent>>();
            eventBus.Subscribe<BetMarkedAsPaidIntegrationEvent, IIntegrationEventHandler<BetMarkedAsPaidIntegrationEvent>>();
            eventBus.Subscribe<BetCompletedAsWonIntegrationEvent, IIntegrationEventHandler<BetCompletedAsWonIntegrationEvent>>();
            eventBus.Subscribe<BetCompletedAsLostIntegrationEvent, IIntegrationEventHandler<BetCompletedAsLostIntegrationEvent>>();
            eventBus.Subscribe<BetCanceledIntegrationEvent, IIntegrationEventHandler<BetCanceledIntegrationEvent>>();
            eventBus.Subscribe<BetCanceledDueToRejectedPaymentIntegrationEvent, IIntegrationEventHandler<BetCanceledDueToRejectedPaymentIntegrationEvent>>();

        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddDbContext<NotificationsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            },
                    ServiceLifetime.Scoped //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            }
            );

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            //services.AddTransient<IWalletsIntegrationEventService, WalletsIntegrationEventService>();

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

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "notifications";
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateAudience = false
                //};
            });

            // adds an authorization policy to make sure the token is for scope 'notifications'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("NotificationsApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "notifications");
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Administrator");
                });
            });

            return services;
        }
    }
}
