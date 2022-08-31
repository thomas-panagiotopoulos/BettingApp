using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Identity.API.Configuration
{
    public class Config
    {
        private readonly IOptions<AppSettings> _settings;

        public Config(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new IdentityResource[]
            {
                //the claims that IdentityResources contain are included in the id_token
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> GetApiScopes =>
           new ApiScope[]
           {
               //the API scopes the Identity Server can provide (we provide an Email claim along 
               //with the ApiResource in order to include it in the access_token
               new ApiScope("sportsbook","Sportsbook Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("betslips","Betslips Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("betting","Betting Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("wallets","Wallets Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("notifications","Notifications Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("sportsbook.signalrhub","Sportsbook Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("betslips.signalrhub","Betslips Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email }),
               new ApiScope("betting.signalrhub","Betting Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email }),

           };

        public static IEnumerable<ApiResource> GetApiResources =>
            new ApiResource[]
            {
                new ApiResource("sportsbook","Sportsbook Service", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    // to add the API aud scope to the token - other solution is to set ValidateAudience = false
                    // in the API authentication setup
                    Scopes = new []{ "sportsbook" } 
                },
                new ApiResource("betslips","Betslips Service", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "betslips" }
                },
                new ApiResource("betting","Betting Service", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "betting" }
                },
                new ApiResource("wallets","Wallets Service", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "wallets" }
                },
                new ApiResource("notifications","Notifications Service", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "notifications" }
                },
                new ApiResource("sportsbook.signalrhub","Sportsbook Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "sportsbook.signalrhub" }
                },
                 new ApiResource("betslips.signalrhub","Betslips Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "betslips.signalrhub" }
                },
                 new ApiResource("betting.signalrhub","Betting Signalr Hub", new string[] { IdentityServerConstants.StandardScopes.Email })
                {
                    Scopes = new []{ "betting.signalrhub" }
                },
            };

        public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
           new Client[]
           {
               // razor pages client
               new Client
               {
                   ClientId = "razor_pages_pkce_client",
                   ClientSecrets = { new Secret("52d8bc589817f14b349b146287d141ad".Sha256()) },

                   ClientUri = $"{configuration.GetValue<string>("WebRazorPagesClient")}", // public uri of the client

                   AllowedGrantTypes = GrantTypes.Code, // for Authorization code flow

                   RequirePkce = true,
                   AllowPlainTextPkce = false,

                   AllowAccessTokensViaBrowser = false, // only for Implicit or Hyblid flow
                   RequireConsent = false,
                   AlwaysIncludeUserClaimsInIdToken = true,  // Include all the claims in id_token, also increases id_token's size

                   // where to redirect to after login
                   RedirectUris = { $"{configuration.GetValue<string>("WebRazorPagesClient")}/signin-oidc" },

                   // where to redirect to after logout
                   PostLogoutRedirectUris = { $"{configuration.GetValue<string>("WebRazorPagesClient")}/signout-callback-oidc" } ,

                   AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "sportsbook",
                        "betslips",
                        "betting",
                        "wallets",
                        "notifications",
                        "sportsbook.signalrhub",
                        "betslips.signalrhub",
                        "betting.signalrhub",
                    },
                   AccessTokenLifetime = 60*60*2, // 2 hours
                   IdentityTokenLifetime= 60*60*2 // 2 hours
               },

           };
    }
}
