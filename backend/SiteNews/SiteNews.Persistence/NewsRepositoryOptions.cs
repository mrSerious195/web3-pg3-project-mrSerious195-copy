namespace SiteNews.Persistence;

public class NewsRepositoryOptions
{
    public required string Connectionstring{get;set;}
    public required string DatabaseName{get;set;}
    public required string ContainerName{get;set;}
}