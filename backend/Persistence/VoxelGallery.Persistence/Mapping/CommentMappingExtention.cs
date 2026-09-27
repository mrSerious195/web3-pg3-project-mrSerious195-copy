using System;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.DataModel.Entities;

namespace VoxelGallery.Persistence.Mapping;

public static class CommentMappingExtention
{
 public static Comment AsEntity(this CommentModel sceneModel)
    {
       
        return new Comment()
        {
          Text=sceneModel.Text,
            SceneID= sceneModel.SceneID
          
            
        };
    }

  public static  CommentModel AsModel(this Comment entity)
    {

         return new CommentModel()
        {
         Text=entity.Text,
         CommenterId = entity.UserId,
         CommentId=entity.Id,
         CommenterName = entity.User==null?"<Ongekend>":entity.User.Name,
          SceneID = entity.SceneID,
          Commenter=entity.User.AsModel(),
           PostTime=entity.PostTime
        };


    }
}
