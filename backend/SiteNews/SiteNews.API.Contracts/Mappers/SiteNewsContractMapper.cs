using System;
using SiteNews.domain;

namespace SiteNews.API.Contracts.Mappers;

public static class SiteNewsContractMapper
{
public static NewsResponseContract ToContract(this SiteNewsModel model)
    {
        return new NewsResponseContract(){
            Id=model.Id,
            Title=model.Title,
            Body=model.Body,
            Time=model.Time.Value
            
        };
        
    }


     public static SiteNewsModel ToModel(this NewsRequestContract contract)
    {
        return new SiteNewsModel(){
            Id=contract.Id,
            Title=contract.Title,
            Body=contract.Body,
            
        };
        
    }
}
