using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoxelGallery.Api.Contracts;
using VoxelGallery.Api.Contracts.Mapping;
using VoxelGallery.Domain.Interfaces;

namespace VoxelGallery.Api.Controllers
{
    [Route("api/Comments")]
    [ApiController]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
         [HttpGet]
        [Authorize(Policy ="User")]
           public async Task<ActionResult<List<CommentResponseContract>>> GetComments([FromQuery]long sceneID)
        {
          var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
          var comments = await commentService.GetCommentsFromScene(sceneID);
            
        return Ok(comments.Select(c=>c.AsContract(userId)).ToList());    
        }
          [HttpPost]
           [Authorize(Policy ="User")]
           public async Task<ActionResult<CommentResponseContract>> MakeComment(CommentRequestContract requestContract)
        {
          var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
          var model = requestContract.AsModel();
        var ret = await  commentService.MakeComment(model,userId);
        return Ok(ret.AsContract(userId));    
        }

        [HttpPut]
         [Authorize(Policy ="User")]
           public async Task<ActionResult<CommentResponseContract>> EditComment(CommentRequestContract requestContract)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
         var Model = await commentService.EdditComment(requestContract.AsModel(),userId);
         if(Model==null)
         return NotFound();
        return Ok(Model.AsContract(userId));    
        }

        [HttpDelete("{commentId}")]
         [Authorize(Policy ="User")]
           public async Task<ActionResult> DeleteComment([FromRoute]long commentId)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
          commentService.DeleteComment(commentId,userId);
        return Ok();    
        }
        
    }
}
