using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoxelGallery.Api.Contracts;
using VoxelGallery.Domain.Interfaces;

namespace VoxelGallery.Api.Controllers
{
    [Route("api/Favorites")]
    [ApiController]
   
    public class FavoriteControler(IFavoriteService favoriteService) : ControllerBase
    {
          [HttpGet]
           [Authorize(Policy ="User")]
           public async Task<IActionResult> GetAllMyFavorites()
        {
              var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
           if(userId==null)
            return Unauthorized();
            var idList = await favoriteService.GetAllFavoriteIDs(userId);
         return Ok(idList);
        }

            [HttpPut]
           [Authorize(Policy ="User")]
           public async Task<IActionResult> SetFavoriteStatus(FavoriteRequestContract contract)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
          bool? isfavorite =  await favoriteService.SetFavoriteStatus(contract.FavoriteId,contract.IsFavorite,userId);
          if(isfavorite==null)
            return NotFound();
           var ret = new  FavoritesResponseContract(){FavoriteId=contract.FavoriteId, IsFavorite=isfavorite.Value};
         return Ok(ret);  
        }
    }
    
}
