using System;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.Interfaces;
using VoxelGallery.Persistence.Mapping;

namespace VoxelGallery.Persistence;

public class CommentRepo(VoxDatabaseContext dbContext) : ICommentRepo
{
    public async Task DeleteComment(long modelId)
    {
            var commentEntity = await dbContext.Comments
       .Where(c=>c.Id==modelId)
       .SingleAsync();

        if (commentEntity != null)
        {
             dbContext.Comments.Remove(commentEntity);
            await  dbContext.SaveChangesAsync();
        }
        
    }

    public async Task<bool> DidUserMakeComment(long modelId, string IdentityServerId)
    {
         var commentEntity =  await dbContext.Comments
       .Include(c=>c.User)
       .Where(c=>c.Id==modelId)
       .SingleAsync();

        if (commentEntity.User == null)
        {
            return false;
        }

       return commentEntity.User.IdentityServerId==IdentityServerId;
    }

    public async Task<CommentModel?> EdditComment(CommentModel model)
    {
       var commentEntity =  dbContext.Comments
       .Include(c=>c.User)
       .Where(c=>c.Id==model.CommentId)
       .First();
       if (commentEntity == null)
        {
            return null;
        }
       commentEntity.Text=model.Text;
       dbContext.Comments.Update(commentEntity);
        await  dbContext.SaveChangesAsync();
       var ret = commentEntity.AsModel();
     //  ret.Commenter.Countryflag=await getFlag(ret.Commenter.Country);
       return ret;
    }

    public async Task<List<CommentModel>> GetCommentsFromScene(long sceneID)
    {
        var entities = await dbContext.Comments
        .Include(c=>c.User)
        .Where(c=>c.SceneID==sceneID).ToListAsync();
        var models = entities.
        Select(c=>c.AsModel()).ToList();
      //  await Parallel.ForEachAsync(models, async(c,ct)=>{
            //if (c.Commenter!=null)
            // c.Commenter.Countryflag=await getFlag(c.Commenter.Country);
         //  });
        return models;
    }

    public async Task<CommentModel> MakeComment(CommentModel model,string IdentityServerId)
    {
     
        var user = await dbContext.Users.Where(u=>u.IdentityServerId==IdentityServerId).SingleAsync();
      
       var entity = model.AsEntity();
       entity.User=user;
       entity.PostTime=DateTime.Now;
       dbContext.Comments.Add(entity);
      await  dbContext.SaveChangesAsync();
        
        var ret = entity.AsModel();
      // ret.Commenter.Countryflag=await getFlag(ret.Commenter.Country);
        return ret;
    }






}
