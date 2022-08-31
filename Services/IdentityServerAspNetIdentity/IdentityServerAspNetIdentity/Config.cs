using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity
{
    public class Config
    {
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
               new ApiScope("couponApi","Coupon Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
           };

        public static IEnumerable<ApiResource> GetApiResources =>
            new ApiResource[]
            {
                new ApiResource("couponApi", "Coupon Service", new string[] { IdentityServerConstants.StandardScopes.Email }),
                
            };
        

        public static IEnumerable<Client> GetClients =>
           new Client[]
           {
               // mvc client
               new Client
               {
                   ClientId = "mvc_client",
                   ClientSecrets = { new Secret("mvc_client_secret".Sha256()) },

                   AllowedGrantTypes = GrantTypes.Code,

                   // where to redirect to after login
                   RedirectUris = { "https://localhost:44366/signin-oidc" },

                   // where to redirect to after logout
                   PostLogoutRedirectUris = { "https://localhost:44366/signout-callback-oidc" } ,

                   AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "couponApi",
                    },

                   // Include all the claims in id_token, also increases id_token's size
                   //AlwaysIncludeUserClaimsInIdToken = true,
               },
               new Client
               {
                   ClientId = "oidc_client",

                   AllowedGrantTypes = GrantTypes.Implicit,
                   RedirectUris = {"https://localhost:44367/Signin"},
                   AllowedCorsOrigins = { "https://localhost:44367" },

                   AllowedScopes =
                   {
                       IdentityServerConstants.StandardScopes.OpenId,
                       "couponApi",
                   },

                   AllowAccessTokensViaBrowser = true,
                   RequireConsent = false,
               }
               
           };
    }
}
