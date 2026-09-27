using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoxelGallery.Api.Contracts;
using VoxelGallery.Domain.Interfaces;

namespace VoxelGallery.Api.Controllers
{
    [Route("api/Likes")]
    [ApiController]
        [Authorize(Policy ="User")]
    public class LikeControler(ILikeService likeService) : ControllerBase
    {
        
       [HttpPut]
        [Authorize(Policy ="User")]
           public async Task<ActionResult<LikeResponseContract>> SetLikeStatus(LikeRequestContract contract)
        {  
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            await likeService.SetLikeStatus(contract.SceneId,contract.IsLiked,userId);
          int LikeCount = await likeService.GetLikeCount(contract.SceneId);
           var ret = new  LikeResponseContract(){CurrentLikeCound=LikeCount, IsLiked=contract.IsLiked};
         return Ok(ret);  
        }
    }

     
}
