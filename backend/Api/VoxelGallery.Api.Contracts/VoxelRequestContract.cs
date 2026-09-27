using System;
using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Api.Contracts;

public class VoxelRequestContract
{
      public long? Id{get;set;}
      [Required]
      [MaxLength(100)]
    public required String Name{get;set;}
public Vector3Contract? CameraPosition{get;set;}
public Vector3Contract? CameraLookAt{get;set;}
    
    public string? VoxelDataAsBase64 {get;set;}
    
    public string? ImgAsBase64 {get;set;}
          [Required]
      [MaxLength(1000)]
    public string Description{get;set;}="";
}

public class Vector3Contract
{
    public int X{get;set;}=0;
    public int Y{get;set;}=0;
    public int Z{get;set;}=0;
}