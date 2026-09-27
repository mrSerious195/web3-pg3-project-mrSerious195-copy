using System;
using System.Security.Cryptography.X509Certificates;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.DataModel.Entities;

namespace VoxelGallery.Persistence.Mapping;

public static class SceneMappingExtension
{

    public static Scene AsEntity(this SceneModel sceneModel)
    {
       
        return new Scene
        {

            Name=sceneModel.Name,
            UploadTime=DateTime.Now,
           Description=sceneModel.Description,
               CameraPosX =sceneModel.CameraPosition.X ,
      CameraPosY =sceneModel.CameraPosition.Y,

      CameraPosZ  =sceneModel.CameraPosition.Z,

      CameraLookatX = sceneModel.CameraLookAt.X,

      CameraLookatY = sceneModel.CameraLookAt.Y ,

      CameraLookatZ  = sceneModel.CameraLookAt.Z

        };
    }

     public static SceneModel AsModel(this Scene sceneEntity,string userIdentity)
    {
         var isLiked=sceneEntity.Likes.Any(u=>u.IdentityServerId==userIdentity);
         var isFavorite = sceneEntity.Favorites.Any(u=>u.IdentityServerId==userIdentity);
        var ret = new SceneModel
        {
            Id=sceneEntity.Id,
            Name=sceneEntity.Name,
            UploadTime=sceneEntity.UploadTime,
            LikeCount = sceneEntity.Likes.Count,
            CameraPosition = new(sceneEntity.CameraPosX,sceneEntity.CameraPosY,sceneEntity.CameraPosZ),
            CameraLookAt = new(sceneEntity.CameraLookatX,sceneEntity.CameraLookatY,sceneEntity.CameraLookatZ),
            Description=sceneEntity.Description,
            IsLiked=isLiked,
            IsFavorite=isFavorite
        };
        if(sceneEntity.Uploader!=null)
        ret.Poster=sceneEntity.Uploader.AsModel();
        return ret;
    }

}
