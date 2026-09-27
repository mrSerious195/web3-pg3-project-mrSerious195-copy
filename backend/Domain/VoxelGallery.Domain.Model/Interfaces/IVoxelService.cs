using System;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Domain.Interfaces;

public interface IVoxelService
{
    public Task<List<SceneModel>> GetAllVoxelScenes(int page,int itemsOnAPage,string searchFor,string userIdentity);

    public Task<SceneModel?> GetVoxelSceneById(long id,string userIdentity);

     public Task<SceneModel> AddVoxelScene(SceneModel scene);

      public Task DeleteVoxelScene(long id);

      public Task<bool> IsThisSceneOwner(long sceneid, string userID);

       public Task<SceneModel?> EdditScene(SceneModel scene,string userIdentity);
   public Task<byte[]?>  GetVoxelPdfById(long id);
    Task<List<SceneModel>> GetFavorites(int page, int pageSize, string userId);
}
