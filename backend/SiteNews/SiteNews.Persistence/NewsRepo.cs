using System;
using Microsoft.Azure.Cosmos;
using SiteNews.domain;
using Microsoft.Extensions.Options;


using SiteNews.Persistence.interfaces;
using SiteNews.Persistence.Mapper;
using SiteNews.Persistence.DataModel;
using Microsoft.Azure.Cosmos.Linq;

namespace SiteNews.Persistence;

public class NewsRepo(IOptions<NewsRepositoryOptions> options):INewsRepo
{
  public async Task<SiteNewsModel?> GetNewsAsync(string Id)
    {
         var newsData = await GetCosmosContainer().ReadItemAsync<NewsStorageDataModel>(
            id:Id,
            partitionKey:new PartitionKey(Id)
         );

       return newsData.Resource.ToModel();
    }

     public async Task<List<SiteNewsModel>> GetNewsAsync()
    {
       
       var newsQuery = GetCosmosContainer().GetItemLinqQueryable<NewsStorageDataModel>().ToFeedIterator();

        var results = new List<NewsStorageDataModel>();

    while (newsQuery.HasMoreResults)
    {
        var response = await newsQuery.ReadNextAsync();
        results.AddRange(response.Resource);
    }


    return results.Select(x=>x.ToModel()).ToList();


    }

     public async Task<SiteNewsModel> CreateNewsAsync(SiteNewsModel news)
    {
        NewsStorageDataModel datamodel = news.ToStorageModel();
        datamodel.id= Guid.NewGuid().ToString();
        datamodel.time=DateTime.Now;
       var createdNews = await GetCosmosContainer().CreateItemAsync(
        item:datamodel
       );

       return createdNews.Resource.ToModel();
    }


     public async Task<SiteNewsModel> UpdateNewsAsync(SiteNewsModel news)
    {
         NewsStorageDataModel datamodel = news.ToStorageModel();
        datamodel.id= news.Id;
        datamodel.time=DateTime.Now;
      var response =   await GetCosmosContainer().ReplaceItemAsync<NewsStorageDataModel>(
        item:datamodel,
            id:datamodel.id,
              partitionKey:new PartitionKey(datamodel.id)
            );

        return response.Resource.ToModel();
    }
   

     public async Task DeleteNewsAsync(string Id)
    {
      await GetCosmosContainer().DeleteItemAsync<NewsStorageDataModel>(
            id:Id,
              partitionKey:new PartitionKey(Id)
            );
    }

    private Container GetCosmosContainer() 
    {
        var client = new CosmosClient(options.Value.Connectionstring);
        var database = client.GetDatabase(options.Value.DatabaseName);
        var container = database.GetContainer(options.Value.ContainerName);

        if (container is null)
        {
            throw new Exception("Could not obtain cosmos container.");
        }

        return container;
    }
}
