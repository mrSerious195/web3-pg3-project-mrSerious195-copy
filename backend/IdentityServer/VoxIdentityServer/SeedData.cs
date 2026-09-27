using System.Security.Claims;
using IdentityModel;
using VoxIdentityServer.Data;
using VoxIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Vox.IdentityServer.contracts;

namespace VoxIdentityServer;

public class SeedData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
      /*  using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<VoxUser>>();
            var admin = userMgr.FindByNameAsync("admin").Result;
            if (admin == null)
            {
                admin = new VoxUser
                {
                    UserName = "admin",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(admin, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                   var userForVoxDataServer = new UserRequestContract()
                {
                    IdentityServerId=admin.Id,
                    Name=admin.UserName,
                };
                using(HttpClient httpClient = new()){
               var resultvox2 = await httpClient.PostAsJsonAsync("http://localhost:5029/api/User",userForVoxDataServer);
                
                 if (!resultvox2.IsSuccessStatusCode)
                {
                   await userMgr.DeleteAsync(admin);
                    throw new Exception($"ERROR {resultvox2.StatusCode} [{resultvox2.ReasonPhrase}]");
                }
                }
                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }


       
            var roleManager = scope.ServiceProvider
               .GetRequiredService<RoleManager<IdentityRole>>();

            var role = new IdentityRole()
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            bool doesContextHaveAdminRole = (await roleManager.FindByNameAsync(role.Name)) != null;
            if (doesContextHaveAdminRole)
            {
                Log.Debug("admin role was already in AspRoles");
            }
            else
            {
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("adding the admin role to AspRoles");
            }
            bool isRoleLinked = userMgr.IsInRoleAsync(admin, role.Name).Result;
            if (admin != null && !isRoleLinked)
            {
                var result = userMgr.AddToRoleAsync(admin, role.Name).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }


        }
        using (var scope = app.Services
        .GetRequiredService<IServiceScopeFactory>().CreateScope())
        {

            var context = scope.ServiceProvider
                .GetRequiredService<ConfigurationDbContext>();

            Log.Debug("Overwriting db clients with Config.cs");
            context.Clients.RemoveRange(context.Clients);
            foreach (var client in Config.Clients())
                context.Clients.Add(client.ToEntity());
            context.SaveChanges();
            Log.Debug("Clients overwrite done");

            Log.Debug("Adding IdentityResources");
            foreach (var resource in Config.IdentityResources)
                if (!context.IdentityResources.Any(db =>
                        resource.Name == db.Name))
                    context.IdentityResources.Add(resource.ToEntity());
            context.SaveChanges();
            Log.Debug("Adding IdentityResources done");

            Log.Debug("Adding ApiScopes");
            foreach (var resource in Config.ApiScopes)
                if (!context.ApiScopes.Any(db =>
                        resource.Name == db.Name))
                    context.ApiScopes.Add(resource.ToEntity());
            context.SaveChanges();
            Log.Debug("Adding ApiScopes done");

        }*/
    }
}
