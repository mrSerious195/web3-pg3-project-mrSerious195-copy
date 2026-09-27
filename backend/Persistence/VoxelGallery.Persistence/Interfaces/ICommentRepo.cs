using System;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Persistence.Interfaces;

public interface ICommentRepo
{
    public Task<List<CommentModel>> GetCommentsFromScene(long sceneID);

    public Task<CommentModel> MakeComment(CommentModel model,string IdentityServerId);

    public Task<CommentModel?> EdditComment(CommentModel model);
    
    public Task DeleteComment(long commentId);

    public Task<bool> DidUserMakeComment(long commentId,string IdentityServerId);
}
