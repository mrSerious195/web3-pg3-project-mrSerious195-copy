using System;
using Vox.IdentityServer.Domein;

namespace VoxelGallery.Domain.Model;

public class CommentModel
{
    public UserModel? Commenter{get;set;}

     public long? CommentId{get;set;}

          public long? CommenterId{get;set;}

        public string? CommenterName{get;set;}

    public long SceneID {get;set;}

    public DateTime PostTime {get;set;}

    public required string Text{get;set;}



}
