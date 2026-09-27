using System;

namespace VoxelGallery.Persistence.Interfaces;

public interface ILikeRepo
{
 public Task RemoveLike(long sceneID, string userIdentity);

    public Task AddLike(long sceneID, string userIdentity);

  public  Task<int> GetLikeCount(long sceneID);

  public Task<bool> IsLiked(long sceneID, string userIdentity);

}
