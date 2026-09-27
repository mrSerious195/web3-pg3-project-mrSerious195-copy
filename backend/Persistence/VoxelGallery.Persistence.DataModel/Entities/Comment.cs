using System;

namespace VoxelGallery.Persistence.DataModel.Entities;

public class Comment
{
    public string Text{get;set;}

    public long SceneID{get;set;}

    public long? UserId{get;set;}

    public long Id{get;set;}

    public DateTime PostTime{get;set;}

    public Scene Scene{get;set;} = null!;

    public User? User{get;set;} = null!;
}
