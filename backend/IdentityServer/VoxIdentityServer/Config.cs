using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace VoxIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
             new IdentityResource( 
            name: "roles",
            userClaims: new[] { JwtClaimTypes.Role }
        )
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("VoxelGallery.Api"),
            new ApiScope("VoxelGallery.Api.m2m"),
        };
        

    public static IEnumerable<Client> Clients(IConfiguration config) =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m-client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(config["m2msecret"].Sha256()) },

                AllowedScopes = { "VoxelGallery.Api.m2m" }
            },
            //postman
             new Client
            {
                ClientId = "postman",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(config["postmansecret"].Sha256()) },

                AllowedScopes = { "VoxelGallery.Api","VoxelGallery.Api.m2m" }
            },
            new Client {
                ClientId = "webapp-client",
                ClientSecrets = {new Secret(config["webapp-secret"].Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "VoxelGallery.Api",
                    "roles"
                },
                
                RedirectUris = [config
            .GetValue<string>("HttpsFrontend")],
                PostLogoutRedirectUris = [config
            .GetValue<string>("HttpsFrontend")],
            
                AllowedCorsOrigins = [config
            .GetValue<string>("HttpsFrontend")],
              
            }
           
        };
        

        
}
