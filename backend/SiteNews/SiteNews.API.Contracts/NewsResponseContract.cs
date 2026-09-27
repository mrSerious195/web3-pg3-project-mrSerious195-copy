using System;

namespace SiteNews.API.Contracts;

public class NewsResponseContract
{
    public required string Id{get;set;}

    public required string Title{get;set;}

    public required string Body {get;set;}

      public required DateTime Time {get;set;}
}
