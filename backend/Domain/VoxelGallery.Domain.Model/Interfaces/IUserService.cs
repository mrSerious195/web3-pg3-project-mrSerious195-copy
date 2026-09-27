using System;

namespace Vox.IdentityServer.Domein.InterFaces;

public interface IUserService
{
    public Task<UserModel?> GetUser(int userId);

    public Task<UserModel?> GetUser(string userDatabaseId);

    public Task<UserModel?> GetUserWithName(string name);

    public Task<UserModel> MakeUser(UserModel model);

      public Task<UserModel?> SetDonation(string userDatabaseId,bool status);
}
