using System;
using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Persistence.DataModel.Entities;

public class Scene
{
    public long Id {get;set;}
public int CameraPosX {get;set;}

public int CameraPosY {get;set;}

public int CameraPosZ {get;set;}

public int CameraLookatX {get;set;}

public int CameraLookatY {get;set;}

public int CameraLookatZ {get;set;}
// I want it to be max
public string Description{get;set;}
[MaxLength(100)]
public string Name{get;set;}

public DateTime UploadTime{get;set;}

public long? UploaderId{get;set;}

public User? Uploader{get;set;} = null!;

public List<User> Likes{get;set;}= new();

public List<User> Favorites{get;set;}= new();

public List<Comment> Comments{get;set;}= new();
}
