using System;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Api.Contracts.Mapping;

public static class CommentMappingExtension
{
    public static CommentResponseContract AsContract(this CommentModel model,string? currentUserID)
    {
     return new CommentResponseContract()
     {
          Poster = model.Commenter.AsContract(currentUserID),
         SceneID = model.SceneID,
         CommentText = model.Text,
         CommentId=model.CommentId.Value,
         PostTime=model.PostTime
     } ;
    }

    public static CommentModel AsModel(this CommentRequestContract contract)
    {
     return new CommentModel()
     {
         CommenterId = contract.CommentId,
         CommentId=contract.CommentId,
         SceneID = contract.SceneID,
         Text = contract.CommentText,
     } ;
    }

}
