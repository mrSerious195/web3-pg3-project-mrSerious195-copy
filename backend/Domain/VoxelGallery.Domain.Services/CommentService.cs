using System;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using VoxelGallery.Domain.Interfaces;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.Interfaces;

namespace VoxelGallery.Domain.Services;

public class CommentService(ICommentRepo commentRepo) : ICommentService
{
    public async Task DeleteComment(long commentId,string IdentityServerId)
    {
         if(await commentRepo.DidUserMakeComment(commentId,IdentityServerId))
         await commentRepo.DeleteComment(commentId);
    }

    public async Task<CommentModel?> EdditComment(CommentModel comment,string IdentityServerId)
    {   
        if(!await commentRepo.DidUserMakeComment(comment.CommentId.Value,IdentityServerId))
            return null;

       return await commentRepo.EdditComment(comment);
    }

    public async Task<List<CommentModel>> GetCommentsFromScene(long sceneID)
    {
        return await commentRepo.GetCommentsFromScene(sceneID);

    }
    public async Task<CommentModel?> MakeComment(CommentModel comment,string IdentityServerId)
    {
       return await commentRepo.MakeComment(comment, IdentityServerId);
    }


}