using System.Data.Common;

namespace VoxelGallery.Api.Contracts;

public class VoxelResponseContract
{
    public required long Id{get;set;}
    public required string Name{get;set;}

    public required DateTime UploadTime {get;set;}

    public UserResponseContract? Poster{get;set;}

    public required Vector3Contract CameraPosition{get;set;} 
    public required Vector3Contract CameraLookAt{get;set;}
    public required string Description{get;set;}
      public required int LikeCount{get;set;}


      public required bool IsLiked{get;set;}
       public required bool IsFavorite{get;set;}

}
