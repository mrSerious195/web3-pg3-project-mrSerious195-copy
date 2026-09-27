using System;
using System.ComponentModel.DataAnnotations;

namespace SiteNews.API.Contracts;

public class NewsRequestContract
{
    public string? Id{get;set;}
      [Required]
      [MaxLength(200)]
    public required string Title{get;set;}
        [Required]
      [MaxLength(1000)]
    public required string Body {get;set;}
}
