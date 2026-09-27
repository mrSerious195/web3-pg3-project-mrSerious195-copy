using System;
using Vox.IdentityServer.Domein;

namespace Vox.IdentityServer.Persistence.Interfaces;

public interface IUserRepo
{
public Task<UserModel?> GetUser(int userId);

public Task<UserModel?> GetUser(string IdentityServerId);

public Task<UserModel?> GetUserWithName(string name);

public Task<UserModel> MakeUser(UserModel model);

 public Task<UserModel?> SetDonation(string userDatabaseId, bool status);
}
