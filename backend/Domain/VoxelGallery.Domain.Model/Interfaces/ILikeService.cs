using System;

namespace VoxelGallery.Domain.Interfaces;

public interface ILikeService
{
  public Task SetLikeStatus(long sceneID,bool IsLiked,string userIdentity);

  public Task<int> GetLikeCount(long sceneID);

  public Task<bool> IsLiked(long sceneID, string userIdentity);

  
}
