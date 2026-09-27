using System;
using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Api.Contracts;

public class CommentRequestContract
{
    
    public long? CommentId{get;set;}
    public required long SceneID{get;set;}
        [MaxLength(500)]
        [Required]
    public required string CommentText{get;set;}

}
