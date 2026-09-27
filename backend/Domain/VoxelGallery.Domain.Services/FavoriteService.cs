using System;
using VoxelGallery.Domain.Interfaces;
using VoxelGallery.Persistence.Interfaces;

namespace VoxelGallery.Domain.Services;

public class FavoriteService(IFavoriteRepo favoriteRepo): IFavoriteService
{
    public async Task<List<long>> GetAllFavoriteIDs(string userIdentity)
    {
        return await favoriteRepo.GetAllFavoriteIDs(userIdentity);
    }

    public async Task<bool?> SetFavoriteStatus(long sceneID, bool IsFavorite, string userIdentity)
    {
           return await favoriteRepo.SetFavoriteStatus(IsFavorite,sceneID,userIdentity);
       
    }

}
