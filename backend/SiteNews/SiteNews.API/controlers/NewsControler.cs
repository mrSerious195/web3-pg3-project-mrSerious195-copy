using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteNews.API.Contracts;
using SiteNews.API.Contracts.Mappers;
using SiteNews.domain;
using SiteNews.domain.Interfaces;
namespace SiteNews.API.controlers
{
    [Route("api/News")]
    [ApiController]
    public class NewsControler(INewsService newsService ) : ControllerBase
    {
            [HttpGet]
            [Authorize("User")]
        public async Task<ActionResult<List<NewsResponseContract>>> GetAllNews()
        {
           var modelList = await newsService.GetNewsAsync();
           var contracts =modelList.Select(x=>x.ToContract()).ToList();
            return Ok(contracts);
        }

             [HttpGet("{Id}")]
             [Authorize("User")]
        public async Task<ActionResult<NewsResponseContract>> GetNews([FromRoute] string id)
        {
             var modelList = await newsService.GetNewsAsync(id);
             if(modelList==null)
                return NotFound();
            return Ok(modelList.ToContract());
        }

                 [HttpPost]
                  [Authorize("Admin")]
        public async Task<ActionResult<NewsResponseContract>> MakeNews(NewsRequestContract newsRequestContract)
        {
            var result = await newsService.CreateNewsAsync(newsRequestContract.ToModel());
            return Ok(result.ToContract());
        }

                [HttpPut]
                 [Authorize("Admin")]
        public async Task<ActionResult<NewsResponseContract>> EdditNews(NewsRequestContract newsRequestContract)
        {
            var result = await newsService.UpdateNewsAsync(newsRequestContract.ToModel());
            return Ok(result.ToContract());
        }
        [HttpDelete("{id}")]
         [Authorize("Admin")]
        public async Task<ActionResult> DeleteNews([FromRoute] string id)
        {
            await newsService.DeleteNewsAsync(id);
            return Ok();
        }
    }

   
}
