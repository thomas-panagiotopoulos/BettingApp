using BettingApp.Services.Identity.API.Configuration;
using BettingApp.Services.Identity.API.Data;
using BettingApp.Services.Identity.API.Models;
using BettingApp.Services.Identity.API.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BettingApp.Services.Identity.API
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
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.AddRazorPages();

            // if Identity.API is running on docker-compose, we set data protection application discriminators in order to
            // prevent cookie confusion when applications are hosted at the same domain and path. For example,
            // under default settings, WebRazorPages may attempt to decrypt Identity.API's antiforgery cookie
            // rather than its own.
            if (Configuration.GetValue<bool>("IsContainerEnv") == true)
            {
                services.AddDataProtection(opts =>
                {
                    opts.ApplicationDiscriminator = "bettingapp.identity";
                })
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            }

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            });

            // Add AspNetIdentity for user store
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                // Password settings.
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireUppercase = true;
                config.Password.RequiredLength = 6;

                // Lockout settings.
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.AllowedForNewUsers = false;

                // User settings.
                config.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                config.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Register other services
            services.AddTransient<ILoginService<ApplicationUser>, LoginService>();
            services.AddTransient<IRedirectService, RedirectService>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "Identity.Cookie";
                options.LoginPath = "/Auth/Signin";
                options.LogoutPath = "/Auth/Signout";

                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

            // Add IdentityServer
            services.AddIdentityServer(options =>
            {
                options.IssuerUri = "null";
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(60);

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
            .AddDeveloperSigningCredential() // not recommended for production - you need to store your key material somewhere secure
            .AddInMemoryIdentityResources(Config.GetIdentityResources)
            .AddInMemoryApiScopes(Config.GetApiScopes)
            .AddInMemoryApiResources(Config.GetApiResources)
            .AddInMemoryClients(Config.GetClients(Configuration))
            .AddAspNetIdentity<ApplicationUser>();
            services.AddTransient<IProfileService, ProfileService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            // Adds IdentityServer
            app.UseIdentityServer();

            // Fix a problem with chrome. Chrome enabled a new feature "Cookies without SameSite must be secure", 
            // the coockies shold be expided from https, but in eShop, the internal comunicacion in aks and docker compose is http.
            // To avoid this problem, the policy of cookies shold be in Lax mode.
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
