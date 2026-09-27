using System;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Domain.Interfaces;

public interface ICommentService
{
    public Task<List<CommentModel>> GetCommentsFromScene(long sceneID);

    public Task<CommentModel?> MakeComment(CommentModel comment,string IdentityServerId);

    public Task<CommentModel?> EdditComment(CommentModel comment,string IdentityServerId);
    
    public Task DeleteComment(long commentId,string IdentityServerId);

}
