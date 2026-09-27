using System;
using Vox.IdentityServer.Domein;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.DataModel.Entities;

namespace VoxelGallery.Persistence.Mapping;

public static class UserMappingExtension
{

    public static User AsEntity(this UserModel model)
    {
        return new User(){
          IdentityServerId =  model.IdentityServerId,
            Name = model.Name,
            RegistrationTime=model.RegistrationTime,
            Country = model.Country,
            Flag=model.Countryflag
        };
    }

    public static UserModel AsModel(this User user)
    {
        return new UserModel(){
            Countryflag=user.Flag,
            Id=user.Id,
          IdentityServerId =  user.IdentityServerId,
            Name = user.Name,
            RegistrationTime=user.RegistrationTime,
            Country = user.Country,
            IsDonor=user.IsDonor
           
        };
    }
}

