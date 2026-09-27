using System;
using SiteNews.domain;
using SiteNews.Persistence.DataModel;

namespace SiteNews.Persistence.Mapper;

public static class NewsStorageMapper
{
    public static NewsStorageDataModel ToStorageModel(this SiteNewsModel model)
    {
        return new NewsStorageDataModel
        {
            body=model.Body,
            title=model.Title,
        };
    }

     public static SiteNewsModel ToModel(this NewsStorageDataModel storageDataModel)
    {
        return new SiteNewsModel
        {
            Time = storageDataModel.time,
            Id=storageDataModel.id,
            Title=storageDataModel.title,
            Body=storageDataModel.body
        };
    }

}
