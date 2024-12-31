using System.Security.Principal;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace netShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("netShop", "netShop Server"),
        new ApiScope(name: "read", "Read data."),
        new ApiScope(name: "write", "write data."),
        new ApiScope(name: "delete", "Delete data."),
    };


    public static IEnumerable<Client> clients => new List<Client>
    {
        // cliente genérico
        new Client
        {
            ClientId = "client",
            ClientSecrets = { new Secret("123243231".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"red", "write", "profile"}
        },
        // aplicação web
        new Client
        {
            ClientId = "netShop",
            ClientSecrets = { new Secret("123243231".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = {"https://localhost:7165/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:7165/signin-callback-oidc" },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "netShop"
            }

        }

    };
}
