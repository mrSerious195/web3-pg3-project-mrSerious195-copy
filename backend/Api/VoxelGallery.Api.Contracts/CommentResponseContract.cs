using System;

namespace VoxelGallery.Api.Contracts;

public class CommentResponseContract
{
         public required UserResponseContract? Poster{get;set;}
    public required long CommentId{get;set;}

    public required long SceneID{get;set;}

    public required string CommentText{get;set;}

    public required DateTime PostTime{get;set;}
}
