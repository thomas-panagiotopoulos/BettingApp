using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity
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
            //services.AddControllersWithViews();
            
            services.AddRazorPages();

            services.AddDbContext<AppDbContext>(config => 
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // AddIdentity registers the services
            services.AddIdentity<AppUser, IdentityRole>(config => 
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
                config.Lockout.AllowedForNewUsers = true;

                // User settings.
                config.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                config.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                // Cookie settings
                config.Cookie.Name = "IndentityServer.Cookie";
                config.LoginPath = "/Auth/Signin";
                config.LogoutPath = "/Auth/Signout";
                
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                config.SlidingExpiration = true;
            });

            // AddIdentityServer
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources)
                .AddInMemoryApiScopes(Config.GetApiScopes)
                .AddInMemoryClients(Config.GetClients)
                .AddAspNetIdentity<AppUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

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

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
