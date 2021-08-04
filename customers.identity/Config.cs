using IdentityServer4.Models;
using System.Collections.Generic;

namespace customers.identity
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("customers.api", "Customers API")
            };

        public static IEnumerable<Client> Clients =>
new List<Client>
{
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("customers.api.secret".Sha256())
                    },

                    AllowedScopes = { "customers.api" }
                }
};

        public static IEnumerable<IdentityResource> IdentityResources =>
                    new IdentityResource[]
    {
                new IdentityResources.OpenId()
    };
    }
}