using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.DataModel.Entities;
using VoxelGallery.Persistence.Interfaces;

namespace VoxelGallery.Persistence;

public class FavoriteRepo(VoxDatabaseContext dbContext) : IFavoriteRepo
{
    public async Task<bool?> AddFavorite(long sceneID, string userIdentity)
    {
         var user = await dbContext.Users.Include(x=>x.Favorites).Where(x=>x.IdentityServerId==userIdentity).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("user isn't in database");
        }
        var scene = dbContext.Scenes.Where(x=>x.Id==sceneID).First();
         if (scene == null)
        {
            return null;
        }
        user.Favorites.Add(scene);
        dbContext.Users.Update(user);
       await dbContext.SaveChangesAsync();
       return true;
    }

    public async Task<List<long>> GetAllFavoriteIDs(string userIdentity)
    {
        var users = await dbContext.Users.Include(x=>x.Favorites).Where(x=>x.IdentityServerId==userIdentity).ToListAsync();
        var user = users.First();
        return  users.First().Favorites.Select(x=>x.Id).ToList();
    }

    public async Task<bool?> RemoveFavorite(long sceneID, string userIdentity)
    {
        var user = await dbContext.Users.Include(x=>x.Favorites).Where(x=>x.IdentityServerId==userIdentity).FirstOrDefaultAsync();
         if (user == null)
        {
            throw new Exception("user isn't in database");
        }
        var scene = dbContext.Scenes.Where(x=>x.Id==sceneID).First();
         if (scene == null)
        {
            return null;
        }
        user.Favorites.Remove(scene);
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return false;
    }

    public async Task<bool?> SetFavoriteStatus(bool isFavorite, long sceneID, string userIdentity)
    {
        if (isFavorite)
        {
            return await AddFavorite( sceneID,  userIdentity);
        }
        else
        {
            return await RemoveFavorite( sceneID,  userIdentity);
        }
    }
}
