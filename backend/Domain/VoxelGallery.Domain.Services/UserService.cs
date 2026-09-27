using System.Net.Http.Json;
using Vox.IdentityServer.Domein;
using Vox.IdentityServer.Domein.InterFaces;
using Vox.IdentityServer.Persistence.Interfaces;

namespace Vox.IdentityServer.Domein.Services;

public class UserService(IUserRepo repo,HttpClient httpClient) : IUserService
{
    public Task<UserModel?> GetUser(int userId)
    {
        return repo.GetUser(userId);
    }

    public Task<UserModel?> GetUser(string userDatabaseId)
    {
         return repo.GetUser(userDatabaseId);
    }

    public Task<UserModel?> GetUserWithName(string name)
    {
         return repo.GetUserWithName(name);
    }

    public async Task<UserModel> MakeUser(UserModel model)
    {
        model.Countryflag= await GetFlag(model.Country);
           return await repo.MakeUser(model);
    }

     public class FlagApiResponse
    {
        public string flag{get;set;}
    }
    
  private async Task<string> GetFlag(string countryName)
    {
        FlagApiResponse[] retrievedData;
         retrievedData = await httpClient.GetFromJsonAsync<FlagApiResponse[]>($"https://restcountries.com/v3.1/name/{countryName}?fields=flag");
       
        if(retrievedData!=null &&retrievedData.Length>0)
        {
            return retrievedData[0].flag;
        }
        else
        {
            return "";
        }
    }

    public async Task<UserModel?> SetDonation(string userDatabaseId, bool status)
    {
        return await repo.SetDonation( userDatabaseId, status);
    
    }
}

