using BettingApp.WebApps.WebRazorPages.Infrastructure;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages
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
            // if WebRazorPages is running on docker-compose, we Set data protection application discriminators in order to
            // prevent cookie confusion when applications are hosted at the same domain and path. For example,
            // under default settings, WebRazorPages may attempt to decrypt Identity.API's antiforgery cookie
            // rather than its own.
            if (Configuration.GetValue<bool>("IsContainerEnv") == true)
            {
                services.AddDataProtection(opts =>
                {
                    opts.ApplicationDiscriminator = "bettingapp.webrazorpages";
                })
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            }

            services.AddCustomRazorPages(Configuration)
                    .AddHttpClientServices(Configuration)
                    .AddCustomAuthentication(Configuration);

            //IdentityModelEventSource.ShowPII = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // Fix samesite issue when running bettingapp from docker-compose locally as by default http protocol is being used
            // Refer to https://github.com/dotnet-architecture/eShopOnContainers/issues/1391
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax });

            app.UseRouting();
            app.UseSession();

            // This addition fixes the case when we post a decimal number in a form and the WebApp erases the
            // decimal separator resulting in a integer number being received, instead of a decimal.
            // This happens due to the different CultureInfo between the browser and web application, because
            // one of them uses a comma (,) an the other one a dot (.) as a decimal part separator.
            app.Use(async (context, next) =>
            {
                var currentThreadCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                currentThreadCulture.NumberFormat = NumberFormatInfo.InvariantInfo;
            
                Thread.CurrentThread.CurrentCulture = currentThreadCulture;
                Thread.CurrentThread.CurrentUICulture = currentThreadCulture;
            
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }

    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomRazorPages(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<AppSettings>(configuration);
            services.AddSession();

            services.AddAutoMapper(typeof(Startup));
            services.AddRazorPages();

            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //add http client services
            services.AddHttpClient<ISportsbookService, SportsbookService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>();

            services.AddHttpClient<IBetslipsService, BetslipsService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>();

            services.AddHttpClient<IBettingService, BettingService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy());  // We use a retry policy when a NotFound response is received,
                                                          // for the cases where we check if a Bet creation request exists
                                                          // and need to wait for it to be created

            services.AddHttpClient<IWalletsService, WalletsService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy());  // We use a retry policy when a NotFound response is received,
                                                          // for the cases where we check if a TopUp/Withdraw request exists
                                                          // and need to wait for it to be created

            services.AddHttpClient<INotificationsService, NotificationsService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            //add custom application services
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // Add Authentication services          

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Cookies
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Bearer
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; //OpenIdConnect
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, 
                        setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl;

                options.ClientId = "razor_pages_pkce_client";
                options.ClientSecret = "52d8bc589817f14b349b146287d141ad";
                options.ResponseType = "code";

                options.SignedOutRedirectUri = "/";
                options.ResponseMode = "form_post";
                options.CallbackPath = "/signin-oidc";

                // Enable PKCE (authorization code flow only)
                options.UsePkce = true;


                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true; // makes a second trip to load claim into the cookie but id_token's size is smaller
                options.RequireHttpsMetadata = false;
               
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("sportsbook");
                options.Scope.Add("betslips");
                options.Scope.Add("betting");
                options.Scope.Add("wallets");
                options.Scope.Add("notifications");
                options.Scope.Add("sportsbook.signalrhub");
                options.Scope.Add("betslips.signalrhub");
                options.Scope.Add("betting.signalrhub");

            });

            return services;
        }
    }
}
