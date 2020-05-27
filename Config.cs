// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace devIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "http://192.168.99.100:9003/signin-oidc","http://192.168.99.100:9002/signin-oidc" },
                    FrontChannelLogoutUri = "http://192.168.99.100:9003/signout-oidc",
                    PostLogoutRedirectUris = { "http://192.168.99.100:9003/signout-callback-oidc", "http://192.168.99.100:9002/signout-callback-oidc"},

                    AllowedScopes = { "openid", "profile", "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                //postman client copy of mvc with new name and callback
                                new Client
                {
                    ClientId = "postman",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                    //FrontChannelLogoutUri = "http://10.1.1.9:9003/signout-oidc",
                    //PostLogoutRedirectUris = { "http://10.1.1.9:9003/signout-callback-oidc", "http://10.1.1.9:9002/signout-callback-oidc"},

                    AllowedScopes = { "openid", "profile", "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
    }
}