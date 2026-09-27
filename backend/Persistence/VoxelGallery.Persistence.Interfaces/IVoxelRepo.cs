using System;
using VoxelGallery.Domain.Model;
namespace VoxelGallery.Persistence.Interfaces;
public interface IVoxelRepo
{
      public Task<List<SceneModel>> GetAllVoxelScenesAsync();

    public Task<SceneModel?> GetVoxelSceneById(long id);   
    public Task<SceneModel> AddVoxelScene(SceneModel scene);

    public Task DeleteVoxelScene(long id);
}
