using System;
using Microsoft.EntityFrameworkCore;
using Vox.IdentityServer.Domein;
using Vox.IdentityServer.Persistence.Interfaces;
using VoxelGallery.Persistence.DataModel;
using VoxelGallery.Persistence.Mapping;

namespace Vox.IdentityServer.Persistence;

public class UserRepo(VoxDatabaseContext dbContext) : IUserRepo
{
    public async Task<UserModel?> GetUser(int userId)
    {
      var userEntity = await dbContext.Users.Where(x=>x.Id==userId).SingleAsync();
        if (userEntity!=null)
        {
            return userEntity.AsModel();
        }
        else
        {
            return null;
        }
    }

    public async Task<UserModel?> GetUser(string IdentityServerId)
    {
         var userEntity =  await dbContext.Users.Where(x=>x.IdentityServerId==IdentityServerId).SingleAsync();
        if (userEntity!=null)
        {
            return userEntity.AsModel();
        }
        else
        {
            return null;
        }
    }

    public async Task<UserModel?> GetUserWithName(string name)
    {
         var userEntity = await dbContext.Users.Where(x=>x.Name==name).SingleAsync();
        if (userEntity!=null)
        {
            return userEntity.AsModel();
        }
        else
        {
            return null;
        }
    }

    public async Task<UserModel> MakeUser(UserModel model)
    {
       var entity = model.AsEntity();
dbContext.Users.Add(entity);
await dbContext.SaveChangesAsync();
var ret = entity.AsModel();
return ret;
    }

    public async Task<UserModel?> SetDonation(string userDatabaseId, bool status)
    {
           var userEntity = await dbContext.Users.Where(x=>x.IdentityServerId==userDatabaseId).SingleAsync();
        if (userEntity!=null)
        {
            userEntity.IsDonor=status;
            dbContext.Users.Update(userEntity);
            await dbContext.SaveChangesAsync();
            return userEntity.AsModel();
        }
        else
        {
            return null;
        }
    }
}