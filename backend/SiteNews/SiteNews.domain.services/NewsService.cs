using System;
using SiteNews.domain;
using SiteNews.domain.Interfaces;
using SiteNews.Persistence.interfaces;

namespace SiteNews.Domain.Services;

public class NewsService(INewsRepo repo) : INewsService
{
    public async Task<SiteNewsModel> CreateNewsAsync(SiteNewsModel news)
    {
        return await repo.CreateNewsAsync(news);
    }

    public async Task DeleteNewsAsync(string Id)
    {
        await repo.DeleteNewsAsync(Id);
    }

    public async Task<SiteNewsModel?> GetNewsAsync(string Id)
    {
        return await repo.GetNewsAsync(Id);
    }

    public async Task<List<SiteNewsModel>> GetNewsAsync()
    {
        return await repo.GetNewsAsync();
    }

    public async Task<SiteNewsModel> UpdateNewsAsync(SiteNewsModel news)
    {
          return await repo.UpdateNewsAsync(news);
    }
}
