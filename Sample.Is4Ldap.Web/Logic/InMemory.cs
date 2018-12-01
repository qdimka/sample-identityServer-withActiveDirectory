using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Sample.Is4Ldap.Web.Logic
{
    public static class InMemory
    {
        public static List<Client> Clients => new List<Client>()
        {
            new Client()
            {
                ClientId = "rocket",
                ClientName = "rocket",
                RequireClientSecret = false,
                ClientSecrets = new List<Secret>()
                {
                    new Secret("rocket")
                },
                AllowedScopes = new List<string>()
                {
                    "openid"
                },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = new List<string>()
                {
                    "http://localhost:3000/_oauth/is4"
                }
            }
        };

        public static List<IdentityResource> IdentityResources => new List<IdentityResource>()
        {
            new IdentityResources.OpenId()
        };

        public static List<ApiResource> ApiResources => new List<ApiResource>()
        {

        };
    }
}
