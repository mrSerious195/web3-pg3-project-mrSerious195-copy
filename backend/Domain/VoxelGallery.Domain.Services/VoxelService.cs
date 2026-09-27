using System;
using System.Net.Http.Json;
using QuestPDF.Fluent;
using VoxelGallery.Domain.Interfaces;
using VoxelGallery.Domain.Model;
using VoxelGallery.Persistence.Interfaces;
using System.Net.Http;
using Duende.IdentityModel.Client;
namespace VoxelGallery.Domain.Services;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Configuration;

public class VoxelService(ISceneDataRepo voxelRepo,
IHttpClientFactory httpClientFactory,
IConfiguration config) : IVoxelService
{
     public class FileRequestContract
{
     public required string Base64{get;set;}

      public required long Id {get;set;}
}
    public async Task<SceneModel> AddVoxelScene(SceneModel scene)
    {
     SceneModel fromDataBase =  await voxelRepo.AddVoxelScene(scene);


        var topostVox = new FileRequestContract()
{
  Id = fromDataBase.Id.Value,
  Base64=scene.VoxelData,

};
    var topostImg = new FileRequestContract()
{
  Id = fromDataBase.Id.Value,
  Base64=scene.ImgData,
};

     var blopclient = httpClientFactory.CreateClient("Blob");
var IDclient = httpClientFactory.CreateClient("IdentityServer"); 
var disco = await IDclient.GetDiscoveryDocumentAsync();
var tokenResponse = await IDclient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m-client",
                ClientSecret = config["m2msecret"],
                Scope = "VoxelGallery.Api.m2m"
            });
        blopclient.SetBearerToken(tokenResponse.AccessToken ?? throw new Exception("Could not obtain token."));
        var retUrlVox =  await blopclient.PostAsJsonAsync("api/VoxFiles",topostVox);
        var retUrlImg =  await blopclient.PostAsJsonAsync("api/PreviewImages",topostImg);
        if (retUrlVox.IsSuccessStatusCode&&retUrlImg.IsSuccessStatusCode)
        {
            return fromDataBase;
        }
        else
        {
            await voxelRepo.DeleteVoxelScene(fromDataBase.Id.Value);
            throw new Exception($"Img and Vox weren't uploaded, token response was {tokenResponse.AccessToken}");
            
        }
    }

    public async Task<List<SceneModel>> GetAllVoxelScenes(int page, int itemsOnAPage, string? searchFor, string userIdentity)
    {
        return await voxelRepo.GetAllVoxelScenes( page,  itemsOnAPage,  searchFor, userIdentity);
    }

    public async Task<SceneModel?> GetVoxelSceneById(long id,string userIdentity)
    {
        return await voxelRepo.GetVoxelSceneById(id, userIdentity);
    }

    public async Task DeleteVoxelScene(long id)
    {
        await voxelRepo.DeleteVoxelScene(id);
    }

    public async Task<bool> IsThisSceneOwner(long sceneid, string userID)
    {
        return await voxelRepo.IsThisSceneOwner(sceneid,userID);
    }

    public async Task<SceneModel?> EdditScene(SceneModel scene,string userIdentity)
    {
         return await voxelRepo.EdditScene( scene, userIdentity);
    }


    public async Task<byte[]?> GetVoxelPdfById(long id)
    {
       var model = await voxelRepo.GetVoxelSceneById( id,"");
        if (model == null)
        {
            return null;
        }
      var pdf =  new VoxPDF(model).GeneratePdf();
      return pdf;
    }

    public async Task<List<SceneModel>> GetFavorites(int page, int pageSize, string userId)
    {
        return await voxelRepo.GetFavorites( page,  pageSize, userId);
    }
}

