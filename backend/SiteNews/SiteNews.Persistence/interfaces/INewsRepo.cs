using System;
using SiteNews.domain;
namespace SiteNews.Persistence.interfaces;

public interface INewsRepo
{
    public Task<SiteNewsModel?> GetNewsAsync(string Id);

     public Task<List<SiteNewsModel>> GetNewsAsync();

     public Task<SiteNewsModel> CreateNewsAsync(SiteNewsModel news);


     public Task<SiteNewsModel> UpdateNewsAsync(SiteNewsModel news);
   

     public Task DeleteNewsAsync(string Id);
}
