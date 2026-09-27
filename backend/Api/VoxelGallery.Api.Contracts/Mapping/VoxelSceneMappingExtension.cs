using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Api.Contracts.Mapping;

public static class VoxelSceneMappingExtension
{
public static VoxelResponseContract AsContract(this SceneModel scene,string? currentUserID)
    {
     return new VoxelResponseContract
     {
         Name=scene.Name,
         Id=scene.Id.Value,
         LikeCount=scene.LikeCount.Value,
         UploadTime=scene.UploadTime.Value,
         Poster=scene.Poster==null?null:scene.Poster.AsContract(currentUserID),
         CameraPosition=scene.CameraPosition.AsContract(),
         CameraLookAt=scene.CameraLookAt.AsContract(),
         Description=scene.Description,
         IsLiked=scene.IsLiked,
        IsFavorite=scene.IsFavorite
     } ;
    }

    public static SceneModel AsModel(this VoxelRequestContract contract)
    {
     return new SceneModel
     {
         Name=contract.Name,
         Id=contract.Id,
         VoxelData = contract.VoxelDataAsBase64,
         ImgData = contract.ImgAsBase64,
         CameraPosition=contract.CameraPosition==null?null:contract.CameraPosition.AsModel(),
         CameraLookAt=contract.CameraLookAt==null?null:contract.CameraLookAt.AsModel(),
         Description=contract.Description
     } ;
    }

    public static Vector3 AsModel(this Vector3Contract contract)
    {
        return new Vector3
        {
            X=contract.X,
            Y=contract.Y,
            Z=contract.Z
        };
    }

    public static Vector3Contract AsContract(this Vector3 contract)
    {
        return new Vector3Contract
        {
            X=contract.X,
            Y=contract.Y,
            Z=contract.Z
        };
    }
}
