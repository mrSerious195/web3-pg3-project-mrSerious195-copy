using System;
using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Persistence.DataModel.Entities;

public class User
{
    public long Id { get; set; } 
[MaxLength(240)]
    public string IdentityServerId{get;set;}
[MaxLength(256)]
    public string Name{get;set;}

[MaxLength(256)]
    public string? Country{get;set;}

    [MaxLength(10)]
    public string Flag{get;set;}="🇧🇪";

       [MaxLength(1)]
    public bool IsDonor{get;set;}=false;

    public DateTime RegistrationTime{get;set;}

    public List<Scene> MadeScenes{get;set;}= new();

    public List<Scene> Likes{get;set;}= new();

    public List<Scene> Favorites{get;set;}= new();

    public List<Comment> Comments{get;set;}= new();
}
