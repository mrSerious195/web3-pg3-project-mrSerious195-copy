using System;
using VoxelGallery.Domain.Interfaces;
using VoxelGallery.Persistence.Interfaces;

namespace VoxelGallery.Domain.Services;

public class LikeService(ILikeRepo likeRepo) : ILikeService
{
    public async Task<int> GetLikeCount(long sceneID)
    {
        return await likeRepo.GetLikeCount(sceneID);
    }

    public async Task<bool> IsLiked(long sceneID, string userIdentity)
    {
         return await  likeRepo.IsLiked(sceneID,userIdentity);
    }

    public async Task SetLikeStatus(long sceneID, bool IsLiked, string userIdentity)
    {
        if (IsLiked)
        {
          await  likeRepo.AddLike(sceneID,userIdentity);
        }
        else
        {
           await likeRepo.RemoveLike(sceneID,userIdentity);
        }
    }
}
