using System;
using Vox.IdentityServer.Domein;

namespace VoxelGallery.Domain.Model;

public class SceneModel
{
    public long? Id{get;set;}

    public required string Name{get;set;}

    public int? LikeCount{get;set;}

     public UserModel? Poster{get;set;}

        public string? UserDatabaseId{get;set;}
public bool IsLiked{get;set;}

public bool IsFavorite{get;set;}
    public  string? VoxelData {get;set;}
public  string? ImgData {get;set;}
public required Vector3? CameraPosition{get;set;} 
public required Vector3? CameraLookAt{get;set;}

public DateTime? UploadTime {get;set;}

public string Description{get;set;}
}

public class Vector3{
    public int X{get;set;}=0;
    public int Y{get;set;}=0;
    public int Z{get;set;}=0;

    public Vector3(){}
    public Vector3(int x,int y,int z)
    {
        X=x;
        Y=y;
        Z=z;
    }
    }