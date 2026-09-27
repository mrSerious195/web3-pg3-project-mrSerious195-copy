namespace VoxelGallery.Domain.Interfaces;

public interface IFavoriteService
{
    public Task<List<long>> GetAllFavoriteIDs(string userIdentity);

      public Task<bool?> SetFavoriteStatus(long sceneID,bool IsFavorite,string userIdentity);
}
