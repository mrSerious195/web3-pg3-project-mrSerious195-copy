using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.DataModel.Entities;
using VoxelGallery.Persistence.Interfaces;

namespace VoxelGallery.Persistence;

public class LikeRepo(VoxDatabaseContext dbContext) : ILikeRepo
{
    public async Task AddLike(long sceneID, string userIdentity)
    {
        var scene = await dbContext.Scenes.Include(x=>x.Likes).Where(x=>x.Id==sceneID).SingleAsync();
         var user = await dbContext.Users.Where(x=>x.IdentityServerId==userIdentity).SingleAsync();
         scene.Likes.Add(user);
          dbContext.Scenes.Update(scene);
        await dbContext.SaveChangesAsync(); 
    }

    public async Task<int> GetLikeCount(long sceneID)
    {
         var scene = await dbContext.Scenes.Include(x=>x.Likes).Where(x=>x.Id==sceneID).SingleAsync();
         var count = scene.Likes.Count(); 
         return count;
    }

    public async Task RemoveLike(long sceneID, string userIdentity)
    {
         var scene = await dbContext.Scenes.Include(x=>x.Likes).Where(x=>x.Id==sceneID).SingleAsync();
        var like = scene.Likes.FirstOrDefault(u=>u.IdentityServerId==userIdentity);
if(like!=null){
         scene.Likes.Remove(like);
          dbContext.Scenes.Update(scene);
         await dbContext.SaveChangesAsync(); 
}
    }

     public async Task<bool> IsLiked(long sceneID, string userIdentity)
    {
         var scene = await dbContext.Scenes.Include(x=>x.Likes).Where(x=>x.Id==sceneID).SingleAsync();
         if(scene==null)
         return false;
         return   scene.Likes.Any(x=>x.IdentityServerId==userIdentity);
          
    }

  
}
