using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGw
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
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = identityUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "sportsbook", "betslips", "betting", "wallets", "notifications, sportsbook.signalrhub, betslips.signalrhub, betting.signalrhub" }
                    };
                    x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            int i = 0;
                        },
                        OnTokenValidated = async ctx =>
                        {
                            int i = 0;
                        },

                        OnMessageReceived = async ctx =>
                        {
                            int i = 0;
                        }
                    };
                });

            var ocelotBuilder = services.AddOcelot(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseOcelot().Wait();
        }
    }
}
