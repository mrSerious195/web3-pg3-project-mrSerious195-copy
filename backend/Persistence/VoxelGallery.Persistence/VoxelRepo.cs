using System.Net.Http.Json;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.Interfaces;
using VoxelGallery.Persistence.Mapping;

namespace VoxelGallery.Persistence;

public class VoxelRepo(VoxDatabaseContext dbConnect) : ISceneDataRepo
{
   

    public async Task<SceneModel> AddVoxelScene(SceneModel scene)
    {
   var user = dbConnect.Users.Where(x=>x.IdentityServerId==scene.UserDatabaseId).FirstOrDefault();
var entity = scene.AsEntity();
if(user!=null)
entity.UploaderId=user.Id;
dbConnect.Scenes.Add(entity);


 
await dbConnect.SaveChangesAsync();
//await WriteAsync(scene.VoxelData,GetVoxFileName(entity.Id));
//await WriteAsync(scene.ImgData,GetImgFileName(entity.Id));
var ret = entity.AsModel("");
//ret.VoxelData=scene.VoxelData;
//ret.ImgData=scene.ImgData;
return ret;
    }

    public async Task<List<SceneModel>> GetAllVoxelScenes(int page, int itemsOnAPage, string? searchFor, string userIdentity)
    {
        if (string.IsNullOrWhiteSpace(searchFor))
        {
        var models = await dbConnect.Scenes.
        Include(s=>s.Uploader).
        Include(s=>s.Likes)
        .Include(s=>s.Favorites.Where(u => u.IdentityServerId == userIdentity))
        .OrderByDescending(x=>x.UploadTime)
        .Skip((page - 1) * itemsOnAPage)
        .Take(itemsOnAPage)
        .Select(e =>
          e.AsModel(userIdentity))
        .ToListAsync();
         
          return models;
        }else{

            //TODO write this shorter (bottom part also looks for name)
        var models =await dbConnect.Scenes.
        Include(s=>s.Uploader).
        Include(s=>s.Likes)
         .Include(s=>s.Favorites.Where(u => u.IdentityServerId == userIdentity))
        .Where(s=>s.Name.Contains(searchFor))
        .OrderByDescending(x=>x.UploadTime)
        .Skip((page - 1) * itemsOnAPage)
        .Take(itemsOnAPage).Select(e =>
          e.AsModel(userIdentity)).ToListAsync();
      

        return models;
        }
    }

    public async Task<SceneModel?> GetVoxelSceneById(long id,string userIdentity)
    {
        var model = await dbConnect.Scenes.Include(s=>s.Uploader)
        .Include(s=>s.Likes)
        .Include(s=>s.Favorites.Where(u => u.IdentityServerId == userIdentity))
        .Where(s=>s.Id==id).Select(e =>
          e.AsModel(userIdentity)).SingleOrDefaultAsync();
       
        return model;
        
    }


    public async Task DeleteVoxelScene(long id)
    {
        var scene = await dbConnect.Scenes.Where(x=>x.Id==id).SingleOrDefaultAsync();
        dbConnect.Scenes.Remove(scene);
        await dbConnect.SaveChangesAsync();
    }

    public async Task<bool> IsThisSceneOwner(long sceneid, string userID)
    {
         var entity = await dbConnect.Scenes.Include(s=>s.Uploader).Where(e=>e.Id==sceneid)
         .SingleOrDefaultAsync();
         return entity.Uploader!=null&&entity.Uploader.IdentityServerId==userID;
    }

    public async Task<SceneModel?> EdditScene(SceneModel scene,string userIdentity)
    {
         var entity = await dbConnect.Scenes.Include(s=>s.Uploader).Include(s=>s.Likes)
         .Include(s=>s.Favorites.Where(u => u.IdentityServerId == userIdentity))
         .Where(s=>s.Id==scene.Id).SingleOrDefaultAsync();
        if (entity == null)
        {
            return null;
        }
         entity.Description=scene.Description;
         entity.Name=scene.Name;
         dbConnect.Update(entity);
         await dbConnect.SaveChangesAsync();
         return entity.AsModel(userIdentity);
    }

    public async Task<List<SceneModel>> GetFavorites(int page, int pageSize, string userId)
    {
         var models = await dbConnect.Scenes
        .Include(s=>s.Uploader)
        .Include(s=>s.Likes)
        .Include(s=>s.Favorites.Where(u => u.IdentityServerId == userId))
        .Where(s=>s.Favorites.Any(u=>u.IdentityServerId==userId))
        .OrderByDescending(x=>x.UploadTime)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(e =>
          e.AsModel(userId))
        .ToListAsync();
         
          return models;
    }
}
