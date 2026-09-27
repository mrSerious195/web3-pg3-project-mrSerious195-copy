using System;

namespace VoxelGallery.Persistence.Interfaces;

public interface IFavoriteRepo
{

    public Task<bool?> SetFavoriteStatus(bool isFavorite,long sceneID, string userIdentity);


    public Task<List<long>> GetAllFavoriteIDs(string userIdentity);

}
