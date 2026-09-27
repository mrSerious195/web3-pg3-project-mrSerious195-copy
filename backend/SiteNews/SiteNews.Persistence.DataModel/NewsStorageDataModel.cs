using System;

namespace SiteNews.Persistence.DataModel;

public class NewsStorageDataModel
{
    public string? id{get;set;}

    public required string title{get;set;}

    public required string body {get;set;}

    public DateTime? time {get;set;}
}
