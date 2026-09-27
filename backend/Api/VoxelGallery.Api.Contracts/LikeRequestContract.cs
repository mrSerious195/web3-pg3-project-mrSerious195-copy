using System;

namespace VoxelGallery.Api.Contracts;

public class LikeRequestContract
{
    public required long SceneId{get;set;}

    public required bool IsLiked{get;set;}
}
